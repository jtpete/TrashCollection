namespace TrashGuy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correctingblackoutandspecialpickupidtoint : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BlackoutDates", "ScheduleId", "dbo.Schedule");
            DropForeignKey("dbo.SpecialPickupDates", "ScheduleId", "dbo.Schedule");
            DropIndex("dbo.Schedule", new[] { "User_Id" });
            DropIndex("dbo.BlackoutDates", new[] { "ScheduleId" });
            DropIndex("dbo.SpecialPickupDates", new[] { "ScheduleId" });
            DropColumn("dbo.Schedule", "Id");
            RenameColumn(table: "dbo.Schedule", name: "User_Id", newName: "Id");
            DropPrimaryKey("dbo.Schedule");
            AlterColumn("dbo.Schedule", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.BlackoutDates", "ScheduleId", c => c.String(maxLength: 128));
            AlterColumn("dbo.SpecialPickupDates", "ScheduleId", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.Schedule", "Id");
            CreateIndex("dbo.Schedule", "Id");
            CreateIndex("dbo.BlackoutDates", "ScheduleId");
            CreateIndex("dbo.SpecialPickupDates", "ScheduleId");
            AddForeignKey("dbo.BlackoutDates", "ScheduleId", "dbo.Schedule", "Id");
            AddForeignKey("dbo.SpecialPickupDates", "ScheduleId", "dbo.Schedule", "Id");
            DropColumn("dbo.Schedule", "ScheduleId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Schedule", "ScheduleId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.SpecialPickupDates", "ScheduleId", "dbo.Schedule");
            DropForeignKey("dbo.BlackoutDates", "ScheduleId", "dbo.Schedule");
            DropIndex("dbo.SpecialPickupDates", new[] { "ScheduleId" });
            DropIndex("dbo.BlackoutDates", new[] { "ScheduleId" });
            DropIndex("dbo.Schedule", new[] { "Id" });
            DropPrimaryKey("dbo.Schedule");
            AlterColumn("dbo.SpecialPickupDates", "ScheduleId", c => c.Int(nullable: false));
            AlterColumn("dbo.BlackoutDates", "ScheduleId", c => c.Int(nullable: false));
            AlterColumn("dbo.Schedule", "Id", c => c.String());
            AddPrimaryKey("dbo.Schedule", "ScheduleId");
            RenameColumn(table: "dbo.Schedule", name: "Id", newName: "User_Id");
            AddColumn("dbo.Schedule", "Id", c => c.String());
            CreateIndex("dbo.SpecialPickupDates", "ScheduleId");
            CreateIndex("dbo.BlackoutDates", "ScheduleId");
            CreateIndex("dbo.Schedule", "User_Id");
            AddForeignKey("dbo.SpecialPickupDates", "ScheduleId", "dbo.Schedule", "ScheduleId", cascadeDelete: true);
            AddForeignKey("dbo.BlackoutDates", "ScheduleId", "dbo.Schedule", "ScheduleId", cascadeDelete: true);
        }
    }
}
