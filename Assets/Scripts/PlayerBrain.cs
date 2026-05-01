using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrain
{
    private PlayerModel playerStatus;
    private BattleView battleView;

    public PlayerBrain(PlayerModel status, BattleView view)
    {
        playerStatus = status;
        battleView = view;
    }

    public IEnumerator ExecuteAttack(EnemyView target)
    {
        playerStatus.isGuarding = false;
        EnemyModel enemy = target.currentEnemyModel;
        enemy.Damage(25);
        battleView.UpdateLog($"{enemy.name} took 25 damage!");
        yield return new WaitForSeconds(1.0f);
    }

    public IEnumerator ExecuteFireball(List<EnemyView> targets)
    {
        int cost = 18;
        if (playerStatus.mp < cost)
        {
            battleView.UpdateLog("No MP!");
            yield return new WaitForSeconds(1.0f);
            yield break;
        }


        playerStatus.mp -= cost;
        battleView.UpdateLog("Fireball! All enemies take 25 damage!");
        yield return new WaitForSeconds(1.0f);

        foreach (var target in targets)
        {
            if (!target.currentEnemyModel.IsDead)
            {
                target.currentEnemyModel.Damage(25);
            }
        }
    yield return new WaitForSeconds(1.0f);
    }
}