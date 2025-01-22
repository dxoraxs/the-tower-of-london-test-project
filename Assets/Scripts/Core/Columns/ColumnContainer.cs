using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnContainer : MonoBehaviour
{
    [SerializeField] private ColliderMouseDownListener _colliderMouseDownListener;

    public void SubscribeToMouseDown(Action action)
    {
        _colliderMouseDownListener.OnClickDown += action;
    }

    public void UnsubscribeFromMouseDown(Action action)
    {
        _colliderMouseDownListener.OnClickDown -= action;
    }
}