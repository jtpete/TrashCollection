namespace TrashGuy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class blackoutandspecialpickuptableadd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Schedule", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.Schedule", new[] { "Id" });
            DropPrimaryKey("dbo.Schedule");
            CreateTable(
                "dbo.BlackoutDates",
                c => new
                    {
                        BlackoutId = c.Int(nullable: false, identity: true),
                        ScheduleId = c.Int(nullable: false),
                        BlackoutDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BlackoutId)
                .ForeignKey("dbo.Schedule", t => t.ScheduleId, cascadeDelete: true)
                .Index(t => t.ScheduleId);
            
            CreateTable(
                "dbo.SpecialPickupDates",
                c => new
                    {
                        SpecialPickupId = c.Int(nullable: false, identity: true),
                        ScheduleId = c.Int(nullable: false),
                        SpecialPickupDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SpecialPickupId)
                .ForeignKey("dbo.Schedule", t => t.ScheduleId, cascadeDelete: true)
                .Index(t => t.ScheduleId);
            
            AddColumn("dbo.Schedule", "User_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Schedule", "Id", c => c.String());
            AddPrimaryKey("dbo.Schedule", "ScheduleId");
            CreateIndex("dbo.Schedule", "User_Id");
            AddForeignKey("dbo.Schedule", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedule", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.SpecialPickupDates", "ScheduleId", "dbo.Schedule");
            DropForeignKey("dbo.BlackoutDates", "ScheduleId", "dbo.Schedule");
            DropIndex("dbo.SpecialPickupDates", new[] { "ScheduleId" });
            DropIndex("dbo.BlackoutDates", new[] { "ScheduleId" });
            DropIndex("dbo.Schedule", new[] { "User_Id" });
            DropPrimaryKey("dbo.Schedule");
            AlterColumn("dbo.Schedule", "Id", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Schedule", "User_Id");
            DropTable("dbo.SpecialPickupDates");
            DropTable("dbo.BlackoutDates");
            AddPrimaryKey("dbo.Schedule", "Id");
            CreateIndex("dbo.Schedule", "Id");
            AddForeignKey("dbo.Schedule", "Id", "dbo.AspNetUsers", "Id");
        }
    }
}
