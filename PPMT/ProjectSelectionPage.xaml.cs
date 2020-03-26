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
using System.Collections;

namespace PPMT
{
    /// <summary>
    /// Interaction logic for ProjectSelectionPage.xaml
    /// </summary>
    public partial class ProjectSelectionPage : Page
    {
        public ProjectSelectionPage(ArrayList bubbleProj)
        {
            InitializeComponent();

            createButtons(bubbleProj);
        }

        //Creates a button for each project in the group
        public void createButtons(ArrayList bubbleProj)
        {

            for (int i = 0; i < bubbleProj.Count; i++)
            {
                Project tempProject = (Project)bubbleProj[i];

                Button button = new Button()
                {
                    Content = tempProject.pName,
                    Tag = tempProject
                };

                button.Click += new RoutedEventHandler(button_Click);
                this.grid.Children.Add(button);

            }

        }

        //Creating a button_click function for each generated button
        private void button_Click(object sender, RoutedEventArgs e)
        {
            Edit.edit.multInitialize((Project) (sender as Button).Tag);

        }
    }

}
