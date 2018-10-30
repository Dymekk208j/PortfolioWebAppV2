namespace PortfolioWebAppV2.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AllMigrations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Achievements",
                c => new
                    {
                        AchivmentId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Date = c.DateTime(nullable: false),
                        ShowInCv = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AchivmentId);
            
            CreateTable(
                "dbo.AdditionalInfoes",
                c => new
                    {
                        AdditionalInfoId = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Title = c.String(),
                        ShowInCv = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AdditionalInfoId);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        ContactId = c.Int(nullable: false, identity: true),
                        GitHubLink = c.String(),
                        LinkedInLink = c.String(),
                        FacebookLink = c.String(),
                        Email1 = c.String(),
                        Email2 = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.ContactId);
            
            CreateTable(
                "dbo.Educations",
                c => new
                    {
                        EducationId = c.Int(nullable: false, identity: true),
                        SchooleName = c.String(),
                        Department = c.String(),
                        Specialization = c.String(),
                        StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CurrentPlaceOfEducation = c.Boolean(nullable: false),
                        ShowInCv = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EducationId);
            
            CreateTable(
                "dbo.EmploymentHistories",
                c => new
                    {
                        EmploymentHistoryId = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        Position = c.String(),
                        CityOfEmployment = c.String(),
                        StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CurrentPlaceOfEmployment = c.Boolean(nullable: false),
                        ShowInCv = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EmploymentHistoryId);
            
            CreateTable(
                "dbo.PrivateInformations",
                c => new
                    {
                        PrivateInformationId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Street = c.String(),
                        City = c.String(),
                        PostCode = c.String(),
                        HouseNumber = c.String(),
                        FlatNumber = c.String(),
                        PhoneNumber = c.String(),
                        HomePage = c.String(),
                        Email = c.String(),
                        ImageLink = c.String(),
                    })
                .PrimaryKey(t => t.PrivateInformationId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ShordDescription = c.String(),
                        FullDescription = c.String(),
                        Commercial = c.Boolean(nullable: false),
                        ShowInCv = c.Boolean(nullable: false),
                        IsIcon = c.Boolean(nullable: false),
                        DateTimeCreated = c.DateTime(nullable: false),
                        AuthorId = c.String(),
                        TempProject = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectId);
            
            CreateTable(
                "dbo.Technologies",
                c => new
                    {
                        TechnologyId = c.Int(nullable: false, identity: true),
                        KnowledgeLevel = c.Int(nullable: false),
                        Name = c.String(),
                        ShowInCv = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TechnologyId);
            
            CreateTable(
                "dbo.TechnologyProjects",
                c => new
                    {
                        Technology_TechnologyId = c.Int(nullable: false),
                        Project_ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Technology_TechnologyId, t.Project_ProjectId })
                .ForeignKey("dbo.Technologies", t => t.Technology_TechnologyId, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.Project_ProjectId, cascadeDelete: true)
                .Index(t => t.Technology_TechnologyId)
                .Index(t => t.Project_ProjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TechnologyProjects", "Project_ProjectId", "dbo.Projects");
            DropForeignKey("dbo.TechnologyProjects", "Technology_TechnologyId", "dbo.Technologies");
            DropIndex("dbo.TechnologyProjects", new[] { "Project_ProjectId" });
            DropIndex("dbo.TechnologyProjects", new[] { "Technology_TechnologyId" });
            DropTable("dbo.TechnologyProjects");
            DropTable("dbo.Technologies");
            DropTable("dbo.Projects");
            DropTable("dbo.PrivateInformations");
            DropTable("dbo.EmploymentHistories");
            DropTable("dbo.Educations");
            DropTable("dbo.Contacts");
            DropTable("dbo.AdditionalInfoes");
            DropTable("dbo.Achievements");
        }
    }
}
