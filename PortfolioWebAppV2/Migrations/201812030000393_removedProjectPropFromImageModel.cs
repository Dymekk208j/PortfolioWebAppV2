namespace PortfolioWebAppV2.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class RemovedProjectPropFromImageModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Images", "Project_ProjectId1", "dbo.Projects");
            DropIndex("dbo.Images", new[] { "Project_ProjectId1" });
            DropColumn("dbo.Images", "Project_ProjectId1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "Project_ProjectId1", c => c.Int());
            CreateIndex("dbo.Images", "Project_ProjectId1");
            AddForeignKey("dbo.Images", "Project_ProjectId1", "dbo.Projects", "ProjectId");
        }
    }
}
