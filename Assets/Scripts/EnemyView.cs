using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class EnemyView : MonoBehaviour
{
    public Slider hpSlider;
    public TextMeshProUGUI hpText;
    public GameObject enemyVisual;
    public Button targetButton;

    public EnemyModel currentEnemyModel;
    public Action<EnemyView> OnTargetSelected;

    public void Setup(EnemyModel models, Action<EnemyView> callback)
    {
        currentEnemyModel = models;
        OnTargetSelected = callback;
        targetButton.onClick.RemoveAllListeners();
        targetButton.onClick.AddListener(() => OnTargetSelected?.Invoke(this));
    }

    public void UpdateEnemyUI()
    {
        if (currentEnemyModel.IsDead)
        {
            enemyVisual.SetActive(false);
            targetButton.interactable = false;
            return;
        }
        hpSlider.maxValue = currentEnemyModel.maxHp;
        hpSlider.value = currentEnemyModel.hp;
        hpText.text = $"{currentEnemyModel.name}\nHP: {currentEnemyModel.hp}";
    }
}