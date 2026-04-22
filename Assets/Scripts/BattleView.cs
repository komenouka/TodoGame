using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BattleView : MonoBehaviour
{
    public TextMeshProUGUI logText;
    public CanvasGroup commandGroup;

    public void UpdateLog(string message)
    {
        logText.text = message;
    }

    public void SetCommandInteractable(bool state)
    {
        commandGroup.interactable = state;
        commandGroup.blocksRaycasts = state;
    }
}