using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudAnimation : MonoBehaviour
{

    public float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void FixedUpdate()
    {
        float newX = transform.position.x - speed * Time.deltaTime;

        if (newX < -16)
            newX = 16;

        transform.position = new Vector3(newX,
            transform.position.y,
            transform.position.z
            );
    }
}
