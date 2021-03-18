using CoWorkingApp.Data;
using CoWorkingApp.App.Enumeratios;
using CoWorkingApp.Models;
using CoWorkingApp.Data.Tools;
using System;
namespace CoWorkingApp.App.Services
{

    public class UserService
    {
        private UserData userData { get; set; }
        public UserService(UserData userData)
        {
            this.userData = userData;
        }


        public User LoginUserService(bool isAdmin = false)
        {
              // login  Admin
                bool loginResult = false;
                
                while (!loginResult)
                {
                    Console.WriteLine("Ingrese su usuario");
                    string userLogin = Console.ReadLine();
                    Console.WriteLine("Ingrese su contraseña");
                    string passwordLogin = EncryptData.GetPassWord();

                    var userLogged = userData.Login(userLogin, passwordLogin, isAdmin);
                    loginResult = userLogged != null;
                    
                    if(loginResult)
                    {
                        return userLogged;
                    };
                    if (!loginResult == false) Console.WriteLine("Usuario o contraseña incorrectos");
                    
                }
                return null;

        }

        public void CreateUserService()
        {
            User newUser = new User();
            Console.WriteLine("ingrese su nombre");
            newUser.Name = Console.ReadLine();
            Console.WriteLine("ingrese su apellido");
            newUser.LastName = Console.ReadLine();
            Console.WriteLine("ingrese su Email");
            newUser.Email = Console.ReadLine();
            Console.WriteLine("ingrese su contraseña");
            newUser.PassWord = EncryptData.EncryptText(EncryptData.GetPassWord());

            if (userData.CreateUser(newUser)) Console.WriteLine("Usuario creado correctamente");
            else Console.WriteLine("algo salio mal creando el nuevo usuario");

        }

        public void UpdateUserService()
        {
            Console.WriteLine("Ingrese el correo del usuario a editar");
            string email = Console.ReadLine();
            var userFound = userData.FindUser(email);

            while (userFound == null)
            {
                Console.WriteLine("Usuario no encontrado, intentelo de nuevo");
                Console.WriteLine("Ingrese el correo del usuario a  editar");
                email = Console.ReadLine();

                userFound = userData.FindUser(email);
            }

            Console.WriteLine("ingrese el nuevo nombre");
            userFound.Name = Console.ReadLine();
            Console.WriteLine("ingrese el nuevo apellido");
            userFound.LastName = Console.ReadLine();
            Console.WriteLine("ingrese su nuevo  Email");
            userFound.Email = Console.ReadLine();
            Console.WriteLine("ingrese su nueva contraseña");
            userFound.PassWord = EncryptData.EncryptText(EncryptData.GetPassWord());

            var userWasUpdated = userData.EditUser(userFound);
            if(userWasUpdated) Console.WriteLine("Usuario actualizado correctamente");
            else Console.WriteLine("Algo salio mal en la actualizacion de la informacion");
        }

        public void UpdateUserPassWordServeice()
        {
            Console.WriteLine("Ingrese el correo del usuario a editar");
            string email = Console.ReadLine();
            var userFound = userData.FindUser(email);

            while (userFound == null)
            {
                Console.WriteLine("Usuario no encontrado, intentelo de nuevo");
                Console.WriteLine("Ingrese el correo del usuario a  editar");
                email = Console.ReadLine();

                userFound = userData.FindUser(email);
            }

            Console.WriteLine("ingrese su nueva contraseña");
            userFound.PassWord = EncryptData.EncryptText(EncryptData.GetPassWord());

            var userWasUpdated = userData.EditUser(userFound);
            if(userWasUpdated) Console.WriteLine("Contraseña actualizada correctamente");
            else Console.WriteLine("Algo salio mal en la actualizacion de la informacion");
        }

        public void DeleteUserService()
        {
            Console.WriteLine("Ingrese el correo del usuario a editar");
            string email = Console.ReadLine();
            var userFound = userData.FindUser(email);

            while (userFound == null)
            {
                Console.WriteLine("Usuario no encontrado, intentelo de nuevo");
                Console.WriteLine("Ingrese el correo del usuario a  editar");
                email = Console.ReadLine();

                userFound = userData.FindUser(email);
            }

            Console.WriteLine($"¿ Estas segur@ que deseas eliminar el usuario: {userFound.Email} ? 1=SI");
            string response = Console.ReadLine();
            if(response == "SI")
            {
                var userWasDelete = userData.DeleteUser(userFound.UserId);
                if(userWasDelete) Console.WriteLine("Usuario eliminado correctamente");
                else Console.WriteLine("Algo salio mal eliminando el usuario");
            }
        }


        public void ExecuteAction(AdminUser adminUserOpciones)
        {
            switch (adminUserOpciones)
            {
                case AdminUser.Create:
                    {
                        Console.WriteLine("Opcion : creando usuarios");
                        this.CreateUserService();
                        break;
                    }
                case AdminUser.Edit:
                    {
                        Console.WriteLine("Opcion : editando usuarios");
                        this.UpdateUserService();
                        break;
                    }
                case AdminUser.Delete:
                    {
                        Console.WriteLine("Opcion : eliminando usuarios");
                        this.DeleteUserService();
                        break;
                    }
                case AdminUser.ChangePassword:
                    {
                        Console.WriteLine("Opcion : cambiando contraseña de  usuarios");
                        this.UpdateUserPassWordServeice();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("OPCION NO VALIDA PARA ESTE MENU");
                        break;
                    }
            }



        }
    }

}