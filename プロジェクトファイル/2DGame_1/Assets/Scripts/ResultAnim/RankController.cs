using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RankController : MonoBehaviour
{
    [SerializeField] private ResultSceneManager _resultSceneManager;
    [SerializeField] private RankTextController _rankTextController;
    [SerializeField] private int _rankPanelNum;

    private Animator anim;

    private const float kSceneChangeSpeed = 0.5f;
    private const float kStartMoveOffset = 15.0f;
    private const float kChangeMoveOffset = 25.0f;
    private const float kScoreUpdateMoveOffset = 15.0f;
    private const float kScoreUpdatePosX = -15.0f;
    private float time = 0.0f;
    private float posX = 0.0f;
    private float posY = 0.0f;
    private float endPosX = 0.0f;
    private float endPosY = 0.0f;
    private bool isStartAnim = false;
    private bool isChangeAnim = false;
    private bool isScoreUpdateAnim = false;
    private bool isScoreUpdate = false;
    private bool isScoreUpdateInit = false;

    private Vector3 startPos = new Vector3(-5, -7, 0);
    private Vector3 changePos;

    Vector3 v3 = new Vector3(-100, -100, 0);

    // Start is called before the first frame update
    void Start()
    {
        //anim = gameObject.GetComponent<Animator>();
        this.transform.position = startPos;
        //this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isStartAnim)
        {
            time += Time.deltaTime;
            StartAnim();
        }

        if (isChangeAnim)
        {
            time += Time.deltaTime;
            SceneChangeAnim();
        }

        if (isScoreUpdateAnim)
        {
            time += Time.deltaTime;
            ScoreUpdateAnim();
        }
    }

    public void ScoreUpdateAnim()
    {
        if (!isScoreUpdate)
        {
            //kScoreUpdatePosX

            if (!isScoreUpdateInit)
            {
                isScoreUpdateInit = true;
                isScoreUpdateAnim = true;
                time = 0;
                changePos = this.transform.position;
                endPosX = kScoreUpdatePosX;
            }

            if (endPosX > this.transform.position.x)
            {
                this.transform.position = new Vector3(endPosX, this.transform.position.y, 0);
                _rankTextController.TextUpdate();
                isScoreUpdate = true;
                isScoreUpdateInit = false;
                return;
            }

            posX = time * kScoreUpdateMoveOffset * -1 + changePos.x;
            posY = this.transform.position.y;
            this.transform.position = new Vector3(posX, posY, 0);
        }
        else
        {
            if (!isScoreUpdateInit)
            {
                isScoreUpdateInit = true;
                time = 0;
                //this.transform.position = startPos;
                //_rankTextController.InActive();
                endPosX = startPos.x;
            }

            if (endPosX < this.transform.position.x)
            {
                this.transform.position = new Vector3(endPosX, this.transform.position.y, 0);
                //_rankTextController.Active();
                isScoreUpdateAnim = false;
                _resultSceneManager.RankingUpdateAnimComplete();
                return;
            }

            posX = time * kScoreUpdateMoveOffset + kScoreUpdatePosX;
            posY = this.transform.position.y;
            this.transform.position = new Vector3(posX, posY, 0);
        }
        
    }

    public void StartAnim()
    {
        //float endTime = kSceneChangeSpeed;

        if (!isStartAnim)
        {
            isStartAnim = true;
            time = 0;
            this.transform.position = startPos;
            _rankTextController.InActive();
            switch (_rankPanelNum)
            {
                case 1:
                    endPosY = 2.5f;
                    break;

                case 2:
                    endPosY = 0.0f;
                    break;

                case 3:
                    endPosY = -2.5f;
                    break;
            }
        }

        if (endPosY < this.transform.position.y)
        {
            this.transform.position = new Vector3(this.transform.position.x, endPosY, 0);
            _rankTextController.TextDisp();
            _rankTextController.Active();
            isStartAnim = false;
            return;
        }

        posX = this.transform.position.x;
        posY = time * kStartMoveOffset + startPos.y;
        this.transform.position = new Vector3(posX, posY, 0);
    }

    public void SceneChangeAnim()
    {
        if (!isChangeAnim)
        {
            isChangeAnim = true;
            time = 0;
            changePos = this.transform.position;
            endPosY = startPos.y;
        }

        if (endPosY > this.transform.position.y)
        {
            this.transform.position = new Vector3(this.transform.position.x, endPosY, 0);
            _rankTextController.InActive();
            isChangeAnim = false;
            return;
        }

        posX = this.transform.position.x;
        posY = time * kChangeMoveOffset * -1 + changePos.y;
        this.transform.position = new Vector3(posX, posY, 0);

        //float endRotaTime = kSceneChangeSpeed;

        //if (!isChangeAnim)
        //{
        //    isChangeAnim = true;
        //    time = 0;
        //    this.gameObject.SetActive(true);
        //}

        //if (endRotaTime < time)
        //{
        //    this.gameObject.SetActive(false);
        //    return;
        //}

        //posX = time * -0.5f + kPosXOffset;
        //posY = this.transform.position.y;
        //this.transform.position = new Vector3(posX, posY, 0);
    }


    public void InActive()
    {
        this.gameObject.SetActive(false);
    }
}
