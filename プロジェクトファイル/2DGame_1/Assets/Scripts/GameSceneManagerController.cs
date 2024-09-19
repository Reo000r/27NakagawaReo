using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // シーン切替
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;  // async

// コントローラー対応で使うかも
//using UnityEngine.EventSystems;

public class GameSceneManagerController : MonoBehaviour
{
    [SerializeField] private TextGeneratorController _textGeneratorController;
    [SerializeField] private CursorController _cursorController;


    [SerializeField] private GameSceneScoreTextManager _gameSceneScoreTextManager;
    [SerializeField] private TextMeshProUGUI _gamemodeText;
    [SerializeField] private GameSceneSoundManager _gameSceneSoundManager;


    private const string kPlayerPrefsScore = "Score";
    private const string kText = "Gamemode:";

    private int score = 0;
    //
    private const float kStartTime = 3.0f;
    private const float kEndTime = 5.5f;
    private const int kEndDelayTime = 3000;
    private float timer = 0.0f;

    private bool isEnd = false;

    private GamemodeState gamemodeState = GamemodeState.TargetStart;

    public int Score { get{ return score; } }

    private enum GamemodeState
    {
        //TargetLoad,
        TargetStart,
        Target,
        TargetEnd,

        GamemodeTotal
    }

    void Start()
    {
        timer = kStartTime;

        PlayerPrefs.SetInt(kPlayerPrefsScore, -1);
        TextUpdate(gamemodeState);
        _textGeneratorController.StartText();
        _cursorController.Active();
        _gameSceneScoreTextManager.Active();

        _gameSceneSoundManager.PlayGameStartSE1();
        _gameSceneSoundManager.PlayGameStartSE2();
    }

    void Update()
    {
        GameUpdate();
        //Debug.Log($"GameSceneManager.GamemodeChange:{gamemodeState}");
    }

    private void GameUpdate()
    {
        // そのStateの時に常に実行
        switch (gamemodeState)
        {
            //case GamemodeState.TargetLoad:

            //    break;

            case GamemodeState.TargetStart:
                timer -= Time.deltaTime;
                TextUpdate(gamemodeState);
                if (timer < 0)
                {
                    timer = kStartTime;
                    GamemodeStateUpdate();
                }
                break;

            case GamemodeState.Target:

                break;

            case GamemodeState.TargetEnd:
                timer -= Time.deltaTime;
                TextUpdate(gamemodeState);
                if (timer < 0)
                {
                    timer = kStartTime;
                    GamemodeStateUpdate();
                }
                else if(!isEnd)
                {
                    isEnd = true;
                    End();

                }
                break;
        }


    }

    public void GamemodeStateUpdate()
    {
        // Stateの更新処理(実質start)
        switch (gamemodeState)
        {
            //case GamemodeState.TargetLoad:
            //    gamemodeState = GamemodeState.TargetStart;
            //    Debug.Log($"GameSceneManager.GamemodeChange:{gamemodeState}");
            //    TextUpdate(gamemodeState);
            //    break;

            //case GamemodeState.GameTitle:
            //    gamemodeState = GamemodeState.TargetStart;
            //    Debug.Log($"GameSceneManager.GamemodeChange:{gamemodeState}");
            //    _textGeneratorController.StartText();
            //    TextUpdate(gamemodeState);
            //    break;

            case GamemodeState.TargetStart:
                gamemodeState = GamemodeState.Target;
                Debug.Log($"GameSceneManager.GamemodeChange:{gamemodeState}");
                TextUpdate(gamemodeState);
                break;

            case GamemodeState.Target:
                gamemodeState = GamemodeState.TargetEnd;
                Debug.Log($"GameSceneManager.GamemodeChange:{gamemodeState}");
                timer = kEndTime;
                TextUpdate(gamemodeState);
                break;

            case GamemodeState.TargetEnd:
                //gamemodeState = GamemodeState.TargetResultStart;
                //Debug.Log($"GameSceneManager.GamemodeChange:{gamemodeState}");
                SceneManager.LoadScene("ResultScene");
                //TextUpdate(gamemodeState);
                break;
        }
    }


    public bool IsStateTarget
    {
        get
        {
            // gamemodeStateがTargetかどうか
            if (gamemodeState == GamemodeState.Target)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public bool IsStateTargetStart
    {
        get
        {
            // gamemodeStateがTargetかどうか
            if (gamemodeState == GamemodeState.TargetStart)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    private void TextUpdate(GamemodeState state)
    {
        _gameSceneScoreTextManager.TextUpdate();

        if (gamemodeState == GamemodeState.TargetStart ||
            //gamemodeState == GamemodeState.TargetLoad ||
            gamemodeState == GamemodeState.TargetEnd)
        {
            _gamemodeText.text = $"timer:{timer},{kText}{state}";
        }
        else
        {
            _gamemodeText.text = kText + state;
        }
    }

    private async void End()
    {
        await Task.Delay(kEndDelayTime);  // いい感じにここを調整(ごり押し)
        PlayerPrefs.SetInt(kPlayerPrefsScore, score);
        Debug.Log($"Score : {PlayerPrefs.GetInt(kPlayerPrefsScore)}");
        _textGeneratorController.EndText();
        _cursorController.InActive();
        _gameSceneScoreTextManager.InActive();
    }


    public void AddScore(int add)
    {
        score += add;
        if (score < 0) score = 0;
        TextUpdate(gamemodeState);
        Debug.Log($"Add1Score : {score}");
    }
}
