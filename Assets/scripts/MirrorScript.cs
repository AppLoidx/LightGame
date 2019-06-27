using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorScript : MonoBehaviour
{
    public float angle = 30;
    private Transform transform;
    //[SerializeField]
    //private GameObject lineGeneratorPrefab;
    public bool isNormalRendered = false;
    public Vector3 normal = new Vector3();
    //private GameObject lineHolder;
    private void Update()
    {
        
        //if (isNormalRendered)
        //{
            
        //    LineRenderer lr = lineHolder.GetComponent<LineRenderer>();
        //    //lr.SetPosition(0, transform.position);
        //    //lr.SetPosition(1, normal);
        //} else
        //{
        //}
    }

    private void Awake()
    {
        //lineHolder = Instantiate(lineGeneratorPrefab);
        transform = GetComponent<Transform>();
    }
    private void OnMouseDown()
    {
        transform.Rotate(new Vector3(0, 0, angle));
    }

}
