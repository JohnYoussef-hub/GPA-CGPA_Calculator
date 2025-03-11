namespace GPA_CGPA_Calculator
{
    partial class frmCourseInformation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCourseInformation));
            this.ctrlAddEditInfo1 = new GPA_CGPA_Calculator.User_Controls.ctrlAddEditInfo();
            this.SuspendLayout();
            // 
            // ctrlAddEditInfo1
            // 
            this.ctrlAddEditInfo1.CourseID = 0;
            this.ctrlAddEditInfo1.Location = new System.Drawing.Point(0, 0);
            this.ctrlAddEditInfo1.Name = "ctrlAddEditInfo1";
            this.ctrlAddEditInfo1.Size = new System.Drawing.Size(570, 440);
            this.ctrlAddEditInfo1.TabIndex = 0;
            // 
            // frmCourseInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 441);
            this.Controls.Add(this.ctrlAddEditInfo1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmCourseInformation";
            this.Text = "Course Information";
            this.ResumeLayout(false);

        }

        #endregion

        private User_Controls.ctrlAddEditInfo ctrlAddEditInfo1;
    }
}