using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//TODO
//CHASE'LERKENKÝ HIZINI ARTTIR

public class EnemyAI : MonoBehaviour
{
   

    public takeDamage td;
    public StarterAssets.ThirdPersonController tpc;

    public int enemyHealth = 100;
    public Animator _animator;

    public bool attack;
    private bool isTriggerAttack;

    //Attack için gameObje üretimi
    public GameObject projectile;


    public NavMeshAgent _navMeshAgent;
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
    }

    private void Update()
    {
        //Check for sight and attack range (Update'te almasýnýn sebebi: Sürekli kontrol etmemiz lazým.)
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

        if (playerInSightRange && playerInAttackRange)
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



       // if (tpc.atack == true)
       // {
       //     AttackFalselama();
       // }


    }
       // private void AttackFalselama()
       // {
       //     
       //     attack = false;
       //     _animator.SetBool("attack", false);
       //     
       // }

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
        _navMeshAgent.speed = 5.0f;
        _animator.SetBool("isSeePlayer", false);
        //Calculate Random point in Range.
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x+randomX,transform.position.y,transform.position.z+randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))  //Ground olarak belirlediðimiz yerden çýkýp çýkmadýðýný denetliyoruz, eðer çýkmýþsa bunun içerisine girmeyecek.
                                                                          //Bunu da raycast kullanarak belirliyoruz.
        {
            walkPointSet = true; 
        }

    }
    private void ChasePlayer()
    {
        attack = false;
        _animator.SetBool("isSeePlayer",true);
        _animator.SetBool("attack", attack);
        _navMeshAgent.SetDestination(_player.position);
        _navMeshAgent.speed = 10.0f;
        Vector3 distance = _player.position - gameObject.transform.position;
        if (distance.magnitude <= 5)
        {
            _navMeshAgent.speed = 4.0f;
        }
        else
        {
            _navMeshAgent.speed = 10.0f;
        }

    }
    private void AttackPlayer()
    {
        Invoke(nameof(Attacking), .2f);
       


        //Make sure enemy doesn't move 
        _navMeshAgent.SetDestination(transform.position);

        transform.LookAt(_player);

  //     if (!alreadyAttacked)
  //     {
  //         //Attack code here
  //
  //         Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
  //         rb.AddForce(transform.forward * 35f, ForceMode.Impulse);
  //         rb.AddForce(transform.up * 10, ForceMode.Impulse);
  //
  //
  //         alreadyAttacked = true;
  //         Invoke(nameof(ResetAttack),timeBetweenAttack);  //Karakter time kadar bekleyip, sonrasýnda aldreadyAttacked false olduðunda tekrardan projecTile instantiate oluyor.
  //     }
    }
    private void Attacking()
    {
        attack = true;
        _animator.SetBool("attack", attack);
    }
    private void Dead()
    {
        _navMeshAgent.SetDestination(transform.position);
    }
    private void YerindeKalma()
    {
        _navMeshAgent.SetDestination(transform.position);
    }
    //Atýlan objeleri destroy et.
    //Yakýna gelince vuruþ animasyonu ekle, mixamodan çek. (Zombi gibi yapalým, uzaktan hiç vurmasýn hatta.)
    //   private void ResetAttack()
    //   {
    //       alreadyAttacked = false;
    //   }

    // public void TakeDamage(int damage)
    // {
    //     health -= damage;
    //
    //     if (health <= 0)
    //     {
    //         Invoke(nameof(DestroyEnemy),1f);
    //     }
    // }
    // private void DestroyEnemy()
    // {
    //     Destroy(gameObject);
    // }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
   //     Gizmos.color = Color.yellow;
   //     Gizmos.DrawWireSphere(transform.position, sightRange);
        
    }


}
