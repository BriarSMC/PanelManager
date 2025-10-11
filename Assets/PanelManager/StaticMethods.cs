using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;


public partial class Panel : MonoBehaviour
{
    public static Panel GetPanel(int ndx) { return panelStack.Single(i => i.PanelIndex == ndx); }
    public static Panel GetPanel(string name) { return panelStack.Single(n => n.PanelName == name); }
    public static Panel GetPanel(GameObject obj) { return panelStack.Single(o => o.PanelObject); }

    public static int Push(Panel panel) { return 0; }

    public static int Pop() { return 1; }
    public static int Pop(int count) { return count; }

    public static void Remove(Panel panel) { }
    public static void Remove(int ndx) { }

    public static int GetCount() { return panelStack.Count; }


}
