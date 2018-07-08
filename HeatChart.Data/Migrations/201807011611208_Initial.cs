namespace HeatChart.Data.Sql.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Address = c.String(maxLength: 200),
                        Email = c.String(maxLength: 50),
                        Landline = c.String(maxLength: 15),
                        Mobile = c.String(maxLength: 15),
                        AdditionalDetails = c.String(maxLength: 1000),
                        CreatedBy = c.String(nullable: false, maxLength: 50),
                        ModifiedBy = c.String(nullable: false, maxLength: 50),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.HeatChartHeader",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        HeatChartNumber = c.String(nullable: false, maxLength: 50),
                        CustomerID = c.Int(nullable: false),
                        JobNumber = c.String(nullable: false, maxLength: 50),
                        DrawingNumber = c.String(nullable: false, maxLength: 50),
                        DrawingRevision = c.String(nullable: false, maxLength: 50),
                        CustomerPONumber = c.String(maxLength: 50),
                        CustomerPODate = c.DateTime(),
                        CustomerPOEquipment = c.String(maxLength: 50),
                        TagNumber = c.String(nullable: false, maxLength: 50),
                        ThirdPartyInspectionID = c.Int(nullable: false),
                        Plant = c.String(),
                        OtherInfo = c.String(),
                        NoOfEquipment = c.Int(),
                        CreatedBy = c.String(nullable: false, maxLength: 50),
                        ModifiedBy = c.String(nullable: false, maxLength: 50),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(),
                        Supplier_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customer", t => t.CustomerID)
                .ForeignKey("dbo.Supplier", t => t.Supplier_ID)
                .ForeignKey("dbo.ThirdPartyInspection", t => t.ThirdPartyInspectionID)
                .Index(t => t.CustomerID)
                .Index(t => t.ThirdPartyInspectionID)
                .Index(t => t.Supplier_ID);
            
            CreateTable(
                "dbo.HeatChartDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        HeatChartHeaderID = c.Int(nullable: false),
                        PartNumber = c.String(nullable: false, maxLength: 100),
                        PartNumberDescription = c.String(maxLength: 500),
                        SheetNo = c.String(nullable: false, maxLength: 50),
                        SpecificationsID = c.Int(nullable: false),
                        Dimension = c.String(),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.HeatChartHeader", t => t.HeatChartHeaderID)
                .ForeignKey("dbo.Specifications", t => t.SpecificationsID)
                .Index(t => t.HeatChartHeaderID)
                .Index(t => t.SpecificationsID);
            
            CreateTable(
                "dbo.HeatChartMaterialHeaderRelationship",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        MaterialRegisterHeaderID = c.Int(nullable: false),
                        MaterialRegisterSubSeriesID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MaterialRegisterHeader", t => t.MaterialRegisterHeaderID)
                .ForeignKey("dbo.MaterialRegisterSubSeries", t => t.MaterialRegisterSubSeriesID)
                .ForeignKey("dbo.HeatChartDetails", t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.MaterialRegisterHeaderID)
                .Index(t => t.MaterialRegisterSubSeriesID);
            
            CreateTable(
                "dbo.MaterialRegisterHeader",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CTNumber = c.String(nullable: false, maxLength: 50),
                        SupplierID = c.Int(nullable: false),
                        SupplierPONumber = c.String(nullable: false, maxLength: 50),
                        SupplierPODate = c.DateTime(nullable: false),
                        ThirdPartyInspectionID = c.Int(nullable: false),
                        RawMaterialFormID = c.Int(nullable: false),
                        SpecificationsID = c.Int(nullable: false),
                        Dimension = c.String(),
                        OtherInfo = c.String(),
                        CreatedBy = c.String(nullable: false, maxLength: 50),
                        ModifiedBy = c.String(nullable: false, maxLength: 50),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        StatusID = c.Int(nullable: false),
                        PartiallyAcceptedRemarks = c.String(maxLength: 100),
                        Quantity = c.String(),
                        IsDeleted = c.Boolean(),
                        Customer_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Status", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("dbo.RawMaterialForm", t => t.RawMaterialFormID, cascadeDelete: true)
                .ForeignKey("dbo.Specifications", t => t.SpecificationsID)
                .ForeignKey("dbo.Supplier", t => t.SupplierID)
                .ForeignKey("dbo.ThirdPartyInspection", t => t.ThirdPartyInspectionID)
                .ForeignKey("dbo.Customer", t => t.Customer_ID)
                .Index(t => t.SupplierID)
                .Index(t => t.ThirdPartyInspectionID)
                .Index(t => t.RawMaterialFormID)
                .Index(t => t.SpecificationsID)
                .Index(t => t.StatusID)
                .Index(t => t.Customer_ID);
            
            CreateTable(
                "dbo.MaterialRegisterSubSeries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SubSeriesNumber = c.String(nullable: false, maxLength: 50),
                        MaterialRegisterHeaderID = c.Int(nullable: false),
                        StatusID = c.Int(nullable: false),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MaterialRegisterHeader", t => t.MaterialRegisterHeaderID)
                .ForeignKey("dbo.Status", t => t.StatusID, cascadeDelete: true)
                .Index(t => t.MaterialRegisterHeaderID)
                .Index(t => t.StatusID);
            
            CreateTable(
                "dbo.LabReport",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        SeqNum = c.Int(nullable: false),
                        LabName = c.String(maxLength: 50),
                        TCNumber = c.String(nullable: false, maxLength: 50),
                        TCDate = c.DateTime(nullable: false),
                        Description = c.String(maxLength: 150),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MaterialRegisterSubSeries", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.MaterialRegisterFileDetail",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        Path = c.String(),
                        MaterialRegisterSubSeriesID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MaterialRegisterSubSeries", t => t.MaterialRegisterSubSeriesID, cascadeDelete: true)
                .Index(t => t.MaterialRegisterSubSeriesID);
            
            CreateTable(
                "dbo.MaterialRegisterSubseriesTestRelationship",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TestID = c.Int(nullable: false),
                        MaterialRegisterSubSeriesID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MaterialRegisterSubSeries", t => t.MaterialRegisterSubSeriesID, cascadeDelete: true)
                .ForeignKey("dbo.Test", t => t.TestID, cascadeDelete: true)
                .Index(t => t.TestID)
                .Index(t => t.MaterialRegisterSubSeriesID);
            
            CreateTable(
                "dbo.Test",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(),
                        IsDeleted = c.Boolean(),
                        CreatedBy = c.String(nullable: false, maxLength: 50),
                        ModifiedBy = c.String(nullable: false, maxLength: 50),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MillDetail",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        HeatOrLotNumber = c.String(maxLength: 50),
                        ProductNumber = c.String(maxLength: 50),
                        MillName = c.String(maxLength: 50),
                        TCNumber = c.String(nullable: false, maxLength: 50),
                        TCDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MaterialRegisterSubSeries", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RawMaterialForm",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(),
                        IsDeleted = c.Boolean(),
                        CreatedBy = c.String(nullable: false, maxLength: 50),
                        ModifiedBy = c.String(nullable: false, maxLength: 50),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Specifications",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(),
                        IsDeleted = c.Boolean(),
                        CreatedBy = c.String(nullable: false, maxLength: 50),
                        ModifiedBy = c.String(nullable: false, maxLength: 50),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Supplier",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Address = c.String(maxLength: 200),
                        Email = c.String(maxLength: 50),
                        Landline = c.String(maxLength: 15),
                        Mobile = c.String(maxLength: 15),
                        AdditionalDetails = c.String(maxLength: 1000),
                        CreatedBy = c.String(nullable: false, maxLength: 50),
                        ModifiedBy = c.String(nullable: false, maxLength: 50),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ThirdPartyInspection",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Address = c.String(maxLength: 200),
                        Email = c.String(maxLength: 50),
                        Landline = c.String(maxLength: 15),
                        Mobile = c.String(maxLength: 15),
                        AdditionalDetails = c.String(maxLength: 1000),
                        CreatedBy = c.String(nullable: false, maxLength: 50),
                        ModifiedBy = c.String(nullable: false, maxLength: 50),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Dimension",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(),
                        IsDeleted = c.Boolean(),
                        CreatedBy = c.String(nullable: false, maxLength: 50),
                        ModifiedBy = c.String(nullable: false, maxLength: 50),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Error",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        InnerMessage = c.String(),
                        StackTrace = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UsersId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UsersId, cascadeDelete: true)
                .Index(t => t.UsersId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 200),
                        HashedPassword = c.String(nullable: false, maxLength: 200),
                        Salt = c.String(nullable: false, maxLength: 200),
                        IsLocked = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRole", "UsersId", "dbo.Users");
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Role");
            DropForeignKey("dbo.MaterialRegisterHeader", "Customer_ID", "dbo.Customer");
            DropForeignKey("dbo.HeatChartHeader", "ThirdPartyInspectionID", "dbo.ThirdPartyInspection");
            DropForeignKey("dbo.HeatChartDetails", "SpecificationsID", "dbo.Specifications");
            DropForeignKey("dbo.HeatChartMaterialHeaderRelationship", "ID", "dbo.HeatChartDetails");
            DropForeignKey("dbo.HeatChartMaterialHeaderRelationship", "MaterialRegisterSubSeriesID", "dbo.MaterialRegisterSubSeries");
            DropForeignKey("dbo.HeatChartMaterialHeaderRelationship", "MaterialRegisterHeaderID", "dbo.MaterialRegisterHeader");
            DropForeignKey("dbo.MaterialRegisterHeader", "ThirdPartyInspectionID", "dbo.ThirdPartyInspection");
            DropForeignKey("dbo.MaterialRegisterHeader", "SupplierID", "dbo.Supplier");
            DropForeignKey("dbo.HeatChartHeader", "Supplier_ID", "dbo.Supplier");
            DropForeignKey("dbo.MaterialRegisterHeader", "SpecificationsID", "dbo.Specifications");
            DropForeignKey("dbo.MaterialRegisterHeader", "RawMaterialFormID", "dbo.RawMaterialForm");
            DropForeignKey("dbo.MaterialRegisterSubSeries", "StatusID", "dbo.Status");
            DropForeignKey("dbo.MaterialRegisterHeader", "StatusID", "dbo.Status");
            DropForeignKey("dbo.MillDetail", "ID", "dbo.MaterialRegisterSubSeries");
            DropForeignKey("dbo.MaterialRegisterSubseriesTestRelationship", "TestID", "dbo.Test");
            DropForeignKey("dbo.MaterialRegisterSubseriesTestRelationship", "MaterialRegisterSubSeriesID", "dbo.MaterialRegisterSubSeries");
            DropForeignKey("dbo.MaterialRegisterSubSeries", "MaterialRegisterHeaderID", "dbo.MaterialRegisterHeader");
            DropForeignKey("dbo.MaterialRegisterFileDetail", "MaterialRegisterSubSeriesID", "dbo.MaterialRegisterSubSeries");
            DropForeignKey("dbo.LabReport", "ID", "dbo.MaterialRegisterSubSeries");
            DropForeignKey("dbo.HeatChartDetails", "HeatChartHeaderID", "dbo.HeatChartHeader");
            DropForeignKey("dbo.HeatChartHeader", "CustomerID", "dbo.Customer");
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.UserRole", new[] { "UsersId" });
            DropIndex("dbo.MillDetail", new[] { "ID" });
            DropIndex("dbo.MaterialRegisterSubseriesTestRelationship", new[] { "MaterialRegisterSubSeriesID" });
            DropIndex("dbo.MaterialRegisterSubseriesTestRelationship", new[] { "TestID" });
            DropIndex("dbo.MaterialRegisterFileDetail", new[] { "MaterialRegisterSubSeriesID" });
            DropIndex("dbo.LabReport", new[] { "ID" });
            DropIndex("dbo.MaterialRegisterSubSeries", new[] { "StatusID" });
            DropIndex("dbo.MaterialRegisterSubSeries", new[] { "MaterialRegisterHeaderID" });
            DropIndex("dbo.MaterialRegisterHeader", new[] { "Customer_ID" });
            DropIndex("dbo.MaterialRegisterHeader", new[] { "StatusID" });
            DropIndex("dbo.MaterialRegisterHeader", new[] { "SpecificationsID" });
            DropIndex("dbo.MaterialRegisterHeader", new[] { "RawMaterialFormID" });
            DropIndex("dbo.MaterialRegisterHeader", new[] { "ThirdPartyInspectionID" });
            DropIndex("dbo.MaterialRegisterHeader", new[] { "SupplierID" });
            DropIndex("dbo.HeatChartMaterialHeaderRelationship", new[] { "MaterialRegisterSubSeriesID" });
            DropIndex("dbo.HeatChartMaterialHeaderRelationship", new[] { "MaterialRegisterHeaderID" });
            DropIndex("dbo.HeatChartMaterialHeaderRelationship", new[] { "ID" });
            DropIndex("dbo.HeatChartDetails", new[] { "SpecificationsID" });
            DropIndex("dbo.HeatChartDetails", new[] { "HeatChartHeaderID" });
            DropIndex("dbo.HeatChartHeader", new[] { "Supplier_ID" });
            DropIndex("dbo.HeatChartHeader", new[] { "ThirdPartyInspectionID" });
            DropIndex("dbo.HeatChartHeader", new[] { "CustomerID" });
            DropTable("dbo.Users");
            DropTable("dbo.UserRole");
            DropTable("dbo.Role");
            DropTable("dbo.Error");
            DropTable("dbo.Dimension");
            DropTable("dbo.ThirdPartyInspection");
            DropTable("dbo.Supplier");
            DropTable("dbo.Specifications");
            DropTable("dbo.RawMaterialForm");
            DropTable("dbo.Status");
            DropTable("dbo.MillDetail");
            DropTable("dbo.Test");
            DropTable("dbo.MaterialRegisterSubseriesTestRelationship");
            DropTable("dbo.MaterialRegisterFileDetail");
            DropTable("dbo.LabReport");
            DropTable("dbo.MaterialRegisterSubSeries");
            DropTable("dbo.MaterialRegisterHeader");
            DropTable("dbo.HeatChartMaterialHeaderRelationship");
            DropTable("dbo.HeatChartDetails");
            DropTable("dbo.HeatChartHeader");
            DropTable("dbo.Customer");
        }
    }
}
