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
    /// Interaction logic for ManagerLogin.xaml
    /// </summary>
    public partial class ManagerLogin : Window
    {
        private Employee emp;
        private int count;

        public ManagerLogin()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ManagerConfirm confirm = new ManagerConfirm();
            //TODO if user exists
            emp = DB.Employees.GetByUsername(username.Text.ToLower());
            if (emp.isManager == true && username.Text.ToLower() == emp.username && true == emp.VerifyPassword(password.Password.ToString()))
            {
                confirm.Show();
                this.Close();
            }
            else if (count < 3)
            {
                MessageBox.Show("Error Wrong Username, Password, or the User is not a manager!");
                count++;
            }
            else
            {
                DB.Employees.Delete(Window1.emptemp);
                this.Close();
            }
        }
    }
}
