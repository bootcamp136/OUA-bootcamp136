using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AddGeneratedRooms : MonoBehaviour
{
    public List <GameObject> surfaces;
    public List<NavMeshSurface>  _navMeshSurfaces;  //SerializeFieldDemeyegerekyok
    void Awake()
    {

        Invoke(nameof(ZeminleriEkleme), 1.7f);
        Invoke(nameof(Ekleme), 2.0f);
        Invoke(nameof(Olusturma), 2.5f);
    }

    private void ZeminleriEkleme()
    {
        surfaces.AddRange(GameObject.FindGameObjectsWithTag("Zemin")); 
    }
    private void Ekleme()
    {
       for(int i = 0; i < surfaces.Count; i++)
        {
            _navMeshSurfaces.Add(surfaces[i].GetComponent<NavMeshSurface>());
        }
       
    }

    private void Olusturma()
    {
        for (int i = 0; i < _navMeshSurfaces.Count; i++)
        {
            _navMeshSurfaces[i].BuildNavMesh();
        }
    }
      
}
