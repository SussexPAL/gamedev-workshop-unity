using UnityEngine;

public class recolor : MonoBehaviour
{
    // Global fields. Serialized fields can be modified directly from the Unity inspector.
    [SerializeField] float recolor_speed = 1;
    Material cube_material;   // Will hold a reference to this gameObject's transform object

    // Start is called once on object creation
    void Start()
    {
        cube_material = GetComponent<MeshRenderer>().material; // Finds and returns a "MeshRenderer" component attached to this gameObject, and references its material field
    }

    // Update is called once per frame
    void Update()
    {   
        cube_material.color = new Color(0, 0, (Time.time * recolor_speed) % 1);
    }
}
