using UnityEngine;

public class Monster0103 : monstercontrol
{
    public float attackRange;
    public float trackingRange;
    public float trackingCD;
    public float strollCD;
    public float attackCD = 0.5f;
    public Weapon weapon;
    monsterAI myAI;
    Animator animator;
    float trackingtiming;
    float strolltiming;
    float attacktiming;
    bool seeTarget;
    LayerMask layerMask;
    RaycastHit2D hit;
    void Start()
    {
        myAI = GetComponent<monsterAI>();
        animator = GetComponent<Animator>();
        monsterState = MonsterState.Idle;
        //注册
        weapon.role = role;
        layerMask = ~LayerMask.GetMask("Monster") & ~LayerMask.GetMask("Weapon") & ~LayerMask.GetMask("Room");
    }
    void Update()
    {
       // Debug.LogWarning(monsterState);
        switch (monsterState)
        {
            case MonsterState.Idle:
                Idle();
                break;
            case MonsterState.Tracking:
                Tracking();
                break;
            case MonsterState.Stroll:
                Stroll();
                break;
            case MonsterState.Attack:
                Attack();
                break;
            case MonsterState.Die:
                Die();
                break;
            default:
                break;
        }
    }
    public void UpdateLookAt(Vector2 targetPos)
    {
        if (transform.position.x > targetPos.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (transform.position.x < targetPos.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        weapon.UpdateLookAt(targetPos);
    }
    void Idle()
    {
        if (isStart)
        {
            monsterState = MonsterState.Stroll;
            animator.SetBool("run", true);
        }
    }
    void Tracking()
    {
        if (Time.time - trackingtiming >= trackingCD)
        {
            trackingtiming = Time.time;
            myAI.UpdatePath(targetPosition.position);
        }
        myAI.NextTarget();
        if (Vector2.Distance(transform.position, targetPosition.position) <= attackRange)
        {
            monsterState = MonsterState.Attack;
            animator.SetBool("run", false);
        }

        RaycastDetection();
        if (!seeTarget)
        {
            monsterState = MonsterState.Stroll;
        }
        UpdateLookAt(myAI.nextTargetPosition);
    }
    void Stroll()
    {
        RaycastDetection();
        if (seeTarget)
        {
            monsterState = MonsterState.Tracking;
        }
        UpdateLookAt(myAI.nextTargetPosition);
        if (Time.time - strolltiming >= strollCD)
        {
            strolltiming = Time.time;
            myAI.RandomPath();
        }
        myAI.NextTarget();
    }
    void Attack()
    {
        if (Vector2.Distance(transform.position, targetPosition.position) > attackRange)
        {
            monsterState = MonsterState.Tracking;
        }
        UpdateLookAt(targetPosition.position);
        if (Time.time - attacktiming >= attackCD)
        {
            attacktiming = Time.time;
            weapon.ShootButtonDown();
        }
    }
    void Die()
    {
        myAI.NextTarget();
    }
    public override void BeAttack(float data)
    {
        base.BeAttack(data);
        if (hp <= 0)
        {
            myAI.UpdatePath((transform.position - targetPosition.position).normalized * 2 + transform.position);
            weapon.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// 视野检测
    /// </summary>
    public void RaycastDetection()
    {
        hit = Physics2D.Raycast(transform.position + Vector3.up, (targetPosition.position - (transform.position + Vector3.up)).normalized, trackingRange, layerMask);
        //Debug.DrawLine(transform.position+Vector3.up, transform.position+(targetPosition.position - transform.position).normalized*10, Color.red);

        if (hit.transform != null && hit.transform == targetPosition)
        {
            seeTarget = true;
            Debug.DrawLine(transform.position + Vector3.up, hit.transform.position, Color.red);
        }
        else
        {
            seeTarget = false;
        }
    }
}
