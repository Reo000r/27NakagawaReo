using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // �V�[���ؑ�
using UnityEngine.UI;
using TMPro;

// �R���g���[���[�Ή��Ŏg������
//using UnityEngine.EventSystems;

public class GameManagerSingleton : MonoBehaviour
{
    // �Q�[�����[�h�ۑ��݂̂ɏ���������

    public static GameManagerSingleton Instance;

    [SerializeField] private TextGeneratorController _textGeneratorController;

    [SerializeField] private TextMeshProUGUI _gamemodeText;

    private const string kText = "Gamemode:";

    private int score = 0;
    //
    private const float kStartTime = 3.0f;
    private const float kEndTime = 6.0f;
    private float timer = 0.0f;

    private GamemodeState gamemodeState = GamemodeState.GameTitle;

    private enum GamemodeState
    {
        GameTitle,
        //KanRanking,
        //TargetRanking,

        KanStart,
        Kan,
        KanEnd,

        //TargetLoad,
        TargetStart,
        Target,
        TargetEnd,

        TargetResultStart,
        TargetResult,
        TargetResultEnd,

        GamemodeTotal
    }

    private void Awake()
    {
        // �V���O���g�������Ă�H
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        timer = kStartTime;

        TextUpdate(gamemodeState);
    }

    void Update()
    {
        GameUpdate();
        Debug.Log($"GameManagerSingleton.GamemodeChange:{gamemodeState}");
    }

    private void GameUpdate()
    {
        // ����State�̎��ɏ�Ɏ��s
        switch (gamemodeState)
        {
            case GamemodeState.GameTitle:
                TextUpdate(gamemodeState);
                GamemodeStateUpdate();
                break;

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
                break;
                
            case GamemodeState.TargetResultStart:

                break;
                
            case GamemodeState.TargetResult:

                break;
                
            case GamemodeState.TargetResultEnd:

                break;
        }

        
    }

    public void GamemodeStateUpdate()
    {
        // State�̍X�V����(����start)
        switch (gamemodeState)
        {
            //case GamemodeState.TargetLoad:
            //    gamemodeState = GamemodeState.TargetStart;
            //    Debug.Log($"GameManagerSingleton.GamemodeChange:{gamemodeState}");
            //    TextUpdate(gamemodeState);
            //    break;

            case GamemodeState.GameTitle:
                gamemodeState = GamemodeState.TargetStart;
                Debug.Log($"GameManagerSingleton.GamemodeChange:{gamemodeState}");
                _textGeneratorController.StartText();
                TextUpdate(gamemodeState);
                break;

            case GamemodeState.TargetStart:
                gamemodeState = GamemodeState.Target;
                Debug.Log($"GameManagerSingleton.GamemodeChange:{gamemodeState}");
                TextUpdate(gamemodeState);
                break;

            case GamemodeState.Target:
                gamemodeState = GamemodeState.TargetEnd;
                Debug.Log($"GameManagerSingleton.GamemodeChange:{gamemodeState}");
                timer = kEndTime;
                _textGeneratorController.EndText();
                TextUpdate(gamemodeState);
                break;

            case GamemodeState.TargetEnd:
                gamemodeState = GamemodeState.TargetResultStart;
                //Debug.Log($"GameManagerSingleton.GamemodeChange:{gamemodeState}");
                SceneManager.LoadScene("ResultScene");
                TextUpdate(gamemodeState);
                break;

            case GamemodeState.TargetResultStart:

                break;

            case GamemodeState.TargetResult:

                break;

            case GamemodeState.TargetResultEnd:

                break;

        }
    }


    public bool IsStateTarget
    { 
        get
        {
            // gamemodeState��Target���ǂ���
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
            // gamemodeState��Target���ǂ���
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

    public void SetStateTarget()
    { 
        // TargetStart ���I������ꍇTarget�ɂ���
        gamemodeState = GamemodeState.Target;
        Debug.Log($"GameManagerSingleton.GamemodeChange:{gamemodeState}");
        TextUpdate(gamemodeState);
    }
    
    //public void SetStateTargetEnd()
    //{ 
    //    // Target ���I������ꍇTargetEnd�ɂ���
    //    gamemodeState = GamemodeState.TargetEnd;
    //    Debug.Log($"GameManagerSingleton.GamemodeChange:{gamemodeState}");
    //    TextUpdate(gamemodeState);
    //}

    private void TextUpdate(GamemodeState state)
    {
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

    // �֐����s��
    // �N���X��.Instance(�ϐ���).�֐���

    public void AddScore()
    {
        score += 1;
        Debug.Log($"AddScore : {score}");
    }

    public void AddScore(int add)
    {
        score += add;
        Debug.Log($"AddScore : {score}");
    }

    public void DispScore()
    {
        Debug.Log($"Score : {score}");
    }

    // �V�[���ؑ�
    public void TitleSceneLoad()
    {
        Debug.Log("TitleScene Load");
        SceneManager.LoadScene("TitleScene");
    }

    // �V�[���ؑ�
    public void GameSceneLoad()
    {
        Debug.Log("GameScene Load");
        SceneManager.LoadScene("GameScene");
    }

    // �V�[���ؑ�
    public void ResultSceneLoad()
    {
        Debug.Log("ResultScene Load");
        SceneManager.LoadScene("ResultScene");
    }

}
