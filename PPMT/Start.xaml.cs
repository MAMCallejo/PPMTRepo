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
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Start : Page
    {
        public Start()
        {
            InitializeComponent();
        }

        string n;
        string s;
        string d;
        string dead;
        string sav;
        int parsedSav;
        string projectCategory;

        ImpactCriteria pg = new ImpactCriteria();

        private void okButtonClicked(object sender, RoutedEventArgs e)
        {
            n = name.Text;
            s = sponsor.Text;
            d = date.Text;
            dead = deadline.Text;
            sav = savings.Text;
            parsedSav = int.Parse(sav);
            projectCategory = (string) cmbCategory.SelectedItem;

            App.Current.Properties["projectName"] = n;
            App.Current.Properties["projectSponsor"] = s;
            App.Current.Properties["requestDate"] = d;
            App.Current.Properties["deadlineDate"] = dead;
            App.Current.Properties["savings"] = parsedSav;

            if (n.Length >= 5 && s.Length >= 5)
            {
                this.NavigationService.Navigate(pg);
            }
            else if (s.Length < 5 && n.Length < 5)
            {
                MessageBox.Show("The minimum length for the Project Sponsor and Project Name must include at least 5 characters");
            }
            else if (n.Length < 5)
            {
                MessageBox.Show("The minimum length for the Project Name must include at least 5 characters");
            }
            else if (s.Length < 5)
            {
                MessageBox.Show("The minimum length for the Project Sponsor must include at least 5 characters");
            }
        }

        private void cancelButtonClicked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Cancelled");
        }

        private void MaskNumericInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !TextIsNumeric(e.Text);
        }

        private void MaskNumericPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string input = (string)e.DataObject.GetData(typeof(string));
                if (!TextIsNumeric(input)) e.CancelCommand();
            }
            else
            {
                e.CancelCommand();
            }
        }

        private bool TextIsNumeric(string input)
        {
            return input.All(c => Char.IsDigit(c) || Char.IsControl(c));
        }
    }
}
