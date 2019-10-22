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
using Microsoft.Win32;


namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Events = new List<Event> { };
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
                else if (i % 3 == 2)
                {
                    Events.Add(new Event { Name = tmp.Name, Date = tmp.Date });
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

                MessageBox.Show(Events[i].Date.ToString("dd/MM/yyyy HH:mm:ss"));
                //MessageBox.Show(List_Events.SelectedItem.ToString());
            }
        }
        private void List_Events_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (List_Events.SelectedItem != null)
            {
                int i = List_Events.SelectedIndex;
                TextBox1.Text = Events[i].Date.ToString("dd/MM/yyyy HH:mm:ss");
            }
            else
            {
                TextBox1.Text = "Wybierz wydarzenie, aby wyswietlic date";
            }
        }
        private void Delete_Event_Click(object sender, RoutedEventArgs e)
        {
            int i = List_Events.SelectedIndex;
            string temp = Events[i].Name;
 
            string path = @"e:\repos\WpfApp1\WpfApp1\events.txt";
            string s;
            string[] Lines = File.ReadAllLines(path);
            File.Delete(path);// Deleting the file
            StreamWriter sw = new StreamWriter(path, true);
            int j;
            int k = 1;
            for (j = 0; j < ((Events.Count())*3 - 1); j++)
            {
                if (String.Equals(Lines[j], temp))
                {
                    MessageBox.Show(Lines[j]);
                    j += 3;
                }
                if (j < ((Events.Count()) * 3 - 1))
                    sw.Write(Lines[j] + "\n");
                else { };
            }
            sw.Close();
            Events.RemoveAt(i);
            List_Events.Items.Refresh();
            MessageBox.Show(Events.Count().ToString());
                
            
        }
        private void Sort_Events_Click(object sender, RoutedEventArgs e)
        {
            Sortowanie_Events();
            List_Events.Items.Refresh();
        }

        private void Gen_ICal_Click(object sender, RoutedEventArgs e)
        {
            int i = List_Events.SelectedIndex;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "iCalendar (*.ics)|*.ics";
            saveFileDialog1.ShowDialog();
            StreamWriter iCal = new StreamWriter(saveFileDialog1.FileName);
            iCal.Write("BEGIN:VCALENDAR\n");
            iCal.Write("VERSION:2.0\n");
            iCal.Write("PRODID:Kamil\n");
            iCal.Write("X - WR - TIMEZONE:Europe / Warsaw\n");
            iCal.Write("BEGIN:VEVENT\n");
            iCal.Write("DTSTART:" + Events[i].Date.Year.ToString("0000") + Events[i].Date.Month.ToString("00") + Events[i].Date.Day.ToString("00") + "T"
                + (Events[i].Date.Hour-1).ToString("00") + Events[i].Date.Minute.ToString("00") + Events[i].Date.Second.ToString("00") + "Z\n");
            iCal.Write("DTEND:" + Events[i].Date.Year.ToString("0000") + Events[i].Date.Month.ToString("00") + Events[i].Date.Day.ToString("00") + "T"
                + (Events[i].Date.Hour-1).ToString("00") + (Events[i].Date.Minute + 5).ToString("00") + Events[i].Date.Second.ToString("00") + "Z\n");
            iCal.Write("SUMMARY:" + Events[i].Name+"\n");
            iCal.Write("END:VEVENT\n");
            iCal.Write("END:VCALENDAR\n");
            iCal.Close();
        }

        private void Gen_ICal__All_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "iCalendar (*.ics)|*.ics";
            saveFileDialog1.ShowDialog();
            StreamWriter iCal = new StreamWriter(saveFileDialog1.FileName);
            iCal.Write("BEGIN:VCALENDAR\n");
            iCal.Write("VERSION:2.0\n");
            iCal.Write("PRODID:Kamil\n");
            iCal.Write("X - WR - TIMEZONE:Europe / Warsaw\n");
            for (int j = 0; j < Events.Count; j++)
            {
                iCal.Write("BEGIN:VEVENT\n");
                iCal.Write("DTSTART:" + Events[j].Date.Year.ToString("0000") + Events[j].Date.Month.ToString("00") + Events[j].Date.Day.ToString("00") + "T"
                    + (Events[j].Date.Hour - 1).ToString("00") + Events[j].Date.Minute.ToString("00") + Events[j].Date.Second.ToString("00") + "Z\n");
                iCal.Write("DTEND:" + Events[j].Date.Year.ToString("0000") + Events[j].Date.Month.ToString("00") + Events[j].Date.Day.ToString("00") + "T"
                    + (Events[j].Date.Hour - 1).ToString("00") + (Events[j].Date.Minute + 5).ToString("00") + Events[j].Date.Second.ToString("00") + "Z\n");
                iCal.Write("SUMMARY:" + Events[j].Name + "\n");
                iCal.Write("END:VEVENT\n");
            }
            iCal.Write("END:VCALENDAR\n");
            iCal.Close();
        }
    }

    public class Event
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}

//Text="{Binding SelectedItem.Date, ElementName=List_Events}"