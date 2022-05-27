using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SurfaceAyarlama : MonoBehaviour
{
    [SerializeField] NavMeshSurface _surface;
    Vector3 pozisyon;
    void Start()
    {
        StartCoroutine(SetFunctions());
    }
    //Oyun baþladýktan 2 sn sonra enemy oluþuyor.
    //En son oluþan odanýn pozisyonunu 1.5 saniye sonra alýyoruz.
    //Hata verirse aralardaki süreyi uzatmayý dene.
    private void Teleporting()  //1.8 olsun.
    {
        this.gameObject.transform.position = pozisyon;
    }
    private void GetPositionFromOutside()   // 1.6 olsun.
    {
        pozisyon = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>().lastRoomPosition;
    }
    private void BuildingNavv() //1.9 olsun.
    {
        _surface.BuildNavMesh();
      //  Invoke(nameof(reBuildNav),2.0f);
        
    }
   // private void reBuildNav()
   // {
   //     _surface.RemoveData();
   // }
    IEnumerator SetFunctions()
    {
        yield return new WaitForSeconds(1.6f);
        GetPositionFromOutside();
        yield return new WaitForSeconds(.2f);
        Teleporting();
        yield return new WaitForSeconds(.1f);
        BuildingNavv();
        
    }

    



}
