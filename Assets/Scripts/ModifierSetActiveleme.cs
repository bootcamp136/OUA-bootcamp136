using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ModifierSetActiveleme : MonoBehaviour
{
    Vector3 thisPosition;
    RoomTemplates _rt;
    Vector3 lastPosition;
    Vector3 secondLastPosition;
    [SerializeField] NavMeshModifierVolume _navMeshModifierVolume;
    
    void Start()
    {
        _rt = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        thisPosition = this.gameObject.transform.position;

        Invoke(nameof(LastVeSecondLastPozisyonuEsitleme), 1.55f);
        Invoke(nameof(KontrolEtme), 1.6f);
    }

    private void LastVeSecondLastPozisyonuEsitleme()
    {
        lastPosition = _rt.rooms[_rt.rooms.Count - 1].transform.position;
        secondLastPosition= _rt.rooms[_rt.rooms.Count - 2].transform.position;
    }

    private void KontrolEtme()
    {
        if( thisPosition==lastPosition || thisPosition == secondLastPosition)
        {
            _navMeshModifierVolume.area = 0;
                //0 walkable, 1 not walkable ...
        }
    }
  
   
}
