using UnityEngine;

public partial class Panel : MonoBehaviour
{
    public void SetPanelName(string panelName)
    {
        this.PanelName = string.IsNullOrEmpty(panelName)
            || string.IsNullOrWhiteSpace(panelName) ? this.PanelName : panelName;
    }
}
