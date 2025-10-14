using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeReference] PanelManager panelManager;

    void Start()
    {
        panelManager.ManagerEnable(true);
    }


}
