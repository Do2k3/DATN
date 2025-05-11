namespace TienThinhCandy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateChatbot : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_ChatbotData",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Keyword = c.String(maxLength: 300),
                        SampleQuestion = c.String(maxLength: 500),
                        SampleAnswer = c.String(maxLength: 3000),
                        IsAIGenerated = c.Boolean(nullable: false),
                        UserQuestion = c.String(maxLength: 3000),
                        GPTAnswer = c.String(maxLength: 3000),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tb_ChatbotData");
        }
    }
}
