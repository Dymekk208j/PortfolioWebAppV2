namespace PortfolioWebAppV2.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class DeletedLinkFromImageModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Images", "Link");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "Link", c => c.String());
        }
    }
}
