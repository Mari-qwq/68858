namespace WarehouseApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Clients = new System.Windows.Forms.Button();
            this.Products = new System.Windows.Forms.Button();
            this.Futura = new System.Windows.Forms.Button();
            this.Reports = new System.Windows.Forms.Button();
            this.buttonPrice = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Clients
            // 
            this.Clients.BackColor = System.Drawing.Color.Chocolate;
            this.Clients.Location = new System.Drawing.Point(88, 246);
            this.Clients.Name = "Clients";
            this.Clients.Size = new System.Drawing.Size(205, 72);
            this.Clients.TabIndex = 0;
            this.Clients.Text = "Клиенты";
            this.Clients.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.Clients.UseVisualStyleBackColor = false;
            this.Clients.Click += new System.EventHandler(this.Clients_Click);
            // 
            // Products
            // 
            this.Products.BackColor = System.Drawing.Color.Chocolate;
            this.Products.Location = new System.Drawing.Point(88, 344);
            this.Products.Name = "Products";
            this.Products.Size = new System.Drawing.Size(205, 72);
            this.Products.TabIndex = 1;
            this.Products.Text = "Товары";
            this.Products.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.Products.UseVisualStyleBackColor = false;
            this.Products.Click += new System.EventHandler(this.Products_Click);
            // 
            // Futura
            // 
            this.Futura.BackColor = System.Drawing.Color.Chocolate;
            this.Futura.Location = new System.Drawing.Point(848, 246);
            this.Futura.Name = "Futura";
            this.Futura.Size = new System.Drawing.Size(205, 72);
            this.Futura.TabIndex = 2;
            this.Futura.Text = "Заказы";
            this.Futura.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.Futura.UseVisualStyleBackColor = false;
            this.Futura.Click += new System.EventHandler(this.Futura_Click);
            // 
            // Reports
            // 
            this.Reports.BackColor = System.Drawing.Color.Chocolate;
            this.Reports.Location = new System.Drawing.Point(848, 344);
            this.Reports.Name = "Reports";
            this.Reports.Size = new System.Drawing.Size(205, 72);
            this.Reports.TabIndex = 3;
            this.Reports.Text = "Отчеты";
            this.Reports.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.Reports.UseVisualStyleBackColor = false;
            // 
            // buttonPrice
            // 
            this.buttonPrice.BackColor = System.Drawing.Color.Chocolate;
            this.buttonPrice.Location = new System.Drawing.Point(470, 486);
            this.buttonPrice.Name = "buttonPrice";
            this.buttonPrice.Size = new System.Drawing.Size(205, 72);
            this.buttonPrice.TabIndex = 4;
            this.buttonPrice.Text = "Прайс Лист";
            this.buttonPrice.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.buttonPrice.UseVisualStyleBackColor = false;
            this.buttonPrice.Click += new System.EventHandler(this.buttonPrice_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::WarehouseApp.Properties.Resources.warehouse_wallpaper;
            this.ClientSize = new System.Drawing.Size(1201, 691);
            this.Controls.Add(this.buttonPrice);
            this.Controls.Add(this.Reports);
            this.Controls.Add(this.Futura);
            this.Controls.Add(this.Products);
            this.Controls.Add(this.Clients);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Button Clients;
        private Button Products;
        private Button Futura;
        private Button Reports;
        private Button buttonPrice;
    }
}