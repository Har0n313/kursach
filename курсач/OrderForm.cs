using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace курсач
{
    public partial class OrderForm : Form
    {
        private int user_Id;
        private SqlConnection connection = new SqlConnection("Data Source=ПК;Initial Catalog=wholesale_market;Integrated Security=True;");

        public OrderForm(int Id)
        {
            InitializeComponent();
            LoadProducts();
            user_Id = Id;

            // Инициализация DataGridView и добавление столбцов
            dataGridViewOrders.Columns.Add("ProductID", "ID продукта");
            dataGridViewOrders.Columns.Add("ProductName", "Название");
            dataGridViewOrders.Columns.Add("Quantity", "Количество");
            dataGridViewOrders.Columns.Add("Price", "Цена");

            dataGridViewOrders.RowsRemoved += DataGridViewOrders_RowsRemoved;
        }

        private void DataGridViewOrders_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            UpdateTotalCost(); // Обновляем общую стоимость при удалении строки
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoadProducts()
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT Product_ID, Name, Price FROM Products", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int productId = reader.GetInt32(0);
                    string productName = reader.GetString(1);
                    decimal price = reader.GetDecimal(2);
                    comboBoxProducts.Items.Add(new ProductItem(productId, productName, price));
                }
                reader.Close();
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ProductItem selectedProduct = (ProductItem)comboBoxProducts.SelectedItem;
            int quantity = (int)numericUpDownQuantity.Value;
            decimal total = selectedProduct.Price * quantity;

            dataGridViewOrders.Rows.Add(selectedProduct.ProductId, selectedProduct.ProductName, quantity, selectedProduct.Price);

            UpdateTotalCost(); // Обновляем общую стоимость
            comboBoxProducts.Text = "";
        }

        

        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction(); // Начинаем транзакцию

                // Получение последнего Order_ID и вычисление следующего
                SqlCommand getLastOrderIdCmd = new SqlCommand("SELECT ISNULL(MAX(Order_ID), 0) FROM Orders", connection, transaction);
                int lastOrderId = Convert.ToInt32(getLastOrderIdCmd.ExecuteScalar());
                int nextOrderId = lastOrderId + 1;

                // Добавление заказа в таблицу Orders
                SqlCommand cmd = new SqlCommand("INSERT INTO Orders (Order_ID, Order_Date, User_ID, Status) VALUES (@OrderID, @OrderDate, @UserID, @Status)", connection, transaction);
                cmd.Parameters.AddWithValue("@OrderID", nextOrderId);
                cmd.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@UserID", user_Id); // Используем ID текущего пользователя
                cmd.Parameters.AddWithValue("@Status", "Pending");
                cmd.ExecuteNonQuery();

                // Добавление деталей заказа и обновление количества товара в таблице Products
                foreach (DataGridViewRow row in dataGridViewOrders.Rows)
                {
                    if (row.Cells["ProductID"].Value != null && row.Cells["Quantity"].Value != null)
                    {
                        int productId = Convert.ToInt32(row.Cells["ProductID"].Value);
                        int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);

                        // Добавление деталей заказа в таблицу ProductsOrders
                        SqlCommand cmdOrderDetails = new SqlCommand("INSERT INTO ProductsOrders (Order_ID, Product_ID, Quantity) VALUES (@OrderID, @ProductID, @Quantity)", connection, transaction);
                        cmdOrderDetails.Parameters.AddWithValue("@OrderID", nextOrderId);
                        cmdOrderDetails.Parameters.AddWithValue("@ProductID", productId);
                        cmdOrderDetails.Parameters.AddWithValue("@Quantity", quantity);
                        cmdOrderDetails.ExecuteNonQuery();

                        // Обновление количества товара в таблице Products
                        SqlCommand cmdUpdateProduct = new SqlCommand("UPDATE Products SET Amount = Amount - @Quantity WHERE Product_ID = @ProductID", connection, transaction);
                        cmdUpdateProduct.Parameters.AddWithValue("@Quantity", quantity);
                        cmdUpdateProduct.Parameters.AddWithValue("@ProductID", productId);
                        cmdUpdateProduct.ExecuteNonQuery();
                    }
                }

                transaction.Commit(); // Завершаем транзакцию

                MessageBox.Show("Заказ успешно создан!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridViewOrders.Rows.Clear();
                UpdateTotalCost(); // Обновляем общую стоимость после размещения заказа
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Удаление выбранной строки из таблицы заказов
            if (dataGridViewOrders.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridViewOrders.SelectedRows)
                {
                    dataGridViewOrders.Rows.Remove(row);
                }
            }
            UpdateTotalCost(); // Обновляем общую стоимость после удаления строки
        }

        private void UpdateTotalCost()
        {
            decimal totalCost = 0;
            foreach (DataGridViewRow row in dataGridViewOrders.Rows)
            {
                if (row.Cells["Quantity"].Value != null && row.Cells["Price"].Value != null)
                {
                    int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                    decimal price = Convert.ToDecimal(row.Cells["Price"].Value);
                    totalCost += quantity * price;
                }
            }
            labelTotalCost.Text = $"Общая стоимость: {totalCost:C2}";
        }

        private void productsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.productsBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.wholesale_marketDataSet);
        }

        private void productsBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {
            this.Validate();
            this.productsBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.wholesale_marketDataSet);
        }

        private void OrderForm_Load(object sender, EventArgs e)
        {
            this.productsTableAdapter.Fill(this.wholesale_marketDataSet.Products);
        }

        private void btnViewOrders_Click(object sender, EventArgs e)
        {
            UserOrdersForm userOrdersForm = new UserOrdersForm(user_Id);
            userOrdersForm.Show();
        }
    }

    public class ProductItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }

        public ProductItem(int id, string name, decimal price)
        {
            ProductId = id;
            ProductName = name;
            Price = price;
        }

        public override string ToString()
        {
            return ProductName;
        }
    }
}
