using UnityEngine;

public class expand : MonoBehaviour
{
    // Global fields. Serialized fields can be modified directly from the Unity inspector.
    [SerializeField] float expand_speed = 3;
    Transform cube_transform;   // Will hold a reference to this gameObject's transform object

    // Start is called once on object creation
    void Start()
    {
        cube_transform = GetComponent<Transform>(); // Finds and returns a "Transform" component attached to this gameObject
    }

    // Update is called once per frame
    void Update()
    {
        cube_transform.localScale += new Vector3(0, 0, expand_speed * Time.deltaTime);
    }
}
