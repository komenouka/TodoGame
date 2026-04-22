using System.Collections;
using UnityEngine;

public class PlayerTurnProcessor
{
    private PlayerModel playerStatus;
    private BattleView battleView;

    public PlayerTurnProcessor(PlayerModel status, BattleView view)
    {
        playerStatus = status;
        battleView = view;
    }

    public IEnumerator ExecuteAttack(EnemyView target, System.Action onComplete)
    {
        playerStatus.isGuarding = false;
        
        EnemyModel enemy = target.currentEnemyModel;
        enemy.Damage(25);
        battleView.UpdateLog($"{enemy.name}に25のダメージ！");

        yield return new WaitForSeconds(1.0f);
        onComplete?.Invoke();
    }
}