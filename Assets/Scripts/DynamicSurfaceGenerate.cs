using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DynamicSurfaceGenerate : MonoBehaviour
{
    [SerializeField] NavMeshSurface _navMeshSurface;

    void Start()
    {
        Invoke(nameof(Generating), 2f);
    }


    private void Generating()
    {
        _navMeshSurface.BuildNavMesh();
    }
 
}
