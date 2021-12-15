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
    public partial class Form3 : Form
    {
        public Form3(int employeeID)
        {
            InitializeComponent();
            this.employeeID = employeeID;
            db = new NorthwindEntities();
        }
        int employeeID;
        NorthwindEntities db;

        private void Form3_Load(object sender, EventArgs e)
        {
            cbCustomer.DataSource = db.Customers.ToList();
            cbCustomer.DisplayMember = "CompanyName";
            cbCustomer.ValueMember = "CustomerID";

            cbProductName.DataSource = db.Products.ToList();
            cbProductName.DisplayMember = "ProductName";
            cbProductName.ValueMember = "ProductID";

            cbCategories.DisplayMember = "CategoryName";
            cbCategories.ValueMember = "CategoryID";
            cbCategories.DataSource = db.Categories.ToList();
            cbCategories.SelectedIndex = -1;

        }

        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            string customerID = Convert.ToString(cbCustomer.SelectedValue);
            int productID = Convert.ToInt32(cbProductName.SelectedValue);
            decimal unitPrice = nudPrice.Value;
            short quantity = Convert.ToInt16(nudQuantity.Value);
            float discount = Convert.ToSingle(nudDiscount.Value);

            /*
              Burada iki yöntem vardır. Order oluştururken bir OrderID ye ulaşmalı ve bu OrderID yi Order_Detail Tablosunda kullanacaksın. YA DA iki tabloya birden bir kayıt atılacak.
             */
            Order order = new Order();
            order.CustomerID = customerID;
            order.EmployeeID = employeeID;
            order.OrderDate = DateTime.Now;

            Order_Detail orderDetail = new Order_Detail();
            orderDetail.ProductID = productID;
            orderDetail.UnitPrice = unitPrice;
            orderDetail.Quantity = quantity;
            orderDetail.Discount = discount;
            
            /* Yukarıdaki tanımlama şeklinin daha kısa halidir. Böyle yazılırsa daha iyi olur.
            Order order = new Order()
            {
                CustomerID = customerID,
                EmployeeID = employeeID,
                OrderDate = DateTime.Now
            };
            order.Order_Details.Add(new Order_Detail()
            {
                ProductID = productID,
                UnitPrice = unitPrice,
                Quantity = quantity,
                Discount = discount
            });
            */


            order.Order_Details.Add(orderDetail);
            db.Orders.Add(order);
            bool kontrol = db.SaveChanges() > 0;
            if (kontrol)
            {
                MessageBox.Show("Kayıt Başarılı");
            }
        }

        private void cbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCategories.SelectedIndex < 0)
            {
                return;
            }
            //cbProductName.Items.Clear();
            int categoryID = Convert.ToInt32(cbCategories.SelectedValue);
            cbProductName.DataSource = db.Products.Where(a=>a.CategoryID == categoryID).ToList();
            cbProductName.DisplayMember = "ProductName";
            cbProductName.ValueMember = "ProductID";
        }
    }
}
