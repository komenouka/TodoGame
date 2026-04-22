using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleController : MonoBehaviour
{
    public PlayerView playerView;
    public List<EnemyView> enemyViews;
    public BattleView battleView;

    private PlayerModel playerModel;
    private PlayerTurnProcessor playerProcessor;
    private EnemyTurnProcessor enemyProcessor;
    private bool isProcessing;

    private PlayerModel _player;

    private void InitializeBattle()
    {
        _player = new PlayerModel("プレイヤー", 100, 50);
        playerModel = _player;

        playerProcessor = new PlayerTurnProcessor(playerModel, battleView);
        enemyProcessor = new EnemyTurnProcessor(playerModel, battleView);

        List<EnemyModel> initialEnemies = new List<EnemyModel>
        {
            new EnemyModel("スライムA", 50),
            new EnemyModel("スライムB", 50),
            new EnemyModel("スライムC", 50)
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
            battleView.UpdateLog("勝利！");
        }
        else if (playerModel.IsDead)
        {
            battleView.UpdateLog("敗北！");
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
        playerView.UpdatePlayerUI(_player);
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