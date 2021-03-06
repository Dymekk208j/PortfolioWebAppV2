namespace PortfolioWebAppV2.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class GuidPropAddedToImageModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "Guid", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Images", "Guid");
        }
    }
}
