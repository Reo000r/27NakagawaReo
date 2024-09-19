using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultScorePanelController : MonoBehaviour
{
    private const float kStartMoveOffset = 40.0f;
    private const float kChangeMoveOffset = 60.0f;
    private float time = 0.0f;
    private float posX = 0.0f;
    private float posY = 0.0f;
    private float endPosX = 0.0f;
    private float endPosY = 0.0f;
    private bool isStartAnim = false;
    private bool isChangeAnim = false;
    Vector3 startPos = new Vector3(15, 1.5f, 0);
    Vector3 changePos;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = startPos;
        //this.gameObject.SetActive(false);
    }

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
    }

    public void StartAnim()
    {
        //float endTime = kSceneChangeSpeed;

        if (!isStartAnim)
        {
            isStartAnim = true;
            time = 0;
            this.transform.position = startPos;
            endPosX = 3.0f;
        }

        if (endPosX > this.transform.position.x)
        {
            this.transform.position = new Vector3(endPosX, this.transform.position.y, 0);
            isStartAnim = false;
            return;
        }

        posX = time * kStartMoveOffset * -1 + startPos.x;
        posY = this.transform.position.y;
        this.transform.position = new Vector3(posX, posY, 0);
    }

    public void SceneChangeAnim()
    {
        //float endTime = kSceneChangeSpeed;

        if (!isChangeAnim)
        {
            isChangeAnim = true;
            time = 0;
            changePos = this.transform.position;
            endPosX = startPos.x;
        }

        if (endPosX < this.transform.position.x)
        {
            this.transform.position = new Vector3(endPosX, this.transform.position.y, 0);
            isChangeAnim = false;
            return;
        }

        posX = time * kChangeMoveOffset + changePos.x;
        posY = this.transform.position.y;
        this.transform.position = new Vector3(posX, posY, 0);
    }

}
