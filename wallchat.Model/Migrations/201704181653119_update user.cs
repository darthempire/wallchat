namespace wallchat.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateuser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UserId", c => c.String());
            DropColumn("dbo.Users", "Name");
            DropColumn("dbo.Users", "Surname");
            DropColumn("dbo.Users", "DateBirth");
            DropColumn("dbo.Users", "ProfileImageUrl");
            DropColumn("dbo.Users", "IsBlocked");
            DropColumn("dbo.Users", "BlockDate");
            DropColumn("dbo.Users", "Information");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Information", c => c.String());
            AddColumn("dbo.Users", "BlockDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "IsBlocked", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "ProfileImageUrl", c => c.String());
            AddColumn("dbo.Users", "DateBirth", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "Surname", c => c.String(maxLength: 14));
            AddColumn("dbo.Users", "Name", c => c.String(maxLength: 14));
            DropColumn("dbo.Users", "UserId");
        }
    }
}
