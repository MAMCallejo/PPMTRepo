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

namespace PPMT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void newProject(object sender, RoutedEventArgs e)
        {
            /*
            //How to open in a certain area
            Main.Content = new ImpactCriteria();
            */

            NewProj subWindow = new NewProj();
            subWindow.Owner = this;
            subWindow.Show();
        }

        private void slideOut(object sender, RoutedEventArgs e)
        {
            if(scrollOut.IsVisible == true)
            {
                scrollOut.Visibility = Visibility.Collapsed;
            }
            else
            {
                scrollOut.Visibility = Visibility.Visible;
            }

            if(scrollOut.IsVisible == true)
            {
                Grid.SetColumnSpan(graphSize, 1);
            }
            else
            {
                Grid.SetColumnSpan(graphSize, 3);
            }

        }
    }
}
