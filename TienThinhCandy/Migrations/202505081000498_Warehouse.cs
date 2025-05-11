namespace TienThinhCandy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Warehouse : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_WareHouse",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 250),
                        ProductCode = c.String(),
                        ProductCategoryId = c.Int(nullable: false),
                        Image = c.String(),
                        OriginalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PriceSale = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        PurchaseQuantity = c.Int(nullable: false),
                        RemainingQuantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tb_ProductCategory", t => t.ProductCategoryId, cascadeDelete: true)
                .Index(t => t.ProductCategoryId);
            
            AddColumn("dbo.tb_ProductImage", "WareHouse_Id", c => c.Int());
            CreateIndex("dbo.tb_ProductImage", "WareHouse_Id");
            AddForeignKey("dbo.tb_ProductImage", "WareHouse_Id", "dbo.tb_WareHouse", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tb_ProductImage", "WareHouse_Id", "dbo.tb_WareHouse");
            DropForeignKey("dbo.tb_WareHouse", "ProductCategoryId", "dbo.tb_ProductCategory");
            DropIndex("dbo.tb_ProductImage", new[] { "WareHouse_Id" });
            DropIndex("dbo.tb_WareHouse", new[] { "ProductCategoryId" });
            DropColumn("dbo.tb_ProductImage", "WareHouse_Id");
            DropTable("dbo.tb_WareHouse");
        }
    }
}
