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
        public static MainWindow wind;

        public static MainWindow mainAccess;

        public int counter;

        public MainWindow()
        {
            wind = this;

            counter = 0;

            InitializeComponent();

            mainAccess = this;

            createSlideProj();
        }

        //Open window to get user data
        private void newProject(object sender, RoutedEventArgs e)
        {

            NewProj subWindow = new NewProj();
            subWindow.Owner = this;
            subWindow.Show();
        }

        //Controls the slide out window that contains the list
        private void slideOut(object sender, RoutedEventArgs e)
        {
            slideControls();
        }


        public void slideControls()
        {
            if (scrollOut.IsVisible == true)
            {
                scrollOut.Visibility = Visibility.Collapsed;
            }
            else
            {
                scrollOut.Visibility = Visibility.Visible;
            }

            if (scrollOut.IsVisible == true)
            {
                Grid.SetColumnSpan(graphSize, 1);
            }
            else
            {
                Grid.SetColumnSpan(graphSize, 3);
            }
        }

        //Initialize slide out menu and content
        public void createSlideProj()
        {
            List<Project> list = PPMT.BubbleGraphUserControl.BubbleGraph.list;

            int length = list.Count;

            for (int i = 0; i < length; i++)
            {
                counter++;
                var projMenu = new List<SubItem>();
                projMenu.Add(new SubItem(list[i]));
                var proj1 = new ItemMenu(list[i].pName, projMenu, i);

                Menu.Children.Add(new UserControlMenuItem(proj1));
            }
        }
    }
}
