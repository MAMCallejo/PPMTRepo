using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPMT
{
    public class SubItem
    {
        public SubItem(Project p, UserControlMenuItem screen = null)
        {
            pSponsor = "Sponsor: " + p.pSponsor;

            rDate = "Requested Date: " + p.rDate;

            pDeadline = "Deadline: " + p.pDeadline;

            pSavings = "Estimated Savings: " + p.pSavings;

            value = "Value: " + p.value;

            transformativeG = "Transformative Growth: " + p.transformativeG;

            hrpriority = "HR Priority: " + p.hrpriority;

            risk = "Risk: " + p.risk;

            resource = "Resources" + p.resource;

            data = "Data: " + p.data;

            vendors = "Vendors & Technology: " + p.vendors;

            sponsorship = "Sponsorship: " + p.sponsorship;

            implementation = "Implementation: " + p.implementation;

            category = "Category: " + p.projectCategory;

            Screen = screen;
        }

        public UserControlMenuItem Screen { get; private set; }
        public string pSponsor { get; set; }
        public string rDate { get; set; }
        public string pDeadline { get; set; }
        public string pSavings { get; set; }
        public string value { get; set; }
        public string transformativeG { get; set; }
        public string hrpriority { get; set; }
        public string risk { get; set; }
        public string resource { get; set; }
        public string data { get; set; }
        public string vendors { get; set; }
        public string sponsorship { get; set; }
        public string implementation { get; set; }
        public string category { get; set; }

    }
}
