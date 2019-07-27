namespace ClothBazar.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Enpenses34 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Drugs", "DrugClass_Id", "dbo.DrugClasses");
            DropIndex("dbo.Drugs", new[] { "DrugClass_Id" });
            CreateTable(
                "dbo.Requestclasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DrugRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        DrugName = c.String(),
                        Requestclass_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Requestclasses", t => t.Requestclass_Id)
                .Index(t => t.Requestclass_Id);
            
            DropColumn("dbo.Drugs", "DrugClass_Id");
            DropTable("dbo.DrugClasses");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DrugClasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Drugs", "DrugClass_Id", c => c.Int());
            DropForeignKey("dbo.DrugRequests", "Requestclass_Id", "dbo.Requestclasses");
            DropIndex("dbo.DrugRequests", new[] { "Requestclass_Id" });
            DropTable("dbo.DrugRequests");
            DropTable("dbo.Requestclasses");
            CreateIndex("dbo.Drugs", "DrugClass_Id");
            AddForeignKey("dbo.Drugs", "DrugClass_Id", "dbo.DrugClasses", "Id");
        }
    }
}
