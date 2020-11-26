namespace JH_Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class twoMig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReservAccounts", "password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReservAccounts", "password");
        }
    }
}
