using UnityEngine;
using TMPro;

public interface IBattleView
{
    void UpdateLog(string message);
    void SetCommandInteractable(bool state);
}

public class BattleView : MonoBehaviour, IBattleView
{
    [SerializeField] private TextMeshProUGUI logText;
    [SerializeField] private CanvasGroup commandGroup;

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