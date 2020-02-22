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
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace PPMT
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class BubbleGraphUserControl : UserControl
    {
        public BubbleGraphUserControl()
        {
            InitializeComponent();

            SeriesCollection = new SeriesCollection
            {
                new ScatterSeries
                {
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

        private void btnTest(object sender, RoutedEventArgs e)
        {
        }

        private void ImpactPage(object sender, RoutedEventArgs e)
        {

        }
    }
}
