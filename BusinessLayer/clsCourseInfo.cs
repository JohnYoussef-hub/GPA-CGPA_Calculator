using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using DataAccessLayer;

namespace BusinessLayer
{
    public class clsCourseInfo
    {
        public enum Mode { Add = 1, Update = 2 }
        private Mode _mode;

        public int CourseInfoID { get; set; }
        public int CourseID { get; set; }
        public string InstructorName { get; set; }
        public string CourseCode { get; set; }
        public string Notes { get; set; }


        public clsCourseInfo()
        {
            CourseInfoID = 0;
            CourseID = 0;
            InstructorName = "";
            CourseCode = "";
            Notes = "";
            _mode = Mode.Add;
        }


        public clsCourseInfo(int CourseID)
        {
            string InstructorName = "";
            string CourseCode = "";
            string Notes = "";

            if(clsCourseInfoData.GetCourseInfoByCourseID(CourseID, ref InstructorName, ref CourseCode, ref Notes))
            {
                this.CourseID = CourseID;
                this.InstructorName = InstructorName;
                this.CourseCode = CourseCode;
                this.Notes = Notes;
            }
        }


        public clsCourseInfo(int courseID, string instructorName, string courseCode, string notes)
        {
            CourseID = courseID;
            InstructorName = instructorName;
            CourseCode = courseCode;
            Notes = notes;
            _mode = Mode.Update;
        }



        public bool AddCourseInfo()
        {
            return clsCourseInfoData.AddCourseInfo(this.CourseID, this.InstructorName, this.CourseCode, this.Notes);
        }

        public bool UpdateCourseInfo()
        {
            return clsCourseInfoData.UpdateCourseInfo(this.CourseID, this.InstructorName, this.CourseCode, this.Notes);
        }


        public static clsCourseInfo GetCourseInfoByCourseID(int CourseID)
        {
            string InstructorName = "";
            string CourseCode = "";
            string Notes = "";

            if(clsCourseInfoData.GetCourseInfoByCourseID(CourseID, ref InstructorName, ref CourseCode, ref Notes))
            {
                return new clsCourseInfo(CourseID, InstructorName, CourseCode, Notes);
            }
            else return null;
        }


        public static bool HasCourseInfo(int CourseID)
        {
            return clsCourseInfoData.HasCourseInfo(CourseID);
        }

        public static bool GetFullCourseInfoByCourseID(int courseID, ref string courseName, ref string instructorName, ref string courseCode, ref int semester, ref int hours, ref string grade, ref string notes)
        {
            return clsCourseInfoData.GetFullCourseInfoByCourseID(courseID, ref courseName, ref instructorName, ref courseCode, ref semester, ref hours, ref grade , ref notes);
        }


        public bool Save()
        {
            switch(_mode)
            {
                case Mode.Add:
                    _mode = Mode.Update;
                    return AddCourseInfo();
                case Mode.Update:
                    return UpdateCourseInfo();
                default:
                    return false;
            }
        }


    }
}
