﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace курсач
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }

        private void current_orders_Click(object sender, EventArgs e)
        {
            CurentOrder curentOrder = new CurentOrder();
            curentOrder.Show();
        }

        private void Order_List_Click(object sender, EventArgs e)
        {
            Users users = new Users();
            users.Show();
        }

        private void AddProduct_Click(object sender, EventArgs e)
        {
            AddDeleteProduct addDeleteProduct = new AddDeleteProduct();
            addDeleteProduct.Show();
        }
    }
}
