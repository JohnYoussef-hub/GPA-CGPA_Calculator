namespace GPA_Calc
{
    partial class frmAddCourse
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddCourse));
            this.ctrlAddCourse1 = new GPA_Calc.User_Controls.ctrlAddCourse();
            this.SuspendLayout();
            // 
            // ctrlAddCourse1
            // 
            this.ctrlAddCourse1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlAddCourse1.Location = new System.Drawing.Point(0, 0);
            this.ctrlAddCourse1.Name = "ctrlAddCourse1";
            this.ctrlAddCourse1.Size = new System.Drawing.Size(847, 345);
            this.ctrlAddCourse1.TabIndex = 0;
            // 
            // frmAddCourse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 345);
            this.Controls.Add(this.ctrlAddCourse1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmAddCourse";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Course";
            this.ResumeLayout(false);

        }

        #endregion

        private User_Controls.ctrlAddCourse ctrlAddCourse1;
    }
}