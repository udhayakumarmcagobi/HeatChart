using HeatChart.DataRepository.Sql.Interfaces;
using HeatChart.Entities.Sql;
using HeatChart.Entities.Sql.Domain;
using HeatChart.Entities.Sql.Interfaces;
using HeatChart.Infrastructure.Common.Utilities;
using HeatChart.Infrastructure.Dependency.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace HeatChart.Web.Core
{
    public class ApiControllerBaseExtended : ApiController
    {
        protected List<Type> _requiredRepositories;

        protected readonly IDataRepositoryFactory _dataRepositoryFactory;

        protected IEFRepository<Error> _errorsRepository;
        protected IEFRepository<Supplier> _suppliersRepository;
        protected IEFRepository<ThirdPartyInspection> _thirdPartyInspectionsRepository;
        protected IEFRepository<Specifications> _specificationsRepository;
        protected IEFRepository<Dimension> _dimensionRepository;
        protected IEFRepository<Customer> _customersRepository;
        protected IEFRepository<RawMaterialForm> _rawMaterialFormsRepository;
        protected IEFRepository<Test> _testsRepository;

        protected IEFRepository<MaterialRegisterHeader> _materialRegisterHeadersRepository;
        protected IEFRepository<MaterialRegisterSubSeries> _materialRegisterSubseriessRepository;

        protected IEFRepository<HeatChartHeader> _heatChartHeadersRepository;
        protected IEFRepository<HeatChartDetails> _heatChartDetailsRepository;

        protected IEFRepository<MillDetail> _millDetailsRepository;
        protected IEFRepository<LabReport> _labReportssRepository;

        protected IEFRepository<MaterialRegisterSubseriesTestRelationship> _materialRegisterSubseriesTestRelationshipRepository;

        protected IEFRepository<MaterialRegisterFileDetail> _materialRegisterFileDetailsRepository;

        protected IEFRepository<HeatChartMaterialHeaderRelationship> _heatChartMaterialHeaderRelationshipRepository;

        private HttpRequestMessage RequestMessage;
        public ApiControllerBaseExtended(IDataRepositoryFactory dataRepositoryFactory)
        {
            _dataRepositoryFactory = dataRepositoryFactory;            
        }

        protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage request, List<Type> repos, Func<HttpResponseMessage> function)
        {
            HttpResponseMessage response = null;

            try
            {
                RequestMessage = request;
                InitRepositories(repos);
                response = function.Invoke();
            }
            catch (DbUpdateException ex)
            {
                LogError(ex);
                response = request.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                LogError(ex);
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return response;
        }

        protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage request, Func<HttpResponseMessage> function)
        {
            HttpResponseMessage response = null;

            try
            {
                RequestMessage = request;
                response = function.Invoke();
            }
            catch (DbUpdateException ex)
            {
                LogError(ex);
                response = request.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                LogError(ex);
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return response;
        }
        private void InitRepositories(List<Type> entities)
        {
            _errorsRepository = _dataRepositoryFactory.GetDataRepository<Error>(RequestMessage);

            if (entities.Any(e => e.FullName == typeof(Customer).FullName))
            {
                _customersRepository = _dataRepositoryFactory.GetDataRepository<Customer>(RequestMessage);
            }

            if (entities.Any(e => e.FullName == typeof(Supplier).FullName))
            {
                _suppliersRepository = _dataRepositoryFactory.GetDataRepository<Supplier>(RequestMessage);
            }

            if (entities.Any(e => e.FullName == typeof(ThirdPartyInspection).FullName))
            {
                _thirdPartyInspectionsRepository = _dataRepositoryFactory.GetDataRepository<ThirdPartyInspection>(RequestMessage);
            }

            if (entities.Any(e => e.FullName == typeof(Specifications).FullName))
            {
                _specificationsRepository = _dataRepositoryFactory.GetDataRepository<Specifications>(RequestMessage);
            }

            if (entities.Any(e => e.FullName == typeof(Dimension).FullName))
            {
                _dimensionRepository = _dataRepositoryFactory.GetDataRepository<Dimension>(RequestMessage);
            }

            if (entities.Any(e => e.FullName == typeof(RawMaterialForm).FullName))
            {
                _rawMaterialFormsRepository = _dataRepositoryFactory.GetDataRepository<RawMaterialForm>(RequestMessage);
            }

            if (entities.Any(e => e.FullName == typeof(Test).FullName))
            {
                _testsRepository = _dataRepositoryFactory.GetDataRepository<Test>(RequestMessage);
            }

            if (entities.Any(e => e.FullName == typeof(Error).FullName))
            {
                _errorsRepository = _dataRepositoryFactory.GetDataRepository<Error>(RequestMessage);
            }

            if (entities.Any(e => e.FullName == typeof(MaterialRegisterHeader).FullName))
            {
                _materialRegisterHeadersRepository = _dataRepositoryFactory.GetDataRepository<MaterialRegisterHeader>(RequestMessage);
            }

            if (entities.Any(e => e.FullName == typeof(MaterialRegisterSubSeries).FullName))
            {
                _materialRegisterSubseriessRepository = _dataRepositoryFactory.GetDataRepository<MaterialRegisterSubSeries>(RequestMessage);
            }

            if (entities.Any(e => e.FullName == typeof(HeatChartHeader).FullName))
            {
                _heatChartHeadersRepository = _dataRepositoryFactory.GetDataRepository<HeatChartHeader>(RequestMessage);
            }

            if (entities.Any(e => e.FullName == typeof(HeatChartDetails).FullName))
            {
                _heatChartDetailsRepository = _dataRepositoryFactory.GetDataRepository<HeatChartDetails>(RequestMessage);
            }

            if (entities.Any(e => e.FullName == typeof(LabReport).FullName))
            {
                _labReportssRepository = _dataRepositoryFactory.GetDataRepository<LabReport>(RequestMessage);
            }

            if (entities.Any(e => e.FullName == typeof(MillDetail).FullName))
            {
                _millDetailsRepository = _dataRepositoryFactory.GetDataRepository<MillDetail>(RequestMessage);
            }

            if (entities.Any(e => e.FullName == typeof(MaterialRegisterSubseriesTestRelationship).FullName))
            {
                _materialRegisterSubseriesTestRelationshipRepository = _dataRepositoryFactory.GetDataRepository<MaterialRegisterSubseriesTestRelationship>(RequestMessage);
            }

            if (entities.Any(e => e.FullName == typeof(MaterialRegisterFileDetail).FullName))
            {
                _materialRegisterFileDetailsRepository = _dataRepositoryFactory.GetDataRepository<MaterialRegisterFileDetail>(RequestMessage);
            }

            if (entities.Any(e => e.FullName == typeof(HeatChartMaterialHeaderRelationship).FullName))
            {
                _heatChartMaterialHeaderRelationshipRepository = _dataRepositoryFactory.GetDataRepository<HeatChartMaterialHeaderRelationship>(RequestMessage);
            }
        }

        protected IEFRepository<TEntity> GetCurrentDataRepository<TEntity>(HttpRequestMessage request) where TEntity :class, IEntityBase, new()
        {
            return _dataRepositoryFactory.GetDataRepository<TEntity>(request);
        }

        private void LogError(Exception ex)
        {
            try
            {
                if (ConfigHelper.LogProviderRequests) ObjHelper.LogProviderRequest(ex, "ApiControllerBaseExtended");

                Error _error = new Error()
                {
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    DateCreated = DateTime.Now
                };

                _errorsRepository.Insert(_error);
                _errorsRepository.Commit();
            }
            catch { }
        }
    }
}