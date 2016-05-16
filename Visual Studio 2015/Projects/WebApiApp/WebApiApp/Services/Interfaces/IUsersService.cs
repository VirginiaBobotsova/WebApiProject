namespace WebApiApp.Services.Interfaces
{
    using System.Collections.Generic;
    using WebApiApp.Models;

    public interface IUsersService
    {
            IEnumerable<UserViewModel> GetUsers();
            void MakeAdmin(string userId, string userToMakeAdminId);
            UserViewModel GetUser(string id);
        
    }
}
