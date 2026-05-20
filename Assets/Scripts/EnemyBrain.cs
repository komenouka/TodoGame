using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain
{
    private readonly PlayerModel _playerModel;
    private readonly IBattlePresenter _presenter;

    public EnemyBrain(PlayerModel pModel, IBattlePresenter presenter)
    {
        _playerModel = pModel;
        _presenter = presenter;
    }

    public IEnumerator ExecuteTurn(List<EnemyModel> enemies, System.Action onComplete)
    {
        foreach (EnemyModel enemyattacker in enemies)
        {
            if (_playerModel.IsDead || enemyattacker.IsDead) continue;

            _presenter.DisplayLog($"{enemyattacker.name} attacks!");
            yield return new WaitForSeconds(0.5f);

            _playerModel.Damage(10);
            _presenter.RefreshUI();
            _presenter.DisplayLog("Player took 10 damage!");

            yield return new WaitForSeconds(0.8f);
        }
        onComplete?.Invoke();
    }
}