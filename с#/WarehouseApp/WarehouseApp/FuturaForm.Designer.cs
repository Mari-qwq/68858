namespace WarehouseApp
{
    partial class FuturaForm
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
            this.dataGridViewFutura = new System.Windows.Forms.DataGridView();
            this.dataGridViewInfo = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.OrdersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изменитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddOrderToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteOrderToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.UpdateOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddProductТоварToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteProductToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UpdateProductToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFutura)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInfo)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewFutura
            // 
            this.dataGridViewFutura.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFutura.Location = new System.Drawing.Point(137, 147);
            this.dataGridViewFutura.Name = "dataGridViewFutura";
            this.dataGridViewFutura.RowHeadersWidth = 51;
            this.dataGridViewFutura.RowTemplate.Height = 29;
            this.dataGridViewFutura.Size = new System.Drawing.Size(415, 303);
            this.dataGridViewFutura.TabIndex = 0;
            this.dataGridViewFutura.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewFutura_CellClick);
            this.dataGridViewFutura.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewFutura_CellContentClick);
            // 
            // dataGridViewInfo
            // 
            this.dataGridViewInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewInfo.Location = new System.Drawing.Point(705, 147);
            this.dataGridViewInfo.Name = "dataGridViewInfo";
            this.dataGridViewInfo.RowHeadersWidth = 51;
            this.dataGridViewInfo.RowTemplate.Height = 29;
            this.dataGridViewInfo.Size = new System.Drawing.Size(415, 303);
            this.dataGridViewInfo.TabIndex = 1;
            this.dataGridViewInfo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewInfo_CellContentClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OrdersToolStripMenuItem,
            this.изменитьToolStripMenuItem,
            this.ExitToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1252, 28);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // OrdersToolStripMenuItem
            // 
            this.OrdersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddOrderToolStripMenuItem1,
            this.DeleteOrderToolStripMenuItem1,
            this.UpdateOrderToolStripMenuItem});
            this.OrdersToolStripMenuItem.Name = "OrdersToolStripMenuItem";
            this.OrdersToolStripMenuItem.Size = new System.Drawing.Size(72, 24);
            this.OrdersToolStripMenuItem.Text = "Заказы";
            // 
            // изменитьToolStripMenuItem
            // 
            this.изменитьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddProductТоварToolStripMenuItem,
            this.DeleteProductToolStripMenuItem,
            this.UpdateProductToolStripMenuItem});
            this.изменитьToolStripMenuItem.Name = "изменитьToolStripMenuItem";
            this.изменитьToolStripMenuItem.Size = new System.Drawing.Size(137, 24);
            this.изменитьToolStripMenuItem.Text = "Товары в заказе";
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(67, 24);
            this.ExitToolStripMenuItem.Text = "Выйти";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // AddOrderToolStripMenuItem1
            // 
            this.AddOrderToolStripMenuItem1.Name = "AddOrderToolStripMenuItem1";
            this.AddOrderToolStripMenuItem1.Size = new System.Drawing.Size(224, 26);
            this.AddOrderToolStripMenuItem1.Text = "Добавить заказ";
            this.AddOrderToolStripMenuItem1.Click += new System.EventHandler(this.AddOrderToolStripMenuItem1_Click);
            // 
            // DeleteOrderToolStripMenuItem1
            // 
            this.DeleteOrderToolStripMenuItem1.Name = "DeleteOrderToolStripMenuItem1";
            this.DeleteOrderToolStripMenuItem1.Size = new System.Drawing.Size(224, 26);
            this.DeleteOrderToolStripMenuItem1.Text = "Удалить заказ";
            this.DeleteOrderToolStripMenuItem1.Click += new System.EventHandler(this.DeleteOrderToolStripMenuItem1_Click);
            // 
            // UpdateOrderToolStripMenuItem
            // 
            this.UpdateOrderToolStripMenuItem.Name = "UpdateOrderToolStripMenuItem";
            this.UpdateOrderToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.UpdateOrderToolStripMenuItem.Text = "Обновить";
            this.UpdateOrderToolStripMenuItem.Click += new System.EventHandler(this.UpdateOrderToolStripMenuItem_Click);
            // 
            // AddProductТоварToolStripMenuItem
            // 
            this.AddProductТоварToolStripMenuItem.Name = "AddProductТоварToolStripMenuItem";
            this.AddProductТоварToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.AddProductТоварToolStripMenuItem.Text = "Добавить товар";
            this.AddProductТоварToolStripMenuItem.Click += new System.EventHandler(this.AddProductТоварToolStripMenuItem_Click);
            // 
            // DeleteProductToolStripMenuItem
            // 
            this.DeleteProductToolStripMenuItem.Name = "DeleteProductToolStripMenuItem";
            this.DeleteProductToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.DeleteProductToolStripMenuItem.Text = "Удалить товар";
            this.DeleteProductToolStripMenuItem.Click += new System.EventHandler(this.DeleteProductToolStripMenuItem_Click);
            // 
            // UpdateProductToolStripMenuItem
            // 
            this.UpdateProductToolStripMenuItem.Name = "UpdateProductToolStripMenuItem";
            this.UpdateProductToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.UpdateProductToolStripMenuItem.Text = "Обновить";
            this.UpdateProductToolStripMenuItem.Click += new System.EventHandler(this.UpdateProductToolStripMenuItem_Click);
            // 
            // FuturaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1252, 609);
            this.Controls.Add(this.dataGridViewInfo);
            this.Controls.Add(this.dataGridViewFutura);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FuturaForm";
            this.Text = "FuturaForm";
            this.Load += new System.EventHandler(this.FuturaForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFutura)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInfo)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataGridView dataGridViewFutura;
        private DataGridView dataGridViewInfo;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem OrdersToolStripMenuItem;
        private ToolStripMenuItem AddOrderToolStripMenuItem1;
        private ToolStripMenuItem DeleteOrderToolStripMenuItem1;
        private ToolStripMenuItem UpdateOrderToolStripMenuItem;
        private ToolStripMenuItem изменитьToolStripMenuItem;
        private ToolStripMenuItem AddProductТоварToolStripMenuItem;
        private ToolStripMenuItem DeleteProductToolStripMenuItem;
        private ToolStripMenuItem UpdateProductToolStripMenuItem;
        private ToolStripMenuItem ExitToolStripMenuItem;
    }
}