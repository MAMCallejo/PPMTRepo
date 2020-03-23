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

        public static MainWindow mainAccess;

        public MainWindow()
        {

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
            Menu.Children.RemoveRange(1 , Menu.Children.Count - 1);

            //Dictionary<int, Project> SlideOutList = PPMT.BubbleGraphUserControl.BubbleGraph.JSONList;

            List<Project> SlideOutList = PPMT.BubbleGraphUserControl.BubbleGraph.JSONList;

            int length = SlideOutList.Count;

            /*foreach(KeyValuePair<int, Project> entry in SlideOutList)
            {
                var projMenu = new List<SubItem>();

                projMenu.Add(new SubItem(entry.Value));

                var newProj = new ItemMenu(entry.Value.pName, projMenu, entry.Value.index);

                Menu.Children.Add(new UserControlMenuItem(newProj));
            }*/

            for (int i = 0; i < length; i++)
            {

                var projMenu = new List<SubItem>();

                projMenu.Add(new SubItem(SlideOutList[i]));

                var newProj = new ItemMenu(SlideOutList[i].pName, projMenu, i);

                Menu.Children.Add(new UserControlMenuItem(newProj));

            }
        }
    }
}
