namespace StockPlot
{
    partial class OrganizeFavorites
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
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelChkd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(12, 41);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(184, 214);
            this.checkedListBox1.TabIndex = 0;
            this.checkedListBox1.ThreeDCheckBoxes = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(12, 12);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelChkd
            // 
            this.btnDelChkd.Location = new System.Drawing.Point(93, 12);
            this.btnDelChkd.Name = "btnDelChkd";
            this.btnDelChkd.Size = new System.Drawing.Size(103, 23);
            this.btnDelChkd.TabIndex = 2;
            this.btnDelChkd.Text = "Delete Checked";
            this.btnDelChkd.UseVisualStyleBackColor = true;
            this.btnDelChkd.Click += new System.EventHandler(this.btnDelChkd_Click);
            // 
            // OrganizeFavorites
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(207, 264);
            this.Controls.Add(this.btnDelChkd);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.checkedListBox1);
            this.MaximizeBox = false;
            this.Name = "OrganizeFavorites";
            this.Text = "Organize";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelChkd;
    }
}