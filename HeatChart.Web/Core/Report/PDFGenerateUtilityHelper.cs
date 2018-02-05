using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HeatChart.Web.Core.Report
{
    public class PDFGenerateUtilityHelper
    {
        // Method to add single cell to the body  
        public static void AddCellToBody(PdfPTable tableLayout, string cellText, int colspan)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.TIMES_ROMAN, 7, 1, iTextSharp.text.BaseColor.DARK_GRAY)))
            {
                Colspan = colspan,
                Padding = 7f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255)
            });
        }

        public static void AddCellToBodyRowSpan(PdfPTable tableLayout, string cellText, int colspan, int rowSpan)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.TIMES_ROMAN, 7, 1, iTextSharp.text.BaseColor.DARK_GRAY)))
            {
                Colspan = colspan,
                Rowspan = rowSpan,
                Padding = 7f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255)
            });
        }

        // Method to add single cell to the body  
        public static void AddCellToBodyNoBorder(PdfPTable tableLayout, string cellText, int colspan)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.TIMES_ROMAN, 7, 1, iTextSharp.text.BaseColor.DARK_GRAY)))
            {
                Colspan = colspan,
                Padding = 7f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255),
                Border = Rectangle.NO_BORDER
            });
        }

        // Method to add single cell to the body  
        public static void AddCellToBodyHeader(PdfPTable tableLayout, string cellText, int colspan)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.TIMES_ROMAN, 7, 1, iTextSharp.text.BaseColor.DARK_GRAY)))
            {
                Colspan = colspan,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Padding = 7f,
                BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255)
            });
        }

        public static void AddCellToBodyHeaderNoBorder(PdfPTable tableLayout, string cellText, int colspan)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.TIMES_ROMAN, 7, 1, iTextSharp.text.BaseColor.DARK_GRAY)))
            {
                Colspan = colspan,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Padding = 7f,
                BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255),
                Border = Rectangle.NO_BORDER,
                BorderWidth = 0
            });
        }

        public static void AddCellToDetailHeader(PdfPTable tableLayout, string cellText, int colspan)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.TIMES_ROMAN, 7, Font.BOLD, iTextSharp.text.BaseColor.BLACK)))
            {
                Colspan = colspan,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Padding = 7f,
                BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255)
            });
        }

        public static void AddCellToDetailHeaderSmallPadding(PdfPTable tableLayout, string cellText, int colspan)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.TIMES_ROMAN, 7, Font.BOLD, iTextSharp.text.BaseColor.BLACK)))
            {
                Colspan = colspan,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Padding = 3f,
                BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255)
            });
        }

        public static void AddCellToDetailHeaderSpecified(PdfPTable tableLayout, string cellText, int colspan)
        {
            PdfPTable specifiedHeatChartTable = new PdfPTable(2);
            specifiedHeatChartTable.HorizontalAlignment = Element.ALIGN_CENTER;

            AddCellToDetailHeaderSmallPadding(specifiedHeatChartTable, "Specification", 1);
            AddCellToDetailHeaderSmallPadding(specifiedHeatChartTable, "Dimension", 1);

            PdfPTable specifiedTable = new PdfPTable(1);
            specifiedTable.HorizontalAlignment = Element.ALIGN_CENTER;

            AddCellToDetailHeaderSmallPadding(specifiedTable, "Specified", 2);

            specifiedTable.AddCell(new PdfPCell(specifiedHeatChartTable)
            {
                HorizontalAlignment = Element.ALIGN_TOP,
                VerticalAlignment = Element.ALIGN_TOP,
                Padding = 0f,
            });


            tableLayout.AddCell(new PdfPCell(specifiedTable)
            {
                Colspan = colspan,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 0f,
                BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255)
            });
        }

        public static void AddCellToDetailHeaderUtilized(PdfPTable tableLayout, string cellText, int colspan)
        {
            PdfPTable utilizedHeatChartTable = new PdfPTable(2);
            utilizedHeatChartTable.HorizontalAlignment = Element.ALIGN_CENTER;

            AddCellToDetailHeaderSmallPadding(utilizedHeatChartTable, "Specification", 1);
            AddCellToDetailHeaderSmallPadding(utilizedHeatChartTable, "Dimension", 1);

            PdfPTable utilizedTable = new PdfPTable(1);
            utilizedTable.HorizontalAlignment = Element.ALIGN_CENTER;

            AddCellToDetailHeaderSmallPadding(utilizedTable, "Utilized", 2);

            utilizedTable.AddCell(new PdfPCell(utilizedHeatChartTable)
            {
                HorizontalAlignment = Element.ALIGN_TOP,
                VerticalAlignment = Element.ALIGN_TOP,
                Padding = 0f,
            });


            tableLayout.AddCell(new PdfPCell(utilizedTable)
            {
                Colspan = colspan,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 0f,
                BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255)
            });
        }

        public static void AddCellToHeading(PdfPTable tableLayout, string cellText, int colspan)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.TIMES_ROMAN, 12, 1, iTextSharp.text.BaseColor.BLACK)))
            {
                Colspan = colspan,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 7f,
                BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255)
            });
        }

        public static void AddCellToDetailHeading(PdfPTable tableLayout, string cellText, int colspan)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.TIMES_ROMAN, 12, Font.BOLD, iTextSharp.text.BaseColor.BLACK)))
            {
                Colspan = colspan,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 10f,
                BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255)
            });
        }

        public static void AddCellToDetailHeadingSheet(PdfPTable tableLayout, string cellText, int colspan)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.TIMES_ROMAN, 8, Font.BOLD, iTextSharp.text.BaseColor.BLACK)))
            {
                Colspan = colspan,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 10f,
                BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255)
            });
        }

        public static void AddImageIntoCell(PdfPTable tableLayout, iTextSharp.text.Image image, int colspan)
        {
            PdfPTable headerImageTable = new PdfPTable(12);
            headerImageTable.HorizontalAlignment = Element.ALIGN_CENTER;

            tableLayout.AddCell(new PdfPCell(image)
            {
                Colspan = colspan,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_TOP, 
                PaddingBottom  = 5,
                PaddingTop = 5           
            });

            tableLayout.AddCell(new PdfPCell(headerImageTable)
            {
                Colspan = colspan,
                HorizontalAlignment = Element.ALIGN_TOP,
                VerticalAlignment = Element.ALIGN_TOP,
            });

        }
    }
}