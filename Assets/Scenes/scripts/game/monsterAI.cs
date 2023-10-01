using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.IO;
using Path = Pathfinding.Path;
using Unity.VisualScripting;
using Unity.Burst.CompilerServices;

public class monsterAI : MonoBehaviour
{
    public Transform targetPosition;
    public float speed;
    public float randomRadius;
    public Vector3 nextTargetPosition;
    Vector3 targetLastPosition;
    private Path path;
    private int currentWaypoint=0;
    Seeker seeker;
    private bool reachedEndOfPath;
    [HideInInspector]
    private float nextWaypointDistance;
    // Start is called before the first frame update
    public void Start()
    {
        seeker = GetComponent<Seeker>();
        seeker.StartPath(transform.position,targetPosition.position, OnPathComplete);
        targetPosition = GetComponent<Transform>();
    }
    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {

            path = p;

            currentWaypoint = 0;

        }
    }
    public void RandomPath()
    {

        var point = Random.insideUnitSphere * randomRadius;

        point += transform.position;

        UpdatePath(point);

    }
    public void UpdatePath(Vector2 targetPosition)
    {

        if (Vector2.Distance(targetPosition, targetLastPosition) > 1)
        {

            targetLastPosition = targetPosition;

            seeker.StartPath(transform.position, targetPosition, OnPathComplete);

        }
    }
    public void NextTarget()
    {

        if (path == null) { return; }

        reachedEndOfPath = false;//标记是否已经到达目标点

        float distanceToWaypoint;

        while (true)
        {

            distanceToWaypoint = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);

            if (distanceToWaypoint < nextWaypointDistance)
            {

                if (currentWaypoint + 1 < path.vectorPath.Count) { currentWaypoint++; }

                else { reachedEndOfPath = true; break; }

            }

            else { break; }

        }

        var speedFactor = reachedEndOfPath ? Mathf.Sqrt(distanceToWaypoint / nextWaypointDistance) : 1f;

        if (!reachedEndOfPath)
        {

            nextTargetPosition = path.vectorPath[currentWaypoint];

            Vector3 dir = (nextTargetPosition - transform.position).normalized;

            Vector3 velocity = dir * speed * speedFactor;

            transform.position += velocity * Time.deltaTime;

        }

        else { path = null; }

    }
    void Update()
    {
        UpdatePath(targetPosition.position);
        NextTarget();
    }
}
