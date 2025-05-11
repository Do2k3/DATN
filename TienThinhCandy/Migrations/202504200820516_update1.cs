namespace TienThinhCandy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.tb_SystemSetting");
            AlterColumn("dbo.tb_SystemSetting", "SettingKey", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.tb_SystemSetting", "SettingValue", c => c.String(maxLength: 4000));
            AlterColumn("dbo.tb_SystemSetting", "SettingDescription", c => c.String(maxLength: 4000));
            AddPrimaryKey("dbo.tb_SystemSetting", "SettingKey");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.tb_SystemSetting");
            AlterColumn("dbo.tb_SystemSetting", "SettingDescription", c => c.String());
            AlterColumn("dbo.tb_SystemSetting", "SettingValue", c => c.String());
            AlterColumn("dbo.tb_SystemSetting", "SettingKey", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.tb_SystemSetting", "SettingKey");
        }
    }
}
