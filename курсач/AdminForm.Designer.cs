namespace курсач
{
    partial class AdminForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.AddProduct = new System.Windows.Forms.Button();
            this.current_orders = new System.Windows.Forms.Button();
            this.User_List = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AddProduct
            // 
            this.AddProduct.Location = new System.Drawing.Point(447, 40);
            this.AddProduct.Name = "AddProduct";
            this.AddProduct.Size = new System.Drawing.Size(164, 68);
            this.AddProduct.TabIndex = 0;
            this.AddProduct.Text = "Добавить/Удалить продукт ";
            this.AddProduct.UseVisualStyleBackColor = true;
            this.AddProduct.Click += new System.EventHandler(this.AddProduct_Click);
            // 
            // current_orders
            // 
            this.current_orders.Location = new System.Drawing.Point(234, 40);
            this.current_orders.Name = "current_orders";
            this.current_orders.Size = new System.Drawing.Size(164, 68);
            this.current_orders.TabIndex = 1;
            this.current_orders.Text = "Текущие заказы";
            this.current_orders.UseVisualStyleBackColor = true;
            this.current_orders.Click += new System.EventHandler(this.current_orders_Click);
            // 
            // User_List
            // 
            this.User_List.Location = new System.Drawing.Point(28, 40);
            this.User_List.Name = "User_List";
            this.User_List.Size = new System.Drawing.Size(164, 68);
            this.User_List.TabIndex = 2;
            this.User_List.Text = "Посмотреть список пользователей";
            this.User_List.UseVisualStyleBackColor = true;
            this.User_List.Click += new System.EventHandler(this.Order_List_Click);
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 204);
            this.Controls.Add(this.User_List);
            this.Controls.Add(this.current_orders);
            this.Controls.Add(this.AddProduct);
            this.Name = "AdminForm";
            this.Text = "AdminForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button AddProduct;
        private System.Windows.Forms.Button current_orders;
        private System.Windows.Forms.Button User_List;
    }
}