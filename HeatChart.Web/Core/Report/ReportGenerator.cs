using HeatChart.Entities.Sql;
using HeatChart.Infrastructure.Common.Utilities;
using HeatChart.ViewModels.DatasetVM;
using HeatChart.Web.Core.AWS;
using Microsoft.Reporting.WebForms;
using ModelMapper.DomainToViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;

namespace HeatChart.Web.Core.Report
{
    public class ReportGenerator
    {
        public static string NewReport = "HeatChart.Web.angular.templates.heatcharts.report.HeatChartReport.rdlc";

        public static Task<Stream> GeneratePDF(List<HeatChartHeaderDatasetVM> heatChartHeaderDatasetVMs, List<HeatChartDetailsDatasetVM> heatChartDetailsDatasetVMs, string filePath)
        {
            return Task.Run(() =>
            {
                try
                {
                   // string binPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "bin");

                    string binPath = "C:\\inetpub\\wwwroot\\HeatChart\\bin";

                    var assembly = Assembly.Load(System.IO.File.ReadAllBytes(binPath + "\\HeatChart.Web.dll"));

                    using (Stream stream = assembly.GetManifestResourceStream(NewReport))
                    {
                        var viewer = new ReportViewer();
                        viewer.LocalReport.EnableExternalImages = true;

                        viewer.LocalReport.LoadReportDefinition(stream);

                        Warning[] warnings;
                        string[] streamids;
                        string mimeType;
                        string encoding;
                        string filenameExtension;

                        ReportDataSource heatChartHeaderDataSource = new ReportDataSource("HeatChartHeaderDS", heatChartHeaderDatasetVMs);
                        ReportDataSource heatChartDetailsDataSource = new ReportDataSource("HeatChartDetailsDS", heatChartDetailsDatasetVMs);

                        viewer.LocalReport.DataSources.Add(heatChartHeaderDataSource);
                        viewer.LocalReport.DataSources.Add(heatChartDetailsDataSource);

                        viewer.LocalReport.Refresh();

                        byte[] bytes = viewer.LocalReport.Render(
                            "PDF", null, out mimeType, out encoding, out filenameExtension,
                            out streamids, out warnings);

                        System.IO.Stream newStream = new System.IO.MemoryStream(bytes);

                        return newStream;                     
                       
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            });
        }

    }
}