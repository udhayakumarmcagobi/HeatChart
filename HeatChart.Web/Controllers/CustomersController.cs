using HeatChart.DataRepository.Sql.Interfaces;
using HeatChart.Entities.Sql.Domain;
using HeatChart.Web.Core;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Linq;
using HeatHeatChart.ViewModels.Domain;
using HeatChart.DataRepository.Sql.Extensions;
using HeatChart.Web.Infrastructure.Extensions;
using System;
using System.Net;
using ModelMapper;

namespace HeatChart.Web.Controllers
{    
    [Authorize(Roles = "Admin, User")]
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiControllerBase
    {
        private readonly IEFRepository<Error> _errorRepository;
        private readonly IEFRepository<Customer> _customersRepository;
        public CustomersController(IEFRepository<Customer> customersRepository,
            IEFRepository<Error> errorRepository
           ) : base(errorRepository)
        {
            _errorRepository = errorRepository;
            _customersRepository = customersRepository;
        }


        [HttpGet]
        [Route("search/{page:int=0}/{pageSize=4}/{filter?}")]
        public HttpResponseMessage Search(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {
            int currentPage = page.Value; int currentPageSize = pageSize.Value;

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null; List<Customer> customers = null;

                int totalCustomers = 0;

                if (!string.IsNullOrEmpty(filter))
                {
                    filter = filter.Trim().ToLower();

                    customers = _customersRepository.FindBy(c => c.Name.ToLower().Contains(filter) && c.IsDeleted == false)
                        .OrderByDescending(x => x.ModifiedOn)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totalCustomers = _customersRepository.FindBy(c => c.IsDeleted == false && c.Name.ToLower().Contains(filter)).Count();
                }
                else
                {
                    customers = _customersRepository.GetAll().Where(c => c.IsDeleted == false)
                                .OrderByDescending(x => x.ModifiedOn)
                                .Skip(currentPage * currentPageSize)
                                .Take(currentPageSize)
                            .ToList();

                    totalCustomers = _customersRepository.FindBy(x => x.IsDeleted == false).Count();
                }
                
                IEnumerable<CustomerVM> customersVM = AutoMapper.Map<IEnumerable<Customer>, IEnumerable<CustomerVM>>(customers);

                PaginationSet<CustomerVM> pagedSet = new PaginationSet<CustomerVM>()
                {
                    Page = currentPage,
                    TotalCount = totalCustomers,
                    TotalPages = (int)Math.Ceiling((decimal)totalCustomers / currentPageSize),
                    Items = customersVM
                };
                response = request.CreateResponse<PaginationSet<CustomerVM>>(HttpStatusCode.OK, pagedSet);
                return response;
            });
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(HttpRequestMessage request, CustomerVM customerVM)
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
                    if (_customersRepository.CustomerExists(customerVM.Email, customerVM.Name))
                    {
                        ModelState.AddModelError("Invalid user", "Email or Name already exists");
                        response = request.CreateResponse(HttpStatusCode.BadRequest,
                            ModelState.Keys.SelectMany(k => ModelState[k].Errors).Select(m => m.ErrorMessage).ToArray());
                    }
                    else
                    {
                        Customer newCustomer = new Customer();
                        newCustomer = AutoMapper.Map<CustomerVM, Customer>(customerVM);

                        _customersRepository.Insert(newCustomer);
                        _customersRepository.Commit();

                        // Update view model 
                        customerVM = AutoMapper.Map<Customer, CustomerVM>(newCustomer);
                        response = request.CreateResponse<CustomerVM>(HttpStatusCode.Created, customerVM);
                    }
                }
                return response;
            });
        }

        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, CustomerVM customerVM)
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
                    Customer _customer = _customersRepository.GetSingleByCustomerID(customerVM.ID);
                    _customer.UpdateCustomer(customerVM);

                    _customersRepository.Commit();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, CustomerVM customerVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest,
                        ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                              .Select(m => m.ErrorMessage).ToArray());
                }
                else
                {
                    customerVM.IsDeleted = true;
                    Customer _customer = _customersRepository.GetSingleByCustomerID(customerVM.ID);
                    _customer.UpdateCustomer(customerVM);

                    _customersRepository.Commit();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }

                return response;
            });
        }

    }
}
