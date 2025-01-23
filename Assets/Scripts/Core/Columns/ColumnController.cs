using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ColumnController : MonoBehaviour
{
    public readonly Subject<int> OnColumnClick = new();
    [SerializeField] private ColumnContainer[] _columns;
    [Space, SerializeField] private float _horizontalOffset;
    [SerializeField] private float _verticalDiskOffset;
    [SerializeField] private float _startVerticalDiskOffset;

    public Vector3 GetPositionByIndex(int columnIndex, int verticalIndex)
    {
        var horizontalPositionByColumnIndex = GetHorizontalPositionByColumnIndex(columnIndex);
        var verticalPositionByDiskIndex = GetVerticalPositionByDiskIndex(verticalIndex);
        return new Vector3(horizontalPositionByColumnIndex, verticalPositionByDiskIndex, 0);
    }

    public float GetHorizontalPositionByColumnIndex(int index)
    {
        return _horizontalOffset * (index - _columns.Length / 2f + .5f);
    }

    public float GetVerticalPositionByDiskIndex(int index)
    {
        return _startVerticalDiskOffset + index * (1 + _verticalDiskOffset);
    }

    private void Start()
    {
        for (var i = 0; i < _columns.Length; i++)
        {
            var container = _columns[i];
            var index = i;
            container.SubscribeToMouseDown(() => OnColumnMouseDown(index));
        }
    }

    private void OnColumnMouseDown(int index)
    {
        OnColumnClick.OnNext(index);
    }

    private void OnValidate()
    {
        ResetColumnPosition();
    }

    private void ResetColumnPosition()
    {
        for (var i = 0; i < _columns.Length; i++)
        {
            var column = _columns[i];
            var position = column.transform.position;
            position.x = GetHorizontalPositionByColumnIndex(i);

            column.transform.position = position;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(Vector3.zero + Vector3.up * GetVerticalPositionByDiskIndex(0), new Vector3(4, 1, 1));
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Vector3.zero + Vector3.up * GetVerticalPositionByDiskIndex(1), new Vector3(3, 1, 1));
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero + Vector3.up * GetVerticalPositionByDiskIndex(2), new Vector3(2, 1, 1));
    }

    [ContextMenu("Find all children column")]
    private void FindAllColumn()
    {
        _columns = GetComponentsInChildren<ColumnContainer>();
    }
}