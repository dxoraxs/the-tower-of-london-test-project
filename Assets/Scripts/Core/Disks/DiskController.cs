using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using DG.Tweening;
using UnityEngine;

public class DiskController : MonoBehaviour
{
    [SerializeField] private DiskContainer[] _disks;
    [SerializeField] private float _speed = 1;
    private readonly Dictionary<DiskType, DiskContainer> _diskContainers = new();

    [ContextMenu("Find all children disks")]
    private void FindAllDisks()
    {
        _disks = GetComponentsInChildren<DiskContainer>();
    }

    private void Start()
    {
        foreach (var diskContainer in _disks)
        {
            _diskContainers.Add(diskContainer.Type, diskContainer);
        }
    }

    public void FastSetDisksToColumn(Dictionary<DiskType, Vector3> positions)
    {
        foreach (var position in positions)
        {
            _diskContainers[position.Key].transform.position = position.Value;
        }
    }

    public async UniTask StartMoveToPoint(DiskType type, Vector3 position)
    {
        var duration = (position - _diskContainers[type].transform.position).magnitude / _speed;
        await _diskContainers[type].transform.DOMove(position, duration).SetSpeedBased().ToUniTask();
    }

    public async UniTask StartMoveToPoint(DiskType type, Vector3[] positions)
    {
        foreach (var position in positions)
        {
            await StartMoveToPoint(type, position);
        }
    }
}