using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lackluster
{
     public class Employee
    {
        public string id { get; set; }
        public string username { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public bool isActive { get; set; }
        public bool isManager { get; set; }

        public Employee(string username, string firstName, string lastName, string email, bool isActive, bool isManager)
        {
            this.username = username;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.isActive = isActive;
            this.isManager = isManager;
        }

        public Employee()
        {

        }

        public void Save()
        {
            DB.Employees.Update(this);
        }

        private string GetDBPassword()
        {
            return DB.Employees.GetPassword(this);
        }

        public bool GetDBPassword(string password)
        {
            return DB.Employees.SetPassword(this, password);
        }

        public bool VerifyPassword(string password)
        {
            //gets hashed password from db
            string dbPassword = GetDBPassword();
            byte[] dbHashedBytes = Convert.FromBase64String(dbPassword);

            //Hashing text password
            byte[] salt = new byte[16];
            Array.Copy(dbHashedBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] textHash = pbkdf2.GetBytes(20);

            for (int i = 0; i < 20; i++)
            {
                if (dbHashedBytes[i + 16] != textHash[i])
                {
                    return false;
                }
            }
            
            return true;
        }

        public static string HashPassword(string password)
        {
            byte[] salt;

            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);

            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];

            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string PasswordHash = Convert.ToBase64String(hashBytes);

            return PasswordHash;
        }
    }
}
