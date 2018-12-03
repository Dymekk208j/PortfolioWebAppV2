namespace PortfolioWebAppV2.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddedFavoritePropertyInImageModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "Favorite", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Images", "Favorite");
        }
    }
}
