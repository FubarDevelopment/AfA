using System;
using System.Data.Entity.Migrations;

namespace FubarDev.Afa.EntityFramework6.Migrations
{
    /// <summary>
    /// Erste Migration für die Datenbank-Erstellung
    /// </summary>
    public partial class InitialCreate : DbMigration
    {
        /// <summary>
        /// Anwendung dieser Migration
        /// </summary>
        public override void Up()
        {
            CreateTable(
                "dbo.AdditionalWriteUpOrOff",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        Asset_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Asset", t => t.Asset_Id, cascadeDelete: true)
                .Index(t => t.Asset_Id);
            
            CreateTable(
                "dbo.Asset",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Account = c.String(nullable: false, maxLength: 7),
                        Name1 = c.String(nullable: false, maxLength: 30),
                        Name2 = c.String(maxLength: 30),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CostCentre = c.String(maxLength: 10),
                        AcquisitionValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RemainingValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AcquisitionDate = c.DateTime(nullable: false),
                        DispatchDate = c.DateTime(),
                        UserValue = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AssetWriteOff",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        YearsSinceAcquisition = c.Int(nullable: false),
                        CurrentValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Lifetime = c.Int(nullable: false),
                        Rounding = c.Int(nullable: false),
                        Precision = c.Int(nullable: false),
                        CalculationMethod = c.Int(nullable: false),
                        Percent = c.Decimal(precision: 18, scale: 2),
                        ChangeToLinear = c.Boolean(nullable: false),
                        Asset_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Asset", t => t.Asset_Id, cascadeDelete: true)
                .Index(t => t.Asset_Id);
            
            CreateTable(
                "dbo.CalculatedWriteOff",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Month = c.DateTime(nullable: false),
                        Depreciation = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RemainingValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        IsFixed = c.Boolean(nullable: false),
                        AssetWriteOff_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AssetWriteOff", t => t.AssetWriteOff_Id, cascadeDelete: true)
                .Index(t => t.AssetWriteOff_Id);
            
        }

        /// <summary>
        /// Rückgängig machen der Migration
        /// </summary>
        public override void Down()
        {
            DropForeignKey("dbo.AssetWriteOff", "Asset_Id", "dbo.Asset");
            DropForeignKey("dbo.CalculatedWriteOff", "AssetWriteOff_Id", "dbo.AssetWriteOff");
            DropForeignKey("dbo.AdditionalWriteUpOrOff", "Asset_Id", "dbo.Asset");
            DropIndex("dbo.CalculatedWriteOff", new[] { "AssetWriteOff_Id" });
            DropIndex("dbo.AssetWriteOff", new[] { "Asset_Id" });
            DropIndex("dbo.AdditionalWriteUpOrOff", new[] { "Asset_Id" });
            DropTable("dbo.CalculatedWriteOff");
            DropTable("dbo.AssetWriteOff");
            DropTable("dbo.Asset");
            DropTable("dbo.AdditionalWriteUpOrOff");
        }
    }
}
