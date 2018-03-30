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
        public int id { get; set; }
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

        //method to change employees password
        public void SetPassword(string password)
        {
            DB.Employees.UpdatePassword(this, HashPassword(password));
            this.Save();
        }

        //Takes employee hashed password from DB, then hashes this password paramater and compares
        public bool VerifyPassword(string password)
        {
            try
            {
                //gets hashed password from db
                //Stored in this.password field
                string dbPassword = DB.Employees.GetPassword(this);
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
            }catch(Exception e)
            {
                return false;
            }
        }

        private static string HashPassword(string password)
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
