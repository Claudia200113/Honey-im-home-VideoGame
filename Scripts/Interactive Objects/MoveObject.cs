using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float SpeedH;
    public float SpeedV;

    float MoveH;
    float MoveV;

    private void OnMouseDrag()
    {
        MoveH -= SpeedH * Input.GetAxis("Mouse X");
        MoveV += SpeedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(MoveV, MoveH, 0f);
    }
}
