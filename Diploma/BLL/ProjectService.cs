using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diploma.Database;
using Diploma.Models;

namespace Diploma.BLL
{
    public class ProjectService : IProjectService
    {
        public void Create(Project project, Promo promo, string userEmail)
        {
            using (var context = new DiplomaDBContext())
            {
                var prom = context.ListOfPromos.Where(x => x.Id == promo.Id).FirstOrDefault();
                if (prom == null)
                {
                        context.ListOfPromos.Add(promo);
                        context.SaveChanges();
                }

                context.ListOfProjects.Add(project);

                var user = context.ListOfUsers.SingleOrDefault(x => x.Email == userEmail);
                user?.ListOfProjects.Add(project);

                context.SaveChangesAsync();
            }
        }

        public void Update(Guid id, Project updatedProject, string userEmail)
        {
            using (var context = new DiplomaDBContext())
            {
                var user = context.ListOfUsers.SingleOrDefault(x => x.Email == userEmail);
                if (user != null)
                {
                    var project = user.ListOfProjects.SingleOrDefault(x => x.Id == id);
                    if (project != null)
                        project = updatedProject;
                }
                context.SaveChangesAsync();
            }
        }

        public void Delete(Project project, string userEmail)
        {
            using (var context = new DiplomaDBContext())
            {
                var proj = context.ListOfProjects.Where(x => x.Id == project.Id).FirstOrDefault(); 

                var user = context.ListOfUsers.Where(x => x.Email==userEmail).FirstOrDefault();

                if (user != null)
                {
                    proj.Promo = null;
                    context.ListOfProjects.Attach(proj);
                    user.ListOfProjects.Remove(proj);
                    context.ListOfProjects.Remove(proj);
                    context.SaveChanges();
                }
            }
        }

        public Project GetById(Guid id)
        {
            using (var context = new DiplomaDBContext())
            {
                return context.ListOfProjects.SingleOrDefault(x => x.Id == id); ;
            }
        }

        public IEnumerable<Project> GetAll()
        {
            using (var context = new DiplomaDBContext())
            {
                return context.ListOfProjects;
            }
        }

        public IEnumerable<Project> GetAllByUserId(string userEmail)
        {
            using (var context = new DiplomaDBContext())
            {
                var singleOrDefault = context.ListOfUsers.SingleOrDefault(x => x.Email == userEmail);
                if (singleOrDefault != null)
                    return singleOrDefault.ListOfProjects;
                return null;
            }
        }
    }
}