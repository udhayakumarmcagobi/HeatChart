namespace HeatChart.Data.Sql.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_01212018_Changes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HeatChartHeader", "CustomerPODate", c => c.DateTime());
            AddColumn("dbo.HeatChartDetails", "PartNumberDescription", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HeatChartDetails", "PartNumberDescription");
            DropColumn("dbo.HeatChartHeader", "CustomerPODate");
        }
    }
}
