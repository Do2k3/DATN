namespace TienThinhCandy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateAll : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tb_BillDetail", "BillId", "dbo.tb_Bill");
            DropForeignKey("dbo.tb_BillDetail", "ProductId", "dbo.tb_Product");
            DropIndex("dbo.tb_BillDetail", new[] { "BillId" });
            DropIndex("dbo.tb_BillDetail", new[] { "ProductId" });
            DropTable("dbo.tb_BillDetail");
            DropTable("dbo.tb_Bill");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.tb_Bill",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BillCode = c.String(),
                        CustomerName = c.String(),
                        CreatorName = c.String(),
                        CustomerPhone = c.String(),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        TypePayment = c.Int(nullable: false),
                        CreatorId = c.String(),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tb_BillDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BillId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.tb_BillDetail", "ProductId");
            CreateIndex("dbo.tb_BillDetail", "BillId");
            AddForeignKey("dbo.tb_BillDetail", "ProductId", "dbo.tb_Product", "Id", cascadeDelete: true);
            AddForeignKey("dbo.tb_BillDetail", "BillId", "dbo.tb_Bill", "Id", cascadeDelete: true);
        }
    }
}
