using SeaBattle.Api.Model.Model;
using SeaBattle.Data.Model;

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
