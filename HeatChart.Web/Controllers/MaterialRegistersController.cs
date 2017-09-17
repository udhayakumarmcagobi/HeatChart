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
using System.Diagnostics;

namespace HeatChart.Web.Controllers
{
    [Authorize(Roles = "Admin, User")]
    [RoutePrefix("api/materialregisters")]
    public class MaterialRegistersController : HeatChartBase
    {
        #region Constructors
        public MaterialRegistersController(IDataRepositoryFactory dataRepositoryFactory)
            : base(dataRepositoryFactory)
        {           
        }

        #endregion

        #region Material Register Headers

        [Route("recent")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            _requiredRepositories = new List<Type>() { typeof(MaterialRegisterHeader), typeof(Error) };

            return CreateHttpResponse(request, _requiredRepositories, () =>
            {
                HttpResponseMessage response = null;

                var materialRegisterHeaders = _materialRegisterHeadersRepository.GetAll().Where(x => !x.IsDeleted)
                                               .OrderByDescending(x => x.ModifiedOn).
                                               Take(4).ToList();

                var materialRegisterHeaderVMs = DomainToViewModelCustomMapper.MapMaterialRegisterHeaders(materialRegisterHeaders);

                response = request.CreateResponse(HttpStatusCode.OK, materialRegisterHeaderVMs);

                return response;
            });
        }

