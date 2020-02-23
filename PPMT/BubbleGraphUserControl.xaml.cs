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

        public static BubbleGraphUserControl BubbleGraph;

        public BubbleGraphUserControl()
        {
            InitializeComponent();

            BubbleGraph = this;

            SeriesCollection = new SeriesCollection
            {
                new ScatterSeries
                {
                    Title = "MainSeries",

                    Values = new ChartValues<ScatterPoint>
                    {
                        new ScatterPoint(1.7, 1.7, 100),
                        new ScatterPoint(1.5, 0.3, 800),
                        new ScatterPoint(1, 1, 100),
                        new ScatterPoint(0.5, 0.7, 240),
                        new ScatterPoint(0.7,1.6,280),
                        new ScatterPoint(0.7,1.61,300)
                    },

                    MinPointShapeDiameter = 15,

                    MaxPointShapeDiameter = 45

                },

            };

            DataContext = this;
        }
        

        public SeriesCollection SeriesCollection { get; set; }

        private void UpdateAllOnClick(object sender, RoutedEventArgs e)
        {
            var r = new Random();
            foreach (var series in SeriesCollection)
            {
                foreach (var bubble in series.Values.Cast<ScatterPoint>())
                {
                    bubble.X = r.NextDouble() * 10;
                    bubble.Y = r.NextDouble() * 10;
                    bubble.Weight = r.NextDouble() * 10;
                }
            }
        }

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

                    series.Values.Add(new ScatterPoint(XCoord, YCoord, newProject.value));

                }

            }

        }

    }
}
