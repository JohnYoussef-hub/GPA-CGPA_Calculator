using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsAppData
    {
        public static DataTable GetCoursesBySemester(int semester)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessLink.connectionString);
            string sql = "SELECT CourseID ,CourseName, Hours, Grade FROM Courses WHERE Semester = @Semester";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Semester", semester);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return dt;


        }


        public static bool AddCourse(string courseName, int hours, string grade, int semester)
        {
            bool success = false;

            SqlConnection connection = new SqlConnection(clsDataAccessLink.connectionString);
            string sql = "INSERT INTO Courses (CourseName, Hours, Grade, Semester) VALUES (@CourseName, @Hours, @Grade, @Semester)";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@CourseName", courseName);
            command.Parameters.AddWithValue("@Hours", hours);
            command.Parameters.AddWithValue("@Grade", grade);
            command.Parameters.AddWithValue("@Semester", semester);
            try
            {
                connection.Open();
                int rows = command.ExecuteNonQuery();
                if (rows > 0) success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return success;
        }


        public static double GetGPA(int semester)
        {
            double gpa = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessLink.connectionString);
            string sql = @"
            SELECT 
                SUM(Hours) AS TotalHours, 
                SUM(Hours * 
                    CASE 
                        WHEN Grade = 'A'  THEN 4.00
                        WHEN Grade = 'A-' THEN 3.66
                        WHEN Grade = 'B+' THEN 3.33
                        WHEN Grade = 'B'  THEN 3.00
                        WHEN Grade = 'B-' THEN 2.66
                        WHEN Grade = 'C+' THEN 2.33
                        WHEN Grade = 'C'  THEN 2.00
                        WHEN Grade = 'C-' THEN 1.66
                        WHEN Grade = 'D+' THEN 1.33
                        WHEN Grade = 'D'  THEN 1.00
                        ELSE 0.00
                    END
                ) AS QualityPoints 
            FROM Courses 
            WHERE Semester = @Semester";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Semester", semester);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    double totalHours = reader["TotalHours"] != DBNull.Value ? Convert.ToDouble(reader["TotalHours"]) : 0;
                    double qualityPoints = reader["QualityPoints"] != DBNull.Value ? Convert.ToDouble(reader["QualityPoints"]) : 0;
                    if (totalHours > 0) gpa = qualityPoints / totalHours;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return gpa;
        }


        public static double GetCGPA()
        {
            double cgpa = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessLink.connectionString);
            string sql = @"
            SELECT 
                SUM(Hours) AS TotalHours, 
                SUM(Hours * 
                    CASE 
                        WHEN Grade = 'A'  THEN 4.00
                        WHEN Grade = 'A-' THEN 3.67
                        WHEN Grade = 'B+' THEN 3.33
                        WHEN Grade = 'B'  THEN 3.00
                        WHEN Grade = 'B-' THEN 2.67
                        WHEN Grade = 'C+' THEN 2.33
                        WHEN Grade = 'C'  THEN 2.00
                        WHEN Grade = 'C-' THEN 1.67
                        WHEN Grade = 'D+' THEN 1.33
                        WHEN Grade = 'D'  THEN 1.00
                        ELSE 0.00
                    END
                ) AS QualityPoints 
            FROM Courses";
            SqlCommand command = new SqlCommand(sql, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    double totalHours = reader["TotalHours"] != DBNull.Value ? Convert.ToDouble(reader["TotalHours"]) : 0;
                    double qualityPoints = reader["QualityPoints"] != DBNull.Value ? Convert.ToDouble(reader["QualityPoints"]) : 0;
                    if (totalHours > 0) cgpa = qualityPoints / totalHours;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return cgpa;
        }

        public static bool DeleteCourse(int id)
        {
            bool success = false;
            SqlConnection connection = new SqlConnection(clsDataAccessLink.connectionString);
            string sql = "DELETE FROM Courses WHERE CourseID = @ID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@ID", id);
            try
            {
                connection.Open();
                int rows = command.ExecuteNonQuery();
                if (rows > 0) success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return success;
        }

        public static bool UpdateCourse(int id, string courseName, int hours, string grade, int semester)
        {
            bool success = false;
            SqlConnection connection = new SqlConnection(clsDataAccessLink.connectionString);
            string sql = "UPDATE Courses SET CourseName = @CourseName, Hours = @Hours, Grade = @Grade, Semester = @Semester WHERE CourseID = @ID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@ID", id);
            command.Parameters.AddWithValue("@CourseName", courseName);
            command.Parameters.AddWithValue("@Hours", hours);
            command.Parameters.AddWithValue("@Grade", grade);
            command.Parameters.AddWithValue("@Semester", semester);
            try
            {
                connection.Open();
                int rows = command.ExecuteNonQuery();
                if (rows > 0) success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return success;
        }

        public static int GetSemesterByID(int id)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLink.connectionString);
            string sql = "SELECT Semester FROM Courses WHERE CourseID = @ID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@ID", id);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return Convert.ToInt32(reader["Semester"]);
                }
                else return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();

            }
        }
    }
}




////////////////////////////// SQLite //////////////////////




//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SQLite;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DataAccessLayer
//{
//    public class clsAppData
//    {
//        private static string connectionString = $"Data Source={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CGPA_Calc.db")};Version=3;";
//        static clsAppData()
//        {
//            if (!File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CGPA_Calc.db")))
//            {
//                CreateDatabase();
//            }
//        }

//        private static void CreateDatabase()
//        {
//            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
//            {
//                connection.Open();

//                string createCoursesTableQuery = @"
//                    CREATE TABLE IF NOT EXISTS Courses (
//                        CourseID INTEGER PRIMARY KEY AUTOINCREMENT,
//                        CourseName TEXT NOT NULL,
//                        Hours INTEGER NOT NULL,
//                        Grade TEXT NOT NULL,
//                        Semester INTEGER NOT NULL
//                    );
//                ";

//                string createCourseInfoTableQuery = @"
//                    CREATE TABLE IF NOT EXISTS CourseInfo (
//                        CourseInfoID INTEGER PRIMARY KEY AUTOINCREMENT,
//                        CourseID INTEGER NOT NULL,
//                        InstructorName TEXT NOT NULL,
//                        Notes TEXT,
//                        CourseCode TEXT,
//                        FOREIGN KEY (CourseID) REFERENCES Courses(CourseID)
//                    );
//                ";

//                using (SQLiteCommand command = new SQLiteCommand(createCoursesTableQuery, connection))
//                {
//                    command.ExecuteNonQuery();
//                }

//                using (SQLiteCommand command = new SQLiteCommand(createCourseInfoTableQuery, connection))
//                {
//                    command.ExecuteNonQuery();
//                }
//            }
//        }

//        public static DataTable GetCoursesBySemester(int semester)
//        {
//            DataTable dt = new DataTable();

//            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
//            using (SQLiteCommand command = new SQLiteCommand("SELECT CourseID, CourseName, Hours, Grade FROM Courses WHERE Semester = @Semester", connection))
//            {
//                command.Parameters.AddWithValue("@Semester", semester);

//                try
//                {
//                    connection.Open();
//                    using (SQLiteDataReader reader = command.ExecuteReader())
//                    {
//                        dt.Load(reader);
//                    }
//                }
//                catch (Exception)
//                {
//                    throw;
//                }
//            }
//            return dt;
//        }

//        public static bool AddCourse(string courseName, int hours, string grade, int semester)
//        {
//            bool success = false;

//            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
//            using (SQLiteCommand command = new SQLiteCommand("INSERT INTO Courses (CourseName, Hours, Grade, Semester) VALUES (@CourseName, @Hours, @Grade, @Semester)", connection))
//            {
//                command.Parameters.AddWithValue("@CourseName", courseName);
//                command.Parameters.AddWithValue("@Hours", hours);
//                command.Parameters.AddWithValue("@Grade", grade);
//                command.Parameters.AddWithValue("@Semester", semester);

//                try
//                {
//                    connection.Open();
//                    int rows = command.ExecuteNonQuery();
//                    if (rows > 0) success = true;
//                }
//                catch (Exception)
//                {
//                    throw;
//                }
//            }
//            return success;
//        }

//        public static double GetGPA(int semester)
//        {
//            double gpa = 0;

//            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
//            using (SQLiteCommand command = new SQLiteCommand(@"
//                SELECT 
//                    SUM(Hours) AS TotalHours, 
//                    SUM(Hours * 
//                        CASE 
//                            WHEN Grade = 'A'  THEN 4.00
//                            WHEN Grade = 'A-' THEN 3.66
//                            WHEN Grade = 'B+' THEN 3.33
//                            WHEN Grade = 'B'  THEN 3.00
//                            WHEN Grade = 'B-' THEN 2.66
//                            WHEN Grade = 'C+' THEN 2.33
//                            WHEN Grade = 'C'  THEN 2.00
//                            WHEN Grade = 'C-' THEN 1.66
//                            WHEN Grade = 'D+' THEN 1.33
//                            WHEN Grade = 'D'  THEN 1.00
//                            ELSE 0.00
//                        END
//                    ) AS QualityPoints 
//                FROM Courses 
//                WHERE Semester = @Semester", connection))
//            {
//                command.Parameters.AddWithValue("@Semester", semester);

