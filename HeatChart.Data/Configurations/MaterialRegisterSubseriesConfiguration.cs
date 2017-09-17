using HeatChart.Entities.Sql;

namespace HeatChart.Data.Sql.Configurations
{
    public class MaterialRegisterSubSeriesConfiguration : EntityBaseConfiguration<MaterialRegisterSubSeries>
    {
        public MaterialRegisterSubSeriesConfiguration()
        {
            Property(mrs => mrs.StatusID).IsRequired();
            Property(mrs => mrs.SubSeriesNumber).IsRequired().HasMaxLength(50);

            Property(mrs => mrs.IsDeleted).IsOptional();

            HasRequired(mrs => mrs.MaterialRegisterHeader).WithMany(mh => mh.MaterialRegisterSubSeriess).
                HasForeignKey(rel => rel.MaterialRegisterHeaderID).WillCascadeOnDelete(false);

            HasOptional(mrs => mrs.LabReport)
                .WithRequired(rel => rel.MaterialRegisterSubSeries).WillCascadeOnDelete(false);

            HasOptional(mrs => mrs.MillDetail)
                .WithRequired(rel => rel.MaterialRegisterSubSeries).WillCascadeOnDelete(false);

        }
    }
}
