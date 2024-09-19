using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // シーン切替
using System.Threading.Tasks;  // async

public class ResultPerformanceManager : MonoBehaviour
{
    [SerializeField] private GameObject _resultBack;
    [SerializeField] private GameObject _rank1Panel;
    [SerializeField] private GameObject _rank2Panel;
    [SerializeField] private GameObject _rank3Panel;
    [SerializeField] private GameObject _scorePanel;
    [SerializeField] private GameObject _target1;
    [SerializeField] private GameObject _target2;
    [SerializeField] private GameObject _target3;
    [SerializeField] private GameObject _retryButton;
    [SerializeField] private GameObject _titleButton;

    [SerializeField] private Animator _rank1PanelAnim;
    [SerializeField] private Animator _rank2PanelAnim;
    [SerializeField] private Animator _rank3PanelAnim;

    [SerializeField] private ResultBackController _resultBackController;
    [SerializeField] private ScoreTextController _scoreTextController1;
    [SerializeField] private ScoreTextController _scoreTextController2;
    [SerializeField] private ScoreTextController _scoreTextController3;
    [SerializeField] private ResultButtonController _resultButtonController1;
    [SerializeField] private ResultButtonController _resultButtonController2;
    [SerializeField] private RankController _rankController1;
    [SerializeField] private RankController _rankController2;
    [SerializeField] private RankController _rankController3;
    [SerializeField] private ResultScorePanelController _resultScorePanelController;
    [SerializeField] private ResultTargetController _resultTargetController1;
    [SerializeField] private ResultTargetController _resultTargetController2;
    [SerializeField] private ResultTargetController _resultTargetController3;

    [SerializeField] private ResultSoundManager _resultSoundManager;

    [SerializeField] private RectTransform _scoreText1Rect;
    [SerializeField] private RectTransform _scoreText2Rect;
    [SerializeField] private RectTransform _scoreText3Rect;

    //private const string kStartAnim = "StartAnimTrigger";
    //private const string kEndAnim = "EndAnimTrigger";

    //private const string kRank = "Rank";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void ResultAnim()
    {
        // Animatorで沼ったので全排除してスクリプトで全て解決した

        _resultBack.SetActive(true);

        await Task.Delay(1000);
        await Task.Delay(200);
        _rankController1.StartAnim();
        _rankController2.StartAnim();
        _rankController3.StartAnim();
        //Debug.Log("start");
        //_rank1PanelAnim.SetTrigger($"{kStartAnim}{kRank}1");
        //await Task.Delay(350);
        //Debug.Log("start");
        //_rank2PanelAnim.SetTrigger($"{kStartAnim}{kRank}2");
        //await Task.Delay(350);
        //Debug.Log("start");
        //_rank3PanelAnim.SetTrigger($"{kStartAnim}{kRank}3");

        await Task.Delay(1000);
        _resultScorePanelController.StartAnim();
        await Task.Delay(500);
        await Task.Delay(200);
        _resultTargetController1.StartAnim();
        await Task.Delay(200);
        _resultTargetController2.StartAnim();
        await Task.Delay(200);
        _resultTargetController3.StartAnim();

        await Task.Delay(500);
        _resultSoundManager.PlayReloadSE();
        await Task.Delay(300);

        await Task.Delay(300);
        _resultTargetController1.ChangeAnim();
        await Task.Delay(300);
        _resultTargetController2.ChangeAnim();
        await Task.Delay(300);
        _resultTargetController3.ChangeAnim();

        //await Task.Delay(2000);
        //await Task.Delay(200);

    }

    public async void ScoreUpdateAnim()
    {
        //Debug.Log("ScoreUpdateAnim");
        await Task.Delay(300);
        _rankController1.ScoreUpdateAnim();
        _rankController2.ScoreUpdateAnim();
        _rankController3.ScoreUpdateAnim();

        //await Task.Delay(n);
        // 上からスコア更新通知を出す
    }

    public async void ButtonAnim()
    {
        await Task.Delay(300);
        _resultButtonController1.StartAnim();
        _resultButtonController2.StartAnim();
    }

    public async void SceneChangeAnim(string sceneName)
    {
        // SceneChangeAnim()
        await Task.Delay(500);
        _scoreTextController1.SceneChangeAnim();
        _scoreTextController2.SceneChangeAnim();
        _scoreTextController3.SceneChangeAnim();
        _resultButtonController1.SceneChangeAnim();
        _resultButtonController2.SceneChangeAnim();

        await Task.Delay(400);
        await Task.Delay(100);
        _rankController3.SceneChangeAnim();
        await Task.Delay(100);
        _rankController2.SceneChangeAnim();
        await Task.Delay(100);
        _rankController1.SceneChangeAnim();
        _resultScorePanelController.SceneChangeAnim();

        await Task.Delay(1000);
        _resultBackController.SceneChangeAnim();

        await Task.Delay(600);
        SceneManager.LoadScene($"{sceneName}");
    }
}
