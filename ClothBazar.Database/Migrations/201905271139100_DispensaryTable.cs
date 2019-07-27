namespace ClothBazar.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DispensaryTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DispensaryDrugTables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ServiceAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RemainAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StafftSeriveAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PricePerTab = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                        ProductionDate = c.String(),
                        ExpireDate = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DispensaryDrugTables");
        }
    }
}
