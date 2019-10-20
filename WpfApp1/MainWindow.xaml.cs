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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            DataContext = this;
            Events = new List<Event>
         {
            new Event { Name = "Event 1", Date = DateTime.Parse("5/1/2008 ")},
            new Event { Name = "Event 2", Date = DateTime.Parse("5/5/2025 ")},
            new Event { Name = "Event 3", Date = DateTime.Parse("5/10/2020 ")},
            new Event { Name = "Event 4", Date = DateTime.Parse("17/1/2018 ")},
         };
        }
        static public List<Event> Events { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int i = List_Events.SelectedIndex;
            MessageBox.Show(Events[i].Date.ToString());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Event temp = new Event();
            //temp.Name = Event_Name.Text;
            //temp.Date = DateTime.Parse(Event_Date.ToString());
            //Events.Add(temp);
            List_Events.Items.Refresh();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Window1 okno = new Window1();
            okno.ShowDialog();
            List_Events.Items.Refresh();
        }
    }

    public class Event
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}