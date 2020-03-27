using System;
using System.Drawing;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Newtonsoft.Json;

namespace PPMT
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Edit : Window
    {
        public static Edit edit;

        Tuple<double, double> tup;

        Project project;

        ScatterPoint oldBubble;

        ProjectSelectionPage tempPage;

        public Edit(Tuple<double, double> t, ArrayList bubbleProject)
        {

            InitializeComponent();

            //Creates project selection page if there is more than one project otherwise just do the normal constructor
            tempPage = new ProjectSelectionPage(bubbleProject);

            if (bubbleProject.Count > 1)
            {
                edit = this;
                tup = t;
                Main.Content = tempPage;
            }
            else
            {

                project = (Project) bubbleProject[0];

                oldBubble = new ScatterPoint(t.Item1, t.Item2, project.value);

                n.Text = project.pName;
                edit = this;
                tup = t;


                val1.Value = project.value;
                val2.Value = project.transformativeG;
                val3.Value = project.hrpriority;
                val4.Value = project.risk;
                val5.Value = project.resource;
                val6.Value = project.data;
                val7.Value = project.vendors;
                val8.Value = project.sponsorship;
                val9.Value = project.implementation;
                

                
            }

        }

        public void multInitialize(Project proj)
        {
            oldBubble = new ScatterPoint(tup.Item1, tup.Item2, proj.value);

            Main.Content = null;

            n.Text = proj.pName;
            project = proj;

            val1.Value = proj.value;
            val2.Value = proj.transformativeG;
            val3.Value = proj.hrpriority;
            val4.Value = proj.risk;
            val5.Value = proj.resource;
            val6.Value = proj.data;
            val7.Value = proj.vendors;
            val8.Value = proj.sponsorship;
            val9.Value = proj.implementation;


            Console.WriteLine(proj.pName);
        }


        private void saveButtonClicked(object sender, RoutedEventArgs e)
        {

            int index = 0;

            for (int i = 0; i < PPMT.BubbleGraphUserControl.BubbleGraph.JSONList.Count; i++)
            {
                if (PPMT.BubbleGraphUserControl.BubbleGraph.JSONList[i].pName == project.pName)
                {
                    index = i;
                    break;
                }
            }

            PPMT.BubbleGraphUserControl.BubbleGraph.deleteProject(oldBubble, tup, project);

            project.pName = n.Text;

            project.value = val1.Value;

            project.transformativeG = val2.Value;

            project.hrpriority = val3.Value;

            project.risk = val4.Value;

            project.resource = val5.Value;

            project.data = val6.Value;

            project.vendors = val7.Value;

            project.sponsorship = val8.Value;

            project.implementation = val9.Value;

            PPMT.BubbleGraphUserControl.BubbleGraph.AddNewProject(project);

            PPMT.BubbleGraphUserControl.BubbleGraph.JSONList[index] = project;

            PPMT.MainWindow.mainAccess.createSlideProj();

            string docPath = Directory.GetCurrentDirectory();

            string convertedJson = JsonConvert.SerializeObject(PPMT.BubbleGraphUserControl.BubbleGraph.JSONList, Formatting.Indented);

            File.WriteAllText((System.IO.Path.Combine(docPath, "projects.json")), convertedJson);

            edit.Close();
        }

        private void deleteButtonClicked(object sender, RoutedEventArgs e)
        {

            int index = 0;

            for (int i = 0; i < PPMT.BubbleGraphUserControl.BubbleGraph.JSONList.Count; i++)
            {
                if (PPMT.BubbleGraphUserControl.BubbleGraph.JSONList[i].pName == project.pName)
                {
                    index = i;
                    break;
                }
            }

            PPMT.BubbleGraphUserControl.BubbleGraph.deleteProject(oldBubble, tup, project);

            PPMT.BubbleGraphUserControl.BubbleGraph.JSONList.RemoveAt(index);

            PPMT.MainWindow.mainAccess.createSlideProj();

            string docPath = Directory.GetCurrentDirectory();

            string convertedJson = JsonConvert.SerializeObject(PPMT.BubbleGraphUserControl.BubbleGraph.JSONList, Formatting.Indented);

            File.WriteAllText((System.IO.Path.Combine(docPath, "projects.json")), convertedJson);

            edit.Close();

        }

    }
}
