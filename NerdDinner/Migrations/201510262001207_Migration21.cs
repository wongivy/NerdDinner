namespace NerdDinner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration21 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Dinners", "HostedBy", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Dinners", "HostedBy", c => c.String(nullable: false, maxLength: 4000));
        }
    }
}