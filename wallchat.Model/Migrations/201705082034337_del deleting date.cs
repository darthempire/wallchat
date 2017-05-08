namespace wallchat.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deldeletingdate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Files", "DeleteDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Files", "DeleteDate", c => c.DateTime(nullable: false));
        }
    }
}
