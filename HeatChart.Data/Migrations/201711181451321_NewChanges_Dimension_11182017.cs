namespace HeatChart.Data.Sql.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewChanges_Dimension_11182017 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.HeatChartDetails", new[] { "DimensionID" });
            DropIndex("dbo.MaterialRegisterHeader", new[] { "DimensionID" });
            RenameColumn(table: "dbo.HeatChartDetails", name: "DimensionID", newName: "Dimension_ID");
            RenameColumn(table: "dbo.MaterialRegisterHeader", name: "DimensionID", newName: "Dimension_ID");
            AddColumn("dbo.HeatChartDetails", "Dimension", c => c.String());
            AddColumn("dbo.MaterialRegisterHeader", "Dimension", c => c.String());
            DropColumn("dbo.MaterialRegisterHeader", "Dimension_ID");
            AlterColumn("dbo.MaterialRegisterHeader", "Dimension_ID", c => c.Int());
        }
        
        public override void Down()
        {
            DropIndex("dbo.MaterialRegisterHeader", new[] { "Dimension_ID" });
            DropIndex("dbo.HeatChartDetails", new[] { "Dimension_ID" });
            AlterColumn("dbo.MaterialRegisterHeader", "Dimension_ID", c => c.Int(nullable: false));
            DropColumn("dbo.HeatChartDetails", "Dimension_ID");
            DropColumn("dbo.MaterialRegisterHeader", "Dimension");
            DropColumn("dbo.HeatChartDetails", "Dimension");
            RenameColumn(table: "dbo.MaterialRegisterHeader", name: "Dimension_ID", newName: "DimensionID");
            RenameColumn(table: "dbo.HeatChartDetails", name: "Dimension_ID", newName: "DimensionID");
            CreateIndex("dbo.MaterialRegisterHeader", "DimensionID");
            CreateIndex("dbo.HeatChartDetails", "DimensionID");
        }
    }
}
