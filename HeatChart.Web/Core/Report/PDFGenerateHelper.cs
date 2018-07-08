using HeatChart.Infrastructure.Common.Utilities;
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

            //10f (left), 10f (right), 74f (top), 10f (bottom)
            pdfFileName = string.Format("{0}{1}", pdfFileName, ".pdf");
            Document doc = new Document(PageSize.LEGAL.Rotate(), 0f, 20f, 108f, 110f);
            //Create PDF Table  

            PdfPTable tableLayout = new PdfPTable(12);
            //tableLayout.TotalWidth = doc.PageSize.Width - 100f;
            tableLayout.WidthPercentage = 96.0f;            
            //tableLayout.SplitRows = true;
            

            PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);
            if (isSaveToDirectory)
            {
                PdfWriter.GetInstance(doc, new FileStream(HttpContext.Current.Server.MapPath("~/reports/heatcharts/" + pdfFileName), FileMode.Create));
            }

            writer.PageEvent = new ITextEvents(heatChartHeader);
            doc.Open();

            GetHeatChartHeader(tableLayout, heatChartHeader);

            GetHeatChartDetailHeadingPDFTable(tableLayout, heatChartHeader);

            GetHeatchartDetailHeader(tableLayout);

            GetHeatChartDetailsPDFTable(doc, tableLayout, heatChartHeader.HeatChartDetails.ToList());
                       

            // Closing the document  
            doc.Close();
            byte[] bytes = memoryStream.ToArray();

            System.IO.Stream newStream = new System.IO.MemoryStream(bytes);

            return newStream;

        }

        private static void GetHeatChartDetailHeadingPDFTable(PdfPTable tableLayout, HeatChartHeaderVM heatChartHeaderVM)
        {
            List<string> sheetNoList = new List<string>();

            string sheetNos = string.Empty;

            if (heatChartHeaderVM.HeatChartDetails != null)
            {
                sheetNoList = heatChartHeaderVM.HeatChartDetails.Select(x => x.SheetNo).Distinct().ToList();

                sheetNos = string.Join(", ", sheetNoList);
            }

            PDFGenerateUtilityHelper.AddCellToDetailHeading(tableLayout, "MATERIAL HEAT CHART", 10);
            PDFGenerateUtilityHelper.AddCellToDetailHeadingSheet(tableLayout, string.Format("{0} : {1}", "Sheet No", sheetNos), 2);
        }

        private static void GetHeatChartHeader(PdfPTable tableLayout, HeatChartHeaderVM heatChartHeaderVM)
        {
            PDFGenerateUtilityHelper.AddCellToBodyHeader(tableLayout, string.Format("{0}{1}:{2}{3}", "Customer", "  ", "  ", heatChartHeaderVM.CustomerSelected?.Name), 6);
            PDFGenerateUtilityHelper.AddCellToBodyHeader(tableLayout,
                string.Format("{0}{1}:{2}{3}", "Mfg By", "  ", "  ", ConfigurationReader.CompanyName), 6);

            PDFGenerateUtilityHelper.AddCellToBodyHeader(tableLayout, string.Format("{0}{1}:{2}{3}", "PO Number", "  ", "  ", heatChartHeaderVM.CustomerPONumber), 3);
            PDFGenerateUtilityHelper.AddCellToBodyHeader(tableLayout, string.Format("{0}{1}:{2}{3}", "PO Date", "  ", "  ",
                               heatChartHeaderVM.CustomerPODate != null ? heatChartHeaderVM.CustomerPODate?.ToShortDateString() : string.Empty), 3);

            PDFGenerateUtilityHelper.AddCellToBodyHeader(tableLayout, string.Format("{0}{1}:{2}{3}", "Equipment Name", "  ", "  ", heatChartHeaderVM.CustomerPOEquipment), 6);

            PDFGenerateUtilityHelper.AddCellToBodyHeader(tableLayout, string.Format("{0}{1}:{2}{3}", "Plant / Project", "  ", "  ", heatChartHeaderVM.Plant), 6);
            PDFGenerateUtilityHelper.AddCellToBodyHeader(tableLayout, string.Format("{0}{1}:{2}{3}", "Drawing Number", "  ", "  ", heatChartHeaderVM.DrawingNumber), 4);
            PDFGenerateUtilityHelper.AddCellToBodyHeader(tableLayout, string.Format("{0}{1}:{2}{3}", "Drawing Revision", "  ", "  ", heatChartHeaderVM.DrawingRevision), 2);

            PDFGenerateUtilityHelper.AddCellToBodyHeader(tableLayout, string.Format("{0}{1}:{2}{3}", "Inspection By", "  ", "  ", heatChartHeaderVM.ThirdPartyInspectionSelected.Name), 6);
            PDFGenerateUtilityHelper.AddCellToBodyHeader(tableLayout, string.Format("{0}{1}:{2}{3}", "Tag Number", "  ", "  ", heatChartHeaderVM.TagNumber), 4);
            PDFGenerateUtilityHelper.AddCellToBodyHeader(tableLayout, string.Format("{0}{1}:{2}{3}", "Quantity", "  ", "  ", heatChartHeaderVM.NoOfEquipment), 2);
        }

        public static void GetHeatchartDetailHeader(PdfPTable tableLayout)
        {
            //Add Header

            PDFGenerateUtilityHelper.AddCellToDetailHeader(tableLayout, "Part No", 1);
            PDFGenerateUtilityHelper.AddCellToDetailHeader(tableLayout, "Description", 2);

            PDFGenerateUtilityHelper.AddCellToDetailHeaderSpecified(tableLayout, "Specified", 2);
            PDFGenerateUtilityHelper.AddCellToDetailHeaderUtilized(tableLayout, "Utilized", 2);

            PDFGenerateUtilityHelper.AddCellToDetailHeader(tableLayout, "CT No/HT No", 1);
            PDFGenerateUtilityHelper.AddCellToDetailHeader(tableLayout, "TC No", 1);
            PDFGenerateUtilityHelper.AddCellToDetailHeader(tableLayout, "TC Date", 1);
            PDFGenerateUtilityHelper.AddCellToDetailHeader(tableLayout, "LAB/MFG", 1);
            PDFGenerateUtilityHelper.AddCellToDetailHeader(tableLayout, "Doc Sr.No", 1);
        }

        private static void GetHeatChartDetailsPDFTable(Document doc, PdfPTable tableLayout, List<HeatChartDetailsVM> heatChartDetailList)
        {
            // Add Body
            
            if (heatChartDetailList != null && heatChartDetailList.Any())
            {
                var heatChartGroupedResult = heatChartDetailList.GroupBy(x => new { x.PartNumber }).ToList();

                int pagelimit = 10;
                bool IsFirstPage = true;
                if (heatChartGroupedResult != null && heatChartGroupedResult.Any())
                {
                    //int pageCounter = 1;
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
                                PDFGenerateUtilityHelper.AddCellToBodyRowSpan(tableLayout, group.MaterialRegisterSubSeriesSelected?.SubSeriesNumber, 1,2);
                            }
                            else
                            {
                                PDFGenerateUtilityHelper.AddCellToBodyRowSpan(tableLayout, group.MaterialRegisterSubSeriesSelected?.SubSeriesNumber, 1,1);
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

                        //if (IsFirstPage && i > 6)
                        //{
                        //    IsFirstPage = false;
                        //    doc.Add(tableLayout);
                        //    tableLayout = new PdfPTable(12);
                        //    //tableLayout.TotalWidth = doc.PageSize.Width - 100f;
                        //    tableLayout.WidthPercentage = 95.6f;
                        //    doc.NewPage();
                        //}
                        //else
                        //{
                        //    if (i / pagelimit == pageCounter)
                        //    {
                        //        if (i % pagelimit > 2)
                        //        {
                        //            pageCounter = pageCounter + 2;
                        //        }
                        //        else
                        //        {
                        //            pageCounter = pageCounter + 1;
                        //        }
                        //        doc.Add(tableLayout);
                        //        tableLayout = new PdfPTable(12);
                        //        //tableLayout.TotalWidth = doc.PageSize.Width - 100f;
                        //        tableLayout.WidthPercentage = 95.6f;
                        //        doc.NewPage();
                        //    }
                        //}
                    }
                  
                }

                doc.Add(tableLayout);
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