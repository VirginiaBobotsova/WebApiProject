namespace WebApiApp.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Microsoft.AspNet.Identity;
    using WebApi.Data.Repositories;
    using WebApi.Common;
    using WebApi.Models;
    using WebApiApp.Models;
    using WebApiApp.Services.Interfaces;

    public class UsersService : BaseService, IUsersService
    {
        public UsersService(WebApiData data, IMapper mapper) : base(data, mapper)
        {
        }

        public IEnumerable<UserViewModel> GetUsers()
        {
            var users = this.data.UserRepository.GetAll().ToList();
            return mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<UserViewModel>>(users);
        }


        public UserViewModel GetUser(string id)
        {
            var user = GetById(id);
            return this.mapper.Map<ApplicationUser, UserViewModel>(user);
        }

        private ApplicationUser GetById(string id)
        {
            return this.data.UserRepository.GetById(id);
        }

        public void MakeAdmin(string userId, string userToMakeAdminId)
        {
            var user = GetById(userId);
            if (!user.isAdmin)
            {
                throw new InvalidOperationException(WebApi.Common.Constants.NotAdmin);
            }

            var userToMakeAdmin = GetById(userToMakeAdminId);
            if (userToMakeAdmin == null)
            {
                throw new ArgumentException(WebApi.Common.Constants.UnexistingUserErrorMessage);
            }

            if (userToMakeAdmin.isAdmin)
            {
                throw new InvalidOperationException(WebApi.Common.Constants.AlreadyAdmin);
            }

            userToMakeAdmin.isAdmin = true;
            this.data.Save();
        }
    }
}