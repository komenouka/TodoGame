using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerView : MonoBehaviour
{
    public Slider hpSlider;
    public Slider mpSlider;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI mpText;
    public TextMeshProUGUI levelText;

    public void UpdatePlayerUI(PlayerStatus status)
    {
        hpSlider.maxValue = status.maxHp;
        hpSlider.value = status.hp;
        hpText.text = $"HP: {status.hp} / {status.maxHp}";

        mpSlider.maxValue = status.maxMp;
        mpSlider.value = status.mp;
        mpText.text = $"MP: {status.mp} / {status.maxMp}";

        levelText.text = $"Lv: {status.level}";
    }
}