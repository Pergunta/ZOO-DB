namespace ZOO_db
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.confToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.veterinarianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadEmployeesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.confToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(908, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // confToolStripMenuItem
            // 
            this.confToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.veterinarianToolStripMenuItem,
            this.loadEmployeesToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.confToolStripMenuItem.Name = "confToolStripMenuItem";
            this.confToolStripMenuItem.Size = new System.Drawing.Size(63, 24);
            this.confToolStripMenuItem.Text = "MENU";
            // 
            // veterinarianToolStripMenuItem
            // 
            this.veterinarianToolStripMenuItem.Name = "veterinarianToolStripMenuItem";
            this.veterinarianToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.veterinarianToolStripMenuItem.Text = "Veterinarian";
            this.veterinarianToolStripMenuItem.Click += new System.EventHandler(this.veterinarianToolStripMenuItem_Click);
            // 
            // loadEmployeesToolStripMenuItem
            // 
            this.loadEmployeesToolStripMenuItem.Name = "loadEmployeesToolStripMenuItem";
            this.loadEmployeesToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.loadEmployeesToolStripMenuItem.Text = "Employee";
            this.loadEmployeesToolStripMenuItem.Click += new System.EventHandler(this.loadEmployeesToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(243, 267);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Toronto in 6";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem confToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadEmployeesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem veterinarianToolStripMenuItem;
    }
}

