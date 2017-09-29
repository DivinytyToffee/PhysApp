using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Phis
{
    /// <summary>
    /// Логика взаимодействия для Student.xaml
    /// </summary>
    public partial class StudentWindow : Window
    {
        private static Student _curStudent;

        public StudentWindow(Student student)
        {
            InitializeComponent();
            _curStudent = student;
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double result;
                if (ResBeg100.Text.Contains('.'))
                {
                    _curStudent.ResBeg100M = double.TryParse((ResBeg100.Text.Replace('.', ',')), out result) ? Math.Abs(result) : 0;
                }
                else
                {
                    _curStudent.ResBeg100M = double.TryParse((ResBeg100.Text), out result) ? Math.Abs(result) : 0;
                }
                if (ResBeg3.Text.Contains('.'))
                {
                    _curStudent.ResBeg3Km = double.TryParse((ResBeg3.Text.Replace('.', ',')), out result) ? Math.Abs(result) : 0;
                }
                else
                {
                    _curStudent.ResBeg3Km = double.TryParse((ResBeg3.Text), out result) ? Math.Abs(result) : 0;
                }
                _curStudent.ResPod = double.TryParse(ResPod.Text, out result) ? Math.Abs(result) : 0;
                _curStudent.ResPrig = double.TryParse(ResPrig.Text, out result) ? Math.Abs(result) : 0;
                _curStudent.ResVisit = double.TryParse(ResPos.Text, out result) ? Math.Abs(result) : 0;
                _curStudent.Lichnostnie = double.TryParse(ResPerson.Text, out result) ? Math.Abs(result) : 0;
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                var win = new WindowException(ex.Message);
                win.ShowDialog();
                this.DialogResult = false;
                this.Close();
            }


        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(_curStudent.ResBeg100M != 0)
            ResBeg100.Text = _curStudent.ResBeg100M.ToString();
            if (_curStudent.ResBeg3Km != 0)
                ResBeg3.Text = _curStudent.ResBeg3Km.ToString();
            if (_curStudent.ResPod != 0)
                ResPod.Text = _curStudent.ResPod.ToString();
            if (_curStudent.ResPrig != 0)
                ResPrig.Text = _curStudent.ResPrig.ToString();
            if (_curStudent.ResVisit != 0)
                ResPos.Text = _curStudent.ResVisit.ToString();
            if (_curStudent.Lichnostnie != 0)
                ResPerson.Text = _curStudent.Lichnostnie.ToString();
            Name.Content = _curStudent.Name;
            LName.Content = _curStudent.LastName;
        }

        private void ResPrig_OnMouseLeave(object sender, MouseEventArgs e)
        {
            StatBarText.Text = "Прыжки";
        }
        private void ResBeg3_OnMouseLeave(object sender, MouseEventArgs e)
        {
            StatBarText.Text = "Бег 3 км.";
        }
        private void ResBeg100_OnMouseLeave(object sender, MouseEventArgs e)
        {
            StatBarText.Text = "Бег 100 м.";
        }
        private void ResPod_OnMouseLeave(object sender, MouseEventArgs e)
        {
            StatBarText.Text = "Подтягивания";
        }
        private void ResPos_OnMouseLeave(object sender, MouseEventArgs e)
        {
            StatBarText.Text = "Посещение";
        }
        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void ResPerson_OnMouseLeave(object sender, MouseEventArgs e)
        {
            StatBarText.Text = "Личностные качества";
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            _curStudent.LastName = null;
            this.DialogResult = true;
            this.Close();
        }
    }
}
