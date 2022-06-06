using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class BossAI : MonoBehaviour
{

    [SerializeField] BossTakeDamage td;
    StarterAssets.ThirdPersonController tpc;

    public int BossHealth = 100;
    public Animator _animator;

    public bool attack;
    private bool isTriggerAttack;

    public GameObject projectile;



    [SerializeField] NavMeshAgent _navMeshAgent;
    public Transform _player;
    public LayerMask whatIsGround, whatIsPlayer;

    //ForPatrolling
    public Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange;



    //ForChasing
    public float timeBetweenAttack;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public bool DonerekVurma;

    public bool KayarakVurma;

    public float kaymaRange;

    public bool playerInKaymaRange;

    public bool playerKaymaDurdurmaRange;

    public float durdurmaRange;

    [SerializeField] BossAttack _bossAttack;

    float distanceBetweenObjects;


    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        tpc = GameObject.FindGameObjectWithTag("Player").GetComponent<StarterAssets.ThirdPersonController>();

    }
    private void Update()
    {
       
        //Check for sight and attack range (Update'te almasýnýn sebebi: Sürekli kontrol etmemiz lazým.)
        //Bir tane obje olsun(Merkezde tüm prefab'lerin,bu objenin layer'ýný whatIsObject gibi bir þey yapalým, objeRange yapalým bir de... )
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer); // En sondaki layerMask'imiz.
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        playerInKaymaRange= Physics.CheckSphere(transform.position, kaymaRange, whatIsPlayer);
        playerKaymaDurdurmaRange= Physics.CheckSphere(transform.position, durdurmaRange, whatIsPlayer); ;




        if (!playerInSightRange && !playerInAttackRange)
        {
            DonerekVurmaPasif();
            Patrolling();
        }


        if (playerInSightRange && !playerInAttackRange )
        {
            DonerekVurmaPasif();
            ChasePlayer();

        }

        if (playerInSightRange && playerInAttackRange  )
        {
           KayarakVurmaFalselama();
            DonerekVurmaPasif();
            attack = true;
                _animator.SetBool("attack", attack);
            _navMeshAgent.SetDestination(transform.position);
            transform.LookAt(_player);
        }

      if(playerInKaymaRange && tpc.playerHealth>80)
      {
      
           if (!playerInAttackRange)
           {
               attack = false;
               _animator.SetBool("attack", attack);
               KayarakVurmaTruelama();
           }
           if (playerInAttackRange)
           {
               
                attack = true;
                _animator.SetBool("attack", attack);
                KayarakVurmaFalselama();
           }

            

        }
      


        if (playerInSightRange && playerInAttackRange )
        {
            KayarakVurmaFalselama();
            attack = false;
            _animator.SetBool("attack", attack);
            DonerekVurmaAktif();
            _navMeshAgent.SetDestination(transform.position);
            transform.LookAt(_player);
        }




        if (tpc.playerHealth <= 0)
        {
            Patrolling();
            

        }

        if (td.isDead == true)
        {
            Dead();
        }

    }

    private void takeDamageTruelama()
    {
        td.getHit = true;
    }

    private void Patrolling()
    {
        KayarakVurmaFalselama();
        DonerekVurma = false;
        _animator.SetBool("DonerekVurma", DonerekVurma);
        _animator.SetBool("isSeePlayer", false);
        attack = false;
        _animator.SetBool("attack", attack);
        if (!walkPointSet) SearchWalkPoint(); //Þu kýsmý iyi anla. Set'lenmemiþse SearchWalkPoint'i çalýþtýracak.

        if (walkPointSet)
        {
            _navMeshAgent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached...

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }
    private void SearchWalkPoint()
    {
        _navMeshAgent.speed = 4f;
        _animator.SetBool("isSeePlayer", false);
        //Calculate Random point in Range.
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))  //Ground olarak belirlediðimiz yerden çýkýp çýkmadýðýný denetliyoruz, eðer çýkmýþsa bunun içerisine girmeyecek.                                                                  //Bunu da raycast kullanarak belirliyoruz.
        {
            walkPointSet = true;
        }

    }
    private void ChasePlayer()
    {
        KayarakVurmaFalselama();
        DonerekVurma = false;
        _animator.SetBool("DonerekVurma", DonerekVurma);
        _navMeshAgent.speed = 10f;
        attack = false;
        _animator.SetBool("isSeePlayer", true);
        _animator.SetBool("attack", attack);
        _navMeshAgent.SetDestination(_player.position);
    }
    private void AttackPlayer()
    {

        Attacking();



        _navMeshAgent.SetDestination(transform.position);
        transform.LookAt(_player);
        
    }
    private void Attacking()
    {

       
            attack = true;
            _animator.SetBool("attack", attack);
        
        
    }

    private void Dead()
    {
        YerindeKalma();
    }
    private void YerindeKalma()
    {
        _navMeshAgent.SetDestination(transform.position);
    }
    
    IEnumerator DonerekVurmaAyarlama()
    {
        DonerekVurmaAktif();
        yield return new WaitForSeconds(.7f);
        DonerekVurmaPasif();
    }
    public void DonerekVurmaAktif()
    {
        DonerekVurma = true;
        _animator.SetBool("DonerekVurma", DonerekVurma);
    }
    public void DonerekVurmaPasif()
    {
        DonerekVurma = false;
        _animator.SetBool("DonerekVurma", DonerekVurma);
    }
    public void AttackFalselamaa()
    {
        attack = false;
        _animator.SetBool("attack", attack);
    }
    public void KayarakVurmaTruelama()
    {
        KayarakVurma = true;
        _animator.SetBool("KayarakVurma", KayarakVurma);
    }
    public void KayarakVurmaFalselama()
    {
        KayarakVurma = false;
        _animator.SetBool("KayarakVurma", KayarakVurma);
    }

}
