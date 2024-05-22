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
    public partial class LoginUser : Form
    {
        private SqlConnection connection = new SqlConnection("Data Source=ПК;Initial Catalog=wholesale_market;Integrated Security=True;");

        public LoginUser()
        {
            InitializeComponent();
            passwordTextBox.PasswordChar = '*';
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string email = emailTextBox.Text;
            string password = passwordTextBox.Text;

            if (CheckCredentials(email, password))
            {
                int userId = GetUserId(email, password);
                if (userId != -1)
                {
                    if (email == "1")
                    {
                        AdminForm adminForm = new AdminForm();
                        adminForm.Show();
                    }
                    else
                    {
                        OrderForm orderForm = new OrderForm(userId);
                        orderForm.Show();
                    }
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Неверный email или пароль. Пожалуйста, попробуйте снова.");
                }
            }
            else
            {
                MessageBox.Show("Неверный email или пароль. Пожалуйста, попробуйте снова.");
            }
        }
        private int GetUserId(string email, string password)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT User_ID FROM Users WHERE Email = @Email AND Password = @Password", connection);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                object result = cmd.ExecuteScalar();
                return (result != null) ? Convert.ToInt32(result) : -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
                return -1;
            }
            finally
            {
                connection.Close();
            }
        }
        private bool CheckCredentials(string email, string password)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Email = @Email AND Password = @Password", connection);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
