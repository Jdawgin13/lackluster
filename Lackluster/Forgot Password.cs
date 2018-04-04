using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lackluster
{
    public partial class Forgot_Password: Form
    {
        private Boolean go = false;
        public Employee emp;
        public Forgot_Password()
        {
            InitializeComponent();
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
            textBox4.ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == textBox4.Text && go == true) //&& textBox2.Text == emp.getSecurityAnswer)
            {
                emp.SetPassword(textBox3.Text.ToString());
                emp.Save();
                string text = "Password has been changed.";
                MessageBox.Show(text);
                go = false;
            }
            else
            {
                string text = "Passwords do not match or Answer is not Correct!";
                MessageBox.Show(text);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            go = false;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox5.Text != null && DB.Employees.exists(textBox5.Text.ToLower()))
            {
                emp = DB.Employees.GetByUsername(textBox5.Text.ToLower());
                textBox2.ReadOnly = false;
                textBox3.ReadOnly = false;
                textBox4.ReadOnly = false;
                textBox1.Text = "THE QUESTION IS NYE";
                go = true;
                //textBox1.Text = emp.SecurityQ;
            }
            else
            {
                string text = "Please enter a Valid Username.";
                MessageBox.Show(text);
            }
        }
    }
}
