using HeatChart.DataRepository.Sql.Interfaces;
using HeatChart.Domain.Core.Abstracts;
using HeatChart.Domain.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using HeatChart.DataRepository.Sql.Extensions;
using System.Security.Principal;
using HeatChart.Entities.Sql;
using HeatChart.Entities.Sql.Domain;

namespace HeatChart.Domain.Core.Services
{
    public class MembershipService : IMembershipService
    {
        #region Variables 
        private readonly IEFRepository<Users> _userRepository;
        private readonly IEFRepository<Role> _roleRepository;
        private readonly IEFRepository<UserRole> _userRoleRepository;
        private readonly IEncryptionService _encryptionService;
        #endregion

        #region Constructors
        public MembershipService(IEFRepository<Users> userRepository, 
            IEFRepository<Role> roleRepository, 
            IEFRepository<UserRole> userRoleRepository, 
            IEFRepository<Error> errorRepository, 
            IEFUnitOfWork unitOfWork,
            IEncryptionService encryptionService)           
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _encryptionService = encryptionService;
        }
        #endregion

        #region Public Methods
        public Tuple<Users,string> CreateUser(string username, string email, string password, int[] roles)
        {            
            var existingUser = _userRepository.GetSingleByUsernameOrEmail(username, email);

            if (existingUser != null)
            {
                return new Tuple<Users, string>(null, "Username or Email already in use");                
            }
            var passwordSalt = _encryptionService.CreateSalt();
            var user = new Users()
            {
                Username = username,
                Salt = passwordSalt,
                Email = email,
                IsLocked = false,
                HashedPassword = _encryptionService.EncryptPassword(password, passwordSalt),
                DateCreated = DateTime.Now
         
            };
            _userRepository.Insert(user);
            _userRepository.Commit();
            if (roles != null || roles.Length > 0)
            {
                foreach (var role in roles)
                {
                    addUserToRole(user, role);
                }
            }
            _userRoleRepository.Commit();
            return new Tuple<Users, string>(user, string.Format("{0} - {1}", user.Username, "Registered Successfully"));
        }

        public Tuple<Users, string> UpdateUser(string username, string email, string password)
        {
            var existingUser = _userRepository.GetSingleByUsernameOrEmail(username, email);

            if (existingUser == null)
            {
                return new Tuple<Users, string>(null, "user does not exists");
            }

            var passwordSalt = _encryptionService.CreateSalt();

            existingUser.Salt = passwordSalt;
            existingUser.HashedPassword = _encryptionService.EncryptPassword(password, passwordSalt);

            _userRepository.Commit();

            return new Tuple<Users, string>(existingUser, string.Format("{0} - {1}", existingUser.Username, "updated Successfully"));
        }

        public Tuple<Users, string> DeleteUser(string username, string email)
        {
            var existingUser = _userRepository.GetSingleByUsernameOrEmail(username, email);

            if (existingUser == null)
            {
                return new Tuple<Users, string>(null, "user does not exists");
            }

            existingUser.IsLocked = true;

            _userRepository.Commit();

            return new Tuple<Users, string>(existingUser, string.Format("{0} - {1}", existingUser.Username, "Deleted Successfully"));
        }

        public Users GetUser(int userId)
        {
            return _userRepository.GetSingleByUserID(userId);
        }

        public Tuple<List<Users>, int> GetUsers(int? page, int? pageSize, string filter)
        {
            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;

            List<Users> users = null;

            int totalUsers = 0;

            if (!string.IsNullOrEmpty(filter))
            {
                filter = filter.Trim().ToLower();

                users = _userRepository.FindBy(c => c.Username.ToLower().Contains(filter) && c.IsLocked == false && !c.UserRoles.Any(role => role.RoleId == 1))
                    .OrderByDescending(x => x.DateCreated)
                    .Skip(currentPage * currentPageSize)
                    .Take(currentPageSize)
                    .ToList();

                totalUsers = _userRepository.FindBy(c => c.IsLocked == false && c.Username.ToLower().Contains(filter)).Count();
            }
            else
            {
                users = _userRepository.GetAll().Where(c => c.IsLocked == false && !c.UserRoles.Any(role => role.RoleId == 1))
                            .OrderByDescending(x => x.DateCreated)
                            .Skip(currentPage * currentPageSize)
                            .Take(currentPageSize)
                        .ToList();

                totalUsers = _userRepository.FindBy(x => x.IsLocked == false).Count();
            }

            return new Tuple<List<Users>, int>(users, totalUsers);
        }

        public List<Role> GetUserRoles(string username)
        {
            List<Role> _result = new List<Role>();
            var existingUser = _userRepository.GetFirstOrDefault(user => user.Username.Equals(username));

            if (existingUser != null)
            {
                foreach (var userRole in existingUser.UserRoles)
                {
                    _result.Add(userRole.Role);
                }
            }
            return _result.Distinct().ToList();
        }
        public MembershipContext ValidateUser(string username, string password)
        {
            var membershipCtx = new MembershipContext();

            var user = _userRepository.GetSingleByUsernameOrEmail(username, string.Empty);
            if (user != null && isUserValid(user, password))
            {
                var userRoles = GetUserRoles(user.Username);
                membershipCtx.User = user;

                var identity = new GenericIdentity(user.Username);
                membershipCtx.Principal = new GenericPrincipal(
                    identity,
                    userRoles.Select(x => x.Name).ToArray());
            }

            return membershipCtx;
        }

        #endregion

        #region Helper Methods
        private void addUserToRole(Users user, int roleId)
        {
            var role = _roleRepository.GetFirstOrDefault(r => r.ID == roleId);

            if (role == null) throw new ApplicationException("Role doesn't exist.");

            var userRole = new UserRole()
            {
                RoleId = role.ID,
                UsersId = user.ID
            };

            _userRoleRepository.Insert(userRole);
        }

        private bool isPasswordValid(Users user, string password)
        {
            return string.Equals(_encryptionService.EncryptPassword(password, user.Salt), user.HashedPassword);
        }
        private bool isUserValid(Users user, string password)
        {
            if (isPasswordValid(user, password))
            {
                return !user.IsLocked;
            }
            return false;
        }
        #endregion
    }
}
