namespace ProgramPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _050917Update1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "RecommendedYear", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "RecommendedYear");
        }
    }
}
