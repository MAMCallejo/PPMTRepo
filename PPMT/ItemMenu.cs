using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;

namespace PPMT
{
    public class ItemMenu
    {
        public ItemMenu(string header, List<SubItem> subItems, int num)
        {
            LNum = num;
            Header = LNum + ". "+ header;
            SubItems = subItems;        
        }

        public ItemMenu(string header, UserControlMenuItem screen)
        {
            Header = header;
            Screen = screen;
        }

        public string Header { get; private set; }
        public List<SubItem> SubItems { get; private set; }
        public UserControlMenuItem Screen { get; private set; }
        public int LNum { get; private set; }
    }

}
