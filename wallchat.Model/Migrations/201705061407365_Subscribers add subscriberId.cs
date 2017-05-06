namespace wallchat.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubscribersaddsubscriberId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subscribers", "SubscriberId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subscribers", "SubscriberId");
        }
    }
}
