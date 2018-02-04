using HeatChart.Entities.Sql;

namespace HeatChart.Data.Sql.Configurations
{
    public class HeatChartHeaderConfiguration :EntityBaseConfiguration<HeatChartHeader>
    {
        public HeatChartHeaderConfiguration()
        {
            Property(hch => hch.HeatChartNumber).IsRequired().HasMaxLength(50);
            Property(hch => hch.JobNumber).IsRequired().HasMaxLength(50);
            Property(hch => hch.DrawingNumber).IsRequired().HasMaxLength(50);
            Property(hch => hch.DrawingRevision).IsRequired().HasMaxLength(50);
            Property(hch => hch.TagNumber).IsRequired().HasMaxLength(50);
            Property(hch => hch.CustomerPONumber).HasMaxLength(50);
            Property(hch => hch.CustomerPODate).IsOptional();
            Property(hch => hch.CustomerPOEquipment).HasMaxLength(50);
            Property(hch => hch.OtherInfo).IsOptional();
            Property(hch => hch.NoOfEquipment).IsOptional();

            Property(hch => hch.CreatedBy).IsRequired().HasMaxLength(50);
            Property(hch => hch.CreatedOn).IsRequired();
            Property(hch => hch.ModifiedBy).IsRequired().HasMaxLength(50);
            Property(hch => hch.ModifiedOn).IsRequired();

            Property(hcd => hcd.IsDeleted).IsOptional();

            HasRequired(hcd => hcd.ThirdPartyInspections).
                WithMany(rel => rel.HeatChartHeaders).WillCascadeOnDelete(false);

            HasRequired(hcd => hcd.Customers).
                WithMany(rel => rel.HeatChartHeaders).WillCascadeOnDelete(false);

        }
    }
}
