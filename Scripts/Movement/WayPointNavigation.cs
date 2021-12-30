using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointNavigation : MonoBehaviour
{
    #region Private Members
    [SerializeField] private WaypointPath waypointPath = null;
    [SerializeField] private bool randomPatrol = false;
    [SerializeField] private int currentWaypoint = -1;

    private float idleTimer = 0.0f;
    private float wayPointIdleTimer = 0.0f;
    private float timeSinceReachedWayPoint = 0.0f;

    #endregion
    
    #region Public Varibles

    public Vector2 idleTimeRange = new Vector2(1.0f, 3.0f);
    public float idleTimeBeforeMoving = 0.0f;
    public bool followWayPointPath = true;
    
    #endregion
    
    
    #region Unity Methods
    private void Start()
    {

        if (idleTimeBeforeMoving == 0)
        {
            idleTimeBeforeMoving = Random.Range(idleTimeRange.x, idleTimeRange.y);
        }

        idleTimer = 0.0f;
    }

    private void Update()
    {
        // Update the idle timer
        idleTimer += Time.deltaTime;
    }

    #endregion
    
    //increase current way point or pick a random waypoint
    private void NextWaypoint()
    {
        if (randomPatrol && waypointPath.Waypoints.Count > 1)
        {
            int oldWaypoint = currentWaypoint;
            while (currentWaypoint == oldWaypoint)
            {
                currentWaypoint = Random.Range(0, waypointPath.Waypoints.Count);
            }
        }
        else
            currentWaypoint = (currentWaypoint == waypointPath.Waypoints.Count - 1) ? 0 : currentWaypoint + 1;
    }
    
    public float GetTimeInIdleState()
    {
        return idleTimer;
    }

    public void ResetWayPointIdleTime()
    {
        idleTimer = 0;
    }

    public bool HasWaypointNetwork()
    {
        return waypointPath != null;
    }
    
    public int CurrentWaypoint { get { return currentWaypoint; } }
    
    public Vector3 GetWaypointPosition(bool increment)
    {
       if (increment)
            NextWaypoint();
    
        // Fetch the new waypoint from the waypoint list
        if (waypointPath.Waypoints[currentWaypoint] != null)
        {
            Transform newWaypoint = waypointPath.Waypoints[currentWaypoint];
            return newWaypoint.position;
        }
    
        return Vector3.zero;
    }
    
}
