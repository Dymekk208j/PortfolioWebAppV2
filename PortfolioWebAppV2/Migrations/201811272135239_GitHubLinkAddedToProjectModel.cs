namespace PortfolioWebAppV2.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class GitHubLinkAddedToProjectModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "GitHubLink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "GitHubLink");
        }
    }
}
