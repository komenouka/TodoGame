using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IBattlePresenter
{
    void DisplayLog(string message);
    void RefreshUI();
    void SetCommandInteractable(bool state);
    List<EnemyModel> GetEnemyModels();
}

public class BattleController : MonoBehaviour, IBattlePresenter
{
    [SerializeField] private PlayerView playerView;
    [SerializeField] private List<EnemyView> enemyViews;
    [SerializeField] private BattleView battleView;

    private PlayerModel _playerModel;
    private List<EnemyModel> _enemyModels;

    private PlayerBrain _playerProcessor;
    private EnemyBrain _enemyProcessor;

    private bool _isProcessing;
    private bool _isSelectingTarget;

    private void Start()
    {
        InitializeBattle();
    }

    private void InitializeBattle()
    {
        _playerModel = new PlayerModel("Player", 100, 50);
        _enemyModels = new List<EnemyModel>
        {
            new EnemyModel("SlimeA", 50, 10),
            new EnemyModel("SlimeB", 50, 10),
            new EnemyModel("SlimeC", 50, 10)
        };

        _playerProcessor = new PlayerBrain(_playerModel, this);
        _enemyProcessor = new EnemyBrain(_playerModel, this);

        for (int index = 0; index < enemyViews.Count; index++)
        {
            enemyViews[index].Setup(_enemyModels[index], OnEnemyClicked);
        }

        RefreshUI();
        TogglePlayerControl(true);
    }

    public void OnAttackCommandSelected()
    {
        if (_isProcessing) return;
        _isSelectingTarget = true;
        battleView.UpdateLog("Select a target");
    }

    public void OnFireballCommandSelected()
    {
        if (_isProcessing || _playerModel.mp < 18) return;
        StartCoroutine(FireballSequence());
    }

    private void OnEnemyClicked(EnemyModel clickedEnemyModel)
    {
        if (_isProcessing || !_isSelectingTarget || clickedEnemyModel.IsDead) return;
        _isSelectingTarget = false;
        StartCoroutine(BattleSequence(clickedEnemyModel));
    }

    private IEnumerator BattleSequence(EnemyModel targetEnemy)
    {
        TogglePlayerControl(false);

        yield return _playerProcessor.ExecuteAttack(targetEnemy);
        RefreshUI();

        if (CheckVictory())
        {
            battleView.UpdateLog("Victory!");
            yield break;
        }

        yield return StartCoroutine(_enemyProcessor.ExecuteTurn(_enemyModels, null));

        if (_playerModel.IsDead)
        {
            battleView.UpdateLog("Lose...");
        }
        else
        {
            TogglePlayerControl(true);
        }
    }

    private IEnumerator FireballSequence()
    {
        TogglePlayerControl(false);
        
        yield return _playerProcessor.ExecuteFireball(_enemyModels);
        RefreshUI();

        if (CheckVictory())
        {
            battleView.UpdateLog("Victory!");
            yield break;
        }

        yield return StartCoroutine(_enemyProcessor.ExecuteTurn(_enemyModels, null));

        if (_playerModel.IsDead)
        {
            battleView.UpdateLog("Lose...");
        }
        else
        {
            TogglePlayerControl(true);
        }
    }

    private void TogglePlayerControl(bool canControl)
    {
        _isProcessing = !canControl;
        battleView.SetCommandInteractable(canControl);
    }

    private bool CheckVictory()
    {
        return _enemyModels.Find(e => !e.IsDead) == null;
    }

    public void DisplayLog(string message)
    {
        battleView.UpdateLog(message);
    }

    public void SetCommandInteractable(bool state)
    {
        battleView.SetCommandInteractable(state);
    }

    public void RefreshUI()
    {
        playerView.UpdatePlayerUI(
            _playerModel.hp, 
            _playerModel.maxHp, 
            _playerModel.mp, 
            _playerModel.maxMp, 
            _playerModel.level
        );

        for (int i = 0; i < enemyViews.Count; i++)
        {
            var model = _enemyModels[i];
            enemyViews[i].UpdateEnemyUI(model.name, model.hp, model.maxHp, model.IsDead);
        }
    }

    public List<EnemyModel> GetEnemyModels()
    {
        return _enemyModels;
    }
}