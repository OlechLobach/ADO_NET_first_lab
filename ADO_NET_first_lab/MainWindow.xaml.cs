using Data; // Додайте необхідні using директиви
using System;
using System.Windows;

namespace ADO_NET_first_lab
{
    public partial class MainWindow : Window
    {
        private DBManager dBManager;

        public MainWindow()
        {
            InitializeComponent();
            dBManager = new DBManager();

            // При завантаженні вікна відображаємо всіх студентів
            RefreshDataGrid();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Додавання нового студента при кліку на кнопку
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
            // Оновлення вмісту DataGrid з бази даних
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