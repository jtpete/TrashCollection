namespace TrashGuy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Droppedblackoutandspecialdatetablesandaddedonedateasproperties : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BlackoutDates", "ScheduleId", "dbo.Schedule");
            DropIndex("dbo.BlackoutDates", new[] { "ScheduleId" });
            AddColumn("dbo.Schedule", "VacationStartDate", c => c.DateTime());
            AddColumn("dbo.Schedule", "VacationEndDate", c => c.DateTime());
            DropTable("dbo.BlackoutDates");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BlackoutDates",
                c => new
                    {
                        BlackoutId = c.Int(nullable: false, identity: true),
                        ScheduleId = c.String(maxLength: 128),
                        BlackoutDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.BlackoutId);
            
            DropColumn("dbo.Schedule", "VacationEndDate");
            DropColumn("dbo.Schedule", "VacationStartDate");
            CreateIndex("dbo.BlackoutDates", "ScheduleId");
            AddForeignKey("dbo.BlackoutDates", "ScheduleId", "dbo.Schedule", "Id");
        }
    }
}
