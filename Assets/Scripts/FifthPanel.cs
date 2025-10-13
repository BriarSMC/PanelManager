using UnityEngine;
using UnityEngine.UI;

public class FifthPanel : MonoBehaviour
{
    [SerializeReference] PanelManager panelManager;
    [SerializeReference] Button prevBtn;
    [SerializeReference] Button popBtn;

    void Start()
    {
        prevBtn.onClick.AddListener(() => panelManager.Pop());
        popBtn.onClick.AddListener(() => panelManager.Pop(2));
    }
}
