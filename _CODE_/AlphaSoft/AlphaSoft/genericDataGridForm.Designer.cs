namespace AlphaSoft
{
    partial class genericDataGridForm
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
            this.detailDataGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.detailDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // detailDataGrid
            // 
            this.detailDataGrid.AllowUserToAddRows = false;
            this.detailDataGrid.AllowUserToDeleteRows = false;
            this.detailDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.detailDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.detailDataGrid.Location = new System.Drawing.Point(0, 1);
            this.detailDataGrid.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.detailDataGrid.Name = "detailDataGrid";
            this.detailDataGrid.ReadOnly = true;
            this.detailDataGrid.RowHeadersVisible = false;
            this.detailDataGrid.Size = new System.Drawing.Size(740, 588);
            this.detailDataGrid.TabIndex = 51;
            this.detailDataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.jobDetailDataGrid_CellContentClick);
            this.detailDataGrid.DoubleClick += new System.EventHandler(this.detailDataGrid_DoubleClick);
            // 
            // genericDataGridForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 588);
            this.Controls.Add(this.detailDataGrid);
            this.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "genericDataGridForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "genericDataGridForm";
            this.Load += new System.EventHandler(this.genericDataGridForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.detailDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView detailDataGrid;
    }
}