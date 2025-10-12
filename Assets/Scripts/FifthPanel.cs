using UnityEngine;
using UnityEngine.UI;

public class FifthPanel : MonoBehaviour
{
    [SerializeReference] PanelManager panelManager;
    [SerializeReference] Button prevBtn;
    [SerializeReference] Button jumpBtn;

    void Start()
    {
        prevBtn.onClick.AddListener(() => panelManager.Pop());
        jumpBtn.onClick.AddListener(() => panelManager.Pop(2));
    }
}
