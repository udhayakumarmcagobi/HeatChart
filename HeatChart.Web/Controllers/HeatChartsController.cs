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
using HeatChart.Infrastructure.Dependency.Core;
using HeatChart.Entities.Sql;
using HeatHeatChart.ViewModels;
using ModelMapper.DomainToViewModel;
using HeatChart.Infrastructure.Common.Utilities;
using System.Runtime.Caching;

namespace HeatChart.Web.Controllers
{
    [Authorize(Roles = "Admin, User")]
    [RoutePrefix("api/heatcharts")]
    public class HeatChartsController : HeatChartBase
    {
        #region Constructors
        public HeatChartsController(IDataRepositoryFactory dataRepositoryFactory)
            : base(dataRepositoryFactory)
        {           
        }

        #endregion

        #region Heat Chart Headers

        [Route("recent")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            _requiredRepositories = new List<Type>() { typeof(HeatChartHeader), typeof(Error) };

            return CreateHttpResponse(request, _requiredRepositories, () =>
            {
                HttpResponseMessage response = null;

                var heatChartHeaders = _heatChartHeadersRepository.GetAll()
                                               .OrderByDescending(x => x.ModifiedOn)
                                               .Take(2).ToList();

                var heatChartHeaderVMs = DomainToViewModelCustomMapper.MapHeatChartHeaders(heatChartHeaders);

                response = request.CreateResponse(HttpStatusCode.OK, heatChartHeaderVMs);

                return response;
            });
        }

