namespace TienThinhCandy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateOrders : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Order", "Reason", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Order", "Reason");
        }
    }
}
