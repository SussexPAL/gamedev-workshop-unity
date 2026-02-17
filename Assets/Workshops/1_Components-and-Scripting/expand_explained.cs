using UnityEngine;
/*
    This is just a copy of the expand.cs script with detailed comments! This script is just for your reference, it is not actually used by anything.
*/



public class expand_explained : MonoBehaviour
{
    // Global fields
        /*
        Here we are going to declare a series of variables that will be accessible from anywhere within this script.

        This is where you might decide to declare:
            - Constants             (ID, color, etc.)
            - State                 (current health, current money, etc.)
            - Serialized fields     (Parameters you may want to edit from the inspector, like movement_speed, jump_height, max_hp, etc.)
            - References            (Variables used to access important external components like a Transform, a RigidBody, a UI component, etc.

        In a real project, this section will fill up very fast (And that's okay!), just make sure to keep your fields as organized as possible.
        */
    [SerializeField] float expand_speed = 3;    // Serialized fields can be modified directly from the Unity inspector.
    Transform cube_transform;                   // Will hold a reference to this gameObject's transform object


    // Start is called once on object creation
    void Start()
    {   
            /*
            Our goal here is to find the Transform component of our gameObject (holding its position, rotation, and scale) and store it in a global variable for later use.
            We are going to make use of the GetComponent<T>() function, where T has to be the component's type. This function just loops through all components attached
            to this gameObject and returns the first match.

            For clarity: each gameObject actually contains a reference to its transform by default, just called "transform". Here we are finding it manually to
            demonstrate the usage of GetComponent(), but in the future feel free to access directly via transform.position, transform.localScale, etc.
            */
        cube_transform = GetComponent<Transform>();
    }


    // Update is called once per frame
    void Update()
    {
        /*
        Our goal here is to perform some operation that leads to changes to the gameObject over time. In this case, we simply make the scale 
        (along the z-axis) increase slowly every frame.

        We can only alter the scale as a 3-dimensional vector (Vector3) rather than directly altering a specific value.
        So, we need to create our own Vector3 and only change the values we care about.
        Here, we create a Vector3 with empty x and y values, and a custom z value. This allows us to change the z value independently.
        - expand_speed is our serialized field that determines expansion speed. So, it makes sense we put it into the z value.
        - we then multiply by Time.deltaTime to ensure consistent expansion speed independent of framerate.
            (Time.deltaTime is just a float value representing how many seconds have passed since the last frame.)
        */
        cube_transform.localScale += new Vector3(0, 0, expand_speed * Time.deltaTime);
    }






}
