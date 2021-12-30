using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class Awareness : MonoBehaviour
{
    [SerializeField] private LayerMask layer;
    [Header("Sight Position")] [SerializeField] private Transform scannerPosition;
    [SerializeField] private float sightRange = 10f;
    
    public List<GameObject> enemyList = new List<GameObject>();
    public GameObject closestEnemy;
    
    private Collider[] objectsWithinSight = new Collider[10];
    private void OnEnable()
    {
        InvokeRepeating("LookForTargets", .25f, .25f);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
    public void LookForTargets()
    {
        Array.Clear(objectsWithinSight,0,10);
        enemyList.Clear();
        
        int size  = Physics.OverlapSphereNonAlloc(transform.position, sightRange, objectsWithinSight, layer);
       
        GameObject tempTarget;
        for (int i = 0; i < objectsWithinSight.Length; i++)
        {
            if (objectsWithinSight[i] == null) continue;
            tempTarget = objectsWithinSight[i].gameObject;

            if (tempTarget.tag.Contains("Enemy"))
            {
                enemyList.Add(tempTarget);
            }
        }

        Debug.Log("overlap result " + size);
        
    }
    
    public GameObject GetClosestEnemy()
    {
        return enemyList.Where(i=>i != null)
            .OrderBy(c => Vector3.Distance(scannerPosition.position, c.transform.position))
            .FirstOrDefault();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere (transform.position, sightRange);
    }
}
