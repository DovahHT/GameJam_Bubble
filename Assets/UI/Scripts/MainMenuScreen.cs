using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreen : BaseScreenGeneric<MainMenuScreen>
{
    public Button playButton;

    public override void Initialize()
    {
        base.Initialize();
        playButton.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        MainGameManager.Instance.OpenGameScene();
    }
}
