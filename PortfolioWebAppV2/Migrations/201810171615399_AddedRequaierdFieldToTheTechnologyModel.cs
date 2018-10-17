namespace PortfolioWebAppV2.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddedRequaierdFieldToTheTechnologyModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Technologies", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Technologies", "Name", c => c.String());
        }
    }
}
