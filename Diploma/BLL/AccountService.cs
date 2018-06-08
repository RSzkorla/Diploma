using Diploma.Database;
using Diploma.EmailService;
using Diploma.Models;
using Diploma.Security;
using Diploma.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace Diploma.BLL
{
    public class AccountService : IAccountService
    {
        public User GetByKey(string userEmail)
        {
            User user = new User();
            using (DiplomaDBContext db = new DiplomaDBContext())
            {
                user = db.ListOfUsers.Where(x => x.Email == userEmail).FirstOrDefault();
                return user;
            }
        }

        public void Add(User user)
        {
            using (DiplomaDBContext db = new DiplomaDBContext())
            {
                user.Password = HashService.HashPassword(user.Password);
                db.ListOfUsers.Add(user);
                db.SaveChanges();
            }
        }

        public void Register(UserRegistrationVM user)
        {
            User newUser = new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                EmailActivated = false,
                Password = user.Password,
                ListOfProjects = new List<Project>()
            };

            Add(newUser);
        }

        public void ActivateAccount(string userEmail, string hash)
        {
            using (DiplomaDBContext db = new DiplomaDBContext())
            {
                User user = db.ListOfUsers.Where(x => x.Email == userEmail).FirstOrDefault();

                if (HashService.GenerateUserHash(user) == hash)
                {
                    user.EmailActivated = true;
                    db.SaveChanges();

                }
            }
        }

        public void ChangePassword(string userEmail, string newPassword)
        {
            using (DiplomaDBContext db = new DiplomaDBContext())
            {
                User user = db.ListOfUsers.Where(x => x.Email == userEmail).FirstOrDefault();

                user.Password = HashService.HashPassword(newPassword);
                db.SaveChanges();
            }
        }

        public List<ProjectTask> GetUndoneUserTasks(string userEmail)
        {
            using (DiplomaDBContext db = new DiplomaDBContext())
            {
                User currentUser = db.ListOfUsers.Where(x => x.Email == userEmail).FirstOrDefault();

                var userProjects = currentUser.ListOfProjects.ToList();

                var userTasks = new List<ProjectTask>();

                foreach (var project in userProjects)
                {
                    foreach (var task in project.ListOfProjectTasks)
                    {
                        if (task.EndDate == (DateTime)SqlDateTime.MinValue)
                            userTasks.Add(task);
                    }
                }

                userTasks.Sort((x, y) => DateTime.Compare(x.DeadLine, y.DeadLine));


                return userTasks;
            }
        }

        public List<ProjectTask> GetFailedTasks(string userEmail)
        {
            var compareDate = DateTime.Parse("12/12/2012 00:00:00");
            using (DiplomaDBContext db = new DiplomaDBContext())
            {
                User currentUser = db.ListOfUsers.Where(x => x.Email == userEmail).FirstOrDefault();

                var userProjects = currentUser.ListOfProjects.ToList();

                var userTasks = new List<ProjectTask>();

                foreach (var project in userProjects)
                {
                    foreach (var task in project.ListOfProjectTasks)
                    {
                        if (DateTime.Now.Date.CompareTo(task.DeadLine.Date)>0)
                        userTasks.Add(task);
                    }
                }

                userTasks.Sort((x, y) => DateTime.Compare(x.DeadLine, y.DeadLine));


                return userTasks;

            }
        }


    }
}
