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
    public partial class Orders : Form
    {
        public Orders()
        {
            InitializeComponent();
        }

        private void ordersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ordersBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.wholesale_marketDataSet);

        }

        private void Orders_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "wholesale_marketDataSet.ProductsOrders". При необходимости она может быть перемещена или удалена.
            this.productsOrdersTableAdapter.Fill(this.wholesale_marketDataSet.ProductsOrders);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "wholesale_marketDataSet.Orders". При необходимости она может быть перемещена или удалена.
            this.ordersTableAdapter.Fill(this.wholesale_marketDataSet.Orders);

        }
    }
}
