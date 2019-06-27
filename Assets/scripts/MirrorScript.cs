using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorScript : MonoBehaviour
{
    public float angle = 30;
    private Transform transform;
    public LayerMask layerMask;

    private void Awake()
    {
        transform = GetComponent<Transform>();
    }
    private void OnMouseDown()
    {
        transform.Rotate(new Vector3(0, 0, angle));
    }

}
