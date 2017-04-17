namespace wallchat.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnewFileentity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.String(),
                        PublishDate = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        Size = c.Double(nullable: false),
                        UserId = c.Long(nullable: false),
                        DeleteDate = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Files", "UserId", "dbo.Users");
            DropIndex("dbo.Files", new[] { "UserId" });
            DropTable("dbo.Files");
        }
    }
}
