using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLayer
{
    public class clsApp
    {

        public string CourseName { get; set; }
        public char Grade { get; set; }
        public int CreditHours { get; set; }
        public int Semester { get; set; }


        public clsApp()
        {
            CourseName = "";
            Grade = ' ';
            CreditHours = 0;
            Semester = 0;
        }



        public static DataTable GetCoursesBySemester(int semester)
        {
            DataTable courses = DataAccessLayer.clsAppData.GetCoursesBySemester(semester);
            return courses;
        }

        public static bool AddCourse(string courseName, int creditHours, string grade, int semester)
        {
            bool success = DataAccessLayer.clsAppData.AddCourse(courseName, creditHours, grade, semester);
            return success;
        }

        public static double GetGPA(int semester)
        {
            return DataAccessLayer.clsAppData.GetGPA(semester);
        }

        public static double GetCGPA()
        {
            return DataAccessLayer.clsAppData.GetCGPA();
        }

        public static bool DeleteCourse(int courseID)
        {
            return DataAccessLayer.clsAppData.DeleteCourse(courseID);
        }

        public static int GetSemesterByID(int courseID)
        {
            return DataAccessLayer.clsAppData.GetSemesterByID(courseID);
        }

        public static bool UpdateCourse(int courseID, string courseName, int creditHours, string grade, int semester)
        {
            return DataAccessLayer.clsAppData.UpdateCourse(courseID, courseName, creditHours, grade, semester);
        }
    }
}
