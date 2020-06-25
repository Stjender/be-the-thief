using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakGlass : MonoBehaviour
{
    public Transform brokenObject;
    public float magnitude;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("test");
        Debug.Log(collision.relativeVelocity);
        Destroy(gameObject);
        Instantiate(brokenObject, transform.position, transform.rotation);
        brokenObject.localScale = transform.localScale;
    }
}
