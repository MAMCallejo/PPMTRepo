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

        int index;

        Tuple<double, double> tup;

        Project project;

        ScatterPoint oldBubble;

        public Edit(int i, Tuple<double, double> t, Project proj)
        {
            InitializeComponent();

            oldBubble = new ScatterPoint(t.Item1, t.Item2, proj.value);

            n.Text = proj.pName;
            edit = this;
            index = i;
            tup = t;
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

        }

        private void saveButtonClicked(object sender, RoutedEventArgs e)
        {
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

            PPMT.BubbleGraphUserControl.BubbleGraph.JSONList[index] = project;

            PPMT.MainWindow.mainAccess.createSlideProj();

            string docPath = Directory.GetCurrentDirectory();

            string convertedJson = JsonConvert.SerializeObject(PPMT.BubbleGraphUserControl.BubbleGraph.JSONList, Formatting.Indented);

            File.WriteAllText((System.IO.Path.Combine(docPath, "projects.json")), convertedJson);

            PPMT.BubbleGraphUserControl.BubbleGraph.editProject(oldBubble, tup, project);

            edit.Close();
        }

        private void deleteButtonClicked(object sender, RoutedEventArgs e)
        {
            PPMT.BubbleGraphUserControl.BubbleGraph.JSONList.RemoveAt(index);
            PPMT.BubbleGraphUserControl.BubbleGraph.deleteProject(oldBubble, tup, project);
            PPMT.MainWindow.mainAccess.createSlideProj();

            string docPath = Directory.GetCurrentDirectory();

            string convertedJson = JsonConvert.SerializeObject(PPMT.BubbleGraphUserControl.BubbleGraph.JSONList, Formatting.Indented);

            File.WriteAllText((System.IO.Path.Combine(docPath, "projects.json")), convertedJson);
            edit.Close();


        }

    }
}
