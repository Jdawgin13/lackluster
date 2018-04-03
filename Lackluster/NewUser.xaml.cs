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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Boolean createAccount = false;
        public static Employee emptemp;
        public static String newpass = "";

        public Window1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (username.Text == "" || passwordBox.Password == "" || passwordBox_Confirm.Password == "" || firstname.Text == "" || lastname.Text == "" || email.Text == "" || secquestion.Text == "" || answerbox.Password.ToString() == "" || answerconfirm.Password.ToString() == "")
            {
                MessageBox.Show("One or more fields are Blank!");
            }
            else if (passwordBox.Password.ToString() != passwordBox_Confirm.Password.ToString() || answerbox.Password.ToString() != answerconfirm.Password.ToString())
            {
                MessageBox.Show("Password or Answers do not match!");
            }
            else
            {
                emptemp = new Employee(username.Text.ToLower(),firstname.Text,lastname.Text,email.Text,true,WindowLogIn.isMan);
                //newUser = username.Text.ToLower();
                emptemp.isActive = true;
                newpass = passwordBox.Password.ToString();
                //emptemp.Save();
                //DB.Employees.Create(emptemp);
                ManagerLogin log = new ManagerLogin();
                log.Show();
                this.Close();
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            WindowLogIn.isMan = true;
        }
    }
}
