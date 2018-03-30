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
    /// Interaction logic for AddCustomer.xaml
    /// </summary>
    public partial class AddCustomer : Window
    {
        Manager manager;
        public AddCustomer(Manager callingWindow)
        {
            InitializeComponent();
            manager = callingWindow;
        }

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (txtNewCustomerFirstName.Text != "" || txtNewCustomerLastName.Text != "" || txtNewCustomerPhoneNumber.Text != "" || txtNewCustomerEmail.Text != "")
            {

                if (DB.Customers.GetByNumber(txtNewCustomerPhoneNumber.Text) == null)
                {

                    Customer newCustomer = new Customer();
                    newCustomer.firstName = txtNewCustomerFirstName.Text;
                    newCustomer.lastName = txtNewCustomerLastName.Text;
                    newCustomer.phoneNumber = txtNewCustomerPhoneNumber.Text;
                    newCustomer.email = txtNewCustomerEmail.Text;
                    newCustomer.isActive = true;
                    Customer insertedCustomer = DB.Customers.Create(newCustomer);

                    if (insertedCustomer != null)
                    {

                        manager.currentCustomer = insertedCustomer;
                        manager.txtCustomerPhoneNumberSearch.Text = insertedCustomer.phoneNumber;
                        manager.txtCustomerPhoneNumber.Text = insertedCustomer.phoneNumber;
                        manager.txtCustomerFirstName.Text = insertedCustomer.firstName;
                        manager.txtCustomerLastName.Text = insertedCustomer.lastName;
                        manager.txtCustomerEmail.Text = insertedCustomer.email;

                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Customer already exists with this phone number.");
                }
            }
            else
            {
                MessageBox.Show("You must complete all fields");
            }
        }
    }
}
