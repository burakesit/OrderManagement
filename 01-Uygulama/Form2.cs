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
    public partial class Form2 : Form
    {
        public Form2(int employeeID)
        {
            InitializeComponent();
            this.employeeID = employeeID;
            
        }
        int employeeID;
        NorthwindEntities db = new NorthwindEntities();
        private void Form2_Load(object sender, EventArgs e)
        {
            ListOrders();

        }

        private void ListOrders()
        {           
            List<Order> orders = db.Orders.Where(a => a.EmployeeID == employeeID).OrderByDescending(a => a.OrderDate).ToList();
            ListViewItem lvi;
            lstOrders.Items.Clear();
            foreach (var item in orders)
            {
                lvi = new ListViewItem(item.OrderID.ToString());
                lvi.SubItems.Add(item.Customer.CompanyName);
                lvi.SubItems.Add(item.OrderDate.Value.ToShortDateString());
                lvi.Tag = item;
                lstOrders.Items.Add(lvi);
            }
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //1.Yöntem Sorgu Yazılarak
            /*int orderId = Convert.ToInt32(lstOrders.SelectedItems[0].Text);
            NorthwindEntities db = new NorthwindEntities();
            db.Orders.Remove(db.Orders.Where(a => a.OrderID == orderId).Single());
            */
            //2.Yöntem Sorgu Yazılmadan
            Order order = (Order)lstOrders.SelectedItems[0].Tag; // (Order) yazılınca Cast işlemi yapılmış oldu.           
            //db.Order_Details.RemoveRange(db.Order_Details.Where(a => a.OrderID == order.OrderID).ToList()); // OrderID koşuluna ait birden fazla satır olabileceği için RemoveRange metodu kullanılır.
            //db.SaveChanges();

            order.Order_Details.Clear();//Bu komut 
            db.Orders.Remove(order);
            db.SaveChanges();
            ListOrders();

            #region Yanlış Denemeler
            //ListViewItem lvi = new ListViewItem();
            //Order order1 = new Order();

            //Order order = lstOrders.SelectedItems[0].Tag as Order;

            //lvi = lstOrders.SelectedItems[0].SubItems[1].Tag as ListViewItem;

            //MessageBox.Show(lvi.Text.ToString());
            ////Order selectedOrder = lvi.

            //db.Orders.Remove(selectedOrder);


            //foreach (ListViewItem order in lstOrders.SelectedItems)
            //{
            //    order.Remove();

            //}

            //MessageBox.Show("Sipariş silinmiştir."); 
            #endregion
        }

        private void btnCreateOrder_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(employeeID);
            form3.ShowDialog();
        }
    }
}
