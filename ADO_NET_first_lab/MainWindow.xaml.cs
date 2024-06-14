using System;
using System.Windows;
using System.Collections.Generic;

namespace ADO_NET_first_lab
{
    public partial class MainWindow : Window
    {
        DBManager dBManager;

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                dBManager = new DBManager();

                // Створення таблиці та вставка початкових даних
                dBManager.CreateStudentsGradesTable();
                InsertInitialData();

                // При завантаженні вікна відображаємо всіх студентів
                RefreshDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred during initialization: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void InsertInitialData()
        {
            try
            {
                // Перевірка, чи є дані в таблиці
                if (dBManager.SelectAllStudents().Count == 0)
                {
                    List<Student> initialStudents = new List<Student>
                    {
                        new Student { FullName = "John Doe", GroupName = "Group A", AverageGrade = 3.5m, MinSubject = "Math", MaxSubject = "Physics" },
                        new Student { FullName = "Jane Smith", GroupName = "Group B", AverageGrade = 4.0m, MinSubject = "Biology", MaxSubject = "Chemistry" },
                        new Student { FullName = "Samuel Johnson", GroupName = "Group A", AverageGrade = 2.8m, MinSubject = "History", MaxSubject = "Literature" },
                        new Student { FullName = "Alice Brown", GroupName = "Group C", AverageGrade = 3.2m, MinSubject = "Geography", MaxSubject = "Art" }
                    };

                    foreach (var student in initialStudents)
                    {
                        dBManager.InsertStudent(student);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to insert initial data: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                decimal avgGrade = decimal.Parse(tbAverageGrade.Text);
                Student student = new Student
                {
                    FullName = tbFullName.Text,
                    GroupName = tbGroupName.Text,
                    AverageGrade = avgGrade,
                    MinSubject = tbMinSubject.Text,
                    MaxSubject = tbMaxSubject.Text
                };
                dBManager.InsertStudent(student);
                RefreshDataGrid(); // Оновлення даних у DataGrid
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to add student: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RefreshDataGrid()
        {
            try
            {
                dgStudents.ItemsSource = null; // Очищаємо старі дані
                dgStudents.ItemsSource = dBManager.SelectAllStudents(); // Завантажуємо нові дані
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve students: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}