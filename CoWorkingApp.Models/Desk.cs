using System;
using CoWorkingApp.Models.Enumerations;
namespace CoWorkingApp.Models
{
        public class Desk
        {
            public Guid DeskId {set; get;} = Guid.NewGuid();
            public string Number {set; get;}
            public string Description {set; get;}

            public DeskStatus DeskStatus {get; set;} =  DeskStatus.Active;
        
        }
}