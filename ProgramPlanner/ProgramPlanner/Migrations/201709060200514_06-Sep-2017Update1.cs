namespace ProgramPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _06Sep2017Update1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DirectedSlots", "MajorID", "dbo.Majors");
            DropForeignKey("dbo.OptionalDirecteds", "DirectedSlotID", "dbo.DirectedSlots");
            DropForeignKey("dbo.ProgramDirecteds", "OptionalDirectedID", "dbo.OptionalDirecteds");
            DropForeignKey("dbo.OptionalDirecteds", "CourseID", "dbo.Courses");
            DropIndex("dbo.ProgramDirecteds", new[] { "OptionalDirectedID" });
            DropIndex("dbo.OptionalDirecteds", new[] { "CourseID" });
            DropIndex("dbo.OptionalDirecteds", new[] { "DirectedSlotID" });
            DropIndex("dbo.DirectedSlots", new[] { "MajorID" });
            DropPrimaryKey("dbo.ProgramDirecteds");
            CreateTable(
                "dbo.MajorSlots",
                c => new
                    {
                        MajorSlotID = c.Int(nullable: false, identity: true),
                        MajorID = c.Int(nullable: false),
                        Rule = c.String(),
                    })
                .PrimaryKey(t => t.MajorSlotID)
                .ForeignKey("dbo.Majors", t => t.MajorID)
                .Index(t => t.MajorID);
            
            AddColumn("dbo.ProgramDirecteds", "DirectedSlotID", c => c.Int(nullable: false));
            AddColumn("dbo.DirectedSlots", "CourseID", c => c.Int(nullable: false));
            AddColumn("dbo.DirectedSlots", "MajorSlotID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.ProgramDirecteds", new[] { "ProgramStructureID", "DirectedSlotID" });
            CreateIndex("dbo.ProgramDirecteds", "DirectedSlotID");
            CreateIndex("dbo.DirectedSlots", "CourseID");
            CreateIndex("dbo.DirectedSlots", "MajorSlotID");
            AddForeignKey("dbo.DirectedSlots", "MajorSlotID", "dbo.MajorSlots", "MajorSlotID");
            AddForeignKey("dbo.ProgramDirecteds", "DirectedSlotID", "dbo.DirectedSlots", "DirectedSlotID");
            AddForeignKey("dbo.DirectedSlots", "CourseID", "dbo.Courses", "CourseID");
            DropColumn("dbo.ProgramDirecteds", "OptionalDirectedID");
            DropColumn("dbo.DirectedSlots", "rule");
            DropColumn("dbo.DirectedSlots", "MajorID");
            DropTable("dbo.OptionalDirecteds");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OptionalDirecteds",
                c => new
                    {
                        OptionalDirectedID = c.Int(nullable: false, identity: true),
                        CourseID = c.Int(nullable: false),
                        DirectedSlotID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OptionalDirectedID);
            
            AddColumn("dbo.DirectedSlots", "MajorID", c => c.Int(nullable: false));
            AddColumn("dbo.DirectedSlots", "rule", c => c.String());
            AddColumn("dbo.ProgramDirecteds", "OptionalDirectedID", c => c.Int(nullable: false));
            DropForeignKey("dbo.DirectedSlots", "CourseID", "dbo.Courses");
            DropForeignKey("dbo.ProgramDirecteds", "DirectedSlotID", "dbo.DirectedSlots");
            DropForeignKey("dbo.MajorSlots", "MajorID", "dbo.Majors");
            DropForeignKey("dbo.DirectedSlots", "MajorSlotID", "dbo.MajorSlots");
            DropIndex("dbo.MajorSlots", new[] { "MajorID" });
            DropIndex("dbo.DirectedSlots", new[] { "MajorSlotID" });
            DropIndex("dbo.DirectedSlots", new[] { "CourseID" });
            DropIndex("dbo.ProgramDirecteds", new[] { "DirectedSlotID" });
            DropPrimaryKey("dbo.ProgramDirecteds");
            DropColumn("dbo.DirectedSlots", "MajorSlotID");
            DropColumn("dbo.DirectedSlots", "CourseID");
            DropColumn("dbo.ProgramDirecteds", "DirectedSlotID");
            DropTable("dbo.MajorSlots");
            AddPrimaryKey("dbo.ProgramDirecteds", new[] { "ProgramStructureID", "OptionalDirectedID" });
            CreateIndex("dbo.DirectedSlots", "MajorID");
            CreateIndex("dbo.OptionalDirecteds", "DirectedSlotID");
            CreateIndex("dbo.OptionalDirecteds", "CourseID");
            CreateIndex("dbo.ProgramDirecteds", "OptionalDirectedID");
            AddForeignKey("dbo.OptionalDirecteds", "CourseID", "dbo.Courses", "CourseID");
            AddForeignKey("dbo.ProgramDirecteds", "OptionalDirectedID", "dbo.OptionalDirecteds", "OptionalDirectedID");
            AddForeignKey("dbo.OptionalDirecteds", "DirectedSlotID", "dbo.DirectedSlots", "DirectedSlotID");
            AddForeignKey("dbo.DirectedSlots", "MajorID", "dbo.Majors", "MajorID");
        }
    }
}
