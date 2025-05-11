namespace TienThinhCandy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateContact : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Contact", "Link", c => c.String(nullable: false));
            AlterColumn("dbo.tb_Contact", "Email", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_Contact", "Email", c => c.String(nullable: false));
            DropColumn("dbo.tb_Contact", "Link");
        }
    }
}
