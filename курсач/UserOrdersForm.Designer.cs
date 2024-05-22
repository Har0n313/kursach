namespace курсач
{
    partial class UserOrdersForm
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
            this.dataGridViewUserOrders = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUserOrders)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewUserOrders
            // 
            this.dataGridViewUserOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewUserOrders.Location = new System.Drawing.Point(21, 32);
            this.dataGridViewUserOrders.Name = "dataGridViewUserOrders";
            this.dataGridViewUserOrders.RowHeadersWidth = 51;
            this.dataGridViewUserOrders.RowTemplate.Height = 24;
            this.dataGridViewUserOrders.Size = new System.Drawing.Size(516, 150);
            this.dataGridViewUserOrders.TabIndex = 0;
            // 
            // UserOrdersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 206);
            this.Controls.Add(this.dataGridViewUserOrders);
            this.Name = "UserOrdersForm";
            this.Text = "UserOrdersForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUserOrders)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewUserOrders;
    }
}