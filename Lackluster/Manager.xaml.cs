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
            //Show the txtRentalEntry Box
            txtRentalEntry.Visibility = Visibility.Visible;

            //Set focus to the txtRentalEntry Box
            txtRentalEntry.Focus();
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


                    //Check if the movie is actually a movie in our database
                    if (scannedEntry.upc != "")
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

            //TODO - Check that the movie is actually in stock (not rented out)
        }

        private void btnCompleteRental_Click(object sender, RoutedEventArgs e)
        {
            //Hide the txtRentalEntry Box
            txtRentalEntry.Visibility = Visibility.Hidden;

            //TODO - Complete the Rental Process
        }

        private void btnStartReturn_Click(object sender, RoutedEventArgs e)
        {
            //Show the txtReturnEntry Box
            txtReturnEntry.Visibility = Visibility.Visible;

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

                    //Check if the movie is actually a movie in our database
                    if (scannedEntry.upc != "")
                    {
                        //Add the movie object to the list
                        lstReturn.Items.Add(scannedEntry);

                    }
                    else
                    {
                        MessageBox.Show("This is not an active movie");
                    }

                }

                //Reset the txtRentalEntry box
                txtReturnEntry.Text = "";
            }

            //TODO - Check to make sure the movie is indeed rented out
        }

        private void btnCompleteReturn_Click(object sender, RoutedEventArgs e)
        {
            //Hide the txtReturnEntry Box
            txtReturnEntry.Visibility = Visibility.Hidden;

            //TODO - Complete the Return Process
        }

        private void btnCustomerLookup_Click(object sender, RoutedEventArgs e)
        {
            //Pass the phone number to the Customer object to create a new object
            Customer searchCustomer = new Customer();

            searchCustomer = DB.Customers.GetByNumber(txtCustomerPhoneNumberSearch.Text);
            

            if (searchCustomer.phoneNumber != null)
            {
                //Put the data in the rent boxes
                txtCustomerFirstName.Text = searchCustomer.firstName;
                txtCustomerLastName.Text = searchCustomer.lastName;
                txtCustomerPhoneNumber.Text = searchCustomer.phoneNumber;
                txtCustomerEmail.Text = searchCustomer.email;
            }
            else
            {
                //Phone number not found in database
                MessageBox.Show("Phone number not found");
            }


        }
    }
}

