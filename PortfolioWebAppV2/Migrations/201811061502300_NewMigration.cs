namespace PortfolioWebAppV2.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class NewMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AboutMes", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.AboutMes", "Text", c => c.String(nullable: false));
            AlterColumn("dbo.AboutMes", "ImageLink", c => c.String(nullable: false));
            AlterColumn("dbo.Achievements", "Title", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Achievements", "Description", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Achievements", "Description", c => c.String());
            AlterColumn("dbo.Achievements", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.AboutMes", "ImageLink", c => c.String());
            AlterColumn("dbo.AboutMes", "Text", c => c.String());
            AlterColumn("dbo.AboutMes", "Title", c => c.String());
        }
    }
}