        [HttpGet]
        [Route("search/{page:int=0}/{pageSize=4}/{filter?}")]
        public HttpResponseMessage Search(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {
            _requiredRepositories = new List<Type>() { typeof(HeatChartHeader), typeof(Error) };

            int currentPage = page.Value; int currentPageSize = pageSize.Value;

            return CreateHttpResponse(request, _requiredRepositories, () =>
            {
                HttpResponseMessage response = null;

                List<HeatChartHeader> heatChartHeaders = null;

                int totalHeatCharts = 0;

                if (!string.IsNullOrEmpty(filter))
                {
                    filter = filter.Trim().ToLower();

                    heatChartHeaders = _heatChartHeadersRepository.FindBy(mr => mr.HeatChartNumber.ToLower().Contains(filter))
                        .OrderByDescending(mr => mr.ModifiedOn)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totalHeatCharts = _heatChartHeadersRepository.FindBy(c => c.HeatChartNumber.ToLower().Contains(filter)).Count();
                }
                else
                {
                    heatChartHeaders = _heatChartHeadersRepository.GetAll()
                               .OrderByDescending(mr => mr.ModifiedOn)
                                .Skip(currentPage * currentPageSize)
                                .Take(currentPageSize)
                            .ToList();

                    totalHeatCharts = _heatChartHeadersRepository.GetAll().Count();
                }

                var heatChartHeaderVM = DomainToViewModelCustomMapper.MapHeatChartHeaders(heatChartHeaders);

                

                PaginationSet<HeatChartHeaderVM> pagedSet = new PaginationSet<HeatChartHeaderVM>()
                {
                    Page = currentPage,
                    TotalCount = totalHeatCharts,
                    TotalPages = (int)Math.Ceiling((decimal)totalHeatCharts / currentPageSize),
                    Items = heatChartHeaderVM
                };
                response = request.CreateResponse<PaginationSet<HeatChartHeaderVM>>(HttpStatusCode.OK, pagedSet);
                return response;
            });
        }

        [HttpGet]
        [Route("create")]
        public HttpResponseMessage Create(HttpRequestMessage request)
        {
            currentRequestMessage = request;

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                HeatChartHeaderVM heatChartHeaderVM = new HeatChartHeaderVM();

                GetHeatChartHeaderVM(heatChartHeaderVM);

                if (ConfigurationReader.IsHeatChartNumberAutoCalculate)
                {
                    heatChartHeaderVM.HeatChartNumber = AutoCalculateHCNumber();
                    heatChartHeaderVM.IsHeatChartNumberAutoCalculate = true;
                }

                response = request.CreateResponse(HttpStatusCode.OK, heatChartHeaderVM);

                return response;
            });
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(HttpRequestMessage request, HeatChartHeaderVM heatChartHeaderVM)
        {
            _requiredRepositories = new List<Type>() { typeof(HeatChartHeader), typeof(Error) };

            return CreateHttpResponse(request, _requiredRepositories, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest,
                        ModelState.Keys.SelectMany(k => ModelState[k].Errors).Select(m => m.ErrorMessage).ToArray());
                }
                else
                {
                    if (_heatChartHeadersRepository.HeatChartHeaderExists(heatChartHeaderVM.HeatChartNumber))
                    {
                        ModelState.AddModelError("Invalid Heat Chart", "Heat Chart Number exists");
                        response = request.CreateResponse(HttpStatusCode.BadRequest,
                            ModelState.Keys.SelectMany(k => ModelState[k].Errors).Select(m => m.ErrorMessage).ToArray());
                    }
                    else
                    {
                        HeatChartHeader newHeatChartHeader = new HeatChartHeader();
                        newHeatChartHeader = AutoMapper.Map<HeatChartHeaderVM, HeatChartHeader>(heatChartHeaderVM);

                        _heatChartHeadersRepository.Insert(newHeatChartHeader);
                        _heatChartHeadersRepository.Commit();

                        // Update view model 
                        heatChartHeaderVM = AutoMapper.Map<HeatChartHeader, HeatChartHeaderVM>(newHeatChartHeader);
                        response = request.CreateResponse<HeatChartHeaderVM>(HttpStatusCode.Created, heatChartHeaderVM);
                    }
                }
                return response;
            });
        }

        [HttpGet]
        [Route("edit")]
        public HttpResponseMessage Edit(HttpRequestMessage request, int ID)
        {
            _requiredRepositories = new List<Type>()
            {
                typeof(HeatChartHeader),
                typeof(Error)                
            };

            currentRequestMessage = request;

            return CreateHttpResponse(request, _requiredRepositories, () =>
            {
                HttpResponseMessage response = null;

                var heatChartHeader = _heatChartHeadersRepository.GetSingleByHeatChartHeaderID(ID);
                                               
                var heatChartHeaderVM = DomainToViewModelCustomMapper.MapHeatChartHeader(heatChartHeader);

                GetHeatChartHeaderVM(heatChartHeaderVM);

                response = request.CreateResponse(HttpStatusCode.OK, heatChartHeaderVM);

                return response;
            });
        }

        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, HeatChartHeaderVM heatChartHeaderVM)
        {
            _requiredRepositories = new List<Type>()
            {
                typeof(HeatChartHeader),
                typeof(HeatChartDetails),
                typeof(HeatChartMaterialHeaderRelationship),
                typeof(Error)
            };

            return CreateHttpResponse(request, _requiredRepositories, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest,
                        ModelState.Keys.SelectMany(k => ModelState[k].Errors).Select(m => m.ErrorMessage).ToArray());
                }
                else
                {
                    HeatChartHeader _heatChartHeader = _heatChartHeadersRepository.GetSingleByHeatChartHeaderID(heatChartHeaderVM.ID);

                    if (_heatChartHeader == null)
                    {
                        ModelState.AddModelError("Invalid Heat Chart", "Heat Chart Number does not exists");
                        response = request.CreateResponse(HttpStatusCode.BadRequest,
                            ModelState.Keys.SelectMany(k => ModelState[k].Errors).Select(m => m.ErrorMessage).ToArray());
                    }
                    else
                    {
                        _heatChartHeader.UpdateHeatChartHeader(heatChartHeaderVM);

                        UpdateHeatChartDetailListRemoveHeatChartDetail(_heatChartHeader, heatChartHeaderVM);

                        UpdateHeatChartDetailListAddUpdate(_heatChartHeader, heatChartHeaderVM);

                        _heatChartHeadersRepository.Update(_heatChartHeader);

                        _heatChartHeadersRepository.Commit();
                    }

                    response = request.CreateResponse<HeatChartHeaderVM>(HttpStatusCode.Created, heatChartHeaderVM);
                }
                return response;
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, HeatChartHeaderVM heatChartHeaderVM)
        {
            _requiredRepositories = new List<Type>() { typeof(HeatChartHeader), typeof(Error), typeof(HeatChartDetails) };

            return CreateHttpResponse(request, _requiredRepositories, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest,
                        ModelState.Keys.SelectMany(k => ModelState[k].Errors).Select(m => m.ErrorMessage).ToArray());
                }
                else
                {
                    HeatChartHeader _heatChartHeader = _heatChartHeadersRepository.GetSingleByHeatChartHeaderID(heatChartHeaderVM.ID);

                    if (_heatChartHeader == null)
                    {
                        ModelState.AddModelError("Invalid Heat Chart", "Heat Chart Does not exists");
                        response = request.CreateResponse(HttpStatusCode.BadRequest,
                            ModelState.Keys.SelectMany(k => ModelState[k].Errors).Select(m => m.ErrorMessage).ToArray());
                    }
                    else
                    {
                        _heatChartHeader.IsDeleted = true;

                        _heatChartHeadersRepository.Update(_heatChartHeader);

                        _heatChartHeadersRepository.Commit();
                    }

                    response = request.CreateResponse<HeatChartHeaderVM>(HttpStatusCode.Created, heatChartHeaderVM);
                }
                return response;
            });
        }

        #endregion

        #region Material Register SubSeries

        [HttpGet]
        [Route("heatchartdetailscreate")]
        public HttpResponseMessage HeatChartDetailsCreate(HttpRequestMessage request)
        {
            currentRequestMessage = request;

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var heatChartSubseries = GetHeatChartDetailsVM();

                

                response = request.CreateResponse(HttpStatusCode.OK, heatChartSubseries);

                return response;
            });
        }

        #endregion

        #region Helpers

        #region Material SubSeries
        private void UpdateHeatChartDetailListRemoveHeatChartDetail(HeatChartHeader heatChartHeader, HeatChartHeaderVM heatChartHeaderVM)
        {
            if (heatChartHeader.HeatChartDetails == null || !heatChartHeader.HeatChartDetails.Any()) return;

            var deletableHeatChartDetails = heatChartHeader.HeatChartDetails.
                                            Where(sub => !heatChartHeaderVM.HeatChartDetails.Any(x => x.ID.Equals(sub.ID))).ToList();

            foreach (var heatChartDetail in deletableHeatChartDetails)
            {
                if (heatChartDetail.HeathChartMaterialHeaderRelationships != null)
                {
                    _heatChartMaterialHeaderRelationshipRepository.Delete(heatChartDetail.HeathChartMaterialHeaderRelationships);
                }

                _heatChartDetailsRepository.Delete(heatChartDetail);
            }
        }

        private void UpdateHeatChartDetailListAddUpdate(HeatChartHeader heatChartHeader, HeatChartHeaderVM heatChartHeaderVM)
        {
            if (heatChartHeaderVM.HeatChartDetails == null || !heatChartHeaderVM.HeatChartDetails.Any()) return;

            foreach (var heatChartDetail in heatChartHeaderVM.HeatChartDetails)
            {
                var existingHeatChartDetail = heatChartHeader.HeatChartDetails.Where(x => x.ID == heatChartDetail.ID && x.ID > 0).SingleOrDefault();

                if (existingHeatChartDetail != null)
                {
                    existingHeatChartDetail.UpdateHeatChartDetails(heatChartDetail);

                    UpdateHeatChartDetailMaterialRegisterRelationshipAddUpdate(existingHeatChartDetail, heatChartDetail);

                    _heatChartDetailsRepository.Update(existingHeatChartDetail);
                }
                else
                {
                    var updatedHeatChartDetail = AutoMapper.Map<HeatChartDetailsVM, HeatChartDetails>(heatChartDetail);

                    heatChartHeader.HeatChartDetails.Add(updatedHeatChartDetail);
                }
            }
        }

        private void UpdateHeatChartDetailMaterialRegisterRelationshipAddUpdate(HeatChartDetails heatChartDetails, HeatChartDetailsVM heatChartDetailsVM)
                                                                                
        {

            var relationship = heatChartDetails.HeathChartMaterialHeaderRelationships;

            if(relationship == null)
            {
                relationship = new HeatChartMaterialHeaderRelationship();
            }

            relationship.MaterialRegisterHeaderID = heatChartDetailsVM.MaterialRegisterHeaderSelected.ID;
            relationship.MaterialRegisterSubSeriesID = heatChartDetailsVM.MaterialRegisterSubSeriesSelected.ID;

            if (relationship != null)
            {               
                _heatChartMaterialHeaderRelationshipRepository.Update(relationship);
            }
            else
            {
                _heatChartMaterialHeaderRelationshipRepository.Insert(relationship);
            }
        }

        #endregion
        private void GetHeatChartHeaderVM(HeatChartHeaderVM heatChartHeaderVM)
        {
            heatChartHeaderVM.Customers = CustomerList;
            heatChartHeaderVM.ThirdPartyInspections = ThirdPartyInspectionList;                        
        }

        private HeatChartDetailsVM GetHeatChartDetailsVM()
        {
            return new HeatChartDetailsVM()
            {
                Specifications = SpecificationList,
                Dimensions = DimensionList,
                MaterialRegisterHeaders = FilterValidMaterialRegisterHeaders(DomainToViewModelCustomMapper.MapMaterialRegisterHeaders(MaterialRegisterHeaders)),
                MaterialRegisterSubSeries = FilterValidMaterialRegisterSubSeries(DomainToViewModelCustomMapper.MapMaterialRegisterSubSeriesList(MaterialRegisterSubSeries))
            };
        }

        private string AutoCalculateHCNumber()
        {     
            List<String> HCNumberList = HeatChartHeaders.Select(x => x.HeatChartNumber).ToList();

            return BusinessUtilties.AutoCalculateHeatChartNumber(HCNumberList);

        }

        private List<MaterialRegisterHeaderVM> FilterValidMaterialRegisterHeaders(List<MaterialRegisterHeaderVM> materialRegisterHeaders)
        {
            return materialRegisterHeaders.Where(x => x.Status.Equals("Accepted") || x.Status.Equals("PartiallyAccepted")).ToList();
        }

        private List<MaterialRegisterSubSeriesVM> FilterValidMaterialRegisterSubSeries(List<MaterialRegisterSubSeriesVM> materialRegisterSubSeries)
        {
            return materialRegisterSubSeries.Where(x => x.Status.Equals("Accepted") || x.Status.Equals("PartiallyAccepted")).ToList();
        }

        #endregion

    }
}
