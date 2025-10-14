using System;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

public class FirstPanel : MonoBehaviour
{
    [SerializeReference] PanelManager panelManager;
    [SerializeReference] Button nextBtn;


    void Start()
    {
        nextBtn.onClick.AddListener(() => panelManager.Push("Panel2"));
    }
}
