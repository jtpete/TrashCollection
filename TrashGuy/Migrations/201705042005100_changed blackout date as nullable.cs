namespace TrashGuy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedblackoutdateasnullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SpecialPickupDates", "ScheduleId", "dbo.Schedule");
            DropIndex("dbo.SpecialPickupDates", new[] { "ScheduleId" });
            AlterColumn("dbo.BlackoutDates", "BlackoutDate", c => c.DateTime());
            DropTable("dbo.SpecialPickupDates");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SpecialPickupDates",
                c => new
                    {
                        SpecialPickupId = c.Int(nullable: false, identity: true),
                        ScheduleId = c.String(maxLength: 128),
                        SpecialPickupDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SpecialPickupId);
            
            AlterColumn("dbo.BlackoutDates", "BlackoutDate", c => c.DateTime(nullable: false));
            CreateIndex("dbo.SpecialPickupDates", "ScheduleId");
            AddForeignKey("dbo.SpecialPickupDates", "ScheduleId", "dbo.Schedule", "Id");
        }
    }
}
