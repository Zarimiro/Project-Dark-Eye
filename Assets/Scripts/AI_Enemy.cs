using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

class AI_Enemy : MonoBehaviour {
    
    public enum AI_State_Machine {
        // Hash cods for names of states
      IDLE = 2081823275,
      PATROL = 207038023,
      CHASE = 1463555229,
      ATTACK = 1080829965,
    };

    public AI_State_Machine Current_State = AI_State_Machine.IDLE;
    public float FieldOfView = 180f;
    int Sightmask;
  //  Animator anim;
    NavMeshAgent NavigAgent;
    bool CanSeePlayer; Transform ThisTransform;
    float DisEps=3f;
    float ChaseTimeout = 3f;
    Collider ThisCollider;
    Transform PlayerTransform;
    float CurrentSpeed = 10f;

    GameObject[] waypoints;
    Transform[] Waypoints;
   
    // Use this for initialization
    void Awake () {
        Sightmask = LayerMask.GetMask("Enviroment");
     //   anim = GetComponent<Animator>();
        NavigAgent = GetComponent<NavMeshAgent>();
        ThisTransform = transform;
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        ThisCollider = GetComponent<SphereCollider>();
        //int p = Animator.StringToHash("Idle");
        //Debug.Log(p.ToString());
        waypoints = GameObject.FindGameObjectsWithTag("WayPoint");
        Waypoints = (from GameObject GO in waypoints select GO.transform).ToArray();
        StartCoroutine(State_Idle());
    }

    public IEnumerator State_Idle() {
        Current_State = AI_State_Machine.IDLE;
       // anim.SetTrigger((int)AI_State_Machine.IDLE);
        NavigAgent.Stop();
        while (Current_State == AI_State_Machine.IDLE) {
            if (CanSeePlayer) {
                StartCoroutine(State_Chase());
                yield break;
            }

            if (CanSeePlayer && Vector3.Distance(ThisTransform.position, PlayerTransform.position) <= DisEps) {
                StartCoroutine(State_Attack());
                yield break;

            }

            float Timer = 0f;
            while (Timer <= 3f)
            {
                Timer += Time.deltaTime;
                yield return null;

            }

            StartCoroutine(State_Patrol());
            yield break;

        }
    }
    public IEnumerator State_Chase()
    {
            Current_State = AI_State_Machine.CHASE;
          //  anim.SetTrigger((int)AI_State_Machine.CHASE);
            while (Current_State == AI_State_Machine.CHASE)
            {
                NavigAgent.SetDestination(PlayerTransform.position);
                if (!CanSeePlayer)
                {
                    float ElapsedTime = 0f;
                    while (true)
                    {
                    NavigAgent.Resume();
                    NavigAgent.SetDestination(PlayerTransform.position);
                         NavigAgent.speed = 10f;  
                        ElapsedTime += Time.deltaTime;
                     //   yield return null;
                        if (ElapsedTime >= ChaseTimeout)
                        {
                            if (!CanSeePlayer)
                            {
                            NavigAgent.speed = 4f;
                                StartCoroutine(State_Idle());
                                yield break;
                            }
                            else  break; 
                        }
                    }
                }
                if (Vector3.Distance(ThisTransform.position, PlayerTransform.position) <= DisEps)
                {
                    StartCoroutine(State_Attack());
                    yield break;
                }
                yield return null;
            }  
    }

    public IEnumerator State_Attack() {
        Current_State = AI_State_Machine.ATTACK;
      //  anim.SetTrigger((int)AI_State_Machine.ATTACK);
        NavigAgent.Stop();
        float EllapsedTime = 0f;
        while (Current_State == AI_State_Machine.ATTACK) {
            if (!CanSeePlayer || Vector3.Distance(ThisTransform.position, PlayerTransform.position)> DisEps) {
                NavigAgent.Resume();
                StartCoroutine(State_Chase());
                yield break;
            }
            EllapsedTime += Time.deltaTime;
            if (EllapsedTime >= 1.5f) {
              //  anim.SetTrigger((int)AI_State_Machine.ATTACK);
                EllapsedTime = 0f;
                yield return null;
            }
            
            yield return null;
        }
       
    }  

    public void OnIdleStateCompleted() {

        StopAllCoroutines();
        StartCoroutine(State_Patrol());

    }

    public IEnumerator State_Patrol() {
        
        Current_State = AI_State_Machine.PATROL;
       // anim.SetTrigger((int)AI_State_Machine.PATROL);

        Transform RandomDest = Waypoints[Random.Range(0, Waypoints.Length)];
        NavigAgent.SetDestination(RandomDest.position);
        NavigAgent.Resume();
        while (Current_State == AI_State_Machine.PATROL)
        {
            if (CanSeePlayer) {
                StartCoroutine(State_Chase());
                yield break;
            }
            if (Vector3.Distance(ThisTransform.position, RandomDest.position) <= DisEps) {
                StartCoroutine(State_Idle());
                yield break;
            }
            
            yield return null;


        }
    }

    void Update()
    {

        Debug.Log(Current_State.ToString());
        CanSeePlayer = false;

        if (!ThisCollider.bounds.Contains(PlayerTransform.position)) return;

        CanSeePlayer = HaveLineOfSight(PlayerTransform);
    }

    private bool HaveLineOfSight(Transform PlayerTransform) {
        float Angle = Mathf.Abs(Vector3.Angle(ThisTransform.forward, (ThisTransform.position-PlayerTransform.position).normalized));
        if (Angle >= FieldOfView) return false;
        if (Physics.Linecast(ThisTransform.position, PlayerTransform.position, Sightmask)) return false;
        return true;
    }
}
