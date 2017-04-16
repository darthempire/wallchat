namespace wallchat.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsomefieldsinArticle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "PublishDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Articles", "Header", c => c.String());
            AddColumn("dbo.Articles", "ShortDescription", c => c.String());
            AddColumn("dbo.Articles", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "ImageUrl");
            DropColumn("dbo.Articles", "ShortDescription");
            DropColumn("dbo.Articles", "Header");
            DropColumn("dbo.Articles", "PublishDate");
        }
    }
}
