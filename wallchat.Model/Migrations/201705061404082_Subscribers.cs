namespace wallchat.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Subscribers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Subscribers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        DateCreation = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subscribers", "UserId", "dbo.Users");
            DropIndex("dbo.Subscribers", new[] { "UserId" });
            DropTable("dbo.Subscribers");
        }
    }
}
