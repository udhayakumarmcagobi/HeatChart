using HeatChart.DataRepository.Sql.Interfaces;
using HeatChart.Entities.Sql.Domain;
using HeatChart.Web.Core;
using System.Net.Http;
using System.Web.Http;
using System.Linq;
using HeatChart.DataRepository.Sql.Extensions;
using System.Net;
using HeatChart.Entities.Sql;
using System.Web;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http.Headers;
using HeatHeatChart.ViewModels;
using HeatChart.Web.Core.Report;
using System;
using System.Collections.Generic;
using ModelMapper.DomainToViewModel;
using ModelMapper;
using HeatChart.ViewModels.DatasetVM;
using HeatChart.Infrastructure.Common.Utilities;
using System.Diagnostics;
using HeatChart.Web.Core.AWS;

namespace HeatChart.Web.Controllers
{
    [Authorize(Roles = "Admin, User")]
    [RoutePrefix("api/reports")]
    public class ReportsController : ApiControllerBase
    {
        private readonly IEFRepository<HeatChartHeader> _heatChartHeaderRepository;
        private readonly IEFRepository<MaterialRegisterHeader> _materialRegisterHeaderRepository;                        

        #region Constructors
        public ReportsController(IEFRepository<HeatChartHeader> heatChartHeaderRepository,
            IEFRepository<MaterialRegisterHeader> materialRegisterHeaderRepository,
            IEFRepository<Error> errorRepository)
            : base(errorRepository)
        {
            _heatChartHeaderRepository = heatChartHeaderRepository;
            _materialRegisterHeaderRepository = materialRegisterHeaderRepository;
        }

        #endregion

        #region Generate Heat Chart Headers

        [HttpGet]
        [Route("generateheatchart")]
        public async Task<HttpResponseMessage> GenerateHeatChart(HttpRequestMessage request, int heatChartID)
        {
            string filePath = string.Empty;
            string fileName = string.Empty;

            try
            {

                var heatChartHeader = _heatChartHeaderRepository.GetSingleByHeatChartHeaderID(heatChartID);

                var heatChartHeaderVM = DomainToViewModelCustomMapper.MapHeatChartHeader(heatChartHeader);

                fileName = string.Format("{0}_{1}", heatChartHeader.HeatChartNumber.Replace("/", "_"), "HeatChart.pdf");

                var resultStream =  PDFGenerateHelper.GeneratePDF(fileName, heatChartHeaderVM, string.Empty, ConfigurationReader.IsSaveToDirectory);

                S3UploadObject.WriteAnObject(resultStream, fileName, ConfigurationReader.AWSHeatChartFolderName);

            }
            catch (Exception ex)
            {
                LogError(ex);
            }

            return CreateHttpResponse(request, () =>
            {
                return S3UploadObject.DownloadAnObject(fileName, ConfigurationReader.AWSHeatChartFolderName);
            });
        }

        [HttpGet]
        [Route("generateheatchartRDLC")]
        public async Task<HttpResponseMessage> GenerateHeatChartRDLC(HttpRequestMessage request, int heatChartID)
        {
            string filePath = string.Empty;
            string fileName = string.Empty;

            try
            {

                var heatChartHeader = _heatChartHeaderRepository.GetSingleByHeatChartHeaderID(heatChartID);

                var heatChartHeaders = new List<HeatChartHeader>();
                heatChartHeaders.Add(heatChartHeader);

                var heatChartHeaderVMs = DomainToViewModelCustomMapper.MapHeatChartHeaders(heatChartHeaders);

                var heatChartHeaderDatasetVMs = AutoMapper.Map<IEnumerable<HeatChartHeaderVM>, IEnumerable<HeatChartHeaderDatasetVM>>(heatChartHeaderVMs);

                var heatChartDetailsDatasetVMs =
                    AutoMapper.Map<IEnumerable<HeatChartDetailsVM>, IEnumerable<HeatChartDetailsDatasetVM>>(heatChartHeaderVMs.FirstOrDefault().HeatChartDetails);

                fileName = string.Format("{0}_{1}", heatChartHeader.HeatChartNumber.Replace("/", "_"), "HeatChart.pdf");

                var resultStream = await ReportGenerator.GeneratePDF(heatChartHeaderDatasetVMs.ToList(), heatChartDetailsDatasetVMs.ToList(), fileName);

                S3UploadObject.WriteAnObject(resultStream, fileName, ConfigurationReader.AWSHeatChartFolderName);

            }
            catch (Exception ex)
            {
                LogError(ex);
            }

            return CreateHttpResponse(request, () =>
            {
                return S3UploadObject.DownloadAnObject(fileName, ConfigurationReader.AWSHeatChartFolderName);
            });
        }

        #endregion

        #region Material Registers

        [MimeMultipart]
        [Route("file/materialregisterupload")]
        public async Task<HttpResponseMessage> Upload(HttpRequestMessage request, string CTNumber, string subSeriesNumber, string date)
        {            
            HttpResponseMessage response = null;

            try
            {               
                var uploadPath = HttpContext.Current.Server.MapPath(ConfigurationReader.MaterialRegisterFilePath);

                string materialRegisterFileUniqueIdentifier = string.Format("{0}_{1}_{2}", CTNumber.Replace("/", ""), subSeriesNumber.Replace("/", ""), date);
                var result = await request.Content.ReadAsMultipartAsync();

                string fileName = string.Format("{0}_{1}", materialRegisterFileUniqueIdentifier, result.Contents?.FirstOrDefault()?.Headers?.ContentDisposition?.FileName.Replace("\"", ""));

                var inputStream = request.Content.ReadAsStreamAsync().Result;

                S3UploadObject.WriteAnObject(inputStream, fileName, ConfigurationReader.AWSMaterialRegisterFolderName);

                // Create response
                FileUploadResult fileUploadResult = new FileUploadResult
                {
                    LocalFilePath = materialRegisterFileUniqueIdentifier,

                    FileName = materialRegisterFileUniqueIdentifier,
                };

                response = request.CreateResponse(HttpStatusCode.OK, fileUploadResult);
            }
            catch(Exception ex)
            {
                LogError(ex);
            }

            return CreateHttpResponse(request, () =>
            {
                return response;
            });
        }

        [HttpGet]
        [Route("file/download")]
        public async Task<HttpResponseMessage> Download(string fileName)
        {           
            try
            {
                if (!string.IsNullOrEmpty(fileName))
                {
                    return S3UploadObject.DownloadAnObject(fileName, ConfigurationReader.AWSMaterialRegisterFolderName);
                }
                return this.Request.CreateResponse(HttpStatusCode.NotFound, "File not found.");
            }
            catch (Exception ex)
            {
                LogError(ex);
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        #endregion
    }
}
