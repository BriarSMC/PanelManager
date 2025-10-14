using UnityEngine;
using UnityEngine.UI;

public class FifthPanel : MonoBehaviour
{
    [SerializeReference] PanelManager panelManager;
    [SerializeReference] Button prevBtn;
    [SerializeReference] Button popBtn;
    [SerializeReference] Button popToBtn;
    [SerializeReference] Panel toPanel;

    void Start()
    {
        prevBtn.onClick.AddListener(() => panelManager.Pop());
        popBtn.onClick.AddListener(() => panelManager.Pop(2));
        popToBtn.onClick.AddListener(() => panelManager.PopTo(toPanel));
    }
}
