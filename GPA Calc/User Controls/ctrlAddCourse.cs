using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPA_Calc.User_Controls
{
    public partial class ctrlAddCourse : UserControl
    {
        int CourseID;
        public int Sem;

        public ctrlAddCourse()
        {
            InitializeComponent();
        }

        public Bunifu.UI.WinForms.BunifuDropdown DrpSemester
        {
            get { return drpSemester; }
            set { drpSemester = value; }
        }

        public Bunifu.UI.WinForms.BunifuDropdown DrpHours
        {
            get { return drpHours; }
        }

        public Bunifu.UI.WinForms.BunifuDropdown DrpGrade
        {
            get { return drpGrade; }
        }

        public Bunifu.UI.WinForms.BunifuTextBox TxtCourseName
        {
            get { return txtCourseName; }
        }

        public void FillForm(int id ,string CrsName, int CrsHours, string CrsGrade, int CrsSem)
        {
            CourseID = id;
            txtCourseName.Text = CrsName;
            txtCourseName.SelectionStart = txtCourseName.Text.Length;
            txtCourseName.Focus();


            drpHours.SelectedIndex = CrsHours;
            drpGrade.SelectedIndex = drpGrade.Items.IndexOf(CrsGrade);
            drpSemester.SelectedIndex = CrsSem - 1;

            btnAdd.Text = "Update";
            lblMain.Text = "Update Course";

            
        }


        private void ctrlAddCourse_Load(object sender, EventArgs e)
        {
            this.ParentForm.KeyPreview = true;
            this.ParentForm.KeyDown += ctrlAddCourse_KeyDown;

            if (btnAdd.Text != "Update")
            {
                drpGrade.SelectedIndex = 0;
                drpHours.SelectedIndex = 1;
                drpSemester.SelectedIndex = Sem;
            }
            else return;
        }

        private void ctrlAddCourse_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAdd.PerformClick();
                e.SuppressKeyPress = true;
            }
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCourseName.Text))
            {
                MessageBox.Show("Please enter a course name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            else if(btnAdd.Text == "Update")
            {
                int sem = Convert.ToInt32(drpSemester.SelectedItem);
                string CourseName = txtCourseName.Text;
                int CreditHours = Convert.ToInt32(drpHours.SelectedItem);
                string Grade = drpGrade.SelectedItem.ToString();

                clsApp.UpdateCourse(CourseID, CourseName, CreditHours, Grade, sem);
                
                frmAddCourse parentForm = this.ParentForm as frmAddCourse;
                parentForm?.OnDataBack(sem);
                this.ParentForm.Close();


                txtCourseName.Text = "";
                drpGrade.SelectedIndex = 0;
                drpHours.SelectedIndex = 0;
                drpSemester.SelectedIndex = 0;

                this.ParentForm.DialogResult = DialogResult.OK;
            }

            else
            {
                BusinessLayer.clsApp.AddCourse(txtCourseName.Text, Convert.ToInt32(drpHours.SelectedItem), drpGrade.SelectedItem.ToString(), Convert.ToInt32(drpSemester.SelectedItem));

                int sem = Convert.ToInt32(drpSemester.SelectedItem);
                frmAddCourse parentForm = this.ParentForm as frmAddCourse;
                parentForm?.OnDataBack(sem);
                this.ParentForm.Close();

                txtCourseName.Text = "";
                drpGrade.SelectedIndex = 0;
                drpHours.SelectedIndex = 0;
                drpSemester.SelectedIndex = 0;
            }
        }

        
    }
}
