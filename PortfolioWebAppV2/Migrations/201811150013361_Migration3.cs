namespace PortfolioWebAppV2.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Migration3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Educations", "SchooleName", c => c.String(nullable: false));
            AlterColumn("dbo.Educations", "Department", c => c.String(nullable: false));
            AlterColumn("dbo.Educations", "Specialization", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Educations", "Specialization", c => c.String());
            AlterColumn("dbo.Educations", "Department", c => c.String());
            AlterColumn("dbo.Educations", "SchooleName", c => c.String());
        }
    }
}
