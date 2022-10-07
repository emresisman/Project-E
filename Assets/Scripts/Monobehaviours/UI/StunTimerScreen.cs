using System.Collections;
using UnityEngine;
using TMPro;


public class StunTimerScreen : Singleton<StunTimerScreen>, IUIScreen
{
    [SerializeField] private TMP_Text stunTimerText;

    protected override void Awake()
    {
        base.Awake();
        gameObject.SetActive(false);
    }

    public void SetTimerText(float time)
    {
        stunTimerText.text = time.ToString("0.00") + " s";
    }

    public void ShowScreen()
    {
        gameObject.SetActive(true);
    }

    public void HideScreen()
    {
        gameObject.SetActive(false);
    }
}