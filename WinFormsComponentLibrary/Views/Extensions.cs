using System.Collections.Generic;
using System.Windows.Forms;

namespace WCL.Views
{
    public static class Extensions
    {
        public static IEnumerable<Control> GetControls(this Control self)
        {
            var controls = self.Controls;
            for (var i = 0; i < controls.Count; i++)
            {
                yield return controls[i];
            }
        }

        public static IList<Control> GetControlsRecursively(this Control self)
        {
            var result = new List<Control>();
            var controls = self.Controls;
            for (var i = 0; i < controls.Count; i++)
            {
                result.Add(controls[i]);
                result.AddRange(GetControlsRecursively(controls[i]));
            }
            return result;
        }

        public static bool SelectNextControl(this Control parent, Control basis)
        {
            var begin = parent.GetNextControl(basis, true);
            var target = begin;
            while (target == null || target.CanSelect == false || target.TabStop == false)
            {
                target = parent.GetNextControl(target, true);
                if (begin?.Equals(target) == true)
                {
                    break;
                }
            }
            if (target == null)
            {
                return false;
            }
            target.Select();
            return true;
        }
    }
}
