using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardDetect : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D guardCollider;
    private GuardProximityCircle guardCircle;
    private GameObject pigeon;
    public LayerMask layerMask;
    private float rotation;
    public float detectRadius = 2f;
    public bool isPigeonDetected;
    public bool canDetect;

    private Mesh mesh;
    private float detectAngle;
    private float detectDistance;
    private Vector3 origin;

    // Start is called before the first frame update
    void Start()
    {
        pigeon = FindObjectOfType<PigeonMovement>().gameObject;
        guardCollider = transform.parent.GetComponentInChildren<Collider2D>();

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        canDetect = true;
    }

    void Update()
    {
        if (!canDetect)
        {
            mesh.Clear();
            return;
        }
        origin = guardCollider.transform.position;
        detectAngle = 25f;
        detectDistance = 2f;
        int rayCount = 50;
        float angleIncr = detectAngle / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = transform.InverseTransformPoint(origin);
        int vertexIndex = 1;
        int triangleIndex = 0;
        float angle = guardCollider.transform.eulerAngles.z + 90 + detectAngle / 2; // +90 because sprite faces up, not right
        for (int i = 0; i < rayCount; i++)
        {
            Vector3 directionVector = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            Vector3 vertex;
            RaycastHit2D ray = Physics2D.Raycast(origin, directionVector, detectDistance, layerMask);
            vertex = transform.InverseTransformPoint(origin) + transform.InverseTransformVector(directionVector) * detectDistance;
            isPigeonDetected = false;
            if (ray.collider == null) {}
            else if (ray.collider.gameObject == pigeon)
            {
                isPigeonDetected = true;
            }
            else // ray collides with wall
            {
                vertex = transform.InverseTransformPoint(ray.point);
            }
            // vertex = transform.InverseTransformPoint(origin) + transform.InverseTransformVector(directionVector) * detectDistance;
            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;
                triangleIndex += 3;
            }
            vertexIndex++;
            angle -= angleIncr;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    // Update is called once per frame
    // void Update()
    // {
    //     rotation = gameObject.transform.rotation.z;
    //     if (!guardCircle.isNearPlayer) {}
    //     else
    //     {
    //         Vector2 direction = transform.rotation * Vector3.forward;
    //         RaycastHit2D ray = Physics2D.Raycast(transform.position, direction, detectRadius, playerLayer);
    //         // Debug.Log(ray.collider);
    //         if (ray.collider == pigeon.GetComponent<Collider2D>())
    //         {
    //             isPigeonDetected = true;
    //         }
    //         else
    //         {
    //             isPigeonDetected = false;
    //         }
    //     }
    // }
}
