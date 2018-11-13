namespace PortfolioWebAppV2.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Migration2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PrivateInformations", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.PrivateInformations", "LastName", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PrivateInformations", "LastName", c => c.String());
            AlterColumn("dbo.PrivateInformations", "FirstName", c => c.String());
        }
    }
}
