using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class Panel
{
    private static int m_PanelCount = 0;
    public static List<Panel> m_PanelList = new List<Panel>();

    public GameObject PanelObject { get; private set; }
    public int PanelIndex { get; private set; }
    public string PanelName { get; private set; }

    /**
    * Constructors
    **/

    public Panel(GameObject panelObject, string panelName)
    {
        this.PanelObject = panelObject.gameObject;
        this.PanelName = panelName;
        this.PanelIndex = m_PanelCount++;

        if (PanelExists(panelObject))
        {
            throw new ApplicationException($"Panel already exists. Not added. Name: {panelName}");
        }

    }

    public Panel(GameObject panelObject)
    {
        this.PanelObject = panelObject.gameObject;
        this.PanelName = panelObject.name;
        this.PanelIndex = m_PanelCount++;

        if (PanelExists(panelObject))
        {
            throw new ApplicationException($"Panel already exists. Not added. Name: {panelObject.name}");
        }

    }

    /**
    * Public Methods
    **/

    public Panel GetPanel(int ndx) { return m_PanelList.Single(i => i.PanelIndex == ndx); }
    public Panel GetPanel(string name) { return m_PanelList.Single(n => n.PanelName == name); }
    public Panel GetPanel(GameObject obj) { return m_PanelList.Single(o => o.PanelObject); }

    public void SetPanelName(string panelName)
    {
        this.PanelName = string.IsNullOrEmpty(panelName)
            || string.IsNullOrWhiteSpace(panelName) ? this.PanelName : panelName;
    }

    /** 
    * Private Methods
    **/

    private bool PanelExists(Panel panel)
    {
        foreach (Panel p in m_PanelList)
        {
            if (p.PanelObject == panel.PanelObject) return true;
        }
        return false;
    }

    private bool PanelExists(GameObject panel)
    {
        foreach (Panel p in m_PanelList)
        {
            if (p.PanelObject == panel) return true;
        }
        return false;
    }

    private bool PanelExists(string panel)
    {
        foreach (Panel p in m_PanelList)
        {
            if (p.PanelName == panel) return true;
        }
        return false;
    }
}
