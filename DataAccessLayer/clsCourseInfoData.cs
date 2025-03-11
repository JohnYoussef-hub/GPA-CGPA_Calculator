using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsCourseInfoData
    {
        public static bool AddCourseInfo(int CourseID, string ConstructorName, string CourseCode, string Notes)
        {
            string query = "INSERT INTO CourseInfo (CourseID, InstructorName, Notes, CourseCode) " +
                           "VALUES (@CourseID, @InstructorName, @Notes, @CourseCode)";

            try
            {
                using (SqlConnection conn = new SqlConnection(clsDataAccessLink.connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CourseID", CourseID);
                        cmd.Parameters.AddWithValue("@InstructorName", ConstructorName);
                        cmd.Parameters.AddWithValue("@CourseCode", CourseCode);
                        cmd.Parameters.AddWithValue("@Notes", Notes);
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static bool UpdateCourseInfo(int CourseID, string InstructorName, string CourseCode, string Notes)
        {
            string query = "UPDATE CourseInfo SET InstructorName = @InstructorName, CourseCode = @CourseCode, Notes = @Notes " +
                           "WHERE CourseID = @CourseID";
            try
            {
                using (SqlConnection conn = new SqlConnection(clsDataAccessLink.connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CourseID", CourseID);
                        cmd.Parameters.AddWithValue("@InstructorName", InstructorName);
                        cmd.Parameters.AddWithValue("@CourseCode", CourseCode);
                        cmd.Parameters.AddWithValue("@Notes", Notes);
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public static bool GetCourseInfoByCourseID(int CourseID, ref string InstructorName, ref string CourseCode, ref string Notes)
        {
            string query = "SELECT InstructorName, Notes, CourseCode FROM CourseInfo WHERE CourseID = @CourseID";
            try
            {
                using (SqlConnection conn = new SqlConnection(clsDataAccessLink.connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CourseID", CourseID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                InstructorName = reader.GetString(reader.GetOrdinal("InstructorName"));
                                Notes = reader.GetString(reader.GetOrdinal("Notes"));
                                CourseCode = reader.GetString(reader.GetOrdinal("CourseCode"));
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static bool HasCourseInfo(int CourseID)
        {
            string query = "SELECT COUNT(*) FROM CourseInfo WHERE CourseID = @CourseID";
            try
            {
                using (SqlConnection conn = new SqlConnection(clsDataAccessLink.connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CourseID", CourseID);
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public static bool GetFullCourseInfoByCourseID(int courseID, ref string courseName,
            ref string instructorName, ref string courseCode,
            ref int semester, ref int hours, ref string grade, ref string notes)
        {
            string query = @"
                SELECT CourseName, InstructorName, CourseCode, Semester, Hours, Grade, Notes
                FROM Courses
                FULL JOIN CourseInfo ON Courses.CourseID = CourseInfo.CourseID
                WHERE Courses.CourseID = @CourseID";
            try
            {
                using (SqlConnection conn = new SqlConnection(clsDataAccessLink.connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CourseID", courseID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                courseName = reader.IsDBNull(reader.GetOrdinal("CourseName")) ? null : reader.GetString(reader.GetOrdinal("CourseName"));
                                instructorName = reader.IsDBNull(reader.GetOrdinal("InstructorName")) ? null : reader.GetString(reader.GetOrdinal("InstructorName"));
                                courseCode = reader.IsDBNull(reader.GetOrdinal("CourseCode")) ? null : reader.GetString(reader.GetOrdinal("CourseCode"));
                                semester = reader.IsDBNull(reader.GetOrdinal("Semester")) ? -1 : Convert.ToInt32(reader["Semester"]);
                                hours = reader.IsDBNull(reader.GetOrdinal("Hours")) ? -1 : Convert.ToInt32(reader["Hours"]);
                                grade = reader.IsDBNull(reader.GetOrdinal("Grade")) ? null : reader.GetString(reader.GetOrdinal("Grade"));
                                notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? null : reader.GetString(reader.GetOrdinal("Notes"));
                                return true;
                            }
                            else return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


    }
}






/////////////////////////////// SQLite /////////////////////////////




//using DataAccessLayer;
//using System;
//using System.Collections.Generic;
//using System.Data.SQLite;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DataAccessLayer
//{
//    public class clsCourseInfoData
//    {
//        private static string connectionString = $"Data Source={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CGPA_Calc.db")};Version=3;";

//        public static bool AddCourseInfo(int CourseID, string InstructorName, string CourseCode, string Notes)
//        {
//            string query = "INSERT INTO CourseInfo (CourseID, InstructorName, Notes, CourseCode) " +
//                           "VALUES (@CourseID, @InstructorName, @Notes, @CourseCode)";

//            try
//            {
//                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
//                {
//                    conn.Open();
//                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
//                    {
//                        cmd.Parameters.AddWithValue("@CourseID", CourseID);
//                        cmd.Parameters.AddWithValue("@InstructorName", InstructorName);
//                        cmd.Parameters.AddWithValue("@CourseCode", CourseCode);
//                        cmd.Parameters.AddWithValue("@Notes", Notes);
//                        int result = cmd.ExecuteNonQuery();
//                        return result > 0;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        public static bool UpdateCourseInfo(int CourseID, string InstructorName, string CourseCode, string Notes)
//        {
//            string query = "UPDATE CourseInfo SET InstructorName = @InstructorName, CourseCode = @CourseCode, Notes = @Notes " +
//                           "WHERE CourseID = @CourseID";
//            try
//            {
//                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
//                {
//                    conn.Open();
//                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
//                    {
//                        cmd.Parameters.AddWithValue("@CourseID", CourseID);
//                        cmd.Parameters.AddWithValue("@InstructorName", InstructorName);
//                        cmd.Parameters.AddWithValue("@CourseCode", CourseCode);
//                        cmd.Parameters.AddWithValue("@Notes", Notes);
//                        int result = cmd.ExecuteNonQuery();
//                        return result > 0;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        public static bool GetCourseInfoByCourseID(int CourseID, ref string InstructorName, ref string CourseCode, ref string Notes)
//        {
//            string query = "SELECT InstructorName, Notes, CourseCode FROM CourseInfo WHERE CourseID = @CourseID";
//            try
//            {
//                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
//                {
//                    conn.Open();
//                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
//                    {
//                        cmd.Parameters.AddWithValue("@CourseID", CourseID);
//                        using (SQLiteDataReader reader = cmd.ExecuteReader())
//                        {
//                            if (reader.Read())
//                            {
//                                InstructorName = reader.GetString(reader.GetOrdinal("InstructorName"));
//                                Notes = reader.GetString(reader.GetOrdinal("Notes"));
//                                CourseCode = reader.GetString(reader.GetOrdinal("CourseCode"));
//                                return true;
//                            }
//                            else
//                            {
//                                return false;
//                            }
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        public static bool HasCourseInfo(int CourseID)
//        {
//            string query = "SELECT COUNT(*) FROM CourseInfo WHERE CourseID = @CourseID";
//            try
//            {
//                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
//                {
//                    conn.Open();
//                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
//                    {
//                        cmd.Parameters.AddWithValue("@CourseID", CourseID);
//                        int count = Convert.ToInt32(cmd.ExecuteScalar());
//                        return count > 0;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        public static bool GetFullCourseInfoByCourseID(int courseID, ref string courseName,
//            ref string instructorName, ref string courseCode,
//            ref int semester, ref int hours, ref string grade, ref string notes)
//        {
//            string query = @"
//                SELECT CourseName, InstructorName, CourseCode, Semester, Hours, Grade, Notes
//                FROM Courses
//                LEFT JOIN CourseInfo ON Courses.CourseID = CourseInfo.CourseID
//                WHERE Courses.CourseID = @CourseID";
//            try
//            {
//                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
//                {
//                    conn.Open();
//                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
//                    {
//                        cmd.Parameters.AddWithValue("@CourseID", courseID);
//                        using (SQLiteDataReader reader = cmd.ExecuteReader())
//                        {
//                            if (reader.Read())
//                            {
//                                courseName = reader.IsDBNull(reader.GetOrdinal("CourseName")) ? null : reader.GetString(reader.GetOrdinal("CourseName"));
//                                instructorName = reader.IsDBNull(reader.GetOrdinal("InstructorName")) ? null : reader.GetString(reader.GetOrdinal("InstructorName"));
//                                courseCode = reader.IsDBNull(reader.GetOrdinal("CourseCode")) ? null : reader.GetString(reader.GetOrdinal("CourseCode"));
//                                semester = reader.IsDBNull(reader.GetOrdinal("Semester")) ? -1 : Convert.ToInt32(reader["Semester"]);
//                                hours = reader.IsDBNull(reader.GetOrdinal("Hours")) ? -1 : Convert.ToInt32(reader["Hours"]);
//                                grade = reader.IsDBNull(reader.GetOrdinal("Grade")) ? null : reader.GetString(reader.GetOrdinal("Grade"));
//                                notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? null : reader.GetString(reader.GetOrdinal("Notes"));
//                                return true;
//                            }
//                            else return false;
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//    }
//}



