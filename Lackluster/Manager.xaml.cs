using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lackluster
{
    /// <summary>
    /// Interaction logic for Manager.xaml
    /// </summary>
    public partial class Manager : Window
    {
        //Initialize total amount to zero
        double total = 0.00;

        public Employee currentUser; //variable to hold current user
        private Customer currentCustomer; //Variable to hold the customer who was looked up

        public Manager()
        {
            InitializeComponent();
            //Test Here
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            //Create instance of Manager window to control objects
            WindowLogIn login = new WindowLogIn();

            //Open the login window and close the manager window
            login.Show();
            this.Close();
        }

        private void btnStartRental_Click(object sender, RoutedEventArgs e)
        {
            //Make sure there is a currentCustomer set
            if(currentCustomer != null)
            {
                //Hide and lock fields
                txtCustomerPhoneNumberSearch.Focusable = false;
                txtCustomerFirstName.Focusable = false;
                txtCustomerLastName.Focusable = false;
                txtCustomerPhoneNumber.Focusable = false;
                txtCustomerEmail.Focusable = false;
                btnCustomerLookup.Focusable = false;
                btnCustomerAdd.Focusable = false;
                btnUpdateCustomerInfo.Focusable = false;
                tbReturn.Focusable = false;
                tbMovies.Focusable = false;
                tbCustomer.Focusable = false;
                tbReports.Focusable = false;
                tbEmployee.Focusable = false;

                //Show the txtRentalEntry Box and btnDelete
                txtRentalEntry.Visibility = Visibility.Visible;
                btnDeleteRentalEntry.Visibility = Visibility.Visible;

                //Set focus to the txtRentalEntry Box
                txtRentalEntry.Focus();
            }
            else
            {
                //Warn user they need to look up a customer
                MessageBox.Show("You need to look up a customer");
            }
        }

        private void txtScanEntry_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //Look for the Return key press
            if (e.Key == Key.Return)
            {
                //Variable to hold whether movie is already in the list
                bool found = false;

                //Search each list entry
                foreach (Movie i in lstRent.Items)
                {
                    //Determine if the upc entered is already in the list
                    if (i.upc.ToString().Equals(txtRentalEntry.Text))
                    {
                        //Set found to true since the movie is already in the list
                        found = true;
                    }
                }

                //Check if the movie is already in the list
                if (found)
                {
                    MessageBox.Show("Movie already added");
                }
                else
                {

                    //Create a movie object by passing the scanned text
                    Movie scannedEntry = new Movie();
                    scannedEntry = DB.Movies.Get(txtRentalEntry.Text);


                    //Check if the movie is actually a movie in our database & not already rented out
                    if (scannedEntry != null && !scannedEntry.isRented)
                    {
                        //Add the movie object to the list
                        lstRent.Items.Add(scannedEntry);

                        //Increase the total
                        total = total + Convert.ToDouble(scannedEntry.price);
                    } else
                    {
                        MessageBox.Show("This is not an active movie");
                    }

                    //Set the txtTotal box to the total
                    txtTotal.Text = String.Format("{0:C}", total);
                }

                //Reset the txtRentalEntry box
                txtRentalEntry.Text = "";
            }

        }

        private void btnCompleteRental_Click(object sender, RoutedEventArgs e)
        {
            //Hide the txtRentalEntry Box and btnDelete
            txtRentalEntry.Visibility = Visibility.Hidden;
            btnDeleteRentalEntry.Visibility = Visibility.Hidden;

            //Check if there are movies in the list
            if (lstRent.HasItems)
            {
                foreach(Movie i in lstRent.Items)
                {
                    DB.Rentals.Create(currentUser, currentCustomer, i);
                }
                //Show message showing how many movies were rented and the total
                MessageBox.Show($"You rented {lstRent.Items.Count} movie(s)\nFor a total of {txtTotal.Text}\nFor {txtCustomerFirstName.Text} {txtCustomerLastName.Text}");

                //Clear the list
                lstRent.Items.Clear();

                //Reset txtTotal
                txtTotal.Text = "$0.00";

                //Reset total
                total = 0.00;


            }

            //UnHide and unlock fields
            txtCustomerPhoneNumberSearch.Focusable = true;
            txtCustomerFirstName.Focusable = true;
            txtCustomerLastName.Focusable = true;
            txtCustomerPhoneNumber.Focusable = true;
            txtCustomerEmail.Focusable = true;
            btnCustomerLookup.Focusable = true;
            btnCustomerAdd.Focusable = true;
            btnUpdateCustomerInfo.Focusable = true;
            tbReturn.Focusable = true;
            tbMovies.Focusable = true;
            tbCustomer.Focusable = true;
            tbReports.Focusable = true;
            tbEmployee.Focusable = true;

            //Clear out the customer
            currentCustomer = null;
            txtCustomerPhoneNumberSearch.Text = "";
            txtCustomerFirstName.Text = "";
            txtCustomerLastName.Text = "";
            txtCustomerPhoneNumber.Text = "";
            txtCustomerEmail.Text = "";
        }

        private void btnStartReturn_Click(object sender, RoutedEventArgs e)
        {
            //Show the txtReturnEntry Box and btnDeleteReturnEntry
            txtReturnEntry.Visibility = Visibility.Visible;
            btnDeleteReturnEntry.Visibility = Visibility.Visible;

            //Lock tabs
            tbRent.Focusable = false;
            tbMovies.Focusable = false;
            tbCustomer.Focusable = false;
            tbReports.Focusable = false;
            tbEmployee.Focusable = false;

            //Set focus to the txtReturnEntry Box
            txtReturnEntry.Focus();
        }

        private void txtReturnEntry_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //Look for the Return key press
            if (e.Key == Key.Return)
            {
                //Variable to hold whether movie is already in the list
                bool found = false;

                //Search each list entry
                foreach (Movie i in lstReturn.Items)
                {
                    //Determine if the upc entered is already in the list
                    if (i.upc.ToString().Equals(txtReturnEntry.Text))
                    {
                        //Set found to true since the movie is already in the list
                        found = true;
                    }
                }

                //Check if the movie is already in the list
                if (found)
                {
                    MessageBox.Show("Movie already added");
                }
                else
                {

                    //Create a movie object by passing the scanned text
                    Movie scannedEntry = new Movie();
                    scannedEntry = DB.Movies.Get(txtReturnEntry.Text);

                    //Check if the movie is actually a movie in our database & is rented out
                    if (scannedEntry != null && scannedEntry.isRented)
                    {
                        //Add the movie object to the list
                        lstReturn.Items.Add(scannedEntry);

                    }
                    else
                    {
                        MessageBox.Show("This movie is not rented out");
                    }

                }

                //Reset the txtRentalEntry box
                txtReturnEntry.Text = "";
            }
        }

        private void btnCompleteReturn_Click(object sender, RoutedEventArgs e)
        {
            //Hide the txtReturnEntry Box and btnDeleteReturnEntry
            txtReturnEntry.Visibility = Visibility.Hidden;
            btnDeleteReturnEntry.Visibility = Visibility.Hidden;

            //Check if there are movies in the list
            if (lstReturn.HasItems){

                foreach (Movie i in lstReturn.Items)
                {
                    DB.Rentals.Return(currentUser, i);
                }

                //Show message showing how many movies were rented and the total
                MessageBox.Show($"You returned {lstReturn.Items.Count} movie(s)");

                //Clear the list
                lstReturn.Items.Clear();
            }

            //Unlock tabs
            tbRent.Focusable = true;
            tbMovies.Focusable = true;
            tbCustomer.Focusable = true;
            tbReports.Focusable = true;
            tbEmployee.Focusable = true;
        }

        private void btnCustomerLookup_Click(object sender, RoutedEventArgs e)
        {
            //Pass the phone number to the Customer object to create a new object
            Customer searchCustomer = new Customer();

            searchCustomer = DB.Customers.GetByNumber(txtCustomerPhoneNumberSearch.Text);
            

            if (searchCustomer != null)
            {
                //Put the data in the rent boxes
                txtCustomerFirstName.Text = searchCustomer.firstName;
                txtCustomerLastName.Text = searchCustomer.lastName;
                txtCustomerPhoneNumber.Text = searchCustomer.phoneNumber;
                txtCustomerEmail.Text = searchCustomer.email;

                //Set the current customer to the found searched customer for later use in the rental process 
                //(eg. Creating the rental record)
                currentCustomer = searchCustomer;
            }
            else
            {
                //Phone number not found in database
                MessageBox.Show("Phone number not found");

                //set currentCustomer to null
                currentCustomer = null;
            }


        }

        private void btnDeleteRentalEntry_Click(object sender, RoutedEventArgs e)
        {
            //Make sure an item in the list is selected
            if (lstRent.SelectedIndex > -1)
            {
                //Retrieve the selected movie object from the list
                Movie soonToBeDeletedMovie = lstRent.SelectedItem as Movie;

                //Reduce the total by the movie price
                total -= Convert.ToDouble(soonToBeDeletedMovie.price);

                //Update txtTotal with new price
                txtTotal.Text = String.Format("{0:C}", total);

                //Delete the item
                lstRent.Items.RemoveAt(lstRent.SelectedIndex);
            }

            //Give focuse back to the text entry box
            txtRentalEntry.Focus();
        }

        private void btnDeleteReturnEntry_Click(object sender, RoutedEventArgs e)
        {
            //Make sure an item in the list is selected
            if (lstReturn.SelectedIndex > -1)
            {
               //Delete the item
               lstReturn.Items.RemoveAt(lstReturn.SelectedIndex);
            }

            //Give focuse back to the text entry box
            txtReturnEntry.Focus();
        }
    }
}

