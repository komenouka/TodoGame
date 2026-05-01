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
    private bool isSelectingTarget;

    private void Start()
    {
        InitializeBattle();
    }

    private void InitializeBattle()
    {
        playerModel = new PlayerModel("Player", 100, 50);
        playerProcessor = new PlayerBrain(playerModel, battleView);
        enemyProcessor = new EnemyBrain(playerModel, battleView, this);

        List<EnemyModel> initialEnemies = new List<EnemyModel>
        {
            new EnemyModel("SlimeA", 50, 10),
            new EnemyModel("SlimeB", 50, 10),
            new EnemyModel("SlimeC", 50, 10)
        };

        for (int index = 0; index < enemyViews.Count; index++)
        {
            enemyViews[index].Setup(initialEnemies[index], OnEnemyClicked);
        }

        UpdateAllViews();
        TogglePlayerControl(true);
    }

    public void OnAttackCommandSelected()
    {
        if (isProcessing) return;
        isSelectingTarget = true;
        battleView.UpdateLog("Select a target");
    }

    private void OnEnemyClicked(EnemyView clickedEnemyView)
    {
        if (isProcessing || !isSelectingTarget || clickedEnemyView.currentEnemyModel.IsDead) return;
        isSelectingTarget = false;
        StartCoroutine(BattleSequence(clickedEnemyView));
    }

    public void OnFireballCommandSelected()
    {
        if (isProcessing || playerModel.mp < 18) return;
        StartCoroutine(FireballSequence());
    }

    private IEnumerator FireballSequence()
    {
        TogglePlayerControl(false);
        yield return playerProcessor.ExecuteFireball(enemyViews);
        UpdateAllViews();

        if (CheckVictory())
        {
            battleView.UpdateLog("Victory!");
            yield break;
        }

        yield return StartCoroutine(enemyProcessor.ExecuteTurn(enemyViews, null));

        if (playerModel.IsDead)
        {
            battleView.UpdateLog("Lose...");
        }
        else
        {
            TogglePlayerControl(true);
        }
    }

    private IEnumerator BattleSequence(EnemyView target)
    {
        TogglePlayerControl(false);

        yield return playerProcessor.ExecuteAttack(target);
        UpdateAllViews();

        if (CheckVictory())
        {
            battleView.UpdateLog("Victory!");
            yield break;
        }

        yield return StartCoroutine(enemyProcessor.ExecuteTurn(enemyViews, null));

        if (playerModel.IsDead)
        {
            battleView.UpdateLog("Lose...");
        }
        else
        {
            TogglePlayerControl(true);
        }
    }

    public void UpdateAllViews()
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