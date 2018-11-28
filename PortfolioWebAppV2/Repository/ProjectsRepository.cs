using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using PortfolioWebAppV2.Models.DatabaseModels;
using Unity.Attributes;

namespace PortfolioWebAppV2.Repository
{
    public class ProjectsRepository : IRepository<Project, int>, IElementOfCv
    {
        [Dependency]
        public ApplicationDbContext Context { get; set; }

        public ProjectsRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public IEnumerable<Project> GetAll()
        {
            return Context.Projects.ToList();
        }

        public Project Get(int id)
        {
            try
            {
                return Context.Projects.First(a => a.ProjectId == id);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public bool Add(Project entity)
        {
            Context.Projects.Add(entity);
            return Context.SaveChanges() > 0;
        }

        public bool AddOrUpdate(Project entity)
        {
            Context.Projects.AddOrUpdate(entity);
            return Context.SaveChanges() > 0;
        }

        public bool Remove(Project entity)
        {
            try
            {
                var obj = Context.Projects.First(a => a.ProjectId == entity.ProjectId);
                Context.Projects.Remove(obj);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return Context.SaveChanges() > 0;
        }

        public bool Update(Project entity)
        {
            try
            {
                var project = Context.Projects.Single(a => a.ProjectId == entity.ProjectId) ?? throw new InvalidOperationException();
                project.Commercial = entity.Commercial;
                project.DateTimeCreated = entity.DateTimeCreated;
                project.FullDescription = entity.FullDescription;
                project.GitHubLink = entity.GitHubLink;
                project.ShortDescription = entity.ShortDescription;
                project.ShowInCv = entity.ShowInCv;
                project.Title = entity.Title;
                project.Icon = entity.Icon;
                project.ShortDescription = entity.ShortDescription;
                project.Images = entity.Images;
                project.Technologies = entity.Technologies;
                project.AuthorId = entity.AuthorId;

                return Context.SaveChanges() > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public bool ChangeStatusInCv(int id)
        {
            try
            {
                var project = Context.Projects.Single(a => a.ProjectId == id);
                project.ShowInCv = !project.ShowInCv;
                return Update(project);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool AddToPortfolio(int id)
        {
            try
            {
                var project = Context.Projects.Single(a => a.ProjectId == id);
                project.TempProject = true;
                return Update(project);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}