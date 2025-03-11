using System;
using System.Windows.Forms;
using BusinessLayer;

namespace GPA_CGPA_Calculator.User_Controls
{
    public partial class ctrlShowCourseInfo: UserControl
    {
        public ctrlShowCourseInfo()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e) => this.ParentForm.Close();



        public void ShowCourseInfo(int courseID)
        {
            string courseName = string.Empty;
            string instructorName = string.Empty;
            string courseCode = string.Empty;
            int semester = -1;
            int hours = -1;
            string grade = string.Empty;
            string notes = string.Empty;

            try
            {
                bool result = clsCourseInfo.GetFullCourseInfoByCourseID(courseID, ref courseName, ref instructorName, ref courseCode, ref semester, ref hours, ref grade, ref notes);

                if (result)
                {
                    lblCourseName.Text = string.IsNullOrEmpty(lblCourseName.Text) ? "[not set]" : courseName;
                    lblName.Text = string.IsNullOrEmpty(instructorName) ? "[not set]" : instructorName;
                    lblCourseCode.Text = string.IsNullOrEmpty(courseCode) ? "[not set]" : courseCode;
                    lblSemester.Text = semester == -1 ? "[not set]" : semester.ToString();
                    lblHours.Text = hours == -1 ? "[not set]" : hours.ToString();
                    lblGrade.Text = string.IsNullOrEmpty(grade) ? "[not set]" : grade;
                    txtNotes.Text = string.IsNullOrEmpty(notes) ? "[not set]" : notes;
                }
            }
            catch
            {
                MessageBox.Show("An error occurred while trying to retrieve course information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        }
}
