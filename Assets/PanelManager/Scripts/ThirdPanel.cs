using UnityEngine;
using UnityEngine.UI;

public class ThirdPanel : MonoBehaviour
{
    [SerializeReference] PanelManager panelManager;
    [SerializeReference] Button nextBtn;

    void Start()
    {
        nextBtn.onClick.AddListener(() => panelManager.Push("Panel4"));
    }
}
