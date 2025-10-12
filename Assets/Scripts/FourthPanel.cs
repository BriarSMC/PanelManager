using UnityEngine;
using UnityEngine.UI;

public class FourthPanel : MonoBehaviour
{
    [SerializeReference] PanelManager panelManager;
    [SerializeReference] Button prevBtn;
    [SerializeReference] Button nextBtn;

    void Start()
    {
        prevBtn.onClick.AddListener(() => panelManager.Pop());
        nextBtn.onClick.AddListener(() => panelManager.Push("Panel5"));
    }
}
