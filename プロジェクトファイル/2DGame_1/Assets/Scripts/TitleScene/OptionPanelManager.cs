using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionPanelManager : MonoBehaviour
{
    [SerializeField] private OptionBackButtonController _optionBackButtonController;

    [SerializeField] private Vector3 startPos = new Vector3(-20, 0, 0);
    [SerializeField] private Vector3 stayPos = new Vector3(0, 0, 0);
    private float kAnimSpeed = 30f;
    private float time = 0.0f;
    private float posX = 0.0f;
    private float posY = 0.0f;
    private bool isInAnim = false;
    private bool isOutAnim = false;

    private Vector3 changePos;

    //Vector3 v3 = new Vector3(-100, -100, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInAnim)
        {
            time += Time.deltaTime;
            InAnim();
        }

        if (isOutAnim)
        {
            time += Time.deltaTime;
            OutAnim();
        }
    }

    public void InAnim()
    {
        if (!isInAnim)
        {
            isInAnim = true;
            time = 0;
            changePos = this.transform.position;
        }

        if (stayPos.x < this.transform.position.x)
        {
            _optionBackButtonController.EndIn();
            this.transform.position = new Vector3(stayPos.x, this.transform.position.y, 0);
            isInAnim = false;
            return;
        }

        posX = time * kAnimSpeed + changePos.x;
        posY = this.transform.position.y;
        this.transform.position = new Vector3(posX, posY, 0);

    }

    public void OutAnim()
    {
        if (!isOutAnim)
        {
            isOutAnim = true;
            time = 0;
            changePos = this.transform.position;
        }

        if (startPos.x > this.transform.position.x)
        {
            this.transform.position = new Vector3(startPos.x, this.transform.position.y, 0);
            isOutAnim = false;
            return;
        }

        posX = time * kAnimSpeed * -1 + changePos.x;
        posY = this.transform.position.y;
        this.transform.position = new Vector3(posX, posY, 0);

    }
}
