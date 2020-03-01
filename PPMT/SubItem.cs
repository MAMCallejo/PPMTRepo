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
            pSponsor = p.pSponsor;

            rDate = p.rDate;

            pDeadline = p.pDeadline;

            pSavings = p.pSavings;

            value = p.value;

            transformativeG = p.transformativeG;

            hrpriority = p.hrpriority;

            risk = p.risk;

            resource = p.resource;

            data = p.data;

            vendors = p.vendors;

            sponsorship = p.sponsorship;

            implementation = p.implementation;

            Screen = screen;
        }

        public UserControlMenuItem Screen { get; private set; }
        public string pSponsor { get; set; }
        public string rDate { get; set; }
        public string pDeadline { get; set; }
        public int pSavings { get; set; }
        public double value { get; set; }
        public double transformativeG { get; set; }
        public double hrpriority { get; set; }
        public double risk { get; set; }
        public double resource { get; set; }
        public double data { get; set; }
        public double vendors { get; set; }
        public double sponsorship { get; set; }
        public double implementation { get; set; }

    }
}
