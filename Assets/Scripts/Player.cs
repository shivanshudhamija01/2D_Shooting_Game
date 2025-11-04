using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform gunHead;
    [SerializeField] private Transform gun;
    [SerializeField] private float bulletSpeed = 10f;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GunFire();
        }

        GunPointingTowardsMouse();
    }

    // Shoot
    private void GunFire()
    {
        GameObject bulletInstance = Instantiate(bullet,gunHead.transform.position,gunHead.transform.rotation);
        Rigidbody2D rb = bulletInstance.GetComponent<Rigidbody2D>();
        
        Vector3 shootDirection = GunDirection();
        rb.AddForce(shootDirection*bulletSpeed,ForceMode2D.Impulse);
        Destroy(bulletInstance,3f);
    }

    
    private Vector2 GunDirection()
    {
        // Vector2 shootDirection = new Vector2(0,0);
        // shootDirection = (gunHead.position - transform.position).normalized;
        // return shootDirection;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDirection = (mousePosition - Vector3.zero).normalized;
        return aimDirection;
    }

    private void GunPointingTowardsMouse()
    {
        // Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Vector3 aimDirection = (mousePosition - Vector3.zero).normalized;
        Vector3 aimDirection = GunDirection();
        float angle = Mathf.Atan2(aimDirection.y,aimDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f,0f,angle);
    }
    
}
