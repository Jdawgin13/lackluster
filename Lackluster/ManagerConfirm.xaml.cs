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
    /// Interaction logic for ManagerConfirm.xaml
    /// </summary>
    public partial class ManagerConfirm : Window
    {
        public ManagerConfirm()
        {
            InitializeComponent();
            if (WindowLogIn.isMan == true)
            {
                MessageBox.Show("WARNING THIS USER IS SET TO MANAGER!");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowLogIn.isMan = false;
            DB.Employees.Delete(Window1.emptemp);
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DB.Employees.Create(Window1.emptemp);
            Window1.emptemp.SetPassword(Window1.newpass);
            Window1.emptemp.Save();
            WindowLogIn.isMan = false;
            Window1.emptemp.isActive = true;
            MessageBox.Show("User Has Successfully been Created!");
            this.Close();
        }
    }
}
