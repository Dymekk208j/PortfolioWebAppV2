namespace PortfolioWebAppV2.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AboutMes",
                c => new
                    {
                        AboutMeId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Text = c.String(),
                        ImageLink = c.String(),
                    })
                .PrimaryKey(t => t.AboutMeId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AboutMes");
        }
    }
}
