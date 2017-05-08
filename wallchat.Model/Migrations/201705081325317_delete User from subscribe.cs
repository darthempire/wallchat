namespace wallchat.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteUserfromsubscribe : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Subscribers", "UserId", "dbo.Users");
            DropIndex("dbo.Subscribers", new[] { "UserId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Subscribers", "UserId");
            AddForeignKey("dbo.Subscribers", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
