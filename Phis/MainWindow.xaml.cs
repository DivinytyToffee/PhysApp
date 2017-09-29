using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Phis
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly List<List<Student>> GropList = new List<List<Student>>();
        private static bool _checkDel = false;
        

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DirectoryInfo dir = new DirectoryInfo(@".\Group");
            if (!dir.Exists)
            {
                dir.CreateSubdirectory(@".\Group");
            }

            var myfile = dir.GetFiles("*.dat");
            if (myfile.Length <= 0)
            {
                for (int i = 0; i < Groups.Items.Count; i++)
                {
                    var newList = new List<Student>();
                    GropList.Add(newList);
                }
            }
            else
            {
                foreach (var f in myfile)
                {
                    List<Student> newList = Student.Open(f.FullName);
                    GropList.Add(newList);
                }
            }
            
            try
            {
                BitmapImage bm1 = new BitmapImage();
                bm1.BeginInit();
                bm1.UriSource = new Uri(@".\Source\itmo_small_blue_rus.png", UriKind.Relative);
                bm1.CacheOption = BitmapCacheOption.OnLoad;
                bm1.EndInit();
                image1.Source = bm1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось загрузить картинку\n"  + ex.Message);
                throw;
            }
           
        }
        private void Save()
        {
           try
                {
                    DirectoryInfo dir = new DirectoryInfo(@".\GroupBackUp");
                    if (!dir.Exists)
                    {
                        dir.CreateSubdirectory("GroupBackUp");
                    }
                    DirectoryInfo dirfile = new DirectoryInfo(@".\Group");
                    foreach (var file in dirfile.GetFiles(@"*.dat"))
                    {
                        file.CopyTo(
                            dir.FullName + @"\" + DateTime.Now.Day + DateTime.Now.Month + "-" + DateTime.Now.Minute +
                            "-" +
                            file.Name, true);
                    }
                }
                catch (Exception ex)
                {
                    var win = new WindowException(ex.Message);
                    win.ShowDialog();
                }

            
            for (ushort i = 0; i < Groups.Items.Count; i++)
            {
                Student.Save(GropList[i], @"Group\" + i + ".dat");
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           Save();
        }




        private void NewStudent_Button_OnClick(object sender, RoutedEventArgs e)
        {
            Student newStudent = new Student("", "");
            NewStudent nw = new NewStudent(newStudent);
            if (nw.ShowDialog() == true)
            {
                Save();
                GropList[Groups.SelectedIndex].Add(newStudent);
                GropList[Groups.SelectedIndex].Sort((x, y) =>
                   String.Compare(x.LastName, y.LastName, StringComparison.Ordinal) != 0
                       ? String.Compare(x.LastName, y.LastName, StringComparison.Ordinal)
                       : String.Compare(x.Name, y.Name, StringComparison.Ordinal));

                switch (Groups.SelectedIndex)
                {
                    case 0:
                        GroupM3200.Items.Clear();
                        foreach (Student student in GropList[Groups.SelectedIndex])
                            GroupM3200.Items.Add(student.ToString());
                        GroupM3200.SelectedIndex = GropList[Groups.SelectedIndex].IndexOf(newStudent);
                        break;
                    case 1:
                        GroupM3201.Items.Clear();
                        foreach (Student student in GropList[Groups.SelectedIndex])
                            GroupM3201.Items.Add(student.ToString());
                        GroupM3201.SelectedIndex = GropList[Groups.SelectedIndex].IndexOf(newStudent);
                        break;
                    case 2:
                        GroupM3202.Items.Clear();
                        foreach (Student student in GropList[Groups.SelectedIndex])
                            GroupM3202.Items.Add(student.ToString());
                        GroupM3202.SelectedIndex = GropList[Groups.SelectedIndex].IndexOf(newStudent);
                        break;
                }
                MessageBox.Show("Студент добавлен!", "Выполнено", MessageBoxButton.OK, MessageBoxImage.Information);
                
            }
            else
            {
                MessageBox.Show("Данные не были сохранены!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            switch (Groups.SelectedIndex)
            {
                case 0:
                    if (GroupM3200.SelectedIndex != -1)
                        ChageStudent(GropList[Groups.SelectedIndex][GroupM3200.SelectedIndex]);
                    break;
                case 1:
                    if (GroupM3201.SelectedIndex != -1)
                        ChageStudent(GropList[Groups.SelectedIndex][GroupM3201.SelectedIndex]);
                    break;
                case 2:
                    if (GroupM3202.SelectedIndex != -1)
                        ChageStudent(GropList[Groups.SelectedIndex][GroupM3202.SelectedIndex]);
                    break;
            }
        }




        private void M3200_Loaded(object sender, RoutedEventArgs e)
        {
            if (GropList[0].Count <= 0) return;
           GropList[0].Sort(
               (x, y) =>
                   String.Compare(x.LastName, y.LastName, StringComparison.Ordinal) != 0
                       ? String.Compare(x.LastName, y.LastName, StringComparison.Ordinal)
                       : String.Compare(x.Name, y.Name, StringComparison.Ordinal));
            foreach (Student student in GropList[0])
            {
                GroupM3200.Items.Add(student.ToString());
            }
        }
        private void M3201_Loaded(object sender, RoutedEventArgs e)
        {
            if (GropList[1].Count <= 0) return;
            GropList[1].Sort(
               (x, y) =>
                   String.Compare(x.LastName, y.LastName, StringComparison.Ordinal) != 0
                       ? String.Compare(x.LastName, y.LastName, StringComparison.Ordinal)
                       : String.Compare(x.Name, y.Name, StringComparison.Ordinal));
            foreach (Student student in GropList[1])
            {
                GroupM3201.Items.Add(student.ToString());
            }
        }
        private void M3202_Loaded(object sender, RoutedEventArgs e)
        {
            if (GropList[2].Count <= 0) return;
            GropList[2].Sort(
               (x, y) =>
                   String.Compare(x.LastName, y.LastName, StringComparison.Ordinal) != 0
                       ? String.Compare(x.LastName, y.LastName, StringComparison.Ordinal)
                       : String.Compare(x.Name, y.Name, StringComparison.Ordinal));
            foreach (Student student in GropList[2])
            {
                GroupM3202.Items.Add(student.ToString());
            }
        }




        private void Groups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (Groups.SelectedIndex)
            {
                case 0:
                    GroupM3201.SelectedIndex = -1;
                    GroupM3202.SelectedIndex = -1;
                    break;
                case 1:
                    GroupM3200.SelectedIndex = -1;
                    GroupM3202.SelectedIndex = -1;
                    break;
                case 2:
                    GroupM3200.SelectedIndex = -1;
                    GroupM3201.SelectedIndex = -1;
                    break;
            }
        }
        private void Group_M3202_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GropList[Groups.SelectedIndex].Count != 0)
            {
                if (_checkDel)
                {
                    GroupM3202.SelectedIndex = 0;
                    Name.Text = GropList[Groups.SelectedIndex][0].Name;
                    LName.Text = GropList[Groups.SelectedIndex][0].LastName;
                    Balls.Text = GropList[Groups.SelectedIndex][0].Balls.ToString();
                    _checkDel = false;
                }
                else
                {
                    if (GroupM3202.SelectedIndex != -1)
                    {
                        Name.Text = GropList[Groups.SelectedIndex][GroupM3202.SelectedIndex].Name;
                        LName.Text = GropList[Groups.SelectedIndex][GroupM3202.SelectedIndex].LastName;
                        Balls.Text = GropList[Groups.SelectedIndex][GroupM3202.SelectedIndex].Balls.ToString();
                    }
                    else
                    {
                        Name.Text = "";
                        LName.Text = "";
                        Balls.Text = "";
                    }
                }

            }
            else
            {
                Name.Text = "";
                LName.Text = "";
                Balls.Text = "";
            }
        }
        private void Group_M3201_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GropList[Groups.SelectedIndex].Count != 0)
            {
                if (_checkDel)
                {
                    GroupM3201.SelectedIndex = 0;
                    Name.Text = GropList[Groups.SelectedIndex][0].Name;
                    LName.Text = GropList[Groups.SelectedIndex][0].LastName;
                    Balls.Text = GropList[Groups.SelectedIndex][0].Balls.ToString();
                    _checkDel = false;
                }
                else
                {
                    if (GroupM3201.SelectedIndex != -1)
                    {
                        Name.Text = GropList[Groups.SelectedIndex][GroupM3201.SelectedIndex].Name;
                        LName.Text = GropList[Groups.SelectedIndex][GroupM3201.SelectedIndex].LastName;
                        Balls.Text = GropList[Groups.SelectedIndex][GroupM3201.SelectedIndex].Balls.ToString();
                    }
                    else
                    {
                        Name.Text = "";
                        LName.Text = "";
                        Balls.Text = "";
                    }
                }

            }
            else
            {
               
                Name.Text = "";
                LName.Text = "";
                Balls.Text = "";
            }

        }
        private void Group_M3200_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GropList[Groups.SelectedIndex].Count != 0)
            {
                if (_checkDel)
                {
                    GroupM3200.SelectedIndex = 0;
                    Name.Text = GropList[Groups.SelectedIndex][0].Name;
                    LName.Text = GropList[Groups.SelectedIndex][0].LastName;
                    Balls.Text = GropList[Groups.SelectedIndex][0].Balls.ToString();
                    _checkDel = false;
                }
                else
                {
                    if (GroupM3200.SelectedIndex != -1)
                    {
                        Name.Text = GropList[Groups.SelectedIndex][GroupM3200.SelectedIndex].Name;
                        LName.Text = GropList[Groups.SelectedIndex][GroupM3200.SelectedIndex].LastName;
                        Balls.Text = GropList[Groups.SelectedIndex][GroupM3200.SelectedIndex].Balls.ToString();
                    }
                    else
                    {
                        Name.Text = "";
                        LName.Text = "";
                        Balls.Text = "";
                    }
                }
                

            }
            else
            {
               
                Name.Text = "";
                LName.Text = "";
                Balls.Text = "";
            }
        }
            
        

        

        private void Group_M3200_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (GropList[Groups.SelectedIndex].Count != 0)
                ChageStudent(GropList[Groups.SelectedIndex][GroupM3200.SelectedIndex]);
        }
        private void Group_M3201_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (GropList[Groups.SelectedIndex].Count != 0)
                ChageStudent(GropList[Groups.SelectedIndex][GroupM3201.SelectedIndex]);

        }
        private void Group_M3202_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (GropList[Groups.SelectedIndex].Count != 0)
                ChageStudent(GropList[Groups.SelectedIndex][GroupM3202.SelectedIndex]);
        }
        private void ChageStudent(Student curStudent)
        {
            StudentWindow window = new StudentWindow(curStudent);
            try
            {
                if (window.ShowDialog() == true)
                {
                    foreach (var student in GropList[Groups.SelectedIndex])
                    {
                        if (student.LastName != null) continue;
                        GropList[Groups.SelectedIndex].Remove(student);
                        _checkDel = true;
                        break;
                    }
                    if (_checkDel)
                    {
                        Name.Text = "";
                        LName.Text = "";
                        Balls.Text = "";
                        Delete();
                    }
                    else
                    {
                        Name.Text = curStudent.Name;
                        LName.Text = curStudent.LastName;
                        Balls.Text = curStudent.Balls.ToString();
                    }
                    Save();
                    MessageBox.Show("Изменения сохранены", "Выполнено", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Изменения НЕ внесены.", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                var win = new WindowException(ex.Message);
                win.ShowDialog();
            }
        }
        public void Delete()
        {
            if (GropList[Groups.SelectedIndex].Count == 0)
            {
                switch (Groups.SelectedIndex)
                {
                    case 0:
                        GroupM3200.Items.Clear();
                        GroupM3200.SelectedIndex = -1;
                        return;
                    case 1:
                        GroupM3201.Items.Clear();
                        GroupM3201.SelectedIndex = -1;
                        return;
                    case 2:
                        GroupM3202.Items.Clear();
                        GroupM3202.SelectedIndex = -1;
                        return;
                }
            }
            else
            {
                GropList[Groups.SelectedIndex].Sort((x, y) =>
                   String.Compare(x.LastName, y.LastName, StringComparison.Ordinal) != 0
                       ? String.Compare(x.LastName, y.LastName, StringComparison.Ordinal)
                       : String.Compare(x.Name, y.Name, StringComparison.Ordinal));
                switch (Groups.SelectedIndex)
                {
                    case 0:
                        GroupM3200.Items.Clear();
                        foreach (Student student in GropList[Groups.SelectedIndex])
                            GroupM3200.Items.Add(student.ToString());
                        GroupM3200.SelectedIndex = -1;
                        break;
                    case 1:
                        GroupM3201.Items.Clear();
                        foreach (Student student in GropList[Groups.SelectedIndex])
                            GroupM3201.Items.Add(student.ToString());
                        GroupM3201.SelectedIndex = -1;
                        break;
                    case 2:
                        GroupM3202.Items.Clear();
                        foreach (Student student in GropList[Groups.SelectedIndex])
                            GroupM3202.Items.Add(student.ToString());
                        GroupM3202.SelectedIndex = -1;
                        break;
                }
            }
        }
    }
}
