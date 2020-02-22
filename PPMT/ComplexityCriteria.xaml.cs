using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;
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
    /// Interaction logic for ComplexityCriteria.xaml
    /// </summary>
    public partial class ComplexityCriteria : Page
    {
        public ComplexityCriteria()
        {
            InitializeComponent();
        }

        double resource;
        double data;
        double vendors;
        double sponsorship;
        double implementation;

        private void nextButtonClicked(object sender, RoutedEventArgs e)
        {
            resource = (double)one.Content;
            data = (double)two.Content;
            vendors = (double)three.Content;
            sponsorship = (double)four.Content;
            implementation = (double)five.Content;

            App.Current.Properties["resource"] = resource;
            App.Current.Properties["data"] = data;
            App.Current.Properties["vendors"] = vendors;
            App.Current.Properties["sponsorship"] = sponsorship;
            App.Current.Properties["implementation"] = implementation;
            
            Project newProj = new Project()
            {
                pName = ((string)App.Current.Properties["projectName"]),
                pSponsor = ((string)App.Current.Properties["projectSponsor"]),
                rDate = ((string)App.Current.Properties["requestDate"]),
                pDeadline = ((string)App.Current.Properties["deadlineDate"]),
                pSavings = ((int)App.Current.Properties["savings"]),
                value = ((double)App.Current.Properties["value"]),
                strategic = ((double)App.Current.Properties["strategic"]),
                hrpriority = ((double)App.Current.Properties["hrpriority"]),
                risk = ((double)App.Current.Properties["risk"]),
                resource = ((double)App.Current.Properties["resource"]),
                data = ((double)App.Current.Properties["data"]),
                vendors = ((double)App.Current.Properties["vendors"]),
                sponsorship = ((double)App.Current.Properties["sponsorship"]),
                implementation = ((double)App.Current.Properties["implementation"])
            };
            string docPath = Directory.GetCurrentDirectory();

            //open file stream
            using (StreamWriter file = new StreamWriter(System.IO.Path.Combine(docPath, "projects.json"), true))
            {
                JsonSerializer serializer = new JsonSerializer();
                //serialize object directly into file stream
                serializer.Serialize(file, newProj);
            }
        }
    }

}

