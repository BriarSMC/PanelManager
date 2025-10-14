using System;
using UnityEngine;
using UnityEngine.UI;

public class SecondPanel : MonoBehaviour
{
    [SerializeReference] PanelManager panelManager;
    [SerializeReference] Button nextBtn;

    void Start()
    {
        nextBtn.onClick.AddListener(() => panelManager.Push("Panel3"));
    }
}
