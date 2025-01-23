using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScreenGeneric<Screen> : BaseScreen where Screen : BaseScreen
{
    protected static string ViewAddress => typeof(Screen).Name;

    #region Singleton
    private static Screen instance;
    public static Screen Instance => instance ?? InitInstance();

    private static Screen InitInstance()
    {
        instance = Instantiate(Resources.Load<GameObject>(ViewAddress), null).GetComponent<Screen>();
        instance.Initialize();
        return instance;
    }

    private void OnDestroy()
    {
        instance = null;
    }
    #endregion

    protected bool isActive;

    public override void Initialize()    {    }
    public override void Show() { instance.gameObject.SetActive(true); isActive = true; }
    public override void Hide() { instance.gameObject.SetActive(false); isActive = false; }
}
