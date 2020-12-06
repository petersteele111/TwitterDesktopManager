using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TDM.Domain.Models;

namespace TDM.Domain.Services.AuthServices
{
    public interface IAuthenticationService
    {
        Task<User> Login(string screenname);
    }
}
