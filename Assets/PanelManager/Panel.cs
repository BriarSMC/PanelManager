using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.Animations;

public partial class Panel
{
    public GameObject PanelObject { get; private set; }
    public string PanelName { get; private set; }
    public int PanelIndex;

    private PanelManager panelManager;

    /**
    * Constructors
    **/

    public Panel(GameObject panelObject, string panelName)
    {
        this.PanelObject = panelObject.gameObject;
        this.PanelName = panelName;

        // Move to PanelManager
        // if (PanelExists(panelObject))
        // {
        //     throw new ApplicationException($"Panel already exists. Not added. Name: {panelName}");
        // }

    }

    public Panel(GameObject panelObject)
    {
        this.PanelObject = panelObject.gameObject;
        this.PanelName = panelObject.name;

        // Move to PanelManager
        // if (PanelExists(panelObject))
        // {
        //     throw new ApplicationException($"Panel already exists. Not added. Name: {panelObject.name}");
        // }

    }

    void Start()
    {
        /**
         * The parent of all Panel objects is supposed to be a PanelManager object.
         * We get the refernce to the PanelManager object so we can call back to it.
         * Add ourself to the managed panels list.
         **/
        panelManager = transform.parent.gameObject.GetComponent<PanelManager>();
        this.PanelIndex = panelManager.AddManagedPanel(this);
    }
}
