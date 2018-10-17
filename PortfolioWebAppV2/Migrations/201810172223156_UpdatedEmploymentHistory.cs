namespace PortfolioWebAppV2.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedEmploymentHistory : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EmploymentHistories", "CompanyName", c => c.String(nullable: false));
            AlterColumn("dbo.EmploymentHistories", "Position", c => c.String(nullable: false));
            AlterColumn("dbo.EmploymentHistories", "CityOfEmployment", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EmploymentHistories", "CityOfEmployment", c => c.String());
            AlterColumn("dbo.EmploymentHistories", "Position", c => c.String());
            AlterColumn("dbo.EmploymentHistories", "CompanyName", c => c.String());
        }
    }
}
