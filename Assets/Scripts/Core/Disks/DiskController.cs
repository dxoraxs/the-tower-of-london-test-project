using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class DiskController : MonoBehaviour
{
    [SerializeField] private DiskContainer _disksPrefab;
    [SerializeField] private float _speed = 1;
    [SerializeField] private Material[] _materials;
    private readonly Dictionary<int, DiskContainer> _diskContainers = new();

    public void InitializeDisks(int count)
    {
        var materials = new List<Material>(_materials);
        for (var i = 0; i < count; i++)
        {
            var newDisk = Instantiate(_disksPrefab, transform);
            _diskContainers.Add(i, newDisk);

            var randomMaterial = materials[Random.Range(0, materials.Count)];
            materials.Remove(randomMaterial);
            
            newDisk.Initialize(i+2, randomMaterial);
        }
    }

    public void DestroyDisks()
    {
        foreach (var diskContainer in _diskContainers)
        {
            Destroy(diskContainer.Value.gameObject);
        }
        _diskContainers.Clear();
    }

    public void FastSetDisksToColumn(Dictionary<int, Vector3> positions)
    {
        foreach (var position in positions)
        {
            _diskContainers[position.Key].transform.position = position.Value;
        }
    }

    public async UniTask StartMoveToPoint(int type, Vector3 position)
    {
        var duration = (position - _diskContainers[type].transform.position).magnitude / _speed;
        await _diskContainers[type].transform.DOMove(position, duration).ToUniTask();
    }

    public async UniTask StartMoveToPoint(int type, Vector3[] positions)
    {
        foreach (var position in positions)
        {
            await StartMoveToPoint(type, position);
        }
    }
}