namespace Product_Shoes
{
    partial class Order
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Order));
            this.txtSearchOrder = new System.Windows.Forms.TextBox();
            this.SearchOrder = new System.Windows.Forms.Button();
            this.txtResultOrder = new System.Windows.Forms.TextBox();
            this.dataGridViewOrderDetail = new System.Windows.Forms.DataGridView();
            this.dataGridViewOrder = new System.Windows.Forms.DataGridView();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrderDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrder)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSearchOrder
            // 
            this.txtSearchOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtSearchOrder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearchOrder.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchOrder.Location = new System.Drawing.Point(679, 3);
            this.txtSearchOrder.Multiline = true;
            this.txtSearchOrder.Name = "txtSearchOrder";
            this.txtSearchOrder.Size = new System.Drawing.Size(337, 46);
            this.txtSearchOrder.TabIndex = 8;
            // 
            // SearchOrder
            // 
            this.SearchOrder.BackColor = System.Drawing.Color.Black;
            this.SearchOrder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SearchOrder.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SearchOrder.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchOrder.ForeColor = System.Drawing.Color.White;
            this.SearchOrder.Location = new System.Drawing.Point(1022, 3);
            this.SearchOrder.Name = "SearchOrder";
            this.SearchOrder.Size = new System.Drawing.Size(94, 46);
            this.SearchOrder.TabIndex = 7;
            this.SearchOrder.Text = "Search";
            this.SearchOrder.UseVisualStyleBackColor = false;
            this.SearchOrder.Click += new System.EventHandler(this.SearchOrder_Click);
            // 
            // txtResultOrder
            // 
            this.txtResultOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtResultOrder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtResultOrder.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResultOrder.Location = new System.Drawing.Point(679, 55);
            this.txtResultOrder.Multiline = true;
            this.txtResultOrder.Name = "txtResultOrder";
            this.txtResultOrder.Size = new System.Drawing.Size(437, 567);
            this.txtResultOrder.TabIndex = 6;
            this.txtResultOrder.TextChanged += new System.EventHandler(this.txtResultOrder_TextChanged);
            // 
            // dataGridViewOrderDetail
            // 
            this.dataGridViewOrderDetail.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridViewOrderDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOrderDetail.Location = new System.Drawing.Point(2, 351);
            this.dataGridViewOrderDetail.Name = "dataGridViewOrderDetail";
            this.dataGridViewOrderDetail.ReadOnly = true;
            this.dataGridViewOrderDetail.RowHeadersWidth = 51;
            this.dataGridViewOrderDetail.RowTemplate.Height = 24;
            this.dataGridViewOrderDetail.Size = new System.Drawing.Size(671, 342);
            this.dataGridViewOrderDetail.TabIndex = 4;
            // 
            // dataGridViewOrder
            // 
            this.dataGridViewOrder.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridViewOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOrder.Location = new System.Drawing.Point(2, 3);
            this.dataGridViewOrder.Name = "dataGridViewOrder";
            this.dataGridViewOrder.ReadOnly = true;
            this.dataGridViewOrder.RowHeadersWidth = 51;
            this.dataGridViewOrder.RowTemplate.Height = 24;
            this.dataGridViewOrder.Size = new System.Drawing.Size(671, 342);
            this.dataGridViewOrder.TabIndex = 5;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.White;
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(902, 637);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(214, 56);
            this.btnExit.TabIndex = 12;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.Black;
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBack.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(679, 637);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(217, 56);
            this.btnBack.TabIndex = 13;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // Order
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1131, 703);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.txtSearchOrder);
            this.Controls.Add(this.SearchOrder);
            this.Controls.Add(this.txtResultOrder);
            this.Controls.Add(this.dataGridViewOrderDetail);
            this.Controls.Add(this.dataGridViewOrder);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Order";
            this.Text = "Order";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrderDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrder)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSearchOrder;
        private System.Windows.Forms.Button SearchOrder;
        private System.Windows.Forms.TextBox txtResultOrder;
        private System.Windows.Forms.DataGridView dataGridViewOrderDetail;
        private System.Windows.Forms.DataGridView dataGridViewOrder;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnBack;
    }
}