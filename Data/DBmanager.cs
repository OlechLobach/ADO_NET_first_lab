using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Data
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string GroupName { get; set; }
        public decimal AverageGrade { get; set; }
        public string MinSubject { get; set; }
        public string MaxSubject { get; set; }
    }

    public class DBManager
    {
        private string ConnectionString = "Data Source=10.0.0.40,1433;Initial Catalog=StudentsGrades;User ID=student;Password=1111;Encrypt=True;TrustServerCertificate=True;";

        public void CreateStudentsGradesTable()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string sql = @"CREATE TABLE StudentsGrades (
                                    Id INT PRIMARY KEY IDENTITY(1,1),
                                    FullName NVARCHAR(100) NOT NULL,
                                    GroupName NVARCHAR(50) NOT NULL,
                                    AverageGrade DECIMAL(5, 2) NOT NULL,
                                    MinSubject NVARCHAR(50) NULL,
                                    MaxSubject NVARCHAR(50) NULL
                                )";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Failed to create StudentsGrades table: " + ex.Message);
            }
        }

        public void InsertStudent(Student student)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string sql = @"INSERT INTO StudentsGrades (FullName, GroupName, AverageGrade, MinSubject, MaxSubject) 
                                   VALUES (@FullName, @GroupName, @AverageGrade, @MinSubject, @MaxSubject)";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@FullName", student.FullName);
                    cmd.Parameters.AddWithValue("@GroupName", student.GroupName);
                    cmd.Parameters.AddWithValue("@AverageGrade", student.AverageGrade);
                    cmd.Parameters.AddWithValue("@MinSubject", student.MinSubject);
                    cmd.Parameters.AddWithValue("@MaxSubject", student.MaxSubject);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Failed to insert student: " + ex.Message);
            }
        }

        public List<Student> SelectAllStudents()
        {
            List<Student> students = new List<Student>();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM StudentsGrades";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Student student = new Student();
                        student.Id = Convert.ToInt32(reader["Id"]);
                        student.FullName = Convert.ToString(reader["FullName"]);
                        student.GroupName = Convert.ToString(reader["GroupName"]);
                        student.AverageGrade = Convert.ToDecimal(reader["AverageGrade"]);
                        student.MinSubject = Convert.ToString(reader["MinSubject"]);
                        student.MaxSubject = Convert.ToString(reader["MaxSubject"]);
                        students.Add(student);
                    }
                    reader.Close();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Failed to retrieve students: " + ex.Message);
            }
            return students;
        }
    }
}