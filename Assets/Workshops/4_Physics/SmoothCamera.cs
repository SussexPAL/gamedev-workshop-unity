using Unity.VisualScripting;
using UnityEngine;



public class SmoothCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float followSpeed;
    Vector3 offset;             // How far from the target should the camera sit
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = target.position - transform.position;  // Record the default camera offset (distance at scene start)
    }

    // Update is called once per frame
    void Update () {
        Vector3 targetPos = target.position - offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
    }
}
