using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lackluster
{
    public class Customer
    {
        public string id { get; set; }
        public string phoneNumber { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public int points { get; set; }
        public bool isActive { get; set; }

        public Customer()
        {

        }

        public Customer(string phoneNumber, string firstName, string lastName, string email, int points=0, bool isActive=true)
        {
            this.id = id;
            this.phoneNumber = phoneNumber;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.points = points;
            this.isActive = isActive;
        }

        public void Save()
        {
            DB.Customers.Update(this);
        }

        
        public void RentMovie(Movie movie, Employee emp)
        {

        }

        public void RentMovies(List<Movie> movies, Employee emp)
        {

        }



    }
}
