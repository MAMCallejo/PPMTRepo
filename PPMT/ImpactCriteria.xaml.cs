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
    /// Interaction logic for ImpactCriteria.xaml
    /// </summary>
    public partial class ImpactCriteria : Page
    {
        public ImpactCriteria()
        {
            InitializeComponent();
        }

        double value;
        double strategicAlignment;
        //double hrpriority;
        double risk;

        ComplexityCriteria pg = new ComplexityCriteria();

        private void nextButtonClick(object sender, RoutedEventArgs e)
        {
            value = (double)one.Content;
            strategicAlignment = (double)two.Content;
            //hrpriority = (double)three.Content;
            risk = (double)four.Content;

            App.Current.Properties["value"] = value;
            App.Current.Properties["strategicA"] = strategicAlignment;
            //App.Current.Properties["hrpriority"] = hrpriority;
            App.Current.Properties["risk"] = risk;
            
            
            this.NavigationService.Navigate(pg);
        }

        private void backBtn(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
