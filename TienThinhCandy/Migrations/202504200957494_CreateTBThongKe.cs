﻿namespace TienThinhCandy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTBThongKe : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ThongKeTruyCaps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false),
                        SoTruyCap = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ThongKeTruyCaps");
        }
    }
}
