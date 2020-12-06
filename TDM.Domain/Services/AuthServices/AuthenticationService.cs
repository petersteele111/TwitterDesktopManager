using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TDM.Domain.Models;
using TDM.Domain.Services;
using TDM.Domain.Services.AuthServices;

namespace TDM.Services.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly IUserDataService _repo;

        public AuthenticationService(IUserDataService repo)
        {
            _repo = repo;
        }

        public async Task<User> Login(string screenname)
        {
            User existingUser = await _repo.GetByUserName(screenname);
            if (existingUser == null)
            {
                MessageBox.Show($"There is no user with the screen name of {screenname} in the database. Please try again!");
            }
            return existingUser;
        }
    }
}
