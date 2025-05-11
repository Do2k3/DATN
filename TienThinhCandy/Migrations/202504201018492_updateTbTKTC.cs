namespace TienThinhCandy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateTbTKTC : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ThongKeTruyCaps", "ThoiGian", c => c.DateTime(nullable: false));
            DropColumn("dbo.ThongKeTruyCaps", "Time");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ThongKeTruyCaps", "Time", c => c.DateTime(nullable: false));
            DropColumn("dbo.ThongKeTruyCaps", "ThoiGian");
        }
    }
}
