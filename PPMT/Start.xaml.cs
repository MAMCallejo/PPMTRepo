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
        string sav = "0";
        ulong parsedSav;
        string projectC;

        ImpactCriteria pg = new ImpactCriteria();

        private void okButtonClicked(object sender, RoutedEventArgs e)
        {
            n = name.Text;
            s = sponsor.Text;
            d = date.Text;
            dead = deadline.Text;
            sav = savings.Text;           
            projectC = cmbCategory.Text;

            if (sav == "" && n == "" && s == "")
            {
                MessageBox.Show("Please fill out the following: Project Name, Project Sponsor, Savings");
            }
            else if(sav == "" && n == "")
            {
                MessageBox.Show("Please fill out the following: Project Name, Savings");
            }
            else if(sav == "" && s == "")
            {
                MessageBox.Show("Please fill out the following: Project Sponsor, Savings");
            }
            else if(n == "" && s == "")
            {
                MessageBox.Show("Please fill out the following: Project Name, Project Sponsor");
            }
            else if (sav == "")
            {
                MessageBox.Show("Please fill out the following: Savings");
            }
            else if (n == "")
            {
                MessageBox.Show("Please fill out the following: Project Name");
            }
            else if (s == "")
            {
                MessageBox.Show("Please fill out the following: Project Sponsor");
            }
            else
            {


                
                App.Current.Properties["projectName"] = n;
                App.Current.Properties["projectSponsor"] = s;
                App.Current.Properties["requestDate"] = d;
                App.Current.Properties["deadlineDate"] = dead;
                App.Current.Properties["savings"] = parsedSav;
                App.Current.Properties["projectC"] = projectC;

                if (n.Length >= 5 && s.Length >= 5 && sav.Length <= 19)
                {
                    parsedSav = ulong.Parse(sav);
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
                else if(sav.Length > 19)
                {
                    MessageBox.Show("The Savings value is too high");
                }
                
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
