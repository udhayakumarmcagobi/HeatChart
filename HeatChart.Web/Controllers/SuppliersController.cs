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
    [RoutePrefix("api/suppliers")]
    public class SuppliersController : ApiControllerBase
    {
        private readonly IEFRepository<Error> _errorRepository;
        private readonly IEFRepository<Supplier> _suppliersRepository;
        public SuppliersController(IEFRepository<Supplier> suppliersRepository,
            IEFRepository<Error> errorRepository
           ) : base(errorRepository)
        {
            _errorRepository = errorRepository;
            _suppliersRepository = suppliersRepository;
        }


        [HttpGet]
        [Route("search/{page:int=0}/{pageSize=4}/{filter?}")]
        public HttpResponseMessage Search(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {
            int currentPage = page.Value; int currentPageSize = pageSize.Value;

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null; List<Supplier> suppliers = null;

                int totalSuppliers = 0;

                if (!string.IsNullOrEmpty(filter))
                {
                    filter = filter.Trim().ToLower();

                    suppliers = _suppliersRepository.FindBy(c => c.Name.ToLower().Contains(filter) && c.IsDeleted == false)
                       .OrderByDescending(x => x.ModifiedOn)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totalSuppliers = _suppliersRepository.FindBy(c => c.IsDeleted == false && c.Name.ToLower().Contains(filter)).Count();
                }
                else
                {
                    suppliers = _suppliersRepository.GetAll().Where(c => c.IsDeleted == false)
                               .OrderByDescending(x => x.ModifiedOn)
                                .Skip(currentPage * currentPageSize)
                                .Take(currentPageSize)
                            .ToList();

                    totalSuppliers = _suppliersRepository.FindBy(x => x.IsDeleted == false).Count();
                }
                
                IEnumerable<SupplierVM> suppliersVM = AutoMapper.Map<IEnumerable<Supplier>, IEnumerable<SupplierVM>>(suppliers);

                PaginationSet<SupplierVM> pagedSet = new PaginationSet<SupplierVM>()
                {
                    Page = currentPage,
                    TotalCount = totalSuppliers,
                    TotalPages = (int)Math.Ceiling((decimal)totalSuppliers / currentPageSize),
                    Items = suppliersVM
                };
                response = request.CreateResponse<PaginationSet<SupplierVM>>(HttpStatusCode.OK, pagedSet);
                return response;
            });
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(HttpRequestMessage request, SupplierVM supplierVM)
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
                    if (_suppliersRepository.SupplierExists(supplierVM.Email, supplierVM.Name))
                    {
                        ModelState.AddModelError("Invalid user", "Email or Name already exists");
                        response = request.CreateResponse(HttpStatusCode.BadRequest,
                            ModelState.Keys.SelectMany(k => ModelState[k].Errors).Select(m => m.ErrorMessage).ToArray());
                    }
                    else
                    {
                        Supplier newSupplier = new Supplier();
                        newSupplier = AutoMapper.Map<SupplierVM, Supplier>(supplierVM);

                        _suppliersRepository.Insert(newSupplier);
                        _suppliersRepository.Commit();

                        // Update view model 
                        supplierVM = AutoMapper.Map<Supplier, SupplierVM>(newSupplier);
                        response = request.CreateResponse<SupplierVM>(HttpStatusCode.Created, supplierVM);
                    }
                }
                return response;
            });
        }

        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, SupplierVM supplierVM)
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
                    Supplier _supplier = _suppliersRepository.GetSingleBySupplierID(supplierVM.ID);
                    _supplier.UpdateSupplier(supplierVM);

                    _suppliersRepository.Commit();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, SupplierVM supplierVM)
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
                    supplierVM.IsDeleted = true;
                    Supplier _supplier = _suppliersRepository.GetSingleBySupplierID(supplierVM.ID);
                    _supplier.UpdateSupplier(supplierVM);

                    _suppliersRepository.Commit();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }

                return response;
            });
        }

    }
}
