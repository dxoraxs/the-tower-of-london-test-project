using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskContainer : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;

    public void Initialize(float scale, Material material)
    {
        transform.localScale = new Vector3(scale, 1, scale);
        _meshRenderer.material = material;
    }
}