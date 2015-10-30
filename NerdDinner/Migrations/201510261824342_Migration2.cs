namespace NerdDinner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RSVPs", "AttendeeEmail", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RSVPs", "AttendeeEmail", c => c.String(nullable: false, maxLength: 4000));
        }
    }
}
