using System;
using UnityEngine;

public class ColliderMouseDownListener : MonoBehaviour
{
    public event Action OnClickDown;
    
    private void OnMouseDown()
    {
        OnClickDown?.Invoke();
    }
}
