using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Shootah : MonoBehaviour {
    public GameObject Bullet;

    // Update is called once per frame
    void Update() {
        AimAtTarget(SearchForTarget());
        if(Input.GetKeyDown(KeyCode.R)){
            Shoot();
        }
    }

    private void AimAtTarget(GameObject target) {
        if (target != null) {
            // Calculate the direction to the target
            Vector2 direction = target.transform.position - transform.position;

            // Calculate the angle in degrees
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Rotate the object towards the target
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    private GameObject SearchForTarget(){
        return FindAnyObjectByType<PlayerStateMachine>().transform.gameObject;
    }

    private void Shoot(){
        var newBullet = Instantiate(Bullet, transform.GetChild(0).transform.position + transform.forward*500, transform.rotation, transform.parent);
        Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = transform.right * 10;
    }
    // private void DrawLaser() {
    //     float length = 15f;
    //     Debug.DrawRay(transform.position, Vector3.forward * length, Color.green);

    //     Ray ray = new(transform.position, Vector3.down);

    //     if (Physics.Raycast(ray, out RaycastHit hit, length)) {
    //         Debug.DrawRay(transform.position, Vector3.forward * length, Color.red);
    //     }
    // }


}
