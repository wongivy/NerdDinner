namespace NerdDinner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RSVPs", "AttendeeName", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RSVPs", "AttendeeName");
        }
    }
}
