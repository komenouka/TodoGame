using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnProcessor
{
    private PlayerModel playerModel;
    private BattleView battleView;

    public EnemyTurnProcessor(PlayerModel pModel, BattleView view)
    {
        playerModel = pModel;
        battleView = view;
    }

    public IEnumerator ExecuteTurn(List<EnemyView> enemies, System.Action onComplete)
    {
        foreach (EnemyView enemyView in enemies)
        {
            EnemyModel status = enemyView.currentEnemyModel;
            if (playerModel.IsDead || status.IsDead) continue;

            battleView.UpdateLog($"{status.name}の攻撃！");
            playerModel.Damage(10);
            battleView.UpdateLog("プレイヤーは10のダメージを受けた！");
            yield return new WaitForSeconds(0.8f);
        }

        onComplete?.Invoke();
    }
}