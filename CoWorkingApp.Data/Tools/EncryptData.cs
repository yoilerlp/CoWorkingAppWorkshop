using System;
using System.Numerics;
using System.Text;
using System.Security.Cryptography;
namespace CoWorkingApp.Data.Tools {

    public static class EncryptData {

        public static string EncryptText(string text) {
            var sha256 = SHA256.Create();
            var hasBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
            var has = BitConverter.ToString(hasBytes).Replace("-","").ToLower();    
            return has;
        }

        public static string GetPassWord() {

            string fullPassWord = "";
            while (true)
            {
                var passwordInput = Console.ReadKey(true);
                if(passwordInput.Key == ConsoleKey.Enter) {
                   Console.WriteLine("");
                   break;
                }else 
                {
                    Console.Write("*");
                    fullPassWord += passwordInput.KeyChar;
                }
            }

            return fullPassWord;
        } 


    }



}