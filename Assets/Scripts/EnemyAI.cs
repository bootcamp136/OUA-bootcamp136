using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class EnemyAI : MonoBehaviour
{
   
    [SerializeField] takeDamage td;
    StarterAssets.ThirdPersonController tpc;

    public int enemyHealth = 100;
    public Animator _animator;

    public bool attack;
    private bool isTriggerAttack;

    //Attack için gameObje üretimi
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
        if (!playerInSightRange && !playerInAttackRange)
        {
            Patrolling();
        }
       

        if(playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
            
        }

        if (playerInSightRange && playerInAttackRange )
        {
            AttackPlayer();
           
        }
       
        if (tpc.playerHealth <= 0)
        {
            Patrolling();
        }
        
        if (td.isDead==true)
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

        if(distanceToWalkPoint.magnitude< 1f)
        {
            walkPointSet = false;
        }
    }
    private void SearchWalkPoint()
    {
        _navMeshAgent.speed = .5f;
        _animator.SetBool("isSeePlayer", false);
        //Calculate Random point in Range.
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x+randomX,transform.position.y,transform.position.z+randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))  //Ground olarak belirlediðimiz yerden çýkýp çýkmadýðýný denetliyoruz, eðer çýkmýþsa bunun içerisine girmeyecek.                                                                  //Bunu da raycast kullanarak belirliyoruz.
        {
            walkPointSet = true; 
        }

    }
    private void ChasePlayer()
    {
        _navMeshAgent.speed = 1.0f;
        attack = false;
        _animator.SetBool("isSeePlayer",true);
        _animator.SetBool("attack", attack);
        _navMeshAgent.SetDestination(_player.position);
        _navMeshAgent.speed = 10.0f;
    }
    private void AttackPlayer()
    {
     
            Invoke(nameof(Attacking), .2f);
        
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
}
