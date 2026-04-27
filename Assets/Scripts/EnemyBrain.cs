using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain
{
    private PlayerModel playerModel;
    private BattleView battleView;

    public EnemyBrain(PlayerModel pModel, BattleView view)
    {
        playerModel = pModel;
        battleView = view;
    }

    public IEnumerator ExecuteTurn(List<EnemyView> enemies, System.Action onComplete)
    {
        foreach (EnemyView enemyView in enemies)
        {
            EnemyModel enemyattacker = enemyView.currentEnemyModel;
            
            if (playerModel.IsDead || enemyattacker.IsDead) continue;

            battleView.UpdateLog($"[{enemyattacker.name}]ttacks!");

            playerModel.Damage(10);
            battleView.UpdateLog("Player took 10 damage!");

            yield return new WaitForSeconds(0.8f);
        }

        onComplete?.Invoke();
    }
}