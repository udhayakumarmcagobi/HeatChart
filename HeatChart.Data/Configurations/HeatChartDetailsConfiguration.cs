using HeatChart.Entities.Sql;

namespace HeatChart.Data.Sql.Configurations
{
    public class HeatChartDetailsConfiguration :EntityBaseConfiguration<HeatChartDetails>
    {
        public HeatChartDetailsConfiguration()
        {
            Property(hcd => hcd.PartNumber).IsRequired().HasMaxLength(100);
            Property(hcd => hcd.SheetNo).IsRequired().HasMaxLength(50);            

            Property(hcd => hcd.IsDeleted).IsOptional();

            HasRequired(hcd => hcd.HeatChartHeader).WithMany(mh => mh.HeatChartDetails).
                     HasForeignKey(rel => rel.HeatChartHeaderID).WillCascadeOnDelete(false);

            HasRequired(hcd => hcd.Specification).
                    WithMany(rel => rel.HeatChartDetails).WillCascadeOnDelete(false);

            HasOptional(hcd => hcd.HeathChartMaterialHeaderRelationships)
                .WithRequired(rel => rel.HeatChartDetails).WillCascadeOnDelete(false);
        }
    }
}
