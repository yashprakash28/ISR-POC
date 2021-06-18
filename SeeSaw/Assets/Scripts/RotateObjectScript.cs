using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class RotateObjectScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float Speed = 2f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, Time.deltaTime * Speed, 0f);
    }
}
