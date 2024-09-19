using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // ÉVÅ[Éìêÿë÷
using System.Threading.Tasks;  // async

public class TitlePerformanceManager : MonoBehaviour
{
    [SerializeField] private TitleSceneManager _titleSceneManager;

    [SerializeField] private TitleTextController _titleTextController;
    [SerializeField] private TitleButtonController _titleButtonController1;
    [SerializeField] private TitleButtonController _titleButtonController2;
    [SerializeField] private RankResetButtonController _rankResetButtonController;
    [SerializeField] private OptionButtonController _optionButtonController;
    [SerializeField] private OptionPanelManager _optionPanelManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void TitleAnim()
    {
        _titleTextController.StartAnim();
        await Task.Delay(1500);

        await Task.Delay(200);
        _titleButtonController1.StartAnim();
        _titleButtonController2.StartAnim();
        _rankResetButtonController.StartAnim();
        _optionButtonController.StartAnim();
    }

    public async void OptionInAnim()
    {
        await Task.Delay(0);
        _titleButtonController1.SceneChangeAnim();
        _titleButtonController2.SceneChangeAnim();
        //_rankResetButtonController.SceneChangeAnim();
        _optionButtonController.SceneChangeAnim();
        _titleTextController.SceneChangeAnim();

        await Task.Delay(500);
        _optionPanelManager.InAnim();
    }

    public async void OptionOutAnim()
    {
        _optionPanelManager.OutAnim();

        await Task.Delay(500);
        _titleTextController.StartAnim();
        _titleButtonController1.StartAnim();
        _titleButtonController2.StartAnim();
        _rankResetButtonController.StartAnim();
        _optionButtonController.StartAnim();
    }

    public async void SceneChangeAnim(string sceneName)
    {
        await Task.Delay(500);
        _titleButtonController1.SceneChangeAnim();
        _titleButtonController2.SceneChangeAnim();
        //_rankResetButtonController.SceneChangeAnim();
        _optionButtonController.SceneChangeAnim();

        await Task.Delay(300);
        _titleTextController.SceneChangeAnim();

        await Task.Delay(600);
        
        _titleSceneManager.SceneChange(sceneName);

    }
}
