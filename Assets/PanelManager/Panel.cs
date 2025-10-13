using UnityEngine;
using System.Reflection;

public class Panel : MonoBehaviour
{
    [SerializeField] string panelName;

    public GameObject PanelObject { get; private set; }
    public string PanelName { get; private set; }
    public int PanelIndex;

    /**
    * Constructors
    **/

    public Panel()
    {
    }

    public Panel(GameObject panelObject, string panelName)
    {
        this.PanelObject = panelObject.gameObject;
        this.PanelName = panelName;
        this.PanelIndex = -1;
    }

    public Panel(GameObject panelObject)
    {
        this.PanelObject = panelObject.gameObject;
        this.PanelName = string.IsNullOrEmpty(panelName) ? this.PanelName = panelObject.name : panelName;
        this.PanelIndex = -1;
    }

    /** 
     * Unity Methods
     **/
    void Awake()
    {
        /**
         * Set the name of the panel
         **/


        this.PanelObject = this.PanelObject == null ? this.gameObject : this.PanelObject;
        if (string.IsNullOrEmpty(this.PanelName) && !string.IsNullOrEmpty(panelName)) this.PanelName = panelName;
        if (string.IsNullOrEmpty(this.PanelName)) this.PanelName = this.gameObject.name;
    }

    public override string ToString()
    {
        return $"{this.PanelObject.name}/{this.PanelName}/{this.PanelIndex}";
    }

    /**
     * Public Methods
     **/
    public void SetPanelName(string panelName)
    {
        this.PanelName = string.IsNullOrEmpty(panelName)
            || string.IsNullOrWhiteSpace(panelName) ? this.PanelName : panelName;
    }
}
