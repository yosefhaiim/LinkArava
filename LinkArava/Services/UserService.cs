using LinkArava.Dal;
using LinkArava.Models;
using System.Security.Policy;

namespace LinkArava.Services
{
    public class UserService
    {
        private DataLayer db;
        public UserService(DataLayer _db) { db = _db; }

        public async Task<UserModel> getUserById(int id)
        {
            return db.Users.Find(id);
        }

        public async Task<UserModel> getUserByPassword(string un, string udpm)
        {
            UserModel user = db.Users.FirstOrDefault(u => u.UserName == un);
            bool isVerified = BCrypt.Net.BCrypt.Verify(udpm, user.password);
            return isVerified ? user : null;
        }


        public async Task<int>register(UserModel user)
        {
            string uhpw = user.password;
            // hash the password
            string hashedpaw = BCrypt.Net.BCrypt.HashPassword(uhpw);
            // save the hashedpw in the user
            user.password = hashedpaw;

            db.Users.Add(user);
            db.SaveChanges();
            UserModel created = db.Users.FirstOrDefault(u => u.UserName == user.UserName);
            return created.Id;
        }


    }
}
