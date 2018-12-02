namespace PortfolioWebAppV2.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class ImageModelMod : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "ImageType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Images", "ImageType");
        }
    }
}
