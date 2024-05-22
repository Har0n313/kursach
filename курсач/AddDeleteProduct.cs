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
    public partial class AddDeleteProduct : Form
    {
        public AddDeleteProduct()
        {
            InitializeComponent();
            AddDeleteButtonColumn();
        }

        private void productsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.productsBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.wholesale_marketDataSet);
            
        }
        private void AddDeleteButtonColumn()
        {
            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();

            deleteButtonColumn.HeaderText = "Удалить";
            deleteButtonColumn.Text = "Удалить";

            deleteButtonColumn.Name = "DeleteButtonColumn";

            deleteButtonColumn.UseColumnTextForButtonValue = true;

            productsDataGridView.Columns.Add(deleteButtonColumn);
        }
        private void AddDeleteProduct_Load(object sender, EventArgs e)
        {
            this.productsTableAdapter.Fill(this.wholesale_marketDataSet.Products);

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (productsDataGridView.Columns[e.ColumnIndex].Name == "DeleteButtonColumn" && e.RowIndex >= 0)
            {
                int productId = Convert.ToInt32(productsDataGridView.Rows[e.RowIndex].Cells["ProductID"].Value);

                DeleteProduct(productId);

                productsDataGridView.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void DeleteProduct(int productId)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=ПК;Initial Catalog=wholesale_market;Integrated Security=True;"))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Products WHERE Product_ID = @ProductId", connection);
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при удалении продукта из базы данных: " + ex.Message);
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private bool IsIdUnique(int productId)
        {
            string connectionString = "Data Source=ПК;Initial Catalog=wholesale_market;Integrated Security=True;"; // Замените на вашу строку подключения

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Products WHERE Product_ID = @ProductId", connection);
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count == 0; 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при проверке уникальности Id: " + ex.Message);
                    return false;
                }
            }
        }
        private void AddProductToDatabase(int productId, string productName, decimal price, int amount)
        {
            if (!IsIdUnique(productId))
            {
                MessageBox.Show("Id уже существует. Пожалуйста, выберите другой Id.");
                return;
            }

            string connectionString = "Data Source=ПК;Initial Catalog=wholesale_market;Integrated Security=True;"; 
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Products (Product_ID, Name, Price, Amount) VALUES (@ProductId, @ProductName, @Price, @Amount)", connection);
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    cmd.Parameters.AddWithValue("@ProductName", productName);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@Amount", amount);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Продукт успешно добавлен в базу данных.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при добавлении продукта в базу данных: " + ex.Message);
                }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            int Id = int.Parse(textBoxId.Text);
            string Name = textBoxName.Text;
            decimal Prise = numericUpDownPrice.Value;
            int Amount = (int)numericUpDownAmount.Value;
            AddProductToDatabase(Id, Name, Prise, Amount);
        }
    }
}
