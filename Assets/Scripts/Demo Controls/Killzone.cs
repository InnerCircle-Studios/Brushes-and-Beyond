using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(other.gameObject);
    }
}
