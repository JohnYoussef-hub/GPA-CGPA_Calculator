using GPA_Calc.User_Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPA_Calc
{
    public partial class frmAddCourse : Form
    {
        public frmAddCourse()
        {
            InitializeComponent();
        }

        public frmAddCourse(int CrsSem)
        {
            InitializeComponent();
            ctrlAddCourse1.Sem = CrsSem - 1;
        }

        public frmAddCourse(int id ,string CrsName, int CrsHours, string CrsGrade, int CrsSem)
        {
            InitializeComponent();
            ctrlAddCourse1.FillForm(id, CrsName, CrsHours, CrsGrade, CrsSem);
            this.DialogResult = DialogResult.OK;

            this.Text = "Update Course";
        }

        public delegate void DataBackEventHandler(object sender, int sem);
        public event DataBackEventHandler DataBack;

        public void OnDataBack(int sem)
        {
            DataBack?.Invoke(this, sem);
        }

    }
}
