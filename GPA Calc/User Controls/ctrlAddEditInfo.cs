using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;

namespace GPA_CGPA_Calculator.User_Controls
{
    public partial class ctrlAddEditInfo : UserControl
    {

        public Bunifu.UI.WinForms.BunifuTextBox TxtInstructorName { get => txtInstructorName; set => txtInstructorName = value; }
        public Bunifu.UI.WinForms.BunifuTextBox TxtCourseCode { get => txtCourseCode; set => txtCourseCode = value; }
        public Bunifu.UI.WinForms.BunifuTextBox TxtNotes { get => txtNotes; set => txtNotes = value; }


        public int CourseID { get; set; }

        public ctrlAddEditInfo()
        {
            InitializeComponent();
        }


        public void SetInfo()
        {
            clsCourseInfo info = new clsCourseInfo(CourseID);
            if (info != null)
            {
                txtInstructorName.Text = info.InstructorName;
                txtCourseCode.Text = info.CourseCode;
                txtNotes.Text = info.Notes;
            }
            else
            {
                txtInstructorName.Text = "";
                txtCourseCode.Text = "";
                txtNotes.Text = "";
            }
        }



        private void btnClose_Click(object sender, EventArgs e) => this.ParentForm.Close();

        private void btnSave_Click(object sender, EventArgs e)
        {
            string InstructorName = txtInstructorName.Text;
            string CourseCode = txtCourseCode.Text;
            string Notes = txtNotes.Text;

            if (!clsCourseInfo.HasCourseInfo(CourseID))
            {
                //if(string.IsNullOrEmpty(InstructorName) && string.IsNullOrEmpty(CourseCode) && string.IsNullOrEmpty(Notes))
                //{
                //    MessageBox.Show("Please fill at least on field!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}



                clsCourseInfo info = new clsCourseInfo()
                {
                    CourseID = CourseID,
                    InstructorName = InstructorName,
                    CourseCode = CourseCode,
                    Notes = Notes
                };

                try
                {
                    if (info.AddCourseInfo())
                    {
                        MessageBox.Show("Course Info Added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to Add Course Info", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                clsCourseInfo info = new clsCourseInfo(CourseID, InstructorName, CourseCode, Notes);
                try
                {
                    if (info.UpdateCourseInfo())
                    {
                        MessageBox.Show("Course Info Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to Update Course Info", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