//                try
//                {
//                    connection.Open();
//                    using (SQLiteDataReader reader = command.ExecuteReader())
//                    {
//                        if (reader.Read())
//                        {
//                            double totalHours = reader["TotalHours"] != DBNull.Value ? Convert.ToDouble(reader["TotalHours"]) : 0;
//                            double qualityPoints = reader["QualityPoints"] != DBNull.Value ? Convert.ToDouble(reader["QualityPoints"]) : 0;
//                            if (totalHours > 0) gpa = qualityPoints / totalHours;
//                        }
//                    }
//                }
//                catch (Exception)
//                {
//                    throw;
//                }
//            }
//            return gpa;
//        }

//        public static double GetCGPA()
//        {
//            double cgpa = 0;

//            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
//            using (SQLiteCommand command = new SQLiteCommand(@"
//                SELECT 
//                    SUM(Hours) AS TotalHours, 
//                    SUM(Hours * 
//                        CASE 
//                            WHEN Grade = 'A'  THEN 4.00
//                            WHEN Grade = 'A-' THEN 3.67
//                            WHEN Grade = 'B+' THEN 3.33
//                            WHEN Grade = 'B'  THEN 3.00
//                            WHEN Grade = 'B-' THEN 2.67
//                            WHEN Grade = 'C+' THEN 2.33
//                            WHEN Grade = 'C'  THEN 2.00
//                            WHEN Grade = 'C-' THEN 1.67
//                            WHEN Grade = 'D+' THEN 1.33
//                            WHEN Grade = 'D'  THEN 1.00
//                            ELSE 0.00
//                        END
//                    ) AS QualityPoints 
//                FROM Courses", connection))
//            {
//                try
//                {
//                    connection.Open();
//                    using (SQLiteDataReader reader = command.ExecuteReader())
//                    {
//                        if (reader.Read())
//                        {
//                            double totalHours = reader["TotalHours"] != DBNull.Value ? Convert.ToDouble(reader["TotalHours"]) : 0;
//                            double qualityPoints = reader["QualityPoints"] != DBNull.Value ? Convert.ToDouble(reader["QualityPoints"]) : 0;
//                            if (totalHours > 0) cgpa = qualityPoints / totalHours;
//                        }
//                    }
//                }
//                catch (Exception)
//                {
//                    throw;
//                }
//            }
//            return cgpa;
//        }

//        public static bool DeleteCourse(int id)
//        {
//            bool success = false;

//            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
//            using (SQLiteCommand command = new SQLiteCommand("DELETE FROM Courses WHERE CourseID = @ID", connection))
//            {
//                command.Parameters.AddWithValue("@ID", id);

//                try
//                {
//                    connection.Open();
//                    int rows = command.ExecuteNonQuery();
//                    if (rows > 0) success = true;
//                }
//                catch (Exception)
//                {
//                    throw;
//                }
//            }
//            return success;
//        }

//        public static bool UpdateCourse(int id, string courseName, int hours, string grade, int semester)
//        {
//            bool success = false;

//            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
//            using (SQLiteCommand command = new SQLiteCommand("UPDATE Courses SET CourseName = @CourseName, Hours = @Hours, Grade = @Grade, Semester = @Semester WHERE CourseID = @ID", connection))
//            {
//                command.Parameters.AddWithValue("@ID", id);
//                command.Parameters.AddWithValue("@CourseName", courseName);
//                command.Parameters.AddWithValue("@Hours", hours);
//                command.Parameters.AddWithValue("@Grade", grade);
//                command.Parameters.AddWithValue("@Semester", semester);

//                try
//                {
//                    connection.Open();
//                    int rows = command.ExecuteNonQuery();
//                    if (rows > 0) success = true;
//                }
//                catch (Exception)
//                {
//                    throw;
//                }
//            }
//            return success;
//        }

//        public static int GetSemesterByID(int id)
//        {
//            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
//            using (SQLiteCommand command = new SQLiteCommand("SELECT Semester FROM Courses WHERE CourseID = @ID", connection))
//            {
//                command.Parameters.AddWithValue("@ID", id);

//                try
//                {
//                    connection.Open();
//                    using (SQLiteDataReader reader = command.ExecuteReader())
//                    {
//                        if (reader.Read())
//                        {
//                            return Convert.ToInt32(reader["Semester"]);
//                        }
//                        else return 0;
//                    }
//                }
//                catch (Exception)
//                {
//                    throw;
//                }
//            }
//        }
//    }
//}
