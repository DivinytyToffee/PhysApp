using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Phis
{
    /// <summary>
    /// Логика взаимодействия для NewStudent.xaml
    /// </summary>
    public partial class NewStudent : Window
    {
        private static Student _curStudent;

        public NewStudent(Student student)
        {
            _curStudent = student;
            InitializeComponent();
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                _curStudent.LastName = LastName.Text;
                _curStudent.Name = Name.Text;
                double result;
                if (Beg100m.Text.Contains('.'))
                {
                    _curStudent.ResBeg100M = double.TryParse((Beg100m.Text.Replace('.', ',')), out result) ? Math.Abs(result) : 0;
                }
                else
                {
                    _curStudent.ResBeg100M = double.TryParse((Beg100m.Text), out result) ? Math.Abs(result) : 0;
                }
                if (Beg3km.Text.Contains('.'))
                {
                    _curStudent.ResBeg3Km = double.TryParse((Beg3km.Text.Replace('.', ',')), out result) ? Math.Abs(result) : 0;
                }
                else
                {
                    _curStudent.ResBeg3Km = double.TryParse((Beg3km.Text), out result) ? Math.Abs(result) : 0;
                }
                _curStudent.ResPod = double.TryParse(Pod.Text, out result) ? Math.Abs(result) : 0;
                _curStudent.ResPrig = double.TryParse(Prig.Text, out result) ? Math.Abs(result) : 0;
                _curStudent.ResVisit = double.TryParse(Visits.Text, out result) ? Math.Abs(result) : 0;
                _curStudent.Lichnostnie = double.TryParse(Person.Text, out result) ? Math.Abs(result) : 0;
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                var win = new WindowException(ex.Message);
                win.ShowDialog();
                this.DialogResult = false;
                this.Close();
            }


        }

        private void ClearButton_OnClick(object sender, RoutedEventArgs e)
        {
            Visits.Text = "0";
            Beg100m.Text = "0";
            Beg3km.Text = "0";
            LastName.Clear();
            Name.Clear();
            Prig.Text = "0";
            Pod.Text = "0";
            Person.Text = "0";
        }

        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
