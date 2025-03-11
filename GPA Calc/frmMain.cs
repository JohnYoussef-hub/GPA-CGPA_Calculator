using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Bunifu.UI.WinForms.BunifuButton;
using BusinessLayer;
using GPA_CGPA_Calculator;

namespace GPA_Calc
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            RefreshData(1);
            setGPAProg(1);
            setCGPAProg();
        }

        private BunifuButton lastClickedButton = null;

        private void RefreshData(BunifuButton btn)
        {
            dgv1.DataSource = BusinessLayer.clsApp.GetCoursesBySemester(Convert.ToInt32(btn.Tag));
            dgv1.Columns["CourseID"].Visible = false;
        }

        private void RefreshData(int sem)
        {
            dgv1.DataSource = BusinessLayer.clsApp.GetCoursesBySemester(sem);
            dgv1.Columns["CourseID"].Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void setGPAProg(BunifuButton btn)
        {
            double GPA = BusinessLayer.clsApp.GetGPA(Convert.ToInt32(btn.Tag));
            int progress = (int)((GPA / 4) * 100);
            crclGPA.ValueByTransition = progress;

            lblGPA.Text = GPA.ToString("F3");
        }

        public void setGPAProg(int sem)
        {
            double GPA = BusinessLayer.clsApp.GetGPA(Convert.ToInt32(sem));
            int progress = (int)((GPA / 4) * 100);
            crclGPA.ValueByTransition = progress;

            lblGPA.Text = GPA.ToString("F3");
        }

        public void setCGPAProg()
        {
            double CGPA = BusinessLayer.clsApp.GetCGPA();
            int progress = (int)((CGPA / 4) * 100);
            crclCGPA.ValueByTransition = progress;

            lblCGPA.Text = CGPA.ToString("F4");
        }

        private void btnSemester_Click(object sender, EventArgs e)
        {
            BunifuButton ClickedButton = (BunifuButton)sender;
            HandleButtonPress(ClickedButton);
        }

        private void HandleButtonPress(BunifuButton ClickedButton)
        {
            lastClickedButton = ClickedButton;

            int semester = Convert.ToInt32(ClickedButton.Tag);
            RefreshData(semester);

            lblSemesterName.Text = ClickedButton.Text;
            lblCoursesCount.Text = dgv1.Rows.Count.ToString();
            lblTotalHours.Text = dgv1.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToInt32(t.Cells["Hours"].Value)).ToString();

            setGPAProg(ClickedButton);
            setCGPAProg();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddCourse frmAddCourse = new frmAddCourse(Convert.ToInt32(lastClickedButton.Tag));

            //frmAddCourse.DataBack += frmAddCourse_DataBack;

            frmAddCourse.ShowDialog();

            if (lastClickedButton != null)
            {
                RefreshData(lastClickedButton);
                setGPAProg(lastClickedButton);
                setCGPAProg();
            }
        }

        //private void frmAddCourse_DataBack(object sender, int sem)
        //{
        //    RefreshData(sem);
        //    setGPAProg(sem);
        //    setCGPAProg();

        //    if (lastClickedButton != null) frmMain_Load(lastClickedButton, null);
        //}


        private void deleteCourseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgv1.SelectedRows.Count > 0)
            {
                int courseID = Convert.ToInt32(dgv1.SelectedRows[0].Cells["CourseID"].Value);

                DialogResult res = MessageBox.Show("Are you sure you want to delete this course?", "Delete Course", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    int sem = clsApp.GetSemesterByID(courseID);

                    if (clsApp.DeleteCourse(courseID))
                    {
                        BunifuButton btn = (BunifuButton)Controls.Find("btn" + sem, true)[0];
                        RefreshData(btn);
                        setGPAProg(btn);
                        setCGPAProg();

                        lblSemesterName.Text = btn.Text;
                    }
                    else
                    {
                        MessageBox.Show("Course not deleted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a course to delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void editCourseToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (dgv1.SelectedRows.Count > 0)
            {
                int courseID = Convert.ToInt32(dgv1.SelectedRows[0].Cells["CourseID"].Value);
                string courseName = dgv1.SelectedRows[0].Cells["CourseName"].Value.ToString();
                int hours = Convert.ToInt32(dgv1.SelectedRows[0].Cells["Hours"].Value);
                string grade = dgv1.SelectedRows[0].Cells["Grade"].Value.ToString();
                int sem = Convert.ToInt32(clsApp.GetSemesterByID(courseID));

                frmAddCourse frmAddCourse = new frmAddCourse(courseID, courseName, hours, grade, sem);
                frmAddCourse.ShowDialog();

                if (lastClickedButton != null)
                {
                    RefreshData(lastClickedButton);
                    setGPAProg(lastClickedButton);
                    setCGPAProg();
                }
            }
            else
            {
                MessageBox.Show("Please select a course to edit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgv1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            lblCoursesCount.Text = dgv1.Rows.Count.ToString();
            lblTotalHours.Text = dgv1.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToInt32(t.Cells["Hours"].Value)).ToString();
        }

        private void dgv1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            lblCoursesCount.Text = dgv1.Rows.Count.ToString();
            lblTotalHours.Text = dgv1.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToInt32(t.Cells["Hours"].Value)).ToString();
        }

        

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;

            HandleButtonPress(btn1);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            this.KeyPreview = true;
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.F2) || (e.Control && e.Shift && e.KeyCode == Keys.A))
            {
                editCourseToolStripMenuItem_Click(null, EventArgs.Empty);
                e.SuppressKeyPress = true; 
            }
            else if (e.KeyCode == Keys.Delete)
            {
                deleteCourseToolStripMenuItem_Click(null, EventArgs.Empty);
                e.SuppressKeyPress = true;
            }
            else if(e.Control && e.KeyCode == Keys.A)
            {
                btnAdd_Click(null, EventArgs.Empty);
                e.SuppressKeyPress = true;
            }
        }

        private void dgv1_DoubleClick(object sender, EventArgs e)
        {
            editCourseToolStripMenuItem_Click(sender, e);
        }

        private void addCourseInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(dgv1.SelectedRows.Count > 0)
            {
                int CourseID = Convert.ToInt32(dgv1.SelectedRows[0].Cells["CourseID"].Value);
                frmCourseInformation frm = new frmCourseInformation(CourseID);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a course to view information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(dgv1.SelectedRows.Count > 0)
            {
                int CourseID = Convert.ToInt32(dgv1.SelectedRows[0].Cells["CourseID"].Value);
                frmShowCourseInfo frm = new frmShowCourseInfo(CourseID);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a course to view information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}