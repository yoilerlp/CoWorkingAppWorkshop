using System;
using System.Linq;
using CoWorkingApp.Models;
using CoWorkingApp.Data.Tools;
namespace CoWorkingApp.Data
{
    public class UserData
    {
        private JsonManager<User> jsonManager;

        public UserData()
        {
            jsonManager = new JsonManager<User>();
        }

        public User Login(string user, string password, bool isAdmin = false)
        {
            var userCollection = jsonManager.GetCollection();
            if (isAdmin) user = "ADMIN";
            var passwordEncript = EncryptData.EncryptText(password);
            var userFound = userCollection.FirstOrDefault<User>(u => u.Email == user && u.PassWord == passwordEncript);

            return userFound;
        }

        public bool CreateAdmin()
        {

            try
            {
                var userCollection = jsonManager.GetCollection();

                if (!userCollection.Any<User>((user) => user.Name == "ADMIN" && user.LastName == "ADMIN" && user.Email == "ADMIN"))
                {
                    var adminUser = new User()
                    {
                        Name = "ADMIN",
                        LastName = "ADMIN",
                        Email = "ADMIN",
                        UserId = Guid.NewGuid(),
                        PassWord = EncryptData.EncryptText("123456")

                    };
                    userCollection.Add(adminUser);
                    jsonManager.SaveCollection(userCollection);
                }

                return true;
            }
            catch
            {

                return false;
            }

        }


        public bool CreateUser(User newUser)
        {

            try
            {
                var userCollection = jsonManager.GetCollection();

                userCollection.Add(newUser);

                jsonManager.SaveCollection(userCollection);

                return true;
            }
            catch
            {

                return false;
            }
        }

        public User FindUser(string email)
        {
            var userCollection = jsonManager.GetCollection();

            return userCollection.FirstOrDefault(u => u.Email == email);
        }

        public bool EditUser(User editUser)
        {
            try
            {
                var userCollection = jsonManager.GetCollection();
                var userIndex = userCollection.FindIndex(user => user.UserId == editUser.UserId);
                userCollection[userIndex] = editUser;
                jsonManager.SaveCollection(userCollection);
                return true;
            }
            catch
            {

                return false;
            }
        }

        public bool DeleteUser(Guid userId)
        {

            try
            {
                var userCollection = jsonManager.GetCollection();
                userCollection.Remove(userCollection.Find(u => u.UserId == userId));
                jsonManager.SaveCollection(userCollection);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }


}