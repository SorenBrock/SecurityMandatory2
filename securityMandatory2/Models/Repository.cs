using System;
using System.Linq;

namespace securityMandatory2.Models
{
    public class Repository
    {
        private readonly DataModelContainer _db = new DataModelContainer();

        internal User CreateUser(LoginViewModel model)
        {
            if (CheckUserExist(model)) return null;
            var hasher = new Hasher { SaltSize = 10 };
            User user = null;
            try
            {
                var dbUser = new dbUser()
                {
                    username = model.Username,
                    password = hasher.Encrypt(model.Password)
                };
                _db.dbUserSet.Add(dbUser);
                _db.SaveChanges();

                user = new User()
                {
                    Id = dbUser.Id,
                    Username = dbUser.username
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
            return user;
        }

        internal User UserLoginCheck(string username, string password)
        {
            var hasher = new Hasher { SaltSize = 10 };
            User user = null;
            dbUser dbUser = null;
            try
            {
                dbUser = _db.dbUserSet.FirstOrDefault(x => x.username == username);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            if (dbUser != null && password != null && hasher.CompareStringToHash(password, dbUser.password))
                user = new User() { Id = dbUser.Id, Username = dbUser.username };
            return user;
        }

        internal bool CheckUserExist(LoginViewModel model)
        {
            return _db.dbUserSet.Any(x => x.username == model.Username);
        }
    }
}