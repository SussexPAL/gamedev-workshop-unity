using System;
using UnityEngine;

public class rotate : MonoBehaviour
{   
    // Global fields. Serialized fields can be modified directly from the Unity inspector.
    [SerializeField] Vector3 rotation_speed = new Vector3(90, 120, 60);     // How quickly the gameObject should rotate about each axis
    Transform cube_transform;   // Will hold a reference to this gameObject's transform object
    

    // Start is called once on object creation
    void Start()
    {
        cube_transform = GetComponent<Transform>(); // Finds and returns a "Transform" component attached to this gameObject
    }

    // Update is called once per frame
    void Update()
    {
        cube_transform.Rotate(new Vector3(  rotation_speed.x * Time.deltaTime, 
                                            rotation_speed.y * Time.deltaTime, 
                                            rotation_speed.z * Time.deltaTime
                                            ));
    }
}
