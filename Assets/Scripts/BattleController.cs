using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleController : MonoBehaviour
{
    
    public PlayerView playerView;
    public List<EnemyView> enemyViews;
    public BattleView battleView;

    private PlayerModel playerModel;
    private PlayerBrain playerProcessor;
    private EnemyBrain enemyProcessor;
    private bool isProcessing;

    private void Start()
    {
        InitializeBattle(); 
    }

    private void InitializeBattle()
    {
        playerModel = new PlayerModel("Player", 100, 50);

        playerProcessor = new PlayerBrain(playerModel, battleView);
        enemyProcessor = new EnemyBrain(playerModel, battleView);

        List<EnemyModel> initialEnemies = new List<EnemyModel>
        {
            new EnemyModel("SlimeA", 50,10),
            new EnemyModel("SlimeB", 50,10),
            new EnemyModel("SlimeC", 50,10)
        };

        for (int index = 0; index < enemyViews.Count; index++)
        {
            enemyViews[index].Setup(initialEnemies[index], OnEnemyClicked);
        }

        UpdateAllViews();
        TogglePlayerControl(true);
    }

    private void OnEnemyClicked(EnemyView clickedEnemyView)
    {
        if (isProcessing || clickedEnemyView.currentEnemyModel.IsDead) return;
        
        TogglePlayerControl(false);
        StartCoroutine(playerProcessor.ExecuteAttack(clickedEnemyView, () => {
            UpdateAllViews();
            CheckBattleProgress();
        }));
    }

    private void CheckBattleProgress()
    {
        if (CheckVictory())
        {
            battleView.UpdateLog("Victory!");
        }
        else if (playerModel.IsDead)
        {
            battleView.UpdateLog("Lose...");
        }
        else
        {
            StartCoroutine(enemyProcessor.ExecuteTurn(enemyViews, () => {
                UpdateAllViews();
                if (playerModel.IsDead) CheckBattleProgress();
                else TogglePlayerControl(true);
            }));
        }
    }

    private void UpdateAllViews()
    {
        playerView.UpdatePlayerUI(playerModel);
        foreach (var ev in enemyViews) ev.UpdateEnemyUI();
    }

    private void TogglePlayerControl(bool canControl)
    {
        isProcessing = !canControl;
        battleView.SetCommandInteractable(canControl);
    }

    private bool CheckVictory()
    {
        return enemyViews.Find(e => !e.currentEnemyModel.IsDead) == null;
    }
}