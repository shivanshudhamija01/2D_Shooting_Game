using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float range = 10f;

    [SerializeField] private GameObject trailprefab;

    [SerializeField] private Transform shootPosition;

    protected float nextFireTime = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 direction = mousePosition -shootPosition.position;

            onShoot(shootPosition.position, direction);

        }
    }


    private void onShoot(Vector2 shootPosition, Vector2 shootdirection)
    {
        RaycastHit2D hit = Physics2D.Raycast(shootPosition, shootdirection, range);

        // Debug.DrawLine(shootPosition, shootdirection, Color.white, .1f);

        traileffect(shootPosition, shootdirection);

        if (hit.collider != null)
        {
            Destroy(hit.collider.gameObject);
        }
    }
    
    private void traileffect(Vector2 start, Vector2 end)
    {
        GameObject trail = Instantiate(trailprefab, start, Quaternion.identity);

        trail.transform.position = end;    
    }
}
