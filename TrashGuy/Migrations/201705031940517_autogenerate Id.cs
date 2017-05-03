namespace TrashGuy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class autogenerateId : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Schedule",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ScheduleId = c.Int(nullable: false, identity: true),
                        DefaultPickupDay = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedule", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.Schedule", new[] { "Id" });
            DropTable("dbo.Schedule");
        }
    }
}
