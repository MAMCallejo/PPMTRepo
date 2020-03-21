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
    public partial class Edit : Window
    {
        public static Edit edit;
        public static String name; 
        public Edit(String na)
        {
            InitializeComponent();

            edit = this;

            n.Content = na;
           
        }

        public void setName(String n)
        {
            name = n;
        }
    }
}
