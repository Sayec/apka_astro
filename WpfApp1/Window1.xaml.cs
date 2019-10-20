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
            temp.Date = DateTime.Parse(Event_Date.ToString());
            // MainWindow test = new MainWindow();
            MainWindow.Events.Add(temp);
           // MainWindow.List_Events.Items.Refresh();
        }

    }
}
