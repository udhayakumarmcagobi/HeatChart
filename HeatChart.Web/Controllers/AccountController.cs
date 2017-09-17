using HeatChart.DataRepository.Sql.Interfaces;
using HeatChart.Domain.Core.Abstracts;
using HeatChart.Domain.Core.Utilities;
using HeatChart.Entities.Sql.Domain;
using HeatChart.Infrastructure.Common.Utilities;
using HeatChart.ViewModels;
using HeatChart.Web.Core;
using System.Diagnostics;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using HeatChart.Entities.Sql;
using System.Collections.Generic;
using ModelMapper;
using System;
using HeatHeatChart.ViewModels;

namespace HeatChart.Web.Controllers
{
    [Authorize(Roles = "Admin, User")]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiControllerBase
    {
        private readonly IMembershipService _membershipService;

        public AccountController()
        {

        }
        public AccountController(IMembershipService membershipService,
            IEFRepository<Error> _errorsRepository) : base(_errorsRepository)
        {
            _membershipService = membershipService;
        }

        [HttpGet]
        [Route("search/{page:int=0}/{pageSize=4}/{filter?}")]
        public HttpResponseMessage Search(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {
            int currentPage = page.Value; int currentPageSize = pageSize.Value;

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var userResult = _membershipService.GetUsers(page, pageSize, filter);

                List<Users> users = userResult.Item1;

                int totalUsers = userResult.Item2;

                IEnumerable<UserVM> usersVM = AutoMapper.Map<IEnumerable<Users>, IEnumerable<UserVM>>(users);

                PaginationSet<UserVM> pagedSet = new PaginationSet<UserVM>()
                {
                    Page = currentPage,
                    TotalCount = totalUsers,
                    TotalPages = (int)Math.Ceiling((decimal)totalUsers / currentPageSize),
                    Items = usersVM
                };
                response = request.CreateResponse<PaginationSet<UserVM>>(HttpStatusCode.OK, pagedSet);
                return response;
            });
        }

        [AllowAnonymous]
        [Route("authenticate")]
        [HttpPost]
        public HttpResponseMessage Login(HttpRequestMessage request, LoginVM user)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    MembershipContext _userContext = _membershipService.ValidateUser(user.Username, user.Password);

                    if (_userContext.User != null)
                    {
                        response = request.CreateResponse(
                                        HttpStatusCode.OK,
                                        new
                                        {
                                            success = true,
                                            role = _userContext.User.UserRoles.FirstOrDefault().Role.Name,
                                            email = _userContext.User.Email
                                        });
                    }
                    else
                    {
                        response = request.CreateResponse(
                                        HttpStatusCode.OK,
                                        new
                                        {
                                            success = false
                                        });
                    }
                }
                else
                    response = request.CreateResponse(HttpStatusCode.OK, new { success = false });

                return response;
            });
        }

        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public HttpResponseMessage Register(HttpRequestMessage request, RegistrationVM user)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, new { success = false });
                }
                else
                {
                    var _user = _membershipService.CreateUser(user.Username, user.Email, user.Password, new int[] { ConfigurationReader.Membership });

                    if (_user != null && _user.Item1 != null)
                    {
                        response = request.CreateResponse(HttpStatusCode.OK, new
                        {
                            message = _user.Item2,
                            success = true,
                            role = _user.Item1.UserRoles.FirstOrDefault().Role.Name,
                            email = _user.Item1.Email
                        });
                    }
                    else
                    {
                        response = request.CreateResponse(HttpStatusCode.OK, new { message = _user.Item2, success = false });
                    }
                }
                return response;
            });
        }

        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, RegistrationVM userVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest,
                        ModelState.Keys.SelectMany(k => ModelState[k].Errors).Select(m => m.ErrorMessage).ToArray());
                }
                else
                {
                    var _user = _membershipService.UpdateUser(userVM.Username, userVM.Email, userVM.Password);

                    if (_user != null && _user.Item1 != null)
                    {
                        response = request.CreateResponse(HttpStatusCode.OK,
                            new
                            {
                                message = _user.Item2,
                                success = true,
                                role = _user.Item1.UserRoles.FirstOrDefault().Role.Name,
                                email = _user.Item1.Email
                            });
                    }
                    else
                    {
                        response = request.CreateResponse(HttpStatusCode.OK, new { message = _user.Item2, success = false });
                    }
                }
                return response;
            });
        }

        [HttpPost]
        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, RegistrationVM userVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var _user = _membershipService.DeleteUser(userVM.Username, userVM.Email);

                if (_user != null && _user.Item1 != null)
                {
                    response = request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            message = _user.Item2,
                            success = true,
                            role = _user.Item1.UserRoles.FirstOrDefault().Role.Name,
                            email = _user.Item1.Email
                        });
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.OK, new { message = _user.Item2, success = false });
                }

                return response;
            });
        }

        [HttpPost]
        [Route("changePassword")]
        public HttpResponseMessage ChangePassword(HttpRequestMessage request, RegistrationVM userVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest,
                        ModelState.Keys.SelectMany(k => ModelState[k].Errors).Select(m => m.ErrorMessage).ToArray());
                }
                else
                {
                    MembershipContext _userContext = _membershipService.ValidateUser(userVM.Username, userVM.OldPassword);

                    if (_userContext.User != null)
                    {
                        var _user = _membershipService.UpdateUser(userVM.Username, userVM.Email, userVM.Password);

                        if (_user != null && _user.Item1 != null)
                        {
                            response = request.CreateResponse(HttpStatusCode.OK,
                                new
                                {
                                    message = _user.Item2,
                                    success = true,
                                    role = _user.Item1.UserRoles.FirstOrDefault().Role.Name,
                                    email = _user.Item1.Email
                                });
                        }
                        else
                        {
                            response = request.CreateResponse(HttpStatusCode.OK, new { message = _user.Item2, success = false });
                        }
                    }
                    else
                    {
                        response = request.CreateResponse(HttpStatusCode.OK, new { message = "Old Password is incorrect", success = false });
                    }
                }
                return response;
            });
        }
    }
}
