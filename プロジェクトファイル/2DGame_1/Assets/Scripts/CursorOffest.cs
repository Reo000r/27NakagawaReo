using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorOffest : MonoBehaviour
{
    //[SerializeField] private int screenWidth = 1280;
    //[SerializeField] private int screenHeight = 720;

    private Vector2 inputScreen = new Vector2(1280, 720);

    private Vector2 defaultScreen = new Vector2(1280, 720);
    private Vector2 screenType1 = new Vector2(1920, 1080);
    private Vector2 cursorOffset = new Vector2(1.0f, 1.0f);

    // Start is called before the first frame update
    void Start()
    {
        inputScreen = new Vector2(Screen.width, Screen.height);

        Debug.Log($"{inputScreen}");

        cursorOffset = defaultScreen / inputScreen;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 GetScreenOffset()
    {
        return cursorOffset;
    }

    public Vector2 GetDefaultScreenSize()
    {
        return defaultScreen;
    }

    public Vector2 GetType1ScreenSize()
    {
        return screenType1;
    }

    public Vector2 GetScreenSize()
    {
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);

        return screenSize;
    }

    public void SetScreenSize(Vector2 screenSize)
    {
        Screen.SetResolution((int)screenSize.x, (int)screenSize.y, false);
    }
}
