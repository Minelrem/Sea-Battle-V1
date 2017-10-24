using SeaBattle.Api.Model.Model;
using SeaBattle.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Service 
{
    public class Mapper 
    {
        private Mapper()
        {

        }

        public static UserModel MappToUser(UserAccount acc, User user)
        {
            if (user == null || acc == null)
                return null;


            var userModel = new UserModel 
            {
                Email = acc.Email,
                Login = user.Login,
                Name = user.FirstName + ' ' + user.LastName,
                Id = acc.Id
            };

            return userModel;

        }
    }
}
