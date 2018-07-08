using HeatHeatChart.ViewModels;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HeatChart.Web.Core.Report
{
    public partial class HeatChartHeaderPage : PdfPageEventHelper
    {
        private HeatChartHeaderVM _heatChartHeaderVM;
        public HeatChartHeaderPage(HeatChartHeaderVM heatChartHeaderVM)
        {
            _heatChartHeaderVM = heatChartHeaderVM;
        }
        public override void OnStartPage(PdfWriter writer, Document doc)
        {
            PdfPTable headerTable = new PdfPTable(12);
            headerTable.TotalWidth = 700;

            headerTable.HorizontalAlignment = Element.ALIGN_CENTER;


            headerTable.WriteSelectedRows(0, -1, 20, 30, writer.DirectContent);

        }

        private static void GetHeatChartHeadingPDFTable(PdfPTable tableLayout)
        {
            string headerImagePath = HttpContext.Current.Server.MapPath("~/content/images/" + "metalplants_Header.png");

            iTextSharp.text.Image headerImage = iTextSharp.text.Image.GetInstance(headerImagePath);

            PDFGenerateUtilityHelper.AddHeaderImageIntoCell(tableLayout, headerImage, 12);
        }

        private static void GetHeatChartDetailHeadingPDFTable(PdfPTable tableLayout)
        {
            PDFGenerateUtilityHelper.AddCellToDetailHeading(tableLayout, "Material Heat Chart", 12);
        }

        private static void GetHeatChartHeader(PdfPTable tableLayout, HeatChartHeaderVM heatChartHeaderVM)
        {
            PDFGenerateUtilityHelper.AddCellToBodyHeader(tableLayout, string.Format("{0}{1}:{2}{3}", "Customer", "  ", "  ", heatChartHeaderVM.CustomerSelected?.Name), 6);
            PDFGenerateUtilityHelper.AddCellToBodyHeader(tableLayout, string.Format("{0}{1}:{2}{3}", "Mfg By", "  ", "  ", ""), 6);

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


    }
}