using HeatHeatChart.ViewModels;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace HeatChart.Web.Core.Report
{
    public class PDFGenerateHelper
    {
        /// <summary>
        /// Generate the PDF file using validation Details
        /// </summary>
        /// <param name="pdfFileName"></param>
        /// <param name="validationMessage"></param>
        /// <param name="headLinePhrase"></param>
        public static Stream GeneratePDF(string pdfFileName, HeatChartHeaderVM heatChartHeader, string headLinePhrase, bool isSaveToDirectory)
        {
            if (heatChartHeader == null) return null;

            MemoryStream memoryStream = new MemoryStream();
            //file name to be created   
            pdfFileName = string.Format("{0}{1}", pdfFileName, ".pdf");
            Document doc = new Document(PageSize.A4.Rotate(), 10f, 10f, 255f, 20f);
            //Create PDF Table  

            PdfPTable tableLayout = new PdfPTable(12);

            PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);
            if (isSaveToDirectory)
            {
                PdfWriter.GetInstance(doc, new FileStream(HttpContext.Current.Server.MapPath("~/reports/heatcharts/" + pdfFileName), FileMode.Create));
            }

            writer.PageEvent = new ITextEvents(heatChartHeader);
            doc.Open();

            GetHeatChartDetailsPDFTable(tableLayout, heatChartHeader.HeatChartDetails.ToList());
            doc.Add(tableLayout);
            doc.NewPage();

            // Closing the document  
            doc.Close();
            byte[] bytes = memoryStream.ToArray();

            System.IO.Stream newStream = new System.IO.MemoryStream(bytes);

            return newStream;

        }

        private static void GetHeatChartDetailsPDFTable(PdfPTable tableLayout, List<HeatChartDetailsVM> heatChartDetailList)
        {
            // Add Body

            if (heatChartDetailList != null && heatChartDetailList.Any())
            {
                var heatChartGroupedResult = heatChartDetailList.GroupBy(x => new { x.PartNumber }).ToList();

                if (heatChartGroupedResult != null && heatChartGroupedResult.Any())
                {
                    int i = 1;
                    foreach (var groupResult in heatChartGroupedResult)
                    {                       
                        int resultCount = groupResult.Count();

                        int millLabCount = groupResult.Count(x => x.MaterialRegisterSubSeriesSelected != null &&
                                                x.MaterialRegisterSubSeriesSelected.LabReport != null &&
                                                !string.IsNullOrWhiteSpace(x.MaterialRegisterSubSeriesSelected.LabReport.LabName) &&
                                                x.MaterialRegisterSubSeriesSelected.MillDetail != null &&
                                                !string.IsNullOrWhiteSpace(x.MaterialRegisterSubSeriesSelected.MillDetail.MillName));

                        resultCount += millLabCount;

                        PDFGenerateUtilityHelper.AddCellToBodyRowSpan(tableLayout, groupResult.FirstOrDefault().PartNumber, 1, resultCount);
                        PDFGenerateUtilityHelper.AddCellToBodyRowSpan(tableLayout, groupResult.FirstOrDefault().PartNumberDescription, 2, resultCount);

                        PDFGenerateUtilityHelper.AddCellToBodyRowSpan(tableLayout, groupResult.FirstOrDefault().SpecificationSelected?.Name, 1, resultCount);
                        PDFGenerateUtilityHelper.AddCellToBodyRowSpan(tableLayout, groupResult.FirstOrDefault().Dimension, 1, resultCount);
                        PDFGenerateUtilityHelper.AddCellToBodyRowSpan(tableLayout, groupResult.FirstOrDefault().MaterialRegisterHeaderSelected?.SpecificationSelected?.Name, 1, resultCount);
                        PDFGenerateUtilityHelper.AddCellToBodyRowSpan(tableLayout, groupResult.FirstOrDefault().MaterialRegisterHeaderSelected?.Dimension, 1, resultCount);

                        foreach (var group in groupResult)
                        {     
                            if((group.MaterialRegisterSubSeriesSelected?.MillDetail != null && !string.IsNullOrWhiteSpace(group.MaterialRegisterSubSeriesSelected?.MillDetail.TCNumber)) 
                                && (group.MaterialRegisterSubSeriesSelected?.LabReport != null && !string.IsNullOrWhiteSpace(group.MaterialRegisterSubSeriesSelected?.LabReport.LabName)))
                            {
                                PDFGenerateUtilityHelper.AddCellToBodyRowSpan(tableLayout, group.MaterialRegisterHeaderSelected?.CTNumber, 1,2);
                            }
                            else
                            {
                                PDFGenerateUtilityHelper.AddCellToBodyRowSpan(tableLayout, group.MaterialRegisterHeaderSelected?.CTNumber, 1,1);
                            }

                            if (group.MaterialRegisterSubSeriesSelected?.MillDetail != null && !string.IsNullOrWhiteSpace(group.MaterialRegisterSubSeriesSelected?.MillDetail.TCNumber))
                            {
                                AddHeaderCellDetailMill(tableLayout, group.MaterialRegisterSubSeriesSelected?.MillDetail, i);
                                i++;
                            }
                            if (group.MaterialRegisterSubSeriesSelected?.LabReport != null && !string.IsNullOrWhiteSpace(group.MaterialRegisterSubSeriesSelected?.LabReport.LabName))
                            {
                                AddHeaderCellDetailLab(tableLayout, group.MaterialRegisterSubSeriesSelected?.LabReport, i);
                                i++;
                            }
                           
                           
                        }
                    }
                }
            }

        }


        public static void AddHeaderCellDetailMain(PdfPTable tableLayout, dynamic groupResult, int resultCount)
        {

        }

        public static void AddHeaderCellDetailMill(PdfPTable tableLayout, MillDetailVM millDetail, int resultCount)
        {
            PDFGenerateUtilityHelper.AddCellToBody(tableLayout, millDetail.TCNumber, 1);
            PDFGenerateUtilityHelper.AddCellToBody(tableLayout, millDetail.TCDate.ToShortDateString(), 1);
            PDFGenerateUtilityHelper.AddCellToBody(tableLayout, millDetail.MillName, 1);
            PDFGenerateUtilityHelper.AddCellToBody(tableLayout, resultCount.ToString(), 1);
        }

        public static void AddHeaderCellDetailLab(PdfPTable tableLayout, LabReportVM labReport, int resultCount)
        {
            PDFGenerateUtilityHelper.AddCellToBody(tableLayout, labReport.TCNumber, 1);
            PDFGenerateUtilityHelper.AddCellToBody(tableLayout, labReport.TCDate.ToShortDateString(), 1);
            PDFGenerateUtilityHelper.AddCellToBody(tableLayout, labReport.LabName, 1);
            PDFGenerateUtilityHelper.AddCellToBody(tableLayout, resultCount.ToString(), 1);
        }

        /// <summary>
        /// Delete the PDF file which is generated
        /// </summary>
        /// <param name="pdfFileName"></param>
        public static void DeletePDFFile(string pdfFileName)
        {
            string strAttachment = HttpContext.Current.Server.MapPath("~/reports/heatcharts/" + pdfFileName);

            if (System.IO.File.Exists(strAttachment))
            {
                System.IO.File.Delete(strAttachment);
            }
        }
    }
}