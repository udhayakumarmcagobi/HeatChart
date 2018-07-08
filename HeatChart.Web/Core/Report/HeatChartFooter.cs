using HeatChart.Infrastructure.Common.Utilities;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HeatChart.Web.Core.Report
{
    public partial class HeatChartFooter : PdfPageEventHelper
    {
        public override void OnEndPage(PdfWriter writer, Document doc)
        {

            PdfPTable footerTbl = new PdfPTable(12);
            footerTbl.DefaultCell.Border = Rectangle.NO_BORDER;
            footerTbl.TotalWidth = 800;

            footerTbl.HorizontalAlignment = Element.ALIGN_CENTER;
            AddCellToFooterLeft(footerTbl, ConfigurationReader.CompanyName, 2);

            string contactDetails = string.Format("{0} : {1}{2} Tel : {3} Email : {4} Website : {5} UIN : {6}",
                ConfigurationReader.CompanyAddressHeader, ConfigurationReader.CompanyAddress, Environment.NewLine, 
                ConfigurationReader.CompanyTelephone, ConfigurationReader.CompanyEmail, ConfigurationReader.CompanyWebsite, 
                ConfigurationReader.CompanyCIN);

            AddCellToFooterLeft(footerTbl, contactDetails, 8);
            AddCellToFooterLeft(footerTbl, ConfigurationReader.SurveyorName, 2);

            footerTbl.WriteSelectedRows(0, -1, doc.LeftMargin +10, doc.BottomMargin+20, writer.DirectContent);
        }

        private static void AddCellToFooterLeft(PdfPTable tableLayout, string cellText, int colspan)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.TIMES_ROMAN, 7, 1, iTextSharp.text.BaseColor.BLACK)))
            {
                Colspan = colspan,
                HorizontalAlignment = Element.ALIGN_LEFT,
                //Padding = 5f,
                Border = Rectangle.NO_BORDER,
                BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255)
            });
        }

        private static void AddCellToFooterCenter(PdfPTable tableLayout, string cellText, int colspan)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.TIMES_ROMAN, 7, 1, iTextSharp.text.BaseColor.BLACK)))
            {
                Colspan = colspan,
                HorizontalAlignment = Element.ALIGN_CENTER,
                //Padding = 5f,
                Border = Rectangle.NO_BORDER,
                BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255)
            });
        }

        private static void AddCellToFooteRight(PdfPTable tableLayout, string cellText, int colspan)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.TIMES_ROMAN, 7, 1, iTextSharp.text.BaseColor.BLACK)))
            {
                Colspan = colspan,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                //Padding = 5f,
                Border = Rectangle.NO_BORDER,
                BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255)
            });
        }
    }
}