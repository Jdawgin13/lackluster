using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Lackluster
{
    public class Movie
    {
        //Variables for the movie object
        public string id { get; set; }
        public string title { get; set; }
        public string rating { get; set; }
        public string releaseYear { get; set; }
        public string genre { get; set; }
        public string upc { get; set; }
        public string price { get; set; }
        public bool isRented { get; set; }
        public bool isActive { get; set; }

        public Movie(string title, string rating, string year, string genre, string upc, string price, bool isActive)
        {
            this.title = title;
            this.rating = rating;
            this.releaseYear = year;
            this.genre = genre;
            this.upc = upc;
            this.price = price;
            this.isActive = isActive;
        }

        public Movie()
        {

        }

        public void Save() {
            DB.Movies.Update(this);
        }

        public int TotalNumCopies()
        {
            return 0;
        }

        public int NumAvailable()
        {
            return 0;
        }
    }
}
