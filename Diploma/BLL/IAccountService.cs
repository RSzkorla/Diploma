using Diploma.Models;
using Diploma.ViewModels;
using System.Collections.Generic;

namespace Diploma.BLL
{
    public interface IAccountService
    {
        void ActivateAccount(string userEmail, string hash);
        void Add(User user);
        void ChangePassword(string userEmail, string newPassword);
        User GetByKey(string userEmail);
        void Register(UserRegistrationVM user);
        List<ProjectTask> GetUndoneUserTasks(string userEmail);
        List<ProjectTask> GetFailedTasks(string userEmail);


    }
}