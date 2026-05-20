using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public interface IEnemyView
{
    void Setup(EnemyModel model, Action<EnemyModel> onClickCallback);
    void UpdateEnemyUI(string enemyName, int hp, int maxHp, bool isDead);
}

public class EnemyView : MonoBehaviour, IEnemyView
{
    [SerializeField] private Slider hpSlider;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private GameObject enemyVisual;
    [SerializeField] private Button targetButton;

    private EnemyModel _associatedModel;
    private Action<EnemyModel> _onClickCallback;

    public void Setup(EnemyModel model, Action<EnemyModel> onClickCallback)
    {
        _associatedModel = model;
        _onClickCallback = onClickCallback;

        targetButton.onClick.RemoveAllListeners();
        targetButton.onClick.AddListener(() => _onClickCallback?.Invoke(_associatedModel));
    }

    public void UpdateEnemyUI(string enemyName, int hp, int maxHp, bool isDead)
    {
        if (isDead)
        {
            enemyVisual.SetActive(false);
            targetButton.interactable = false;
            return;
        }
        enemyVisual.SetActive(true);
        targetButton.interactable = true;

        hpSlider.maxValue = maxHp;
        hpSlider.value = hp;
        hpText.text = $"{enemyName}\nHP: {hp}";
    }
}