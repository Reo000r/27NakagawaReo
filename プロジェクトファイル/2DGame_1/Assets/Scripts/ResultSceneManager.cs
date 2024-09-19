using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultSceneManager : MonoBehaviour
{
    [SerializeField] private ResultSoundManager _resultSoundManager;

    [SerializeField] private ResultPerformanceManager _resultPerformanceManager;
    [SerializeField] private ScoreTextController _scoreTextController1;
    [SerializeField] private ScoreTextController _scoreTextController2;
    [SerializeField] private ScoreTextController _scoreTextController3;
    [SerializeField] private RankTextController _rankTextController1;
    [SerializeField] private RankTextController _rankTextController2;
    [SerializeField] private RankTextController _rankTextController3;
    [SerializeField] private ResultTargetController _resultTargetController1;
    [SerializeField] private ResultTargetController _resultTargetController2;
    [SerializeField] private ResultTargetController _resultTargetController3;

    private const string kPlayerPrefsScore = "Score";
    private const string kPlayerPrefsRank1 = "Rank1";
    private const string kPlayerPrefsRank2 = "Rank2";
    private const string kPlayerPrefsRank3 = "Rank3";

    private int scoreTextNum = 0;
    private int rankingUpdateNum = 0;
    private int score = -1;
    private int rank1 = 0;
    private int rank2 = 0;
    private int rank3 = 0;

    public int Score { get { return score; } }

    // Start is called before the first frame update
    void Start()
    {
        //ScoreInit();

        score = PlayerPrefs.GetInt(kPlayerPrefsScore);
        if (score == -1)
        {
            Debug.Log("Score Error");
            score = 0;
        }
        ScoreParse();
        RankScoreInit();
        //RankingUpdate();

        if (score > rank3)
        {
            _resultTargetController1.ChangeSprite();
            _resultTargetController2.ChangeSprite();
            _resultTargetController3.ChangeSprite();
        }

        _resultSoundManager.PlayResultBGM();
        _resultPerformanceManager.ResultAnim();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScoreTextAnimComplete()
    {
        scoreTextNum++;
        Debug.Log($"scoreTextNum {scoreTextNum}");
        if (scoreTextNum >= 3)
        {
            if (RankingUpdate())
            {
                // ランキングが更新できたなら
                _resultPerformanceManager.ScoreUpdateAnim();
            }
            else
            {
                // できなければ
                _resultPerformanceManager.ButtonAnim();
            }
        }
    }

    public void RankingUpdateAnimComplete()
    {
        rankingUpdateNum++;
        if (rankingUpdateNum >= 3)
        {
            _resultSoundManager.PlayScoreUpdateSE();
            _resultPerformanceManager.ButtonAnim();
        }
    }

    private void ScoreParse()
    {
        int r;

        r = 0;
        r = score / 100;
        _scoreTextController1.DispScore = r;

        r = 0;
        r = score % 100;
        r /= 10;
        _scoreTextController2.DispScore = r;

        r = 0;
        r = score % 10;
        _scoreTextController3.DispScore = r;
    }

    private void RankScoreInit()
    {
        rank1 = PlayerPrefs.GetInt(kPlayerPrefsRank1);
        rank2 = PlayerPrefs.GetInt(kPlayerPrefsRank2);
        rank3 = PlayerPrefs.GetInt(kPlayerPrefsRank3);

        _rankTextController1.DispRankScore = rank1;
        _rankTextController2.DispRankScore = rank2;
        _rankTextController3.DispRankScore = rank3;
    }

    private bool RankingUpdate()
    {
        rank1 = PlayerPrefs.GetInt(kPlayerPrefsRank1);
        rank2 = PlayerPrefs.GetInt(kPlayerPrefsRank2);
        rank3 = PlayerPrefs.GetInt(kPlayerPrefsRank3);

        Debug.Log($"score {score} {rank3},{rank2},{rank1}");
        if (score <= rank3)
        {
            Debug.Log($"score {score} < {rank3} rank3");
            _rankTextController1.DispUpdateRankScore = rank1;
            _rankTextController2.DispUpdateRankScore = rank2;
            _rankTextController3.DispUpdateRankScore = rank3;
            return false;
        }

        if (score > rank1)
        {
            rank3 = rank2;
            rank2 = rank1;
            rank1 = score;
            _rankTextController1.IsScoreUpdate = true;
        }
        else if (score > rank2)
        {
            rank3 = rank2;
            rank2 = score;
            _rankTextController2.IsScoreUpdate = true;
        }
        else if (score > rank3)
        {
            rank3 = score;
            _rankTextController3.IsScoreUpdate = true;
        }
        else
        {
            Debug.Log($"RankingUpdate Error : {score}");
            return false;
        }

        _rankTextController1.DispUpdateRankScore = rank1;
        _rankTextController2.DispUpdateRankScore = rank2;
        _rankTextController3.DispUpdateRankScore = rank3;
        Debug.Log($"PlayerPrefs Set");
        PlayerPrefs.SetInt(kPlayerPrefsRank1, rank1);
        PlayerPrefs.SetInt(kPlayerPrefsRank2, rank2);
        PlayerPrefs.SetInt(kPlayerPrefsRank3, rank3);
        return true;
    }

    public void SceneChange(string sceneName)
    {
        _resultPerformanceManager.SceneChangeAnim(sceneName);
    }

    private void ScoreInit()
    {
        Debug.LogWarning($"PlayerPrefs Init");

        PlayerPrefs.DeleteAll();

        //PlayerPrefs.SetInt(kPlayerPrefsScore, 50);
        //PlayerPrefs.SetInt(kPlayerPrefsRank1, 30);
        //PlayerPrefs.SetInt(kPlayerPrefsRank2, 20);
        //PlayerPrefs.SetInt(kPlayerPrefsRank3, 10);
    }
}
