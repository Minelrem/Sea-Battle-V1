using SeaBattle.Api.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IUser
    {
        UserModel CurrUser { get;}
        Task<bool> AddUser(UserModel user);
        Task<UserModel> VerifyUser(string email, string password);

    }
}
