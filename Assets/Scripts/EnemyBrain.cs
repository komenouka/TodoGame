using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain
{
    private PlayerModel playerModel;
    private BattleView battleView;
    private BattleController controller;

    public EnemyBrain(PlayerModel pModel, BattleView view, BattleController ctrl)
    {
        playerModel = pModel;
        battleView = view;
        controller = ctrl;
    }

    public IEnumerator ExecuteTurn(List<EnemyView> enemies, System.Action onComplete)
    {
        foreach (EnemyView enemyView in enemies)
        {
            EnemyModel enemyattacker = enemyView.currentEnemyModel;
            if (playerModel.IsDead || enemyattacker.IsDead) continue;

            battleView.UpdateLog($"{enemyattacker.name} attacks!");
            yield return new WaitForSeconds(0.5f);

            playerModel.Damage(10);
            controller.UpdateAllViews();
            battleView.UpdateLog("Player took 10 damage!");

            yield return new WaitForSeconds(0.8f);
        }
        onComplete?.Invoke();
    }
}