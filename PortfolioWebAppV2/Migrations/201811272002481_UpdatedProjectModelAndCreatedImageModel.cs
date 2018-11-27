namespace PortfolioWebAppV2.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedProjectModelAndCreatedImageModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        FileName = c.String(nullable: false),
                        Link = c.String(),
                        Project_ProjectId = c.Int(),
                        Project_ProjectId1 = c.Int(),
                    })
                .PrimaryKey(t => t.ImageId)
                .ForeignKey("dbo.Projects", t => t.Project_ProjectId)
                .ForeignKey("dbo.Projects", t => t.Project_ProjectId1)
                .Index(t => t.Project_ProjectId)
                .Index(t => t.Project_ProjectId1);
            
            AddColumn("dbo.Projects", "ShortDescription", c => c.String(nullable: false));
            AddColumn("dbo.Projects", "Icon_ImageId", c => c.Int());
            AlterColumn("dbo.Projects", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Projects", "FullDescription", c => c.String(nullable: false));
            CreateIndex("dbo.Projects", "Icon_ImageId");
            AddForeignKey("dbo.Projects", "Icon_ImageId", "dbo.Images", "ImageId");
            DropColumn("dbo.Projects", "ShordDescription");
            DropColumn("dbo.Projects", "IsIcon");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Projects", "IsIcon", c => c.Boolean(nullable: false));
            AddColumn("dbo.Projects", "ShordDescription", c => c.String());
            DropForeignKey("dbo.Images", "Project_ProjectId1", "dbo.Projects");
            DropForeignKey("dbo.Images", "Project_ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "Icon_ImageId", "dbo.Images");
            DropIndex("dbo.Projects", new[] { "Icon_ImageId" });
            DropIndex("dbo.Images", new[] { "Project_ProjectId1" });
            DropIndex("dbo.Images", new[] { "Project_ProjectId" });
            AlterColumn("dbo.Projects", "FullDescription", c => c.String());
            AlterColumn("dbo.Projects", "Title", c => c.String());
            DropColumn("dbo.Projects", "Icon_ImageId");
            DropColumn("dbo.Projects", "ShortDescription");
            DropTable("dbo.Images");
        }
    }
}
