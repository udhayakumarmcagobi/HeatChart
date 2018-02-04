using HeatChart.Infrastructure.Common.Utilities;
using HeatHeatChart.ViewModels;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HeatChart.Web.Core.Report
{
    public class ITextEvents : PdfPageEventHelper
    {
        private HeatChartHeaderVM _heatChartHeaderVM;
        public ITextEvents(HeatChartHeaderVM heatChartHeaderVM)
        {
            _heatChartHeaderVM = heatChartHeaderVM;
        }

        // This is the contentbyte object of the writer
        PdfContentByte cb;

        // we will put the final number of pages in a template
        PdfTemplate headerTemplate, footerTemplate;

        // this is the BaseFont we are going to use for the header / footer
        BaseFont bf = null;

        // This keeps track of the creation time
        DateTime PrintTime = DateTime.Now;


        #region Fields
        private string _header;
        #endregion

        #region Properties
        public string Header
        {
            get { return _header; }
            set { _header = value; }
        }
        #endregion


        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            try
            {
                PrintTime = DateTime.Now;
                bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb = writer.DirectContent;
                headerTemplate = cb.CreateTemplate(100, 100);
                footerTemplate = cb.CreateTemplate(100, 100);
            }
            catch (DocumentException de)
            {
                //handle exception here
            }
            catch (System.IO.IOException ioe)
            {
                //handle exception here
            }
        }

        public override void OnEndPage(iTextSharp.text.pdf.PdfWriter writer, iTextSharp.text.Document document)
        {
            base.OnEndPage(writer, document);

            iTextSharp.text.Font baseFontNormal = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);

            iTextSharp.text.Font baseFontBig = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);


            PdfPTable headerTable = new PdfPTable(12);
            headerTable.HorizontalAlignment = Element.ALIGN_CENTER;

            headerTable.TotalWidth = document.PageSize.Width - 184f;
            headerTable.WidthPercentage = 75;
            GetHeaderPDFTable(headerTable, _heatChartHeaderVM);

            //call WriteSelectedRows of PdfTable. This writes rows from PdfWriter in PdfTable
            //first param is start row. -1 indicates there is no end row and all the rows to be included to write
            //Third and fourth param is x and y position to start writing
            headerTable.WriteSelectedRows(0, -1, 92, document.PageSize.Height -30, writer.DirectContent);            

            PdfPTable footerTable = new PdfPTable(12);
            footerTable.HorizontalAlignment = Element.ALIGN_CENTER;

            footerTable.TotalWidth = document.PageSize.Width - 184f;
            footerTable.WidthPercentage = 75;

            AddCellToFooterLeft(footerTable, "", 12);
            AddCellToFooterLeft(footerTable, "", 12);

            AddCellToFooterLeft(footerTable, ConfigurationReader.CompanyName, 4);

            string contactDetails = string.Format("{0} : {1}{2} Tel : {3} Email : {4} Website : {5} UIN : {6}",
                ConfigurationReader.CompanyAddressHeader, ConfigurationReader.CompanyAddress, Environment.NewLine,
                ConfigurationReader.CompanyTelephone, ConfigurationReader.CompanyEmail, ConfigurationReader.CompanyWebsite,
                ConfigurationReader.CompanyCIN);

            AddCellToFooterCenter(footerTable, contactDetails, 6);
            AddCellToFooteRight(footerTable, ConfigurationReader.SurveyorName, 4);

            footerTable.WriteSelectedRows(0, -1, 92, document.BottomMargin + 50, writer.DirectContent);
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);

            headerTemplate.BeginText();
            headerTemplate.SetFontAndSize(bf, 12);
            headerTemplate.SetTextMatrix(0, 0);
            headerTemplate.ShowText((writer.PageNumber - 1).ToString());
            headerTemplate.EndText();

            footerTemplate.BeginText();
            footerTemplate.SetFontAndSize(bf, 12);
            footerTemplate.SetTextMatrix(0, 0);
            footerTemplate.ShowText((writer.PageNumber - 1).ToString());
            footerTemplate.EndText();
        }

        #region Header

        private void GetHeaderPDFTable(PdfPTable headerTable, HeatChartHeaderVM _heatChartHeaderVM)
        {
            GetHeatChartHeadingPDFTable(headerTable);

            GetHeatChartHeader(headerTable, _heatChartHeaderVM);

            GetHeatChartDetailHeadingPDFTable(headerTable, _heatChartHeaderVM);

            GetHeatchartDetailHeader(headerTable);
        }

        private static void GetHeatChartHeadingPDFTable(PdfPTable tableLayout)
        {
            string headerImagePath = HttpContext.Current.Server.MapPath("~/content/images/" + "metalplants_Header.png");

            iTextSharp.text.Image headerImage = iTextSharp.text.Image.GetInstance(headerImagePath);

            PDFGenerateUtilityHelper.AddImageIntoCell(tableLayout, headerImage, 12);
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
            PDFGenerateUtilityHelper.AddCellToDetailHeadingSheet(tableLayout, string.Format("{0} : {1}","Sheet No", sheetNos), 2);
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

        #endregion


        private static void AddCellToFooterLeft(PdfPTable tableLayout, string cellText, int colspan)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.TIMES_ROMAN, 12, Font.BOLD, iTextSharp.text.BaseColor.BLACK)))
            {
                Colspan = colspan,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_BOTTOM,
                Padding = 10f,
                BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255)
            });
        }

        private static void AddCellToFooterCenter(PdfPTable tableLayout, string cellText, int colspan)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.TIMES_ROMAN, 7, 1, iTextSharp.text.BaseColor.BLACK)))
            {
                Colspan = colspan,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 10f,
                BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255)
            });
        }

        private static void AddCellToFooteRight(PdfPTable tableLayout, string cellText, int colspan)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.TIMES_ROMAN, 12, Font.BOLD, iTextSharp.text.BaseColor.BLACK)))
            {
                Colspan = colspan,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_BOTTOM,
                Padding = 10f,
                BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255)
            });
        }
    }
}