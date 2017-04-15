namespace wallchat.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Compareusertonew : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.News", "User_Id", c => c.Long());
            CreateIndex("dbo.News", "User_Id");
            AddForeignKey("dbo.News", "User_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "User_Id", "dbo.Users");
            DropIndex("dbo.News", new[] { "User_Id" });
            DropColumn("dbo.News", "User_Id");
            DropColumn("dbo.News", "UserId");
        }
    }
}
