namespace TienThinhCandy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDatabase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tb_Status", "Order_Id", "dbo.tb_Order");
            DropIndex("dbo.tb_Status", new[] { "Order_Id" });
            AddColumn("dbo.tb_Order", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.tb_Order", "Payment");
           
        }
        
        public override void Down()
        {
           
            AddColumn("dbo.tb_Order", "Payment", c => c.String());
            DropColumn("dbo.tb_Order", "Status");
           
        }
    }
}
