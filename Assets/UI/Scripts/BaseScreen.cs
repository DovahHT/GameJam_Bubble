using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScreen : MonoBehaviour
{
    public abstract void Initialize();
    public abstract void Show();
    public abstract void Hide();
}
