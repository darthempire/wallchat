namespace wallchat.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateuserrepair : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Name", c => c.String(maxLength: 14));
            AddColumn("dbo.Users", "Surname", c => c.String(maxLength: 14));
            AddColumn("dbo.Users", "DateBirth", c => c.DateTime());
            AddColumn("dbo.Users", "ProfileImageUrl", c => c.String());
            AddColumn("dbo.Users", "IsBlocked", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "BlockDate", c => c.DateTime());
            AddColumn("dbo.Users", "Information", c => c.String());
            DropColumn("dbo.Users", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "UserId", c => c.String());
            DropColumn("dbo.Users", "Information");
            DropColumn("dbo.Users", "BlockDate");
            DropColumn("dbo.Users", "IsBlocked");
            DropColumn("dbo.Users", "ProfileImageUrl");
            DropColumn("dbo.Users", "DateBirth");
            DropColumn("dbo.Users", "Surname");
            DropColumn("dbo.Users", "Name");
        }
    }
}
