using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPA_CGPA_Calculator
{
    public partial class frmCourseInformation: Form
    {
        public frmCourseInformation(int CourseID)
        {
            InitializeComponent();
            ctrlAddEditInfo1.CourseID = CourseID;
            ctrlAddEditInfo1.SetInfo();
        }

    }
}
