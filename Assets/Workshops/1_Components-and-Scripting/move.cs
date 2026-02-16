using System;
using UnityEngine;

public class move : MonoBehaviour
{
    // Global fields. Serialized fields can be modified directly from the Unity inspector.
    [SerializeField] float move_speed = 10;
    [SerializeField] float move_magnitude = 4;
    Transform cube_transform;   // Will hold a reference to this gameObject's transform object
    Vector3 original_position;  // The gameObjects coordinate position on start()

    // Start is called once on object creation
    void Start()
    {
        cube_transform = GetComponent<Transform>();     // Finds and returns a "Transform" component attached to this gameObject
        original_position = cube_transform.position;    
    }

    // Update is called once per frame
    void Update()
    {
        float move_amount = move_magnitude * ((float) Math.Sin(move_speed * Time.time));
        cube_transform.position = original_position + new Vector3(0, move_amount, 0);
    }
}
