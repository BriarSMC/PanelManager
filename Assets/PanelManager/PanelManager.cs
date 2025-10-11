using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class PanelManager : MonoBehaviour
{
    public List<Panel> managedPanels;
    private List<Panel> panelStack;

    void Awake()
    {
        managedPanels = new List<Panel>();  // Panel objects will add themselves to this list in no particular order
        panelStack = new List<Panel>();
    }

    // managedPanel Methods

    public int AddManagedPanel(Panel panel)
    {
        if (IsPanelFound(panel)) { throw new ApplicationException($"Panel already exists. Not added. Name: {panel.PanelName}"); }
        managedPanels.Add(panel);
        return managedPanels.Count - 1;
    }

    public int GetManagedPanelCount() { return managedPanels.Count; }

    public int GetPanelStackCount() { return panelStack.Count; }

    public void RemoveFromManagedPanels(Panel panel) { }
    public void RemoveFromManagedPanels(int ndx) { }

    private bool IsPanelFound(Panel panel) { return managedPanels.SingleOrDefault(p => p.PanelObject == panel) != null ? true : false; }
    private bool IsPanelFound(GameObject panel) { return managedPanels.SingleOrDefault(p => p.PanelObject == panel) != null ? true : false; }
    private bool IsPanelFound(string name) { return managedPanels.SingleOrDefault(p => p.PanelName == name) != null ? true : false; }

    // panelStack Methods 

    public Panel GetStackPanel(int ndx) { return panelStack.Single(i => i.PanelIndex == ndx); }
    public Panel GetStackPanel(string name) { return panelStack.Single(n => n.PanelName == name); }
    public Panel GetStackPanel(GameObject obj) { return panelStack.Single(o => o.PanelObject); }

    public int Push(Panel panel) { return 0; }

    public int Pop() { return 1; }
    public int Pop(int count) { return count; }

    public int GetStackPanelCount() { return panelStack.Count; }

}
