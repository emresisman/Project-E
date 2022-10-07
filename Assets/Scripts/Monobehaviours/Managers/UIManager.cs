using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIPanels
{
    StunPanel = 0,
    PausePanel = 1,
}

public class UIManager : Singleton<UIManager>
{
    public void ShowScreen(IUIScreen panel)
    {
        panel.ShowScreen();
    }

    public void HideScreen(IUIScreen panel)
    {
        panel.HideScreen();
    }
}