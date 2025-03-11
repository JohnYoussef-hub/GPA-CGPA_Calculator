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
    public partial class frmShowCourseInfo: Form
    {
        public frmShowCourseInfo(int courseID)
        {
            InitializeComponent();
            ctrlShowCourseInfo1.ShowCourseInfo(courseID);
        }
    }
}
