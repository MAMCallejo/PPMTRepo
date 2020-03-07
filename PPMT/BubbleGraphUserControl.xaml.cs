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

        public List<Project> JSONList;

        public static BubbleGraphUserControl BubbleGraph;

        public BubbleGraphUserControl()
        {
            InitializeComponent();

            BubbleGraph = this;

            BubbleLabels = new Dictionary<Tuple<double, double>, string>();

            BubbleList = new Dictionary<Tuple<double, double>, ArrayList>();

            JSONList = new List<Project>();

            SeriesCollection = new SeriesCollection
            {
                new ScatterSeries
                {

                    Title = "MainSeries",

                    Values = new ChartValues<ScatterPoint>{},

                    MinPointShapeDiameter = 45,

                    MaxPointShapeDiameter = 85,

                    DataLabels = false,

                    LabelPoint = point => BubbleLabels[new Tuple<double, double>(point.X, point.Y)],

                    Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#72bcd4")),

                    Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2b3559")),

                    StrokeThickness = 2,

                },

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

        //Creates a new project on the graph
        public void AddNewProject(Project newProject)
        {

            foreach (var series in SeriesCollection)
            {

                if (series.Title.Equals("MainSeries"))
                {

                    var XCoord = calculateXCoord(newProject.resource, newProject.data, newProject.vendors, newProject.sponsorship, newProject.implementation);

                    var YCoord = calculateYCoord(newProject.value, newProject.transformativeG, newProject.hrpriority, newProject.risk);

                    var newBubble = new ScatterPoint(XCoord, YCoord, newProject.value);

                    var newTuple = new Tuple<double, double>(XCoord, YCoord);

                    if (BubbleLabels.ContainsKey(newTuple))
                    {

                        string newLabel = newProject.index+1 + ". " + newProject.pName;

                        string oldLabel = BubbleLabels[newTuple];

                        BubbleLabels[newTuple] = oldLabel + Environment.NewLine + newLabel;

                        ArrayList listOfProjects = (ArrayList) BubbleList[newTuple];

                        listOfProjects.Add(newProject);

                        BubbleList[newTuple] = listOfProjects;

                    }
                    else
                    {

                        string newLabel = newProject.index+1 + ". " + newProject.pName;

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

                    }

                }

            }

        }

        //Opens slide out menu when a bubble is clicked
        private void ChartOnDataClick(object sender, ChartPoint chartPoint)
        {
            MainWindow.mainAccess.slideControls();
        }
    }
}
