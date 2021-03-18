using System;
using CoWorkingApp.App.Enumeratios;
using CoWorkingApp.Data;
using CoWorkingApp.Models;
using CoWorkingApp.App.Services;

namespace CoWorkingApp.App
{
    class Program
    {
        static User ActiveUser {get; set;}

        static UserData userData = new UserData();
        static DeskData deskData = new DeskData();

        static UserService userService = new UserService(userData);
        static DeskService DeskService = new DeskService(deskData);

        static ReservationService ReservationService = new ReservationService(userData,deskData); 


        static void Main(string[] args)
        {
            bool useSytem = true;

            Console.WriteLine("Bienvenido al sistema gestor de CoWorking - @yoiler_cordoba");
            while (useSytem)
            {
                Console.WriteLine("1: Admin  2: Usuario  3: Salir del sistema");
                string rolSelected = Console.ReadLine();

                // admin
                if (Enum.Parse<UserRole>(rolSelected) == UserRole.Admin)
                {


                    userService.LoginUserService(true);
                    // menu admin
                    bool useAdmin = true;
                    while (useAdmin)
                    {
                        Console.WriteLine("Bienvenido al panel de ADMINISTRADOR");
                        Console.WriteLine("1: Adminstracion de puestos   2: Administracion de usuarios    3: SALIR PANEL ADMIN ");
                        string menuAdminSelected = Console.ReadLine();

                        // admin desks
                        if (Enum.Parse<MenuAdmin>(menuAdminSelected) == MenuAdmin.AdminsDesks)
                        {
                            bool administrarPuestos = true;

                            while (administrarPuestos)
                            {
                                Console.WriteLine("Administracion de puestos");
                                Console.WriteLine("1=Crear 2=Editar 3=Eliminar  4=Bloquear  5=Salir administracion de puestos");
                                string menuPuestosSelected = Console.ReadLine();
                                AdminDesks adminPuestosOpciones = Enum.Parse<AdminDesks>(menuPuestosSelected);

                                // opciones para administrar los puestos
                                if (adminPuestosOpciones == AdminDesks.Exit)
                                {
                                    administrarPuestos = false;
                                    continue;
                                }
                                DeskService.ExecuteAction(adminPuestosOpciones);
                            }

                        }

                        //admin users
                        else if (Enum.Parse<MenuAdmin>(menuAdminSelected) == MenuAdmin.AdminUsers)
                        {
                            bool administrarUsuarios = true;

                            while (administrarUsuarios)
                            {
                                Console.WriteLine("Administracion de usuarios");
                                Console.WriteLine("1=Crear usuario, \n2=Editar usuario,\n3=Eliminar usuario,\n4=Cambiar contraseña \n5=Salir administracion de USUARIOS\n");
                                string menuUsuarioSelected = Console.ReadLine();
                                // opciones para administrar los puestos
                                AdminUser adminUserOpciones = Enum.Parse<AdminUser>(menuUsuarioSelected);
                                if (adminUserOpciones == AdminUser.Exit)
                                {
                                    administrarUsuarios = false;
                                    continue;
                                }
                                userService.ExecuteAction(adminUserOpciones);

                            }
                        }
                        else if (Enum.Parse<MenuAdmin>(menuAdminSelected) == MenuAdmin.Exit) useAdmin = false;
                        else Console.WriteLine("opcion valida en el menu admin");
                    }

                }

                // usuario
                else if (Enum.Parse<UserRole>(rolSelected) == UserRole.User)
                {
                    // login  user
                    ActiveUser =  userService.LoginUserService();

                    bool usarMenuUsuario = true;
                    string usuarioMenu = "";
                    while (usarMenuUsuario)
                    {
                        Console.WriteLine("1=Reservar puesto,  2=Cancelar reserva, 3=Ver historial de reserva  4=Cambiar contraseña,  5=Salir menu usuario");
                        usuarioMenu = Console.ReadLine();
                        MenuUser menuUserOptions = Enum.Parse<MenuUser>(usuarioMenu);
                        if(menuUserOptions == MenuUser.Exit) usarMenuUsuario = false;
                        ReservationService.ExecuteAction(menuUserOptions, ActiveUser);
                    }

                }
                else if (rolSelected == "3") useSytem = false;

                else Console.WriteLine("Opcion no valida");

            }
        }
    }
}
