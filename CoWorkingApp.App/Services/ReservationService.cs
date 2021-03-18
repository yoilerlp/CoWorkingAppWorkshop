using CoWorkingApp.App.Enumeratios;
using CoWorkingApp.Data;
using CoWorkingApp.Models;
using System;
using System.Globalization;
using System.Linq;
using CoWorkingApp.Data.Tools;
namespace CoWorkingApp.App.Services
{

    public class ReservationService
    {
        private UserData userData { get; set; }
        private DeskData deskData { get; set; }
        private ReservationData reservationData { get; set; }
        public ReservationService(UserData userDataParam, DeskData deskDataParam)
        {
            this.deskData = deskDataParam;
            this.userData = userDataParam;
            this.reservationData = new ReservationData();
        }

        public void ReserveDeskService(User ActiveUser)
        {
            var newReservation = new Reservation();

            Console.WriteLine("----- Puestos disponibles -------");
            deskData.GetAvalibleDesk().ForEach((desk) =>
            {
                Console.WriteLine($"Numero del puesto : {desk.Number}");
                Console.WriteLine($"Descripcion : {desk.Description}");
                Console.WriteLine("------------------------------------");
            });

            // seleccionando el desk
            Console.WriteLine("Ingrese el numero del puesto a reservar. EJ A-0001");
            string deskNumber = Console.ReadLine();
            var deskSelected = deskData.FindDesk(deskNumber, true);

            while (deskSelected == null)
            {
                Console.WriteLine("El numero ingresado no corresponde a alguno puesto");
                Console.WriteLine("Ingrese el numero del puesto a editar. EJ A-0001");
                deskNumber = Console.ReadLine();
                deskSelected = deskData.FindDesk(deskNumber, true);
            }
            newReservation.DeskId = deskSelected.DeskId;

            // seleccionando la fecha
            var dateSelected = new DateTime();
            while (dateSelected.Year == 0001 || dateSelected < DateTime.Now)
            {

                Console.WriteLine("Ingrese la fecha de la reserva  Formato : (dd/mm/yyyy) y mayor a hoy");
                DateTime.TryParseExact(Console.ReadLine(), "dd-MM-yyyy", null, DateTimeStyles.None, out dateSelected);
            }
            newReservation.ReservationDate = dateSelected;
            newReservation.UserId = ActiveUser.UserId;

            bool reservationCreated = reservationData.CreateReservation(newReservation);
            if (reservationCreated) Console.WriteLine("Reservacion creada correctamente");
            else Console.WriteLine("Algo salio mal creando la reservacion");
        }

        public void CancelReservationService(User activeUser)
        {
            Console.WriteLine("Estas Son las reservaciones que tienes activas");
            var userActiveReservations = reservationData.GetReservationsByUserId(activeUser.UserId);
            if (userActiveReservations.Count == 0)
            {
                Console.WriteLine("No tienes reservaciones hechas");
                return;
            }
            var userDesks = deskData.GetAvalibleDesk();
            int indexReservation = 1;
            foreach (var reservation in userActiveReservations)
            {

                Console.WriteLine($"indice : {indexReservation}   deskId {userDesks.FirstOrDefault(d => d.DeskId == reservation.DeskId).Number} - {reservation.ReservationDate}");
                Console.WriteLine("----------------------------");
                indexReservation++;
            }

            Console.WriteLine("Ingrese el indice de la reservacion a cancelar");
            int indexReservationToCancel = int.Parse(Console.ReadLine());

            while (indexReservationToCancel < 1 || indexReservationToCancel > indexReservation)
            {
                Console.WriteLine("El indice ingresado no corresponde a ninguna de las reservaciones mostratas");
                Console.WriteLine("-------------------------------------------");

                Console.WriteLine("Ingrese el indice de la reservacion a cancelar");
                indexReservationToCancel = int.Parse(Console.ReadLine());
            }

            var reservationToCancel = userActiveReservations[indexReservationToCancel-1];

            bool reservationWasCancel = reservationData.CalcelReservation(reservationToCancel.ReservationId);
            if(reservationWasCancel) Console.WriteLine("Reservacion cancelada correctamente");
            else Console.WriteLine("Algo salio mal cancelando la reservation");
        }

        public void ChangePasswordUserService(User activeUser)
        {
            
            Console.WriteLine("ingrese su nueva contraseña");
            activeUser.PassWord = EncryptData.EncryptText(EncryptData.GetPassWord());

            var userWasUpdated = userData.EditUser(activeUser);
            if(userWasUpdated) Console.WriteLine("Contraseña actualizada correctamente");
            else Console.WriteLine("Algo salio mal en la actualizacion de la informacion");


        }


        public void SeeReservacionHistory(User activeUser)
        {
            Console.WriteLine("Tus reservas");
            var userActiveReservations = reservationData.GetReservationHistoryByUserId(activeUser.UserId);
            var userDesks = deskData.GetAllDesk();
            foreach (var item in userActiveReservations)
            {
                Console.WriteLine($"{userDesks.FirstOrDefault(d => d.DeskId == item.DeskId).Number} - {item.ReservationDate.ToString("dd-MM-yyyy")} - {(item.ReservationDate > DateTime.Now ? "Active" : " ")} ");
            }
        }

        public void ExecuteAction(MenuUser menuUserOptions, User activeUser)
        {

            switch (menuUserOptions)
            {
                case MenuUser.ReserveDesk:
                    {
                        this.ReserveDeskService(activeUser);
                        break;
                    }
                case MenuUser.CancelReserve:
                    {
                       this.CancelReservationService(activeUser);

                        break;
                    }
                case MenuUser.SeeHistory:
                    {
                       this.SeeReservacionHistory(activeUser);

                        break;
                    }
                case MenuUser.ChangePassword:
                    {
                        this.ChangePasswordUserService(activeUser);
                        break;
                    }
                default:
                    {
                        Console.WriteLine("OPCION NO VALIDA");
                        break;
                    }
            }



        }
    }
}