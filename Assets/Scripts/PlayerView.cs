using UnityEngine;
using UnityEngine.UI;
using TMPro;

public interface IPlayerView
{
    void UpdatePlayerUI(int hp, int maxHp, int mp, int maxMp, int level);
}

public class PlayerView : MonoBehaviour, IPlayerView
{
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Slider mpSlider;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI mpText;
    [SerializeField] private TextMeshProUGUI levelText;

    public void UpdatePlayerUI(int hp, int maxHp, int mp, int maxMp, int level)
    {
        hpSlider.maxValue = maxHp;
        hpSlider.value = hp;
        hpText.text = $"HP: {hp} / {maxHp}";

        mpSlider.maxValue = maxMp;
        mpSlider.value = mp;
        mpText.text = $"MP: {mp} / {maxMp}";

        levelText.text = $"Lv: {level}";
    }
}