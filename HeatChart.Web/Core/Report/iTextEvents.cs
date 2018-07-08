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
                headerTemplate = cb.CreateTemplate(100, 50);
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
            headerTable.DefaultCell.Border = Rectangle.NO_BORDER;

            headerTable.TotalWidth = document.PageSize.Width - 60f;
            headerTable.WidthPercentage = 90;
            GetHeaderPDFTable(headerTable, _heatChartHeaderVM);

            //call WriteSelectedRows of PdfTable. This writes rows from PdfWriter in PdfTable
            //first param is start row. -1 indicates there is no end row and all the rows to be included to write
            //Third and fourth param is x and y position to start writing
            headerTable.WriteSelectedRows(0, -1, 20, document.PageSize.Height -10, writer.DirectContent);            

            PdfPTable footerTable = new PdfPTable(12);
            footerTable.DefaultCell.Border = Rectangle.NO_BORDER;
            footerTable.HorizontalAlignment = Element.ALIGN_CENTER;

            footerTable.TotalWidth = document.PageSize.Width - 59f;
            footerTable.WidthPercentage = 90;


            GetHeatChartFooterPDFTable(footerTable);

            footerTable.WriteSelectedRows(0, -1, 20, document.BottomMargin + 10, writer.DirectContent);
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
            GetHeatChartDetailHeadingPDFTable(headerTable, _heatChartHeaderVM);
        }

        private void GetHeatChartDetailHeadingPDFTable(PdfPTable tableLayout, HeatChartHeaderVM heatChartHeaderVM)
        {
            PDFGenerateUtilityHelper.AddCellToBodyHeader(tableLayout, string.Format("{0}:{1}", "Heat Chart No", heatChartHeaderVM.HeatChartNumber), 12);
        }

        private void GetHeatChartHeadingPDFTable(PdfPTable tableLayout)
        {
            string headerImagePath = HttpContext.Current.Server.MapPath("~/content/images/" + ConfigurationReader.CompanyImageName);

            iTextSharp.text.Image headerImage = iTextSharp.text.Image.GetInstance(headerImagePath);

            PDFGenerateUtilityHelper.AddHeaderImageIntoCell(tableLayout, headerImage, 12);
        }

        private void GetHeatChartFooterPDFTable(PdfPTable tableLayout)
        {
            string headerImagePath = HttpContext.Current.Server.MapPath("~/content/images/" + ConfigurationReader.CompanyAddressImageName);

            iTextSharp.text.Image headerImage = iTextSharp.text.Image.GetInstance(headerImagePath);

            PDFGenerateUtilityHelper.AddFooterImageIntoCell(tableLayout, headerImage, 12);
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
                Border = Rectangle.NO_BORDER,
                BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255)
            });
        }

        private static void AddCellToFooterCenter(PdfPTable tableLayout, string cellText, int colspan)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.TIMES_ROMAN, 8, 1, iTextSharp.text.BaseColor.BLACK)))
            {
                Colspan = colspan,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 10f,
                Border = Rectangle.NO_BORDER,
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
                Border = Rectangle.NO_BORDER,
                BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255)
            });
        }
    }
}