using System;
using System.Collections.Generic;
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

        private Double resourceWeight = 0.1;
        private Double dataWeight = 0.15;
        private Double vendorsWeight = 0.2;
        private Double sponsorshipWeight = 0.3;
        private Double implementationWeight = 0.25;
        private Double valueWeight = 0.3;
        private Double transformativeGWeight = 0.3;
        private Double hrPriorityWeight = 0.3;
        private Double riskWeight = 0.1;

        private Dictionary<Tuple<double, double>, string> BubbleLabels;

        private Dictionary<Tuple<double, double>, int> BubbleLabelInstances;

        public static BubbleGraphUserControl BubbleGraph;

        public BubbleGraphUserControl()
        {
            InitializeComponent();

            BubbleGraph = this;

            BubbleLabels = new Dictionary<Tuple<double, double>, string>();

            BubbleLabelInstances = new Dictionary<Tuple<double, double>, int>();

            SeriesCollection = new SeriesCollection
            {
                new ScatterSeries
                {
                    Title = "MainSeries",

                    Values = new ChartValues<ScatterPoint>{},

                    MinPointShapeDiameter = 45,

                    MaxPointShapeDiameter = 85,

                    DataLabels = true,

                    LabelPoint = point => BubbleLabels[new Tuple<double, double>(point.X, point.Y)]   

                },

            };

            DataContext = this;

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

                        BubbleLabelInstances[newTuple] = BubbleLabelInstances[newTuple] + 1;

                        BubbleLabels[newTuple] = BubbleLabelInstances[newTuple] + " projects";

                    }
                    else
                    {

                        BubbleLabels.Add(newTuple, newProject.pName);

                        BubbleLabelInstances.Add(newTuple, 1);

                    }

                    series.Values.Add(newBubble);

                }
            }
        }


        public void readFile()
        {
            using(StreamReader r = new StreamReader("projects.json"))
            {
                string json = r.ReadToEnd();

                List<Project> list = JsonConvert.DeserializeObject<List<Project>>(json);

                if (list != null)
                {
                    foreach (var project in list)
                    {
                        AddNewProject(project);
                    }
                }

            }
        }

    }
}
