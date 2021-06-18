using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampPosition : MonoBehaviour
{
    public bool pos_clampX, pos_clampY, pos_clampZ;
    public float localX_pos_min, localX_pos_max; // -1.01, -0.76f for vice_left
    public float localY_pos_min, localY_pos_max;
    public float localZ_pos_min, localZ_pos_max;

    void Start()
    {

    }

    void FixedUpdate()
    {
        if(pos_clampX)
        {
            transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, localX_pos_min, localX_pos_max), transform.localPosition.y, transform.localPosition.z);
        }
        if(pos_clampY)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Clamp(transform.localPosition.y, localY_pos_min, localY_pos_max), transform.localPosition.z);
        }
        if(pos_clampZ)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Mathf.Clamp(transform.localPosition.z, localZ_pos_min, localZ_pos_max));
        }
    }
}
