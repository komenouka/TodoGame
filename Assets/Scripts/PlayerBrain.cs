using System.Collections;
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

    public IEnumerator ExecuteAttack(EnemyView target, System.Action onComplete)
    {
        playerStatus.isGuarding = false;
        
        EnemyModel enemy = target.currentEnemyModel;
        enemy.Damage(25);
        battleView.UpdateLog($"{enemy.name} took 25 damage!");

        yield return new WaitForSeconds(1.0f);
        onComplete?.Invoke();
    }
}