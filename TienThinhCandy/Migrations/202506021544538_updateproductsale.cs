namespace TienThinhCandy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateproductsale : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_WareHouse", "Alias", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_WareHouse", "Alias");
        }
    }
}
