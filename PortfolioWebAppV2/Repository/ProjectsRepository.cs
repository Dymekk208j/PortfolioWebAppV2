using PortfolioWebAppV2.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PortfolioWebAppV2.Repository
{
    public class ProjectsRepository : IRepository<Project, int>, IElementOfCv
    {
        [Unity.Attributes.Dependency]
        public ApplicationDbContext Context { get; set; }

        public ProjectsRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public IEnumerable<Project> GetAll()
        {
            return Context.Projects.Include("Icon").ToList();
        }

        public IEnumerable<Image> GetAllIcons()
        {
            return Context.Images.Where(i => i.ImageType == ImageType.Icon).ToList();
        }

        public Project Get(int id)
        {
            try
            {
                return Context.Projects.Include("Icon").Include("Images").First(a => a.ProjectId == id);

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

        public bool Remove(Project entity)
        {
            try
            {
                Project obj = Context.Projects.First(a => a.ProjectId == entity.ProjectId);
                Context.Projects.Remove(obj);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return Context.SaveChanges() > 0;
        }

        public bool Remove(int projectId)
        {
            try
            {
                Project obj = Context.Projects.First(a => a.ProjectId == projectId);
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
                Project existingProject = Context.Projects
                    .Include("Technologies").Include("Icon").FirstOrDefault(p => p.ProjectId == entity.ProjectId);

                if (existingProject == null) return false;
                existingProject.Commercial = entity.Commercial;
                existingProject.DateTimeCreated = entity.DateTimeCreated;
                existingProject.FullDescription = entity.FullDescription;
                existingProject.GitHubLink = entity.GitHubLink;
                existingProject.ShortDescription = entity.ShortDescription;
                existingProject.ShowInCv = entity.ShowInCv;
                existingProject.Title = entity.Title;
                existingProject.Icon = entity.Icon;
                existingProject.AuthorId = entity.AuthorId;
                existingProject.TempProject = entity.TempProject;

                List<Technology> deletedTechnologies = existingProject.Technologies.Except(entity.Technologies, new TechnologyComparere()).ToList();
                List<Technology> addedTechnologies = entity.Technologies.Except(existingProject.Technologies, new TechnologyComparere()).ToList();

                deletedTechnologies.ForEach(c => existingProject.Technologies.Remove(c));
                foreach (Technology c in addedTechnologies)
                {
                    if (Context.Entry(c).State == EntityState.Detached)
                        Context.Technologies.Attach(c);

                    existingProject.Technologies.Add(c);
                }

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
                Project project = Context.Projects.Single(a => a.ProjectId == id);
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
                Project project = Context.Projects.Single(a => a.ProjectId == id);
                project.ShowInCv = true;
                return Update(project);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool RemoveFromPortfolio(int id)
        {
            try
            {
                Project project = Context.Projects.Single(a => a.ProjectId == id);
                project.ShowInCv = false;
                return Update(project);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public IEnumerable<Technology> GetAllTechnologies()
        {
            return Context.Technologies.ToList();
        }
    }

    internal class TechnologyComparere : IEqualityComparer<Technology>
    {
        public bool Equals(Technology x, Technology y)
        {
            if (x != null & y != null)
            {
                return x.TechnologyId == y.TechnologyId && x.Name == y.Name;
            }

            return false;
        }

        public int GetHashCode(Technology obj)
        {
            unchecked
            {
                int hashCode = obj.TechnologyId.GetHashCode();
                hashCode = (hashCode * 397) ^ obj.TechnologyId.GetHashCode();
                return hashCode;
            }
        }
    }
}