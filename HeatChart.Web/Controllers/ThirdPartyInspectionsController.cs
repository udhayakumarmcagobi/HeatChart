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
    [RoutePrefix("api/thirdPartyInspections")]
    public class ThirdPartyInspectionsController : ApiControllerBase
    {
        private readonly IEFRepository<Error> _errorRepository;
        private readonly IEFRepository<ThirdPartyInspection> _thirdPartyInspectionsRepository;
        public ThirdPartyInspectionsController(IEFRepository<ThirdPartyInspection> thirdPartyInspectionsRepository,
            IEFRepository<Error> errorRepository
           ) : base(errorRepository)
        {
            _errorRepository = errorRepository;
            _thirdPartyInspectionsRepository = thirdPartyInspectionsRepository;
        }


        [HttpGet]
        [Route("search/{page:int=0}/{pageSize=4}/{filter?}")]
        public HttpResponseMessage Search(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {
            int currentPage = page.Value; int currentPageSize = pageSize.Value;

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null; List<ThirdPartyInspection> thirdPartyInspections = null;

                int totalThirdPartyInspections = 0;

                if (!string.IsNullOrEmpty(filter))
                {
                    filter = filter.Trim().ToLower();

                    thirdPartyInspections = _thirdPartyInspectionsRepository.FindBy(c => c.Name.ToLower().Contains(filter) && c.IsDeleted == false)
                        .OrderByDescending(x => x.ModifiedOn)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totalThirdPartyInspections = _thirdPartyInspectionsRepository.FindBy(c => c.IsDeleted == false && c.Name.ToLower().Contains(filter)).Count();
                }
                else
                {
                    thirdPartyInspections = _thirdPartyInspectionsRepository.GetAll().Where(c => c.IsDeleted == false)
                                .OrderByDescending(x => x.ModifiedOn)
                                .Skip(currentPage * currentPageSize)
                                .Take(currentPageSize)
                            .ToList();

                    totalThirdPartyInspections = _thirdPartyInspectionsRepository.FindBy(x => x.IsDeleted == false).Count();
                }
                
                IEnumerable<ThirdPartyInspectionVM> thirdPartyInspectionsVM = AutoMapper.Map<IEnumerable<ThirdPartyInspection>, IEnumerable<ThirdPartyInspectionVM>>(thirdPartyInspections);

                PaginationSet<ThirdPartyInspectionVM> pagedSet = new PaginationSet<ThirdPartyInspectionVM>()
                {
                    Page = currentPage,
                    TotalCount = totalThirdPartyInspections,
                    TotalPages = (int)Math.Ceiling((decimal)totalThirdPartyInspections / currentPageSize),
                    Items = thirdPartyInspectionsVM
                };
                response = request.CreateResponse<PaginationSet<ThirdPartyInspectionVM>>(HttpStatusCode.OK, pagedSet);
                return response;
            });
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(HttpRequestMessage request, ThirdPartyInspectionVM thirdPartyInspectionVM)
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
                    if (_thirdPartyInspectionsRepository.ThirdPartyInspectionExists(thirdPartyInspectionVM.Email, thirdPartyInspectionVM.Name))
                    {
                        ModelState.AddModelError("Invalid user", "Email or Name already exists");
                        response = request.CreateResponse(HttpStatusCode.BadRequest,
                            ModelState.Keys.SelectMany(k => ModelState[k].Errors).Select(m => m.ErrorMessage).ToArray());
                    }
                    else
                    {
                        ThirdPartyInspection newThirdPartyInspection = new ThirdPartyInspection();
                        newThirdPartyInspection = AutoMapper.Map<ThirdPartyInspectionVM, ThirdPartyInspection>(thirdPartyInspectionVM);

                        _thirdPartyInspectionsRepository.Insert(newThirdPartyInspection);
                        _thirdPartyInspectionsRepository.Commit();

                        // Update view model 
                        thirdPartyInspectionVM = AutoMapper.Map<ThirdPartyInspection, ThirdPartyInspectionVM>(newThirdPartyInspection);
                        response = request.CreateResponse<ThirdPartyInspectionVM>(HttpStatusCode.Created, thirdPartyInspectionVM);
                    }
                }
                return response;
            });
        }

        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, ThirdPartyInspectionVM thirdPartyInspectionVM)
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
                    ThirdPartyInspection _thirdPartyInspection = _thirdPartyInspectionsRepository.GetSingleByThirdPartyInspectionID(thirdPartyInspectionVM.ID);
                    _thirdPartyInspection.UpdateThirdPartyInspection(thirdPartyInspectionVM);

                    _thirdPartyInspectionsRepository.Commit();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, ThirdPartyInspectionVM thirdPartyInspectionVM)
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
                    thirdPartyInspectionVM.IsDeleted = true;
                    ThirdPartyInspection _thirdPartyInspection = _thirdPartyInspectionsRepository.GetSingleByThirdPartyInspectionID(thirdPartyInspectionVM.ID);
                    _thirdPartyInspection.UpdateThirdPartyInspection(thirdPartyInspectionVM);

                    _thirdPartyInspectionsRepository.Commit();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }

                return response;
            });
        }

    }
}
