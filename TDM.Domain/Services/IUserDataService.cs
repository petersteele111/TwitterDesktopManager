using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TDM.Domain.Models;

namespace TDM.Domain.Services
{
    public interface IUserDataService : IDataService<User>
    {
        Task<User> GetByUserName(string screenname);
    }
}
