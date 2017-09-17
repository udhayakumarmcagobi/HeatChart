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
    [RoutePrefix("api/specifications")]
    public class SpecificationsController : ApiControllerBase
    {
        private readonly IEFRepository<Error> _errorRepository;
        private readonly IEFRepository<Specifications> _specificationsRepository;
        public SpecificationsController(IEFRepository<Specifications> specificationsRepository,
            IEFRepository<Error> errorRepository
           ) : base(errorRepository)
        {
            _errorRepository = errorRepository;
            _specificationsRepository = specificationsRepository;
        }


        [HttpGet]
        [Route("search/{page:int=0}/{pageSize=4}/{filter?}")]
        public HttpResponseMessage Search(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {
            int currentPage = page.Value; int currentPageSize = pageSize.Value;

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null; List<Specifications> specifications = null;

                int totalSpecifications = 0;

                if (!string.IsNullOrEmpty(filter))
                {
                    filter = filter.Trim().ToLower();

                    specifications = _specificationsRepository.FindBy(c => c.Name.ToLower().Contains(filter) && c.IsDeleted == false)
                        .OrderByDescending(x => x.ModifiedOn)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totalSpecifications = _specificationsRepository.FindBy(c => c.IsDeleted == false && c.Name.ToLower().Contains(filter)).Count();
                }
                else
                {
                    specifications = _specificationsRepository.GetAll().Where(c => c.IsDeleted == false)
                               .OrderByDescending(x => x.ModifiedOn)
                                .Skip(currentPage * currentPageSize)
                                .Take(currentPageSize)
                            .ToList();

                    totalSpecifications = _specificationsRepository.FindBy(x => x.IsDeleted == false).Count();
                }
                
                IEnumerable<SpecificationsVM> specificationsVM = AutoMapper.Map<IEnumerable<Specifications>, IEnumerable<SpecificationsVM>>(specifications);

                PaginationSet<SpecificationsVM> pagedSet = new PaginationSet<SpecificationsVM>()
                {
                    Page = currentPage,
                    TotalCount = totalSpecifications,
                    TotalPages = (int)Math.Ceiling((decimal)totalSpecifications / currentPageSize),
                    Items = specificationsVM
                };
                response = request.CreateResponse<PaginationSet<SpecificationsVM>>(HttpStatusCode.OK, pagedSet);
                return response;
            });
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(HttpRequestMessage request, SpecificationsVM specificationVM)
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
                    if (_specificationsRepository.SpecificationExists(specificationVM.Name))
                    {
                        ModelState.AddModelError("Invalid user", "Email or Name already exists");
                        response = request.CreateResponse(HttpStatusCode.BadRequest,
                            ModelState.Keys.SelectMany(k => ModelState[k].Errors).Select(m => m.ErrorMessage).ToArray());
                    }
                    else
                    {
                        Specifications newSpecification = new Specifications();
                        newSpecification = AutoMapper.Map<SpecificationsVM, Specifications>(specificationVM);

                        _specificationsRepository.Insert(newSpecification);
                        _specificationsRepository.Commit();

                        // Update view model 
                        specificationVM = AutoMapper.Map<Specifications, SpecificationsVM>(newSpecification);
                        response = request.CreateResponse<SpecificationsVM>(HttpStatusCode.Created, specificationVM);
                    }
                }
                return response;
            });
        }

        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, SpecificationsVM specificationVM)
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
                    Specifications _specification = _specificationsRepository.GetSingleBySpecificationsID(specificationVM.ID);
                    _specification.UpdateSpecifications(specificationVM);

                    _specificationsRepository.Commit();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, SpecificationsVM specificationVM)
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
                    specificationVM.IsDeleted = true;
                    Specifications _specification = _specificationsRepository.GetSingleBySpecificationsID(specificationVM.ID);
                    _specification.UpdateSpecifications(specificationVM);

                    _specificationsRepository.Commit();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }

                return response;
            });
        }

    }
}
