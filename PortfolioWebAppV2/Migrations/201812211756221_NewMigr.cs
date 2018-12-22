namespace PortfolioWebAppV2.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class NewMigr : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Achievements", "Description", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Achievements", "Description", c => c.String(maxLength: 250));
        }
    }
}
