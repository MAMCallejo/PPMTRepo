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
        private double valueWeight = 0.4;
        private double strategicAWeight = 0.4;
        //private double hrPriorityWeight = 0.3;
        private double riskWeight = 0.2;

        private Dictionary<Tuple<double, double>, string> BubbleLabels;

        private Dictionary<string, List<Project>> hiddenBubbles;

        public Dictionary<Tuple<double, double>, ArrayList> BubbleList;

        public List<Project> JSONList;

        public static BubbleGraphUserControl BubbleGraph;

        public BubbleGraphUserControl()
        {
            InitializeComponent();

            BubbleGraph = this;

            BubbleLabels = new Dictionary<Tuple<double, double>, string>();

            BubbleList = new Dictionary<Tuple<double, double>, ArrayList>();

            hiddenBubbles = new Dictionary<string, List<Project>>();

            hiddenBubbles.Add("Transformative Growth", null);

            hiddenBubbles.Add("Quality of Talent", null);

            hiddenBubbles.Add("Measurement and KPI", null);

            hiddenBubbles.Add("ONA", null);

            hiddenBubbles.Add("Consolidation/Cost", null);

            hiddenBubbles.Add("Other", null);

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

                    Foreground = Brushes.White,

                    FontSize = 15,

                    FontWeight = FontWeights.Bold,

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

                    Foreground = Brushes.White,

                    FontSize = 15,

                    FontWeight = FontWeights.Bold,

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

                    Foreground = Brushes.White,

                    FontSize = 15,

                    FontWeight = FontWeights.Bold,

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

                    Foreground = Brushes.White,

                    FontSize = 15,

                    FontWeight = FontWeights.Bold,

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

                    Foreground = Brushes.White,

                    FontSize = 15,

                    FontWeight = FontWeights.Bold,

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

                    Foreground = Brushes.White,

                    FontSize = 15,

                    FontWeight = FontWeights.Bold,

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

        /* private Double calculateYCoord(Double value, Double transformativeG, Double hrPriority, Double risk)
         {

             return ((value * valueWeight) + (transformativeG * transformativeGWeight) + (hrPriority * hrPriorityWeight) + (risk * riskWeight));

         }*/

        private Double calculateYCoord(Double value, Double strategicA, Double risk)
        {

            return ((value * valueWeight) + (strategicA * strategicAWeight)  + (risk * riskWeight));

        }

        public void deleteProject(ScatterPoint oldBubble, Tuple<double, double> t, Project project)
        {

            BubbleLabels.Remove(t);

            if (!BubbleList.ContainsKey(t))
            {

                int indexToDelete = -1;

                for (int i = 0; i < hiddenBubbles[project.projectCategory].Count; i++)
                {

                    Project hiddenProject = hiddenBubbles[project.projectCategory][i];

                    if (hiddenProject.pName.Equals(project.pName))
                    {

                        indexToDelete = i;

                    }

                }

                if (indexToDelete != -1)
                {

                    hiddenBubbles[project.projectCategory].RemoveAt(indexToDelete);

                }

                return;

            }

            if (BubbleList[t].Count == 1)
            {

                BubbleList.Remove(t);

            }
            else
            {

                string newLabel = "Project: ";

                int indexToRemove = 0;

                for (int i = 0; i < BubbleList[t].Count; i++)
                {

                    Project bubble = (Project) BubbleList[t][i];

                    if (bubble.pName == project.pName)
                    {

                        indexToRemove = i;

                    } else
                    {

                        if (newLabel.Equals("Project: "))
                        {

                            for (int j = 0; j < JSONList.Count; j++)
                            {

                                if (JSONList[j].pName.Equals(bubble.pName))
                                {

                                    newLabel = newLabel + (j+1);

                                }

                            }

                        }
                        else
                        {

                            string labelToAdd = "0";

                            for (int j = 0; j < JSONList.Count; j++)
                            {

                                if (JSONList[j].pName.Equals(bubble.pName))
                                {

                                    labelToAdd = (j+1).ToString();

                                }

                            }

                            if (((newLabel.ToCharArray().Count(c => c == ',')) + 1) == 3 || (newLabel.ToCharArray().Count(c => c == ',')) == 6)
                            {

                                newLabel = newLabel + ", " + Environment.NewLine + "\t" + labelToAdd;

                            }
                            else
                            {

                                newLabel = newLabel + ", " + labelToAdd;

                            }

                        }

                    }

                }

                BubbleList[t].RemoveAt(indexToRemove);

                BubbleLabels.Add(t, newLabel);

            }

            foreach (var series in SeriesCollection)
            {

                if (series.Title.Equals(project.projectCategory))
                {

                    for (int i = 0; i < series.Values.Count; i++)
                    {

                        ScatterPoint seriesValue = (ScatterPoint)series.Values[i];

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

                    //var YCoord = calculateYCoord(newProject.value, newProject.transformativeG, newProject.hrpriority, newProject.risk);

                    var YCoord = calculateYCoord(newProject.value, newProject.strategicA, newProject.risk);

                    var newBubble = new ScatterPoint(XCoord, YCoord, newProject.value);

                    var newTuple = new Tuple<double, double>(XCoord, YCoord);

                    if (BubbleLabels.ContainsKey(newTuple))
                    {

                        string newLabel = "0";

                        for (int i = 0; i < JSONList.Count; i++)
                        {

                            if (JSONList[i].pName.Equals(newProject.pName)) {

                                newLabel = (i+1).ToString();

                            }

                        }

                        string oldLabel = BubbleLabels[newTuple];

                        if ((BubbleList[newTuple].Count + 1) == 4 || (BubbleList[newTuple].Count + 1) == 7)
                        {

                            BubbleLabels[newTuple] = oldLabel + ", " + Environment.NewLine + "\t" + newLabel;

                        } else
                        {

                            BubbleLabels[newTuple] = oldLabel + ", " + newLabel;

                        }

                        BubbleList[newTuple].Add(newProject);

                    }
                    else
                    {

                        string newLabel = "Project: ";

                        for (int i = 0; i < JSONList.Count; i++)
                        {

                            if (JSONList[i].pName.Equals(newProject.pName)) {

                                newLabel = newLabel + (i+1);

                            }

                        }

                        BubbleLabels.Add(newTuple, newLabel);

                        ArrayList listOfProjects = new ArrayList();

                        listOfProjects.Add(newProject);

                        BubbleList.Add(newTuple, listOfProjects);

                    }

                    series.Values.Add(newBubble);

                }
            }
        }

        private string stringFormatter (string stringToFormat)
        {

            string formattedString = "";

            if (stringToFormat.Length <= 20)
            {

                formattedString = stringToFormat;

                return formattedString;

            }
            else if (stringToFormat.Length <= 40)
            {

                for (int i = 20; i < stringToFormat.Length; i++)
                {

                    if (stringToFormat[i] == ' ')
                    {

                        if (i == stringToFormat.Length-1)
                        {

                            formattedString = stringToFormat;

                        } else
                        {

                            formattedString = stringToFormat.Substring(0, i) + Environment.NewLine + stringToFormat.Substring(i+1);

                        }

                        return formattedString;

                    }

                }


            }
            else if (stringToFormat.Length <= 60)
            {

                int newLineIndex = -1;

                for (int i = 20; i < stringToFormat.Length; i++)
                {

                    if (stringToFormat[i] == ' ')
                    {

                        if (newLineIndex == -1)
                        {

                            newLineIndex = i;

                            i = 39;

                        } else 
                        {

                            if (i == stringToFormat.Length - 1)
                            {

                                formattedString = stringToFormat.Substring(0, newLineIndex) + Environment.NewLine + stringToFormat.Substring(newLineIndex + 1);

                            }
                            else
                            {

                                formattedString = stringToFormat.Substring(0, newLineIndex) + Environment.NewLine + stringToFormat.Substring(newLineIndex + 1, i - newLineIndex - 1) + Environment.NewLine + stringToFormat.Substring(i + 1);

                            }

                            Console.WriteLine(formattedString);

                            return formattedString;

                        }

                    }

                }

            }
            else
            {

                List<int> newLineIndexes = new List<int>();

                newLineIndexes.Add(-1);

                newLineIndexes.Add(-1);

                for (int i = 20; i < stringToFormat.Length; i++)
                {

                    if (stringToFormat[i] == ' ')
                    {

                        if (newLineIndexes[0] == -1)
                        {

                            newLineIndexes[0] = i;

                            i = 39;

                        }
                        else if (newLineIndexes[1] == -1)
                        {

                            newLineIndexes[1] = i;

                            i = 59;

                        } else
                        {

                            if (i == stringToFormat.Length - 1)
                            {

                                formattedString = stringToFormat.Substring(0, newLineIndexes[0]) + Environment.NewLine + stringToFormat.Substring(newLineIndexes[0] + 1, newLineIndexes[1] - newLineIndexes[0] - 1) + Environment.NewLine + stringToFormat.Substring(newLineIndexes[1] + 1);

                            }
                            else
                            {

                                formattedString = stringToFormat.Substring(0, newLineIndexes[0]) + Environment.NewLine + stringToFormat.Substring(newLineIndexes[0] + 1, newLineIndexes[1] - newLineIndexes[0] - 1) + Environment.NewLine + stringToFormat.Substring(newLineIndexes[1] + 1, i - newLineIndexes[1] - 1) + Environment.NewLine + stringToFormat.Substring(i + 1);

                            }

                            return formattedString;

                        }

                    }

                }

            }

            return formattedString;

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

                        JSONList.Add(project);

                        AddNewProject(project);

                    }

                }

            }

        }

        //Opens edit window when bubble is clicked
        private void ChartOnDataClick(object sender, ChartPoint chartPoint)
        {
            
            var bubbleTuple = new Tuple<double, double>(chartPoint.X, chartPoint.Y);

            ArrayList bubbleProj = (ArrayList)BubbleList[bubbleTuple];

            Edit subWindow = new Edit(bubbleTuple, bubbleProj);

            subWindow.Show();

        }

        //This is where all of the toggle buttons are in order. When the button state is changed to checked this should happen
        private void labelChecked(object sender, RoutedEventArgs e)
        {

            labelsToggle(labelN.IsChecked ?? false);
        }

        private void tGChecked(object sender, RoutedEventArgs e)
        {

            categoriesToggle(tGN.IsChecked ?? true, "Transformative Growth");
        }

        private void qOTChecked(object sender, RoutedEventArgs e)
        {
            categoriesToggle(qOTN.IsChecked ?? true, "Quality of Talent");
        }

        private void mAKChecked(object sender, RoutedEventArgs e)
        {
            categoriesToggle(mAKN.IsChecked ?? true, "Measurement and KPI");
        }

        private void oNAChecked(object sender, RoutedEventArgs e)
        {
            categoriesToggle(oNAN.IsChecked ?? true, "ONA");
        }

        private void cCChecked(object sender, RoutedEventArgs e)
        {
            categoriesToggle(cCN.IsChecked ?? true, "Consolidation/Cost");
        }

        private void otherChecked(object sender, RoutedEventArgs e)
        {
            categoriesToggle(otherN.IsChecked ?? true, "Other");
        }

        public void categoriesToggle(bool categoryToggled, string categoryName)
        {

            // Show series:
            if (categoryToggled)
            {

                if (hiddenBubbles[categoryName] != null)
                {

                    foreach (Project project in hiddenBubbles[categoryName])
                    {

                        AddNewProject(project);

                    }

                    hiddenBubbles[categoryName] = null;

                }

            } else // Hide Series:
            {

                if (SeriesCollection != null)
                {

                    foreach (ScatterSeries series in SeriesCollection)
                    {

                        if (series.Title.Equals(categoryName))
                        {

                            List<Tuple<double, double>> pointsVisited = new List<Tuple<double, double>>();

                            foreach(ScatterPoint bubbleToHide in series.Values)
                            {

                                var tToHide = new Tuple<double, double>(bubbleToHide.X, bubbleToHide.Y);

                                if (!pointsVisited.Contains(tToHide))
                                {

                                    var bubbleProjects = BubbleList[tToHide];

                                    List<Project> projectsToHide = new List<Project>();

                                    foreach (Project projectToHide in bubbleProjects)
                                    {

                                        if (projectToHide.projectCategory.Equals(categoryName))
                                        {
                                            projectsToHide.Add(projectToHide);

                                        }

                                    }

                                    foreach (Project projectToHide in projectsToHide)
                                    {

                                        deleteProject(bubbleToHide, tToHide, projectToHide);

                                        if (hiddenBubbles[categoryName] == null)
                                        {

                                            hiddenBubbles[categoryName] = new List<Project>();

                                        }

                                        hiddenBubbles[categoryName].Add(projectToHide);

                                    }

                                    pointsVisited.Add(tToHide);

                                }

                            }
                            

                        }

                    }

                }

            }

        }

        private void labelsToggle(bool labelsToggled)
        {

            // Show Labels.
            if (labelsToggled)
            {

                BubbleChart.DataTooltip = null;

                foreach (ScatterSeries series in SeriesCollection)
                {

                    series.DataLabels = true;

                }

            } else // Hide Labels.
            {

                DefaultTooltip newTooltip = new DefaultTooltip();

                newTooltip.ShowSeries = false;

                newTooltip.Background = Brushes.DimGray;

                newTooltip.Foreground = Brushes.White;

                newTooltip.FontSize = 15;

                newTooltip.FontWeight = FontWeights.Bold;

                BubbleChart.DataTooltip = newTooltip;

                foreach (ScatterSeries series in SeriesCollection)
                {

                    series.DataLabels = false;

                }

            }

        }

    }
}
