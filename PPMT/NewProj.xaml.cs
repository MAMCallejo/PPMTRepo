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
using System.Windows.Shapes;

namespace PPMT
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class NewProj : Window
    {
        public NewProj()
        {
            InitializeComponent();
            Main.Content = new Start();
        }

        private void BtnClickStart(object sender, RoutedEventArgs e)
        {
            Main.Content = new Start();
        }

        private void BtnClickImpact(object sender, RoutedEventArgs e)
        {
            Main.Content = new ImpactCriteria();
        }

        private void BtnClickComplexity(object sender, RoutedEventArgs e)
        {
            Main.Content = new ComplexityCriteria();
        }

    }
}
