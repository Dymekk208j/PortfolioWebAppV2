using System;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PortfolioWebAppV2.Models.DatabaseModels.DatabaseContext
{
    public class DbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var user = new ApplicationUser
            {
                FirstName = "Damian",
                LastName = "Dziura",
                UserName = "Dymek",
                Blocked = false,
                Email = "Kontakt@damiandziura.pl"
            };

            userManager.Create(user, "Damian13");
            userManager.Create(new ApplicationUser() { UserName = "Dymekk", FirstName = "Damian2" }, "Damian13");
            for (int i = 0; i < 32; i++)
            {
                userManager.Create(new ApplicationUser() { UserName = "User" + i.ToString(), FirstName = "User" + i.ToString() }, "Damian13");
            }

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!roleManager.RoleExists("Admin"))
            {
                roleManager.Create(new IdentityRole("Admin"));
            }
            userManager.AddToRole(user.Id, "Admin");

            var aboutMePage = new List<AboutMe>
            {
                new AboutMe() {Title = "Tytuł startowy",
                    Text = "Tincidunt integer eu augue augue nunc elit dolor, luctus placerat scelerisque euismod, iaculis eu lacus nunc mi elit, vehicula ut laoreet ac, aliquam sit amet justo nunc tempor, metus vel.",
                    ImageLink = "http://via.placeholder.com/250x300" }
              };
            aboutMePage.ForEach(g => context.AboutMe.Add(g));
            context.SaveChanges();

            var contact = new List<Contact>
            {
                new Contact() { ContactId = 1, Email1 = "kontakt@DamianDziura.pl", Email2 = "Dymekk208j@gmail.com", FacebookLink = "https://www.facebook.com/dymekk208j", GitHubLink = "https://github.com/Dymekk208j/", LinkedInLink = "https://www.linkedin.com/in/damian-dziura-27a821114/", PhoneNumber = "+48 510-075-067" }
            };
            contact.ForEach(g => context.Contacts.Add(g));
            context.SaveChanges();

            var achi = new List<Achievement>
            {
                new Achievement() { AchievementId = 1, Description = "Pierwsze osiagniecie, opis", Title = "Pierwsze osiagniecie", Date=DateTime.Now, ShowInCv = true },
                new Achievement() { AchievementId = 2, Description = "Drugie osiagniecie, opis", Title = "Drugie osiagniecie", Date=DateTime.Now, ShowInCv = false }

              };
            // achievements.ForEach(g => context.Achievements.Add(g));
            achi.ForEach(g => context.Achievements.Add(g));
            context.SaveChanges();

            var additionalInfos = new List<AdditionalInfo>
            {
                new AdditionalInfo() { AdditionalInfoId = 1, Type = AdditionalInfo.TypeOfAddtionalInfo.ForeignLanguages, Title = "Język angielski - Poziom podstawowy", ShowInCv = true},
                new AdditionalInfo() { AdditionalInfoId = 2, Type = AdditionalInfo.TypeOfAddtionalInfo.AdditionalSkills, Title = "Prawo jazdy B. (Dodatkowa um)", ShowInCv = true },
                new AdditionalInfo() { AdditionalInfoId = 3, Type = AdditionalInfo.TypeOfAddtionalInfo.Interests, Title = "Gry komputerowe (Zainteresowania)", ShowInCv = true }

              };
            additionalInfos.ForEach(g => context.AdditionalInfos.Add(g));
            context.SaveChanges();

            var educations = new List<Education>
            {
                new Education() { EducationId = 1, CurrentPlaceOfEducation = true, SchooleName = "Politechnika koszalinska", Department = "WYDZIAŁ ELEKTRONIKI I INFORMATYKI", Specialization = "Inżynieria testów oprogramowania", EndDate = new DateTime(2020, 1, 1, 01, 01, 01), StartDate = new DateTime(2016, 1, 1, 01, 01, 01), ShowInCv = true},
                new Education() { EducationId = 2, CurrentPlaceOfEducation = false, SchooleName = "ZESPÓŁ SZKÓŁ NR. 9 IM. ROMUALLDA TRAGUTTA", Department = "TECHNIKUM INFORMATYCZNE", Specialization = "Technik informatyk", EndDate = new DateTime(2014, 1, 1, 01, 01, 01), StartDate = new DateTime(2010, 1, 1, 01, 01, 01), ShowInCv = true}
              };
            educations.ForEach(g => context.Educations.Add(g));
            context.SaveChanges();

            var employmentHistorie = new List<EmploymentHistory>
            {
                new EmploymentHistory() { EmploymentHistoryId = 1, CompanyName = "Mediadat software", CityOfEmployment="Koszalin", CurrentPlaceOfEmployment = true, Position="Programista", StartDate = new DateTime(2017, 9, 1, 01, 01, 01), ShowInCv = true},
                new EmploymentHistory() { EmploymentHistoryId = 2, CompanyName = "GEIS", CityOfEmployment="Koszalin", CurrentPlaceOfEmployment = false, Position="Brygadzista", StartDate = new DateTime(2016, 1, 1, 01, 01, 01), EndDate = new DateTime(2017, 9, 1, 00, 00, 00), ShowInCv = true}
              };
            employmentHistorie.ForEach(g => context.EmploymentHistories.Add(g));
            context.SaveChanges();



            var technologies = new List<Technology>
            {
                new Technology() { Name="JAVA", KnowledgeLevel = Technology.LevelOfKnowledge.Ok, ShowInCv = true },
                new Technology() { Name="C#", KnowledgeLevel= Technology.LevelOfKnowledge.Ok, ShowInCv = true },
                new Technology() { Name="JavaScript", KnowledgeLevel=Technology.LevelOfKnowledge.Well, ShowInCv = false },
                new Technology() { Name="ASP.net", KnowledgeLevel=Technology.LevelOfKnowledge.Well, ShowInCv = true },
                new Technology() { Name="UML", KnowledgeLevel=Technology.LevelOfKnowledge.VeryWell, ShowInCv = false },
                new Technology() { Name="CSS", KnowledgeLevel=Technology.LevelOfKnowledge.VeryWell, ShowInCv = false },
                new Technology() { Name="Android", KnowledgeLevel=Technology.LevelOfKnowledge.VeryWell, ShowInCv = true },
                new Technology() { Name="SQLite", KnowledgeLevel=Technology.LevelOfKnowledge.VeryWell, ShowInCv = false }

              };
            technologies.ForEach(g => context.Technologies.Add(g));
            context.SaveChanges();

            var project1 =
                new Project()
                {
                    FullDescription = "Pelen opis 1 projektu",
                    ShortDescription = "krotki opis 1 projektu",
                    Title = "Tytul 1 projektu",
                    Commercial = true,
                    ShowInCv = true,
                    DateTimeCreated = new DateTime(2016, 1, 1, 01, 01, 01),
                    Technologies = new List<Technology>()
                };

            project1.Technologies.Add(technologies.Find(a => a.TechnologyId == 1));
            project1.Technologies.Add(technologies.Find(a => a.TechnologyId == 2));
            project1.Technologies.Add(technologies.Find(a => a.TechnologyId == 3));

            context.Projects.Add(project1);
            context.SaveChanges();

            var project2 =
                new Project()
                {
                    FullDescription = "Pelen opis 2 projektu",
                    ShortDescription = "krotki opis 2 projektu",
                    Title = "Tytul 2 projektu",
                    Commercial = true,
                    ShowInCv = true,
                    DateTimeCreated = new DateTime(2016, 1, 1, 01, 01, 01),
                    Technologies = new List<Technology>()
                };

            project2.Technologies.Add(technologies.Find(a => a.TechnologyId == 1));
            project2.Technologies.Add(technologies.Find(a => a.TechnologyId == 2));
            project2.Technologies.Add(technologies.Find(a => a.TechnologyId == 3));

            context.Projects.Add(project2);
            context.SaveChanges();

            var project3 =
                new Project()
                {
                    FullDescription = "Pelen opis 3 projektu",
                    ShortDescription = "krotki opis 3 projektu",
                    Title = "Tytul 3 projektu",
                    Commercial = false,
                    ShowInCv = true,
                    DateTimeCreated = new DateTime(2016, 1, 1, 01, 01, 01),
                    Technologies = new List<Technology>()
                };

            project3.Technologies.Add(technologies.Find(a => a.TechnologyId == 1));
            project3.Technologies.Add(technologies.Find(a => a.TechnologyId == 2));
            project3.Technologies.Add(technologies.Find(a => a.TechnologyId == 3));

            context.Projects.Add(project3);
            context.SaveChanges();

            var project4 =
                new Project()
                {
                    FullDescription = "Pelen opis 4 projektu",
                    ShortDescription = "Krotki opis 4 projektu",
                    Title = "Tytul 4 projektu",
                    Commercial = false,
                    ShowInCv = true,
                    DateTimeCreated = new DateTime(2016, 1, 1, 01, 01, 01),
                    Technologies = new List<Technology>()
                };

            project4.Technologies.Add(technologies.Find(a => a.TechnologyId == 1));
            project4.Technologies.Add(technologies.Find(a => a.TechnologyId == 2));
            project4.Technologies.Add(technologies.Find(a => a.TechnologyId == 3));

            context.Projects.Add(project4);
            context.SaveChanges();

            var privateInformation = new List<PrivateInformation>
            {
                new PrivateInformation() { PrivateInformationId = 1, City = "Koszalin", Email = "Kontakt@DamianDziura.pl", FirstName = "Damian", LastName = "Dziura"
                , Street = "1 maja", HouseNumber = "1", FlatNumber = "3", PostCode="75-800", HomePage = "www.DamianDziura.pl", PhoneNumber="510-075-067", ImageLink= "http://via.placeholder.com/150x180" }
              };
            privateInformation.ForEach(g => context.PrivateInformations.Add(g));
            context.SaveChanges();
            
        }


    }
}