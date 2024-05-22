using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace курсач
{
    public partial class UserOrdersForm : Form
    {
        private int userId;
        private SqlConnection connection = new SqlConnection("Data Source=ПК;Initial Catalog=wholesale_market;Integrated Security=True;");

        public UserOrdersForm(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            LoadUserOrders();
        }

        private void LoadUserOrders()
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT Order_ID, Order_Date, Status FROM Orders WHERE User_ID = @UserID", connection);
                cmd.Parameters.AddWithValue("@UserID", userId);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable ordersTable = new DataTable();
                adapter.Fill(ordersTable);
                dataGridViewUserOrders.DataSource = ordersTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
