using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        public void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Event temp = new Event();
            temp.Name = Event_Name.Text;
            int u = Event_Date.SelectedDate.Value.Day;
            temp.Date = DateTime.Parse(Event_Date.SelectedDate.Value.Day.ToString()+"/"+ Event_Date.SelectedDate.Value.Month.ToString() +"/" + Event_Date.SelectedDate.Value.Year.ToString() + "  " + Event_Hours.Text + ":" + Event_Minutes.Text);

            // MainWindow test = new MainWindow();
            MainWindow.Events.Add(temp);
            bool file_empty= MainWindow.isFileEmpty();
            string path = @"e:\repos\WpfApp1\WpfApp1\events.txt";
            StreamWriter sw = new StreamWriter(path,true);
            if (!file_empty)
            {
                sw.WriteLine(temp.Name); 
            }           
            sw.WriteLine(temp.Date);
            sw.Write("? \n");
            sw.Close();
            this.Close();
           // MainWindow.List_Events.Items.Refresh();
        }

        bool firstTime = true;
        private void Event_Minutes_GotKeyboardFocus(object sender, EventArgs e)
        {
            if (firstTime)
            {
                firstTime = false;
                Event_Minutes.Clear();
            }
        }
        bool firstTime1 = true;
        private void Event_Hours_GotKeyboardFocus(object sender, RoutedEventArgs e)
        {
            if (firstTime1)
            {
                firstTime1 = false;
                Event_Hours.Clear();
            }
        }
    }
}
