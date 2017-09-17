using HeatChart.Entities.Sql;

namespace HeatChart.Data.Sql.Configurations
{
    public class HeatChartMaterialHeaderRelationshipConfiguration : EntityBaseConfiguration<HeatChartMaterialHeaderRelationship>
    {
        public HeatChartMaterialHeaderRelationshipConfiguration()
        {
            HasRequired(rel => rel.MaterialRegisterHeaders).WithMany().HasForeignKey(x => x.MaterialRegisterHeaderID).WillCascadeOnDelete(false);

            HasRequired(rel => rel.MaterialRegisterSubSeries).WithMany().HasForeignKey(x => x.MaterialRegisterSubSeriesID).WillCascadeOnDelete(false);
        }
    }
}
