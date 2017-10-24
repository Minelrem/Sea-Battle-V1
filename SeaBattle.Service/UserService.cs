using Interfaces;
using SeaBattle.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeaBattle.Api.Model.Model;
using System.Data.Entity;
using SeaBattle.Data.Model;
using System.Web.Helpers;

namespace SeaBattle.Service
{
    public class UserService:Base,IUser
    {
        private UserModel _currUser;

        public UserModel CurrUser => _currUser??null;

        public UserService(SeaBattleContext context):base(context)
        {

        }

        public bool AddUser(UserModel user)
        {
            var isUnique =   _context.UserAccounts.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (isUnique.Result != null)
                return false;


            string salt = Crypto.GenerateSalt();

            string hash = Crypto.HashPassword(user.Password + salt);


            var userAcc = new UserAccount
            {
                Email = user.Email,
                IsActivated = false,
                HashPassword = hash,
                Salt = salt
            };

            _context.UserAccounts.Add(userAcc);
             _context.SaveChanges();

            var acc = _context.UserAccounts.FirstOrDefaultAsync(u => u.Email == user.Email);

            var newUser = new User
            {
                UserAccountId = acc.Result.Id,
                Login = user.Login,
                LastName = user.Name,
                FirstName = user.Name,

            };
            
            _context.Users.Add(newUser);
            _context.SaveChanges();

            return true;

        }

        public async Task<UserModel> VerifyUser(string email, string password)
        {
            var acc = _context.UserAccounts.FirstOrDefaultAsync(u => u.Email == email).Result;

            var user = _context.Users.FirstOrDefaultAsync(u => u.UserAccountId == acc.Id).Result;

            if (acc == null || user == null)
                return null;
            
            if (Crypto.VerifyHashedPassword(acc.HashPassword, password + acc.Salt))
            {
                _currUser = Mapper.MappToUser(acc, user);
                return _currUser;
            }
            else
                return null;

        }
        
    }
}
