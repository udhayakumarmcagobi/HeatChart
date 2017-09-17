using HeatChart.DataRepository.Sql.Interfaces;
using HeatChart.Entities.Sql.Domain;
using HeatChart.Infrastructure.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HeatChart.Web.Core
{
    public class ApiControllerBase : ApiController
    {
        protected readonly IEFRepository<Error> _errorsRepository;

        public ApiControllerBase()
        {

        }
        public ApiControllerBase(IEFRepository<Error> errorRepository
            )
        {
            _errorsRepository = errorRepository;            
        }

        protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage request, Func<HttpResponseMessage> function)
        {
            HttpResponseMessage response = null;
            try
            {
                response = function.Invoke();
            }
            catch (DbUpdateException ex)
            {
                if (ConfigHelper.LogProviderRequests) ObjHelper.LogProviderRequest(ex, "ApiControllerBase");
                LogError(ex);
                response = request.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                if (ConfigHelper.LogProviderRequests) ObjHelper.LogProviderRequest(ex, "ApiControllerBase");
                LogError(ex);
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.InnerException?.Message);
                //throw;
            }
            return response;
        }

        protected void LogError(Exception ex)
        {
            try
            {
                Error _error = new Error()
                {
                    Message = ex.Message + ex.InnerException?.Message,
                    StackTrace = ex.StackTrace,
                    DateCreated = DateTime.UtcNow
                };

                _errorsRepository.Insert(_error);
                _errorsRepository.Commit();
            }
            catch (DbUpdateException dbEx)
            {              

            }
            catch (Exception exe)
            {
                throw;
            }
        }    
    }
}
