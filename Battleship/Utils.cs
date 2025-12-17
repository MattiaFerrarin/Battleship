using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleship
{
    public static class Utils
    {
        public static Control GetSpecificControl(Form form, string name)
        {
            return form.Controls.Find(name, false).FirstOrDefault();
        }
        public static void ReplaceControl(Form form, Control control, string name)
        {
            var oldControl = form.Controls.Find(name, false).FirstOrDefault();
            if (oldControl != null) form.Controls.Remove(oldControl);
            form.Controls.Add(control);
        }
        public enum CellState
        {
            Hit,
            Sunk,
            Miss,
            Uncovered
        }
    }
}
