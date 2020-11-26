namespace JH_Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OneMigServise : DbMigration
    {
        public override void Up()
        {

            
            CreateTable(
                "dbo.ReservAccounts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        access_level = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            
        }
        
        public override void Down()
        {

            DropTable("dbo.ReservAccounts");
      
        }
    }
}
