using _01_Uygulama.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _01_Uygulama
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string mailAddress = txtEmail.Text;
            string[] NameArray = mailAddress.Split('.', '@'); //nancy.davoloin@northwind.com
            string firstName = NameArray[0];
            string lastName = NameArray[1];
            string password = txtPassword.Text;
            int year = Convert.ToInt32(password.Substring(password.Length - 4, 4));

            if (NameArray[0] != password.Substring(0,password.Length - 4) || !mailAddress.EndsWith("@northwind.com"))
            {
                return;
            }

            NorthwindEntities db = new NorthwindEntities();
            Employee employee = db.Employees.Where(x => x.FirstName == firstName && x.LastName == lastName && x.BirthDate.Value.Year == year).SingleOrDefault();

            MessageBox.Show(employee !=null ? "Giriş Yapıldı" : "Bilgilerinizi kontrol ediniz.");
            if (employee != null)
            {
                Form2 form2 = new Form2(employee.EmployeeID);
                this.Hide();
                form2.ShowDialog();
                form2.Text = $"{employee.FirstName} {employee.LastName} Hoşgeldiniz";
                this.Show();
            }
            else
            {
                MessageBox.Show("Bilgilernizi kontrol ediniz.");
            }
        }
    }
}
