using System;
namespace CoWorkingApp.Models {

    public class User {
        public Guid UserId {set; get; } = Guid.NewGuid();
        public string Name {set; get;}
        public string LastName {set; get;}
        public string Email {set; get;}
        public string PassWord {set; get;}

    }

}

