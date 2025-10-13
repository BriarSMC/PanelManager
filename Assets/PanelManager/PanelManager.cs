using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection;
using UnityEngine.Rendering;

public class PanelManager : MonoBehaviour
{
    [SerializeReference] Panel initialPanel;

    public List<Panel> managedPanels;
    public List<Panel> panelStack;

    /**
     * Unity Methods
     **/

    void Awake()
    {
        managedPanels = new List<Panel>();  // Panel objects will add themselves to this list in no particular order
        panelStack = new List<Panel>();
    }

    void Start()
    {
        /**
         * Die if no managed panels exist.
         * If initialPanel isn't set, then just use the first one in the managed panels.
         * Disable all panels.
         * Push the initial panel onto the stack.
         **/
        if (managedPanels.Count == 0) throw new ApplicationException("PanalManager: No panels found.");
        if (initialPanel == null) { initialPanel = managedPanels[0]; }
        TurnOffAllPanels();
        Push(initialPanel); // Put it on the stack
    }

    // managedPanel Methods

    public void TurnOffAllPanels() { foreach (Panel panel in managedPanels) { panel.gameObject.SetActive(false); } }
    public void TurnOffPanel(Panel panel) { panel.gameObject.SetActive(false); }
    public void TurnOnPanel(Panel panel) { panel.gameObject.SetActive(true); }

    public int AddManagedPanel(Panel panel)
    {
        Debug.Log($"{this.name}:{MethodBase.GetCurrentMethod().Name}> {panel.gameObject.name}");
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

    public int Push(string name) { return Push(FindManagedPanel(name)); }
    public int Push(Panel panel)
    {
        Debug.Log($"{this.name}:{MethodBase.GetCurrentMethod().Name}(Panel)> {panel?.PanelName}");

        /**
         * If the panel is in the managed list, then we can process it
         * We need to make the current panel (last one in the stake) disabled. (If it exists.)
         * Add the panel to the stack.
         * Enable it.
         * Return the stack index of the entry.
         *
         * Otherwise, bitch, whine and moan about it.
         **/
        if (FindManagedPanel(panel))
        {
            if (panelStack.Count > 0) TurnOffPanel(panelStack[panelStack.Count - 1]);
            panelStack.Add(panel);
            TurnOnPanel(panel);
            return panelStack.Count - 1;
        }
        else
        {
            throw new ApplicationException("Cannot Push Panel. Panel not found in managed list.");
        }
    }

    public int Pop() { return Pop(1); }
    public int Pop(int count)
    {
        /** 
         * If there are not enough panels on the stack, then we will just remove everything after the first panel.
         * Otherwise, remove count entries.
         **/
        TurnOffAllPanels();

        if (panelStack.Count <= 0) throw new ApplicationException("Panel stack is empty. Nothing to pop.");
        if (panelStack.Count == 1) return 0; ;

        if (count >= panelStack.Count)
            panelStack.RemoveRange(1, panelStack.Count - 1);
        else
            panelStack.RemoveRange(panelStack.Count - count, count);

        TurnOnPanel(panelStack[panelStack.Count - 1]);
        return panelStack.Count;
    }

    public int JumpTo(int index) { return 1; }
    public int JumpTo(Panel panel) { return 1; }

    public void Swap()
    {
        if (panelStack.Count <= 1) return;
        TurnOffAllPanels();
        Panel hold = panelStack[panelStack.Count - 1];
        panelStack[panelStack.Count - 1] = panelStack[panelStack.Count - 2];
        panelStack[panelStack.Count - 2] = hold;
        TurnOnPanel(panelStack[panelStack.Count - 1]);

    }

    public int GetStackPanelCount() { return panelStack.Count; }

    /** 
     * Private Methods
     **/

    private Panel FindManagedPanel(string name) { return managedPanels.SingleOrDefault(p => p.PanelName == name); }
    private Panel FindManagedPanel(Panel panel) { return managedPanels.SingleOrDefault(p => p.PanelObject == panel.gameObject); }
    private Panel FindManagedPanel(int i) { return managedPanels[Math.Clamp(i, 0, managedPanels.Count)]; }

}