        [HttpGet]
        [Route("search/{page:int=0}/{pageSize=4}/{filter?}")]
        public HttpResponseMessage Search(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {
            _requiredRepositories = new List<Type>() { typeof(MaterialRegisterHeader), typeof(Error) };

            int currentPage = page.Value; int currentPageSize = pageSize.Value;

            return CreateHttpResponse(request, _requiredRepositories, () =>
            {
                HttpResponseMessage response = null;

                List<MaterialRegisterHeader> materialRegisterHeaders = null;

                int totalMaterialRegisters = 0;

                if (!string.IsNullOrEmpty(filter))
                {
                    filter = filter.Trim().ToLower();

                    materialRegisterHeaders = _materialRegisterHeadersRepository.FindBy(mr => mr.CTNumber.ToLower().Contains(filter) && mr.IsDeleted == false)
                        .OrderByDescending(mr => mr.ModifiedOn)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totalMaterialRegisters = _materialRegisterHeadersRepository.FindBy(c => c.IsDeleted == false && c.CTNumber.ToLower().Contains(filter)).Count();
                }
                else
                {
                    materialRegisterHeaders = _materialRegisterHeadersRepository.GetAll().Where(mr => mr.IsDeleted == false)
                                .OrderByDescending(mr => mr.ModifiedOn)
                                .Skip(currentPage * currentPageSize)
                                .Take(currentPageSize)
                            .ToList();

                    totalMaterialRegisters = _materialRegisterHeadersRepository.FindBy(x => x.IsDeleted == false).Count();
                }

                var materialRegisterHeaderVM = DomainToViewModelCustomMapper.MapMaterialRegisterHeaders(materialRegisterHeaders);

                PaginationSet<MaterialRegisterHeaderVM> pagedSet = new PaginationSet<MaterialRegisterHeaderVM>()
                {
                    Page = currentPage,
                    TotalCount = totalMaterialRegisters,
                    TotalPages = (int)Math.Ceiling((decimal)totalMaterialRegisters / currentPageSize),
                    Items = materialRegisterHeaderVM
                };
                response = request.CreateResponse<PaginationSet<MaterialRegisterHeaderVM>>(HttpStatusCode.OK, pagedSet);
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

                MaterialRegisterHeaderVM materialRegisterHeaderVM = new MaterialRegisterHeaderVM();

                GetMaterialRegisterHeaderVM(materialRegisterHeaderVM);

                if (ConfigurationReader.IsCheckTestNumberAutoCalculate)
                {
                    materialRegisterHeaderVM.CTNumber = AutoCalculateCTNumber();
                    materialRegisterHeaderVM.IsCheckTestNumberAutoCalculate = true;
                }

                response = request.CreateResponse(HttpStatusCode.OK, materialRegisterHeaderVM);

                return response;
            });
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(HttpRequestMessage request, MaterialRegisterHeaderVM materialRegisterHeaderVM)
        {
            _requiredRepositories = new List<Type>() { typeof(MaterialRegisterHeader), typeof(Error) };

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
                    if (_materialRegisterHeadersRepository.MaterialRegisterHeaderExists(materialRegisterHeaderVM.CTNumber))
                    {
                        ModelState.AddModelError("Invalid material register", "CT Number exists");
                        response = request.CreateResponse(HttpStatusCode.BadRequest,
                            ModelState.Keys.SelectMany(k => ModelState[k].Errors).Select(m => m.ErrorMessage).ToArray());
                    }
                    else
                    {
                        MaterialRegisterHeader newMaterialRegisterHeader = new MaterialRegisterHeader();
                        newMaterialRegisterHeader = AutoMapper.Map<MaterialRegisterHeaderVM, MaterialRegisterHeader>(materialRegisterHeaderVM);

                        _materialRegisterHeadersRepository.Insert(newMaterialRegisterHeader);
                        _materialRegisterHeadersRepository.Commit();

                        // Update view model 
                        materialRegisterHeaderVM = AutoMapper.Map<MaterialRegisterHeader, MaterialRegisterHeaderVM>(newMaterialRegisterHeader);
                        response = request.CreateResponse<MaterialRegisterHeaderVM>(HttpStatusCode.Created, materialRegisterHeaderVM);
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
                typeof(MaterialRegisterHeader),
                typeof(Error)                
            };

            currentRequestMessage = request;

            return CreateHttpResponse(request, _requiredRepositories, () =>
            {
                HttpResponseMessage response = null;

                var materialRegisterHeader = _materialRegisterHeadersRepository.GetSingleByMaterialRegisterHeaderID(ID);
                                               
                var materialRegisterHeaderVM = DomainToViewModelCustomMapper.MapMaterialRegisterHeader(materialRegisterHeader);

                GetMaterialRegisterHeaderVM(materialRegisterHeaderVM);

                response = request.CreateResponse(HttpStatusCode.OK, materialRegisterHeaderVM);

                return response;
            });
        }

        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, MaterialRegisterHeaderVM materialRegisterHeaderVM)
        {
            _requiredRepositories = new List<Type>()
            {
                typeof(MaterialRegisterHeader),
                typeof(MillDetail),
                typeof(LabReport),
                typeof(Error),
                typeof(MaterialRegisterSubSeries),
                typeof(MaterialRegisterSubseriesTestRelationship),
                typeof(MaterialRegisterFileDetail),
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
                    MaterialRegisterHeader _materialRegisterHeader = _materialRegisterHeadersRepository.GetSingleByMaterialRegisterHeaderID(materialRegisterHeaderVM.ID);

                    if (_materialRegisterHeader == null)
                    {
                        ModelState.AddModelError("Invalid material register", "CT Number does not exists");
                        response = request.CreateResponse(HttpStatusCode.BadRequest,
                            ModelState.Keys.SelectMany(k => ModelState[k].Errors).Select(m => m.ErrorMessage).ToArray());
                    }
                    else
                    {
                        
                        _materialRegisterHeader.UpdateMaterialRegisterHeader(materialRegisterHeaderVM);

                        UpdateSubSeriesListRemoveSubSeries(_materialRegisterHeader, materialRegisterHeaderVM);

                        UpdateSubSeriesListAddUpdate(_materialRegisterHeader, materialRegisterHeaderVM);

                        _materialRegisterHeadersRepository.Update(_materialRegisterHeader);

                        _materialRegisterHeadersRepository.Commit();
                    }

                    response = request.CreateResponse<MaterialRegisterHeaderVM>(HttpStatusCode.Created, materialRegisterHeaderVM);
                }
                return response;
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, MaterialRegisterHeaderVM materialRegisterHeaderVM)
        {
            _requiredRepositories = new List<Type>() { typeof(MaterialRegisterHeader), typeof(Error), typeof(MaterialRegisterSubSeries) };

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
                    MaterialRegisterHeader _materialRegisterHeader = _materialRegisterHeadersRepository.GetSingleByMaterialRegisterHeaderID(materialRegisterHeaderVM.ID);

                    if (_materialRegisterHeader == null)
                    {
                        ModelState.AddModelError("Invalid material register", "Material Register Does not exists");
                        response = request.CreateResponse(HttpStatusCode.BadRequest,
                            ModelState.Keys.SelectMany(k => ModelState[k].Errors).Select(m => m.ErrorMessage).ToArray());
                    }
                    else
                    {
                        _materialRegisterHeader.IsDeleted = true;

                        _materialRegisterHeadersRepository.Update(_materialRegisterHeader);

                        _materialRegisterHeadersRepository.Commit();
                    }

                    response = request.CreateResponse<MaterialRegisterHeaderVM>(HttpStatusCode.Created, materialRegisterHeaderVM);
                }
                return response;
            });
        }

        #endregion

        #region Material Register SubSeries

        [HttpGet]
        [Route("subseriescreate")]
        public HttpResponseMessage SubSeriesCreate(HttpRequestMessage request)
        {
            currentRequestMessage = request;

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var materialRegisterSubseries = GetMaterialRegisterSubSeriesVM();

                response = request.CreateResponse(HttpStatusCode.OK, materialRegisterSubseries);

                return response;
            });
        }

        [HttpGet]
        [Route("filtersubseries")]
        public HttpResponseMessage FilterSubSeries(HttpRequestMessage request, int materialHeaderID)
        {
            _requiredRepositories = new List<Type>() { typeof(MaterialRegisterSubSeries), typeof(Error) };

            return CreateHttpResponse(request, _requiredRepositories, () =>
            {
                HttpResponseMessage response = null;

                var materialRegisterSubSeries = _materialRegisterSubseriessRepository.GetAll().Where(x => x.MaterialRegisterHeaderID == materialHeaderID)
                                               .OrderBy(x => x.SubSeriesNumber);
                                             

                var materialRegisterSubSeriesVMs = DomainToViewModelCustomMapper.MapMaterialRegisterSubSeriesList(materialRegisterSubSeries.ToList());

                response = request.CreateResponse(HttpStatusCode.OK, materialRegisterSubSeriesVMs);

                return response;
            });
        }

        #endregion

        #region Helpers

        #region Material SubSeries
        private void UpdateSubSeriesListRemoveSubSeries(MaterialRegisterHeader materialRegisterHeader, MaterialRegisterHeaderVM materialRegisterHeaderVM)
        {
            if (materialRegisterHeader.MaterialRegisterSubSeriess == null || !materialRegisterHeader.MaterialRegisterSubSeriess.Any()) return;

            var deletableMaterialRegisterHeaders = materialRegisterHeader.MaterialRegisterSubSeriess.Where(sub => !materialRegisterHeaderVM.MaterialRegisterSubSeriess.Any(x => x.ID == sub.ID)).ToList();

            foreach (var materialSub in deletableMaterialRegisterHeaders)
            {
                if (materialSub.MillDetail != null)
                {
                    _millDetailsRepository.Delete(materialSub.MillDetail);
                }

                if (materialSub.LabReport != null)
                {
                    _labReportssRepository.Delete(materialSub.LabReport);
                }
                _materialRegisterSubseriessRepository.Delete(materialSub);
               
            }
        }

        private void UpdateSubSeriesListAddUpdate(MaterialRegisterHeader materialRegisterHeader, MaterialRegisterHeaderVM materialRegisterHeaderVM)
        {
            if (materialRegisterHeaderVM.MaterialRegisterSubSeriess == null || !materialRegisterHeaderVM.MaterialRegisterSubSeriess.Any()) return;

            foreach (var materialSub in materialRegisterHeaderVM.MaterialRegisterSubSeriess)
            {                
                var existingSubSeries = materialRegisterHeader.MaterialRegisterSubSeriess.Where(x => x.ID == materialSub.ID).SingleOrDefault();

                if(existingSubSeries != null)
                {
                    existingSubSeries.UpdateMaterialRegisterSubSeries(materialSub);

                    UpdateSubSeriesTestRemoveTestRelationship(existingSubSeries, materialSub);
                    UpdateSubSeriesTestAddUpdate(existingSubSeries, materialSub);

                    UpdateMillDetailsAddUpdate(existingSubSeries, materialSub);
                    UpdateLabReportsAddUpdate(existingSubSeries, materialSub);

                    UpdateSubSeriesRemoveFileDetails(existingSubSeries, materialSub);
                    UpdateSubSeriesFileDetailsAddUpdate(existingSubSeries, materialSub);

                    _materialRegisterSubseriessRepository.Update(existingSubSeries);
                }
                else
                {
                    var updatedMaterialSubSeries = AutoMapper.Map<MaterialRegisterSubSeriesVM, MaterialRegisterSubSeries>(materialSub);
                    materialRegisterHeader.MaterialRegisterSubSeriess.Add(updatedMaterialSubSeries);
                }
            }
        }

        #endregion

        #region Mill and Lab
        private void UpdateMillDetailsAddUpdate(MaterialRegisterSubSeries materialRegisterSubSeries, MaterialRegisterSubSeriesVM materialRegisterSubSeriesVM)
        {
            if (materialRegisterSubSeriesVM.MillDetail == null || string.IsNullOrEmpty(materialRegisterSubSeriesVM.MillDetail.TCNumber) )
            {
                if (materialRegisterSubSeries.MillDetail == null) return;
                               
                _millDetailsRepository.Delete(materialRegisterSubSeries.MillDetail);
                return;
              
            }

            var millDetail = materialRegisterSubSeries.MillDetail;

            if (millDetail != null)
            {
                millDetail.UpdateMillDetail(materialRegisterSubSeriesVM.MillDetail);
                _millDetailsRepository.Update(millDetail);
            }
            else
            {
                millDetail = AutoMapper.Map<MillDetailVM, MillDetail>(materialRegisterSubSeriesVM.MillDetail);
                millDetail.ID = materialRegisterSubSeriesVM.ID;
                _millDetailsRepository.Insert(millDetail);
            }           
        }

        private void UpdateLabReportsAddUpdate(MaterialRegisterSubSeries materialRegisterSubSeries, MaterialRegisterSubSeriesVM materialRegisterSubSeriesVM)
        {
            if (materialRegisterSubSeriesVM.LabReport == null || string.IsNullOrEmpty(materialRegisterSubSeriesVM.LabReport.TCNumber))
            {
                if (materialRegisterSubSeries.LabReport == null) return;

                _labReportssRepository.Delete(materialRegisterSubSeries.LabReport);
                return;              
            }

            var labReport = materialRegisterSubSeries.LabReport;

            if (labReport != null)
            {
                labReport.UpdateLabReport(materialRegisterSubSeriesVM.LabReport);
                _labReportssRepository.Update(labReport);
            }
            else
            {
                labReport = AutoMapper.Map<LabReportVM, LabReport>(materialRegisterSubSeriesVM.LabReport);
                labReport.ID = materialRegisterSubSeriesVM.ID;

                _labReportssRepository.Insert(labReport);
            }
        }

        #endregion

        #region MaterialRegisterSubseries Test Relationship
        private void UpdateSubSeriesTestRemoveTestRelationship(MaterialRegisterSubSeries materialRegisterSubSeries,
                                                                MaterialRegisterSubSeriesVM materialRegisterSubSeriesVM)
        {
            if (materialRegisterSubSeries.MaterialRegisterSubSeriesTestRelationships == null || !materialRegisterSubSeries.MaterialRegisterSubSeriesTestRelationships.Any()) return;

            var deletableMaterialSubSeriesTestRelationsips = 
                materialRegisterSubSeries.MaterialRegisterSubSeriesTestRelationships.Where(rel => !materialRegisterSubSeriesVM.SelectedTests.Any(x => x.ID == rel.TestID)).ToList();

            foreach (var testRelation in deletableMaterialSubSeriesTestRelationsips)
            {
                _materialRegisterSubseriesTestRelationshipRepository .Delete(testRelation);
            }
        }

        private void UpdateSubSeriesTestAddUpdate(MaterialRegisterSubSeries materialRegisterSubSeries, MaterialRegisterSubSeriesVM materialRegisterSubSeriesVM)
        {
            if (materialRegisterSubSeriesVM.SelectedTests == null || !materialRegisterSubSeriesVM.SelectedTests.Any()) return;

            foreach (var testRelation in materialRegisterSubSeriesVM.SelectedTests)
            {
                var existingTestRelationship = materialRegisterSubSeries.MaterialRegisterSubSeriesTestRelationships.Where(x => x.TestID == testRelation.ID).SingleOrDefault();

                if (existingTestRelationship != null)
                {
                    existingTestRelationship.UpdateMaterialSubSeriesTestRelationship(testRelation, materialRegisterSubSeries.ID);
                    _materialRegisterSubseriesTestRelationshipRepository.Update(existingTestRelationship);
                }
                else
                {
                    var newTestRelationship = new MaterialRegisterSubseriesTestRelationship()
                    {
                        MaterialRegisterSubSeriesID = materialRegisterSubSeries.ID,
                        TestID = testRelation.ID
                    };

                    materialRegisterSubSeries.MaterialRegisterSubSeriesTestRelationships.Add(newTestRelationship);
                }
            }
        }

        #endregion

        #region Material Register File Details
        private void UpdateSubSeriesRemoveFileDetails(MaterialRegisterSubSeries materialRegisterSubSeries,
                                                                MaterialRegisterSubSeriesVM materialRegisterSubSeriesVM)
        {
            if (materialRegisterSubSeries.MaterialRegisterFileDetails == null || !materialRegisterSubSeries.MaterialRegisterFileDetails.Any()) return;

            var deletableSubSeriesFileDetails =
                materialRegisterSubSeries.MaterialRegisterFileDetails.Where(file => !materialRegisterSubSeriesVM.MaterialRegisterFileDetails.Any(x => x.ID == file.ID)).ToList();

            foreach (var fileDetail in deletableSubSeriesFileDetails)
            {
                _materialRegisterFileDetailsRepository.Delete(fileDetail);
            }
        }

        private void UpdateSubSeriesFileDetailsAddUpdate(MaterialRegisterSubSeries materialRegisterSubSeries, MaterialRegisterSubSeriesVM materialRegisterSubSeriesVM)
        {
            if (materialRegisterSubSeriesVM.MaterialRegisterFileDetails == null || !materialRegisterSubSeriesVM.MaterialRegisterFileDetails.Any()) return;

            foreach (var fileDetail in materialRegisterSubSeriesVM.MaterialRegisterFileDetails)
            {
                var existingMaterialFileDetail= materialRegisterSubSeries.MaterialRegisterFileDetails.Where(x => x.ID == fileDetail.ID).SingleOrDefault();

                if (existingMaterialFileDetail != null)
                {
                    existingMaterialFileDetail.UpdateMaterialRegisterFileDetails(fileDetail, materialRegisterSubSeries.ID);
                    _materialRegisterFileDetailsRepository.Update(existingMaterialFileDetail);
                }
                else
                {
                    var newFileDetail = new MaterialRegisterFileDetail()
                    {
                        FileName = fileDetail.FileName,
                        Path = fileDetail.Path,
                        MaterialRegisterSubSeriesID = materialRegisterSubSeries.ID
                    };

                    materialRegisterSubSeries.MaterialRegisterFileDetails.Add(newFileDetail);
                }
            }
        }

        #endregion

        private void GetMaterialRegisterHeaderVM(MaterialRegisterHeaderVM materialRegisterHeaderVM)
        {
            materialRegisterHeaderVM.Suppliers = SupplierList;
            materialRegisterHeaderVM.ThirdPartyInspections = ThirdPartyInspectionList;
            materialRegisterHeaderVM.RawMaterialForms = RawMaterialFormList;
            materialRegisterHeaderVM.Specifications = SpecificationList;
            materialRegisterHeaderVM.Dimensions = DimensionList;
        }

        private MaterialRegisterSubSeriesVM GetMaterialRegisterSubSeriesVM()
        {
            return new MaterialRegisterSubSeriesVM()
            {               
                Tests = TestList                
            };
        }

        private string AutoCalculateCTNumber()
        {           
            List<String> CTNumberList = MaterialRegisterHeaders.Select(x => x.CTNumber).ToList();

            return BusinessUtilties.AutoCalculateCTNumber(CTNumberList);

        }

        #endregion

    }
}
