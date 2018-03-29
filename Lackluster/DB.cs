using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Lackluster
{
    public static class DB
    {
        private static MySqlConnection connection;
        private static bool ConnectionSetup = false;

        private static void SetupConnection()
        {
            string server = "WildcardDev02";
            string database = "lackluster";
            string uid = "program";
            string password = "program";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            try
            {
                connection = new MySqlConnection(connectionString);
                ConnectionSetup = true;
            }
            catch (Exception e)
            {
                connection.Close();
            }
        }

        private static void CheckConnection()
        {
            if (!ConnectionSetup) { SetupConnection(); }


            //checks for open connection
            if (connection != null && connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }

        }

        //Returns SQL reader
        private static MySqlDataReader SelectQry(string query)
        {
            //Checks for open connection
            CheckConnection();

            //Create a SQL command
            MySqlCommand cmd = connection.CreateCommand();

            //Set the query to the command's CommandText
            cmd.CommandText = query;

            return cmd.ExecuteReader();


        }

        private static int InsertQry(string query)
        {
            //Checks for open connection
            CheckConnection();

            //Create a SQL command
            MySqlCommand cmd = connection.CreateCommand();

            //Set the query to the command's CommandText
            cmd.CommandText = query;

            cmd.ExecuteScalar();
            int last = -1;
            last = Convert.ToInt32(cmd.LastInsertedId);

            return last;
        }

        private static int UpdateQry(string query)
        {
            //Checks for open connection
            CheckConnection();

            //Create a SQL command
            MySqlCommand cmd = connection.CreateCommand();

            //Set the query to the command's CommandText
            cmd.CommandText = query;

            cmd.ExecuteScalar();
            int last = -1;
            last = Convert.ToInt32(cmd.LastInsertedId);

            return last;
        }

        private static MySqlCommand Query(string query)
        {
            try
            {
                CheckConnection();
                //Create a SQL command
                MySqlCommand cmd = connection.CreateCommand();

                //Set the query to the command's CommandText
                cmd.CommandText = query;

                return cmd;
            }
            catch (Exception e)
            {
                connection.Close();
            }
            return null;
        }

        public static class Movies
        {

            //Method to retreive ONE Movie
            //Returns Movie Object
            public static Movie Get(string upc)
            {
                //Query to get movie
                string query = "Select upc.upc as upc, upc.id AS upcID, upc.active as upcActive, upc.Rented as rented, m.* From movies m Inner Join moviesupc upc on m.id = upc.movieId Where upc.upc = '" + upc + "'";

                MySqlDataReader reader = SelectQry(query);

                //Check if anything was returned
                if (reader.Read())
                {
                    Movie movie = new Movie();
                    //Set data to public variables
                    movie.id = reader.GetString("id");
                    movie.title = reader.GetString("title");
                    movie.rating = reader.GetString("rating");
                    movie.releaseYear = reader.GetString("year");
                    movie.genre = reader.GetString("genre");
                    movie.upc = reader.GetString("upc");
                    movie.isRented = reader.GetBoolean("rented");
                    movie.isActive = reader.GetBoolean("upcActive");
                    movie.price = "3.00";

                    reader.Close();
                    return movie;
                }

                reader.Close();
                return null;
            }

            //Gets all movie in db order by movie ID
            //1, Start id Number
            //Returns 50 Movies in List
            public static List<Movie> GetAll(int StartMovieId = 1)
            {
                List<Movie> movies = new List<Movie>();
                string query = "Select m.* From movies m Where m.id >= '" + StartMovieId + "' AND m.id <='" + (StartMovieId + 49) + "'";

                MySqlDataReader reader = SelectQry(query);

                //Check if anything was returned
                while (reader.Read())
                {
                    Movie movie = new Movie();
                    //Set data to public variables
                    movie.id = reader.GetString("id");
                    movie.title = reader.GetString("title");
                    movie.rating = reader.GetString("rating");
                    movie.releaseYear = reader.GetString("year");
                    movie.genre = reader.GetString("genre");
                    movie.isActive = reader.GetBoolean("active");
                    movie.price = "3.00";

                    movies.Add(movie);

                }

                reader.Close();

                return movies;
            }

            //Saves Movie in DB
            //Param is completed Movie Object
            //Return nothing
            public static Movie Create(Movie movie, int numAvailable, int numLeft)
            {

                string query = "Insert into movies (title, rating, year, genre, numOfCopies, availableCopies, active, price) values ('" + movie.title +
                    "', '" + movie.rating + "', '" + movie.releaseYear + "', '" + movie.genre + "', " + numAvailable + "," + numLeft + ", " + movie.isActive + ",'3.00')";

                int last = InsertQry(query);
                
                if (last != -1)
                {
                    movie.id = "" + last;
                    return new Movie();
                }
                else
                {
                    return null;
                }


            }

            //update movie record in DB
            public static Movie Update(Movie movie)
            {
                return new Movie();
            }

            //Sets movie in DB as deactive
            //2 Params
            //1 - Movie Object
            //2 - boolean for either inactivating entire movie or just UPC
            public static bool Delete(Movie movie, bool OnlyUPC)
            {
                if (movie.id == null)
                {
                    return false;
                }
                string query = "Update movies set active = 0 where movies.id = " + movie.id;
                MySqlCommand cmd = DB.Query(query);
                cmd.ExecuteScalar();

                return true;
            }

            public static MySqlDataReader Query(string query)
            {
                MySqlDataReader dataReader;
                MySqlCommand cmd = DB.Query(query);
                dataReader = cmd.ExecuteReader();
                return dataReader;
            }


            //public static List<Movie> GetLateMovies()
            //{
            //    //gets all late movies returns them as list
            //    string query 
            //    return new List<Movie>();
            //}
        }

        public static class Employees
        {
            //Creates new employee in DB 
            //Params Employee objct
            //Returns employee if successfull null otherwise
            public static Employee Create(Employee emp)
            {

                int manager = 0;
                int active = 0;

                if (emp.isActive)
                {
                    active = 1;
                }
                if (emp.isManager)
                {
                    manager = 1;
                }



                string query = "insert into employees (username, fname, lname, email, active, manager) Values (" + emp.username + ", " + emp.firstName + ", " + emp.lastName + ", " + emp.email + ", " + active + ", "+manager+" ) ";

                int insert = InsertQry(query);
                if (insert != -1){
                    emp.id = ""+insert;
                    return emp;
                }
                return null;
            }

            //Marks employee as inactive in DB
            //Returns true is successful
            public static bool Delete(Employee emp)
            {
                emp.isActive = false;
                string query = "update employees set active = 0 where employee.id = " + emp.id;
                int insert = InsertQry(query);
                if (insert != -1)
                {
                    return true;
                }
                return false;
            }

            //Gets employee by username
            //Paramater is string username
            //Returns employee object
            public static Employee GetByUsername(string username)
            {
                string query = "Select * from employees where username='" + username +"'";
                
                MySqlDataReader reader = SelectQry(query);

                if (reader.Read())
                {
                    Employee emp = new Employee();
                    emp.id = reader.GetString("id");
                    emp.username = reader.GetString("username");
                    emp.firstName = reader.GetString("fname");
                    emp.lastName = reader.GetString("lname");
                    emp.email = reader.GetString("email");
                    emp.isActive = reader.GetBoolean("active");
                    emp.isManager = reader.GetBoolean("manager");

                    reader.Close();
                    return emp;
                }

                reader.Close();
                return null;
            }

            //Update employee in DB
            //Param is employee  object to update
            //Returns employee object
            public static bool Update(Employee emp)
            {

              //  string query = "update employees set active = 0 where employee.id = " + emp.id;
                return true;
            }

            //Gets seleected employees hashed DB password
            //Method might be replaced
            public static string GetPassword(Employee emp)
            {
                string query = "Select password from employees where id= " + emp.id;

                MySqlDataReader reader = SelectQry(query);

                string password = null; ;
                if (reader.Read())
                {
                    password = reader.GetString("password");
                }

                return password;
            }

            public static bool SetPassword(Employee emp, string psw)
            {
                return true;
            }

            //method to change employees hashed password
            public static bool ChangePassword(Employee emp, string password)
            {
                return true;
            }

        }

        public static class Customers
        {
            //Gets customer from DB by Phone Number
            //Params phone number string
            //Returns customer object

            
            public static Customer GetByNumber(string number)
            {
                //return new Customer();
                string query = "Select * from customers where phoneNumber='" + number + "'";

                MySqlDataReader reader = SelectQry(query);

                if (reader.Read())
                {
                    Customer cust = new Customer();
                    cust.id = reader.GetString("id");
                    cust.phoneNumber = reader.GetString("phoneNumber");
                    cust.firstName = reader.GetString("fname");
                    cust.lastName = reader.GetString("lname");
                    cust.email = reader.GetString("email");
                    cust.isActive = reader.GetBoolean("active");
                    cust.points = reader.GetInt16("points");

                    reader.Close();
                    return cust;
                }

                reader.Close();
                return null;
            }

            //updates customer info in db
            public static Customer Update(Customer cst)
            {
                return new Customer();
            }

            //creates new Customer in DB
            public static Customer Create(Customer cst)
            {
                return new Customer();
            }

            //sets customer as inactive
            public static bool Delete(Customer cst)
            {
                return true;
            }

        }

        public static class Rentals
        {
            //create new rental in db
            //Returns rental object
            public static bool Create(Employee emp, Customer cst, Movie movie)
            {
                //query to insert record into the rentals table
                string query = $"insert into rentals (upc, customerId, employeeId, checkoutDate, dueDate, returnedDate, returnedById) values ('{movie.upc}', {cst.id}, {emp.id}, '{DateTime.Now.ToString("yyyy-MM-dd")}', '{DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")}', '9999/12/31', 99);";
                
                
                //create command from query
                MySqlCommand cmd = DB.Query(query);

                //Create a reader by executying the SQL command
                MySqlDataReader reader = cmd.ExecuteReader();
                
                //Close the reader for reuse
                reader.Close();

                //Check that only one row was indeed inserted
                if (reader.RecordsAffected == 1)
                {
                    //query to update rented bool in movies upc
                    query = $"update moviesupc set rented = 1 where upc = '{movie.upc}';";

                    cmd = DB.Query(query);

                    //Assign reader to new date from executed query
                    reader = cmd.ExecuteReader();

                    //Check that only one row was updated
                    if (reader.RecordsAffected == 1)
                    {
                        return true;
                    }
                }
                return false;
            }

            //Returns rental in DB
            //Returns rental obj
            public static Rental Return(Employee emp, Customer cst, Movie movie)
            {
                return new Rental();
            }

            //Gets all late rentals
            //Returns list of rentals
            public static List<Rental> Late()
            {
                return new List<Rental>();

            }

            public static void Update(Rental rental)
            {

            }

            //Gets rental information based on vd upc code
            public static Rental GetByUPC(string upc)
            {
                return new Rental();
            }
        }
    }
}
