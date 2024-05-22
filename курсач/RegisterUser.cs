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
using курсач.wholesale_marketDataSetTableAdapters;

namespace курсач
{
    public partial class RegisterUser : Form
    {
        private string connectionString = "Data Source=ПК;Initial Catalog=wholesale_market;Integrated Security=True;";
        private wholesale_marketDataSet _dataSet;
        private UsersTableAdapter _usersAdapter;
        

        public RegisterUser()
        {
            _dataSet = new wholesale_marketDataSet();
            _usersAdapter = new UsersTableAdapter();
            InitializeComponent();
            passwordTextBox.PasswordChar = '*';
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private int GetLastUserId()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT MAX(User_ID) FROM Users";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                object result = command.ExecuteScalar();
                connection.Close();

                if (result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    throw new InvalidOperationException("Нет пользователей в базе данных.");
                }
            }
        }
        private bool IsEmailUnique(string email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);

                connection.Open();
                int count = (int)command.ExecuteScalar();
                connection.Close();

                return count == 0;
            }
        }



        private void RegisterButton_Click(object sender, EventArgs e)
        {
            string name = nameTextBox.Text;
            string email = emailTextBox.Text;
            string password = passwordTextBox.Text;

            try
            {
                int newUserId = RegistersUser(name, email, password);
                MessageBox.Show($"Региcтрация успешна!");
                this.Close();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private int RegistersUser(string name, string email, string password)
        {
            if (!IsEmailUnique(email))
            {
                throw new InvalidOperationException("Email уже иcпользуется. Пожалуйста, используйте другой email.");
            }

            int Id = GetLastUserId() + 1;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Users (User_ID, Name, Email, Password) VALUES (@User_ID, @Name, @Email, @Password);";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@User_ID", Id);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);

                connection.Open();
                object result = command.ExecuteScalar();
                connection.Close();

                if (result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    throw new InvalidOperationException("Ошибка при регистрации пользователя.");
                }
            }
        }

    }
}
