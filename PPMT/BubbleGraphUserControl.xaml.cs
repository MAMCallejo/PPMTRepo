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
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class BubbleGraphUserControl : UserControl
    {

        private double resourceWeight = 0.1;
        private double dataWeight = 0.15;
        private double vendorsWeight = 0.2;
        private double sponsorshipWeight = 0.3;
        private double implementationWeight = 0.25;
        private double valueWeight = 0.3;
        private double transformativeGWeight = 0.3;
        private double hrPriorityWeight = 0.3;
        private double riskWeight = 0.1;

        private Dictionary<Tuple<double, double>, string> BubbleLabels;

        public Dictionary<Tuple<double, double>, ArrayList> BubbleList;

        //public Dictionary<int, Project> JSONList;

        public List<Project> JSONList;

        public static BubbleGraphUserControl BubbleGraph;

        public BubbleGraphUserControl()
        {
            InitializeComponent();

            BubbleGraph = this;

            BubbleLabels = new Dictionary<Tuple<double, double>, string>();

            BubbleList = new Dictionary<Tuple<double, double>, ArrayList>();

            //JSONList = new Dictionary<int, Project>();

            JSONList = new List<Project>();

            SeriesCollection = new SeriesCollection
            {
                new ScatterSeries
                {

                    Title = "Transformative Growth", // Light Blue

                    Values = new ChartValues<ScatterPoint>{},

                    MinPointShapeDiameter = 45,

                    MaxPointShapeDiameter = 85,

                    DataLabels = false,

                    LabelPoint = point => BubbleLabels[new Tuple<double, double>(point.X, point.Y)],

                    Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#72bcd4")),

                    Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2b3559")),

                    StrokeThickness = 2,

                },

                new ScatterSeries
                {

                    Title = "Quality of Talent", // Light Yellow

                    Values = new ChartValues<ScatterPoint>{},

                    MinPointShapeDiameter = 45,

                    MaxPointShapeDiameter = 85,

                    DataLabels = false,

                    LabelPoint = point => BubbleLabels[new Tuple<double, double>(point.X, point.Y)],

                    Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#fff67d")),

                    Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2b3559")),

                    StrokeThickness = 2,

                },

                new ScatterSeries
                {

                    Title = "Measurement and KPI", // Light Red

                    Values = new ChartValues<ScatterPoint>{},

                    MinPointShapeDiameter = 45,

                    MaxPointShapeDiameter = 85,

                    DataLabels = false,

                    LabelPoint = point => BubbleLabels[new Tuple<double, double>(point.X, point.Y)],

                    Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f56566")),

                    Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2b3559")),

                    StrokeThickness = 2,

                },

                new ScatterSeries
                {

                    Title = "ONA", // Dark Blue

                    Values = new ChartValues<ScatterPoint>{},

                    MinPointShapeDiameter = 45,

                    MaxPointShapeDiameter = 85,

                    DataLabels = false,

                    LabelPoint = point => BubbleLabels[new Tuple<double, double>(point.X, point.Y)],

                    Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00008b")),

                    Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2b3559")),

                    StrokeThickness = 2,

                },

                new ScatterSeries
                {

                    Title = "Consolidation/Cost", // Light Orange

                    Values = new ChartValues<ScatterPoint>{},

                    MinPointShapeDiameter = 45,

                    MaxPointShapeDiameter = 85,

                    DataLabels = false,

                    LabelPoint = point => BubbleLabels[new Tuple<double, double>(point.X, point.Y)],

                    Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f9953f")),

                    Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2b3559")),

                    StrokeThickness = 2,

                },

                new ScatterSeries
                {

                    Title = "Other", // Gray

                    Values = new ChartValues<ScatterPoint>{},

                    MinPointShapeDiameter = 45,

                    MaxPointShapeDiameter = 85,

                    DataLabels = false,

                    LabelPoint = point => BubbleLabels[new Tuple<double, double>(point.X, point.Y)],

                    Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9d9d9d")),

                    Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2b3559")),

                    StrokeThickness = 2,

                }

            };

            DataContext = this;

            //Read the data from existing files
            readFile();

        }
        

        public SeriesCollection SeriesCollection { get; set; }



        // Calculate Complexity:

        private Double calculateXCoord(Double resource, Double data, Double vendors, Double sponsorship, Double implementation)
        {

            return ((resource * resourceWeight) + (data * dataWeight) + (vendors * vendorsWeight) + (sponsorship * sponsorshipWeight) + (implementation * implementationWeight));

        }

        // Calculate Impact:

        private Double calculateYCoord(Double value, Double transformativeG, Double hrPriority, Double risk)
        {

            return ((value * valueWeight) + (transformativeG * transformativeGWeight) + (hrPriority * hrPriorityWeight) + (risk * riskWeight));

        }

        public void editProject(ScatterPoint oldBubble, Tuple<double, double> t, Project project)
        {

            var XCoord = calculateXCoord(project.resource, project.data, project.vendors, project.sponsorship, project.implementation);

            var YCoord = calculateYCoord(project.value, project.transformativeG, project.hrpriority, project.risk);

            var newBubble = new ScatterPoint(XCoord, YCoord, project.value);

            var newTuple = new Tuple<double, double>(XCoord, YCoord);

            string newLabel = project.pName;

            if(BubbleList[t].Count == 1)
            {
                BubbleList.Remove(t);
            }
            else
            {
                int count = 0;
                foreach(Project bubble in BubbleList[t])
                {
                    if(bubble.pName == project.pName)
                    {
                        BubbleList[t].RemoveAt(count);
                    }
                    count++;
                }
            }

           // BubbleList.Remove(t);

            BubbleLabels.Remove(t);

            BubbleLabels.Add(newTuple, newLabel);

            ArrayList listOfProjects = new ArrayList();

            listOfProjects.Add(project);

            BubbleList.Add(newTuple, listOfProjects);

            foreach (var series in SeriesCollection)
            {

                if (series.Title.Equals(project.projectCategory))
                {

                   for (int i = 0; i < series.Values.Count; i++)
                    {

                        ScatterPoint seriesValue = (ScatterPoint) series.Values[i];

                        if (seriesValue.X == oldBubble.X && seriesValue.Y == oldBubble.Y && seriesValue.Weight == oldBubble.Weight)
                        {

                            series.Values[i] = newBubble;

                        }

                    }

                }

            }

        }

        public void deleteProject(ScatterPoint oldBubble, Tuple<double, double> t, Project project)
        {

            BubbleList.Remove(t);

            foreach (var series in SeriesCollection)
            {

                if (series.Title.Equals(project.projectCategory))
                {

                    for (int i = 0; i < series.Values.Count; i++)
                    {

                        ScatterPoint seriesValue = (ScatterPoint) series.Values[i];

                        if (seriesValue.X == oldBubble.X && seriesValue.Y == oldBubble.Y && seriesValue.Weight == oldBubble.Weight)
                        {

                            series.Values.RemoveAt(i);

                        }

                    }

                }

            }
           
        }

        //Creates a new project on the graph
        public void AddNewProject(Project newProject)
        {

            foreach (var series in SeriesCollection)
            {

                if (series.Title.Equals(newProject.projectCategory))
                {

                    var XCoord = calculateXCoord(newProject.resource, newProject.data, newProject.vendors, newProject.sponsorship, newProject.implementation);

                    var YCoord = calculateYCoord(newProject.value, newProject.transformativeG, newProject.hrpriority, newProject.risk);

                    var newBubble = new ScatterPoint(XCoord, YCoord, newProject.value);

                    var newTuple = new Tuple<double, double>(XCoord, YCoord);

                    if (BubbleLabels.ContainsKey(newTuple))
                    {

                        string newLabel = newProject.pName;

                        //string oldLabel = BubbleLabels[newTuple];

                        //BubbleLabels[newTuple] = oldLabel + Environment.NewLine + newLabel;

                        BubbleLabels[newTuple] = newLabel;

                        ArrayList listOfProjects = (ArrayList) BubbleList[newTuple];

                        listOfProjects.Add(newProject);

                        BubbleList[newTuple] = listOfProjects;

                    }
                    else
                    {

                        string newLabel = newProject.pName;

                        BubbleLabels.Add(newTuple, newLabel);

                        ArrayList listOfProjects = new ArrayList();

                        listOfProjects.Add(newProject);

                        BubbleList.Add(newTuple, listOfProjects);

                    }

                    series.Values.Add(newBubble);

                }
            }
        }


        //Reads the json file and initializes the list with items
        public void readFile()
        {
            using(StreamReader r = new StreamReader("projects.json"))
            {
                string json = r.ReadToEnd();

                List<Project> fileList = JsonConvert.DeserializeObject<List<Project>>(json);

                if (fileList != null)
                {

                    foreach (var project in fileList)
                    {

                        AddNewProject(project);

                        JSONList.Add(project);

                        //JSONList[project.index] = project;

                    }

                }

            }

        }

        //Opens edit window when bubble is clicked
        private void ChartOnDataClick(object sender, ChartPoint chartPoint)
        {
           // MainWindow.mainAccess.slideControls();
            
            var bubbleTuple = new Tuple<double, double>(chartPoint.X, chartPoint.Y);

            ArrayList bubbleProj = (ArrayList)BubbleList[bubbleTuple];

            Project primaryProject = (Project) bubbleProj[0];

            int index = 0;

            for(int i = 0; i < JSONList.Count; i++)
            {
                if(JSONList[i].pName == primaryProject.pName)
                {
                    index = i;
                    break;
                }
            }

            Edit subWindow = new Edit(index, bubbleTuple, primaryProject, bubbleProj);
            subWindow.Show();

            Console.WriteLine("This is the project you clicked on:" + primaryProject.pName);
        }
    }
}
