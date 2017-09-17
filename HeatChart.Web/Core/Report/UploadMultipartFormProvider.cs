using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace HeatChart.Web.Core.Report
{
    public class UploadMultipartFormProvider : MultipartFormDataStreamProvider
    {
        private string _uniqueyIdentifier = string.Empty;
        public UploadMultipartFormProvider(string rootPath, string uniqueIdentifier) : base(rootPath)
        {
            _uniqueyIdentifier = uniqueIdentifier;
        }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            if (headers != null &&
                headers.ContentDisposition != null)
            {
                return string.Format("{0}_{1}", _uniqueyIdentifier,headers
                    .ContentDisposition
                    .FileName.TrimEnd('"').TrimStart('"'));
            }

            return base.GetLocalFileName(headers);
        }
    }
}