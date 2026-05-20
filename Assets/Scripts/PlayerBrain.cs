using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrain
{
    private readonly PlayerModel _playerStatus;
    private readonly IBattlePresenter _presenter;

    public PlayerBrain(PlayerModel status, IBattlePresenter presenter)
    {
        _playerStatus = status;
        _presenter = presenter;
    }

    public IEnumerator ExecuteAttack(EnemyModel enemyTarget)
    {
        _playerStatus.isGuarding = false;
        enemyTarget.Damage(25);
        _presenter.DisplayLog($"{enemyTarget.name} took 25 damage!");
        yield return new WaitForSeconds(1.0f);
    }

    public IEnumerator ExecuteFireball(List<EnemyModel> targets)
    {
        int cost = 25;
        if (_playerStatus.mp < cost)
        {
            _presenter.DisplayLog("No MP!");
            yield return new WaitForSeconds(1.0f);
            yield break;
        }

        _playerStatus.mp -= cost;
        _presenter.DisplayLog("Fireball! All enemies take 25 damage!");
        yield return new WaitForSeconds(1.0f);

        foreach (var enemy in targets)
        {
            if (!enemy.IsDead)
            {
                enemy.Damage(10);
            }
        }
        yield return new WaitForSeconds(1.0f);
    }
}