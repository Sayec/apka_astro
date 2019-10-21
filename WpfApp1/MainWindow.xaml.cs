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
using System.IO;


namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Events = new List<Event> {};
            string path = @"e:\repos\WpfApp1\WpfApp1\events.txt";
            StreamReader sr = new StreamReader(path);
            string s;
            Event tmp = new Event();
            int i = 0;
            do
            {
                s = sr.ReadLine();
                if (i % 3 == 0)
                {
                    tmp.Name = s;
                }
                else if (i % 3 == 1)
                {
                    tmp.Date = DateTime.Parse(s);
                }
                else if (i%3==2)
                {
                    Events.Add( new Event { Name = tmp.Name, Date = tmp.Date });
                }
                i++;
            } while (s != null);
            sr.Close();
        }

        static public bool isFileEmpty_bool=false;

        static public bool isFileEmpty()
        {
            string path = @"e:\repos\WpfApp1\WpfApp1\events.txt";
            StringBuilder sb = new StringBuilder();
            StreamReader sr = new StreamReader(path);
            string s;
            s = sr.ReadLine();
            sr.Close();
            if (s == null)
            {
                isFileEmpty_bool = true;
            }
            else
            {
                isFileEmpty_bool = false;
            }
            return isFileEmpty_bool;
        }
        static public List<Event> Events { get; set; }
        
        private void Sortowanie_Events()
        {
            double czas;
            Event temp;
            for (int i = 0; i < Events.Count -1 ; i++)
            {
                for (int j = i+1; j < Events.Count; j++)
                {
                    czas = (Events[i].Date - Events[j].Date).TotalDays;
                    if (czas > 0)
                    {
                        temp = Events[j];
                        Events[j] = Events[i];
                        Events[i] = temp;
                    }
                }
            }
            
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int i = List_Events.SelectedIndex;
            MessageBox.Show(((Events[0].Date - Events[1].Date).TotalDays).ToString());
            //MessageBox.Show(Events[i].Date.ToString());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Event temp = new Event();
            //temp.Name = Event_Name.Text;
            //temp.Date = DateTime.Parse(Event_Date.ToString());
            //Events.Add(temp);
            List_Events.Items.Refresh();
        }

        private void Add_Event_Click(object sender, RoutedEventArgs e)
        {
            Window1 okno = new Window1();
            okno.ShowDialog();
            Sortowanie_Events();
            List_Events.Items.Refresh();
        }
        private void List_Events_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (List_Events.SelectedItem != null)
            {
                int i = List_Events.SelectedIndex;

                MessageBox.Show(Events[i].Date.ToString("dd/MM/yyyy H:mm"));
                //MessageBox.Show(List_Events.SelectedItem.ToString());
            }
        }
        private void List_Events_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (List_Events.SelectedItem != null)
            {
                int i = List_Events.SelectedIndex;
                TextBox1.Text = Events[i].Date.ToString("dd/MM/yyyy H:mm");
            }
            else
            {
                TextBox1.Text = "Wybierz wydarzenie, aby wyswietlic date";
            }
        }
        private void Delete_Event_Click(object sender, RoutedEventArgs e)
        {
            int i = List_Events.SelectedIndex;
            Events.RemoveAt(i);
            List_Events.Items.Refresh();
        }

        private void Sort_Events_Click(object sender, RoutedEventArgs e)
        {
            Sortowanie_Events();
            List_Events.Items.Refresh();
        }

        private void Gen_Ical_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    public class Event
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}

//Text="{Binding SelectedItem.Date, ElementName=List_Events}"