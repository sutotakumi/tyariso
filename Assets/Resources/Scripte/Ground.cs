using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [HideInInspector]
    Rigidbody2D rigidbody;
    [HideInInspector]
    Collider2D collider;
    [HideInInspector]
    public float tileSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();
        collider = this.GetComponent<Collider2D>();
        tileSpeed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = rigidbody.velocity;
        velocity.x = -tileSpeed;
        rigidbody.velocity = velocity;

    }
}
