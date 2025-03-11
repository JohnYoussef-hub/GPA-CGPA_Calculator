namespace GPA_CGPA_Calculator
{
    partial class frmShowCourseInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmShowCourseInfo));
            this.ctrlShowCourseInfo1 = new GPA_CGPA_Calculator.User_Controls.ctrlShowCourseInfo();
            this.SuspendLayout();
            // 
            // ctrlShowCourseInfo1
            // 
            this.ctrlShowCourseInfo1.Location = new System.Drawing.Point(0, 0);
            this.ctrlShowCourseInfo1.Name = "ctrlShowCourseInfo1";
            this.ctrlShowCourseInfo1.Size = new System.Drawing.Size(630, 528);
            this.ctrlShowCourseInfo1.TabIndex = 0;
            // 
            // frmShowCourseInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 528);
            this.Controls.Add(this.ctrlShowCourseInfo1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmShowCourseInfo";
            this.Text = "Course Information";
            this.ResumeLayout(false);

        }

        #endregion

        private User_Controls.ctrlShowCourseInfo ctrlShowCourseInfo1;
    }
}