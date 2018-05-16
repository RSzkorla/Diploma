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
        public void Create(Project project, string userEmail)
        {
            using (var context = new DiplomaDBContext())
            {
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

        public void Delete(Guid id, string userEmail)
        {
            using (var context = new DiplomaDBContext())
            {
                var user = context.ListOfUsers.SingleOrDefault(x => x.Email == userEmail);
                if (user != null)
                {
                    var project = context.ListOfProjects.SingleOrDefault(x => x.Id == id);
                    user.ListOfProjects
                      .Remove(project);
                }

                context.SaveChangesAsync();
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