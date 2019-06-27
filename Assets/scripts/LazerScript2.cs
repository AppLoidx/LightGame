using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerScript2 : MonoBehaviour
{
    [SerializeField]
    private GameObject lineGeneratorPrefab;
    private GameObject lineHolder;
    private LineRenderer renderFromHolder;

    public int maxReflectionCount = 5;
    public float maxStepDistance = 2;
    private LineRenderer lineRenderer;
    private GameObject lastTouchedObject = null;
    private Transform transform;
    private float lastZRot = -999;
    private float startZScale;
    public KeyCode lazerBeam = KeyCode.Mouse1;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lastTouchedObject = new GameObject();
        transform = GetComponent<Transform>();
    }

    private void Start()
    {
        startZScale = transform.transform.localScale.z; 
        lineHolder = Instantiate(lineGeneratorPrefab);
        renderFromHolder = lineHolder.GetComponent<LineRenderer>();
    }

    void FixedUpdate()
    {
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = target - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        RotateLazer(angle);

        if (Input.GetKey(lazerBeam)) DrawPredictedReflectionPattern(this.transform.position, this.transform.right, maxReflectionCount, new GameObject[] {null }, new Vector3[] { transform.position});
        else
        {
            Destroy(lineHolder);
            lineHolder = Instantiate(lineGeneratorPrefab);
            renderFromHolder = lineHolder.GetComponent<LineRenderer>();
        }
    }

    private void DrawPredictedReflectionPattern(Vector3 position, Vector3 direction, int reflectionsRemaining, GameObject[] gameObjects, Vector3[] positions)
    {
        if (reflectionsRemaining == 0)
        {

            renderFromHolder.positionCount = positions.Length;
            renderFromHolder.SetPositions(positions);

            return;
        }
        Vector3 startingPosition = position;

        RaycastHit2D hit = Physics2D.Raycast(position, direction);

        bool hitOldObj = false;
        GameObject[] tempArray = new GameObject[gameObjects.Length + 1];
        if (hit )
        {          
            for (int i = 0; i < gameObjects.Length; i++)
            {
                if (gameObjects[i] != null)
                    if (hit.transform.gameObject.Equals(gameObjects[i]) && !hitOldObj)
                    {
                        hitOldObj = true;
                    }
                tempArray.SetValue(gameObjects[i], i);
            }

        }
    
        

        if (hit && (!hitOldObj || System.Math.Round(transform.rotation.z) != System.Math.Round(lastZRot)))
        {
            lastTouchedObject = hit.transform.gameObject;
            tempArray.SetValue(hit.transform.gameObject, tempArray.Length - 1);
            gameObjects = tempArray;
            direction = Vector3.Reflect(direction, hit.normal);
            position = hit.point;

            if (System.Math.Round(transform.rotation.z) != System.Math.Round(lastZRot)) gameObjects = new GameObject[] { null };
        }
        else
        {
            position += direction * maxStepDistance;
        }
        lastZRot = transform.rotation.z;

        Vector3[] newVector = new Vector3[positions.Length + 1];

        for (int i = 0; i < positions.Length; i++)
        {
            newVector.SetValue(positions[i], i);
        }
        newVector.SetValue(position, newVector.Length - 1);

        if (hit.transform.gameObject.tag.Equals("diamond") || hit.transform.transform.tag.Equals("Ground"))
        {
            if (hit.transform.gameObject.tag.Equals("diamond")) Destroy(hit.transform.gameObject);
            DrawPredictedReflectionPattern(position, direction, 0, gameObjects, newVector);
        }
        else
        {
            DrawPredictedReflectionPattern(position, direction, reflectionsRemaining - 1, gameObjects, newVector);
        }
    }

    private void RotateLazer(float angle)
    {
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;

        transform.transform.localScale = transform.right.x <= 0
            ? (Vector3)new Vector2(transform.transform.localScale.x, -startZScale)
            : (Vector3)new Vector2(transform.transform.localScale.x, startZScale);
    }
}
