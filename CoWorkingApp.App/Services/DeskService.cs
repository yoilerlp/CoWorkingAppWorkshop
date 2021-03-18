using CoWorkingApp.Data;
using CoWorkingApp.Models;
using CoWorkingApp.App.Enumeratios;
using CoWorkingApp.Models.Enumerations;
using System;
namespace CoWorkingApp.App.Services
{

    public class DeskService
    {
        private DeskData deskData { get; set; }
        public DeskService(DeskData deskData)
        {
            this.deskData = deskData;
        }

        public void CreateDeskService()
        {
            Desk newDesk = new Desk();
            Console.WriteLine("ingrese el numero del puesto EJ: A-0001");
            newDesk.Number = Console.ReadLine();
            Console.WriteLine("ingrese una  descripcion del puesto");
            newDesk.Description = Console.ReadLine();

            bool deskWasCreated = deskData.CreateDesk(newDesk);
            if (deskWasCreated) Console.WriteLine("Puesto creado correctamente");
            else Console.WriteLine("Algo salio mal creando el puesto");

        }

        public void EditDeskService()
        {
            Console.WriteLine("Ingrese el numero del puesto a editar. EJ A-0001");
            string deskNumber = Console.ReadLine();
            var deskFound = deskData.FindDesk(deskNumber);

            while (deskFound == null)
            {
                Console.WriteLine("El numero ingresado no corresponde a alguno puesto");
                Console.WriteLine("Ingrese el numero del puesto a editar. EJ A-0001");
                deskNumber = Console.ReadLine();
                deskFound = deskData.FindDesk(deskNumber);
            }

            Console.WriteLine("ingrese el nuevo numero del puesto EJ: A-0001");
            deskFound.Number = Console.ReadLine();
            Console.WriteLine("ingrese una nueva descripcion del puesto");
            deskFound.Description = Console.ReadLine();
            Console.WriteLine("Ingrese el nuevo estado del puesto. 1=activo  2=Inactive  3=Bloqueado");
            string newDeskStatus = Console.ReadLine();

            
            while ( ( newDeskStatus != "1" ) && (newDeskStatus != "2") && (newDeskStatus != "3"))
            {
                Console.WriteLine("Ingrese el nuevo estado del puesto. 1=activo  2=Inactive  3=Bloqueado");
                newDeskStatus = Console.ReadLine().Trim();
            }
            deskFound.DeskStatus = Enum.Parse<DeskStatus>(newDeskStatus);

            bool deskWasEdited = deskData.EditDesk(deskFound);
            if (deskWasEdited) Console.WriteLine("Puesto actualizado correctamente");
            else Console.WriteLine("Algo salio mal editando el puesto");

        }


        public void blockDeskService()
        {
            Console.WriteLine("Ingrese el numero del puesto a editar. EJ A-0001");
            string deskNumber = Console.ReadLine();
            var deskFound = deskData.FindDesk(deskNumber);

            while (deskFound == null)
            {
                Console.WriteLine("El numero ingresado no corresponde a alguno puesto");
                Console.WriteLine("Ingrese el numero del puesto a editar. EJ A-0001");
                deskNumber = Console.ReadLine();
                deskFound = deskData.FindDesk(deskNumber);
            }

            deskFound.DeskStatus = DeskStatus.Blocked;

            bool deskWasBlocked = deskData.EditDesk(deskFound);
            if (deskWasBlocked) Console.WriteLine("Puesto bloqueado correctamente");
            else Console.WriteLine("Algo salio mal bloqueando el puesto");
        }

        public void DeleteDeskService()
        {
            Console.WriteLine("Ingrese el numero del puesto a eliminar ");
            string numerDeskToDelete = Console.ReadLine();
            var deskToDelete = deskData.FindDesk(numerDeskToDelete);
            while (deskToDelete == null)
            {
                Console.WriteLine($"El numero {numerDeskToDelete} no fue encontrado, intentelo de nuevo");
                Console.WriteLine("Ingrese el numero del puesto a eliminar ");
                numerDeskToDelete = Console.ReadLine();
                deskToDelete = deskData.FindDesk(numerDeskToDelete);
            }

            Console.WriteLine($"¿ Estas seguro que quieres eliminar el puesto {numerDeskToDelete} ? 1=SI ");
            if (Console.ReadLine() == "1")
            {
                bool deskWasDeleted = deskData.DeleteDesk(deskToDelete.DeskId);
                if (deskWasDeleted) Console.WriteLine("Puesto eliminado correctamente");
                else Console.WriteLine("Algo salio mal eliminando el puesto");
            }

        }


        public void ExecuteAction(AdminDesks AdminPuestosOpciones)
        {
            switch (AdminPuestosOpciones)
            {
                case AdminDesks.Create:
                    {
                        Console.WriteLine("Opcion : creando puestos");
                        this.CreateDeskService();
                        break;
                    }
                case AdminDesks.Edit:
                    {
                        Console.WriteLine("Opcion : editanto puestos");
                        this.EditDeskService();
                        break;
                    }
                case AdminDesks.Delete:
                    {
                        Console.WriteLine("Opcion : eliminando puestos");
                        this.DeleteDeskService();
                        break;
                    }
                case AdminDesks.Block:
                    {
                        Console.WriteLine("Opcion : bloqueado puestos");
                        this.blockDeskService();
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