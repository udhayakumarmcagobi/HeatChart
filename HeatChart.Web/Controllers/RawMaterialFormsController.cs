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
    [RoutePrefix("api/rawMaterialForms")]
    public class RawMaterialFormsController : ApiControllerBase
    {
        private readonly IEFRepository<Error> _errorRepository;
        private readonly IEFRepository<RawMaterialForm> _rawMaterialFormsRepository;
        public RawMaterialFormsController(IEFRepository<RawMaterialForm> rawMaterialFormsRepository,
            IEFRepository<Error> errorRepository
           ) : base(errorRepository)
        {
            _errorRepository = errorRepository;
            _rawMaterialFormsRepository = rawMaterialFormsRepository;
        }


        [HttpGet]
        [Route("search/{page:int=0}/{pageSize=4}/{filter?}")]
        public HttpResponseMessage Search(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {
            int currentPage = page.Value; int currentPageSize = pageSize.Value;

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null; List<RawMaterialForm> rawMaterialForms = null;

                int totalRawMaterialForms = 0;

                if (!string.IsNullOrEmpty(filter))
                {
                    filter = filter.Trim().ToLower();

                    rawMaterialForms = _rawMaterialFormsRepository.FindBy(c => c.Name.ToLower().Contains(filter) && c.IsDeleted == false)
                        .OrderByDescending(x => x.ModifiedOn)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totalRawMaterialForms = _rawMaterialFormsRepository.FindBy(c => c.IsDeleted == false && c.Name.ToLower().Contains(filter)).Count();
                }
                else
                {
                    rawMaterialForms = _rawMaterialFormsRepository.GetAll().Where(c => c.IsDeleted == false)
                                .OrderByDescending(x => x.ModifiedOn)
                                .Skip(currentPage * currentPageSize)
                                .Take(currentPageSize)
                            .ToList();

                    totalRawMaterialForms = _rawMaterialFormsRepository.FindBy(x => x.IsDeleted == false).Count();
                }
                
                IEnumerable<RawMaterialFormVM> rawMaterialFormsVM = AutoMapper.Map<IEnumerable<RawMaterialForm>, IEnumerable<RawMaterialFormVM>>(rawMaterialForms);

                PaginationSet<RawMaterialFormVM> pagedSet = new PaginationSet<RawMaterialFormVM>()
                {
                    Page = currentPage,
                    TotalCount = totalRawMaterialForms,
                    TotalPages = (int)Math.Ceiling((decimal)totalRawMaterialForms / currentPageSize),
                    Items = rawMaterialFormsVM
                };
                response = request.CreateResponse<PaginationSet<RawMaterialFormVM>>(HttpStatusCode.OK, pagedSet);
                return response;
            });
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(HttpRequestMessage request, RawMaterialFormVM rawMaterialFormVM)
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
                    if (_rawMaterialFormsRepository.RawMaterialFormExists(rawMaterialFormVM.Name))
                    {
                        ModelState.AddModelError("Invalid user", "Email or Name already exists");
                        response = request.CreateResponse(HttpStatusCode.BadRequest,
                            ModelState.Keys.SelectMany(k => ModelState[k].Errors).Select(m => m.ErrorMessage).ToArray());
                    }
                    else
                    {
                        RawMaterialForm newRawMaterialForm = new RawMaterialForm();
                        newRawMaterialForm = AutoMapper.Map<RawMaterialFormVM, RawMaterialForm>(rawMaterialFormVM);

                        _rawMaterialFormsRepository.Insert(newRawMaterialForm);
                        _rawMaterialFormsRepository.Commit();

                        // Update view model 
                        rawMaterialFormVM = AutoMapper.Map<RawMaterialForm, RawMaterialFormVM>(newRawMaterialForm);
                        response = request.CreateResponse<RawMaterialFormVM>(HttpStatusCode.Created, rawMaterialFormVM);
                    }
                }
                return response;
            });
        }

        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, RawMaterialFormVM rawMaterialFormVM)
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
                    RawMaterialForm _rawMaterialForm = _rawMaterialFormsRepository.GetSingleByRawMaterialFormID(rawMaterialFormVM.ID);
                    _rawMaterialForm.UpdateRawMaterialForm(rawMaterialFormVM);

                    _rawMaterialFormsRepository.Commit();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, RawMaterialFormVM rawMaterialFormVM)
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
                    rawMaterialFormVM.IsDeleted = true;
                    RawMaterialForm _rawMaterialForm = _rawMaterialFormsRepository.GetSingleByRawMaterialFormID(rawMaterialFormVM.ID);
                    _rawMaterialForm.UpdateRawMaterialForm(rawMaterialFormVM);

                    _rawMaterialFormsRepository.Commit();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }

                return response;
            });
        }

    }
}
