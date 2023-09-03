using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMeshPlane : MonoBehaviour
{
    int widthSegments = 25; // Number of subdivisions along the X-axis.
    int lengthSegments = 25; // Number of subdivisions along the Z-axis.
    float planeWidth = 50f; // Width of the plane.
    float planeLength = 50f; // Length of the plane.

    private MeshFilter meshFilter;
    void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null)
        {
            Debug.LogError("MeshFilter component not found!");
            return;
        }

        GenerateCustomPlane();
    }

    void GenerateCustomPlane()
    {
        Mesh mesh = new Mesh();
        int numVertices = (widthSegments + 1) * (lengthSegments + 1);
        int numTriangles = widthSegments * lengthSegments * 6; // 2 triangles per quad, 3 vertices per triangle.

        Vector3[] vertices = new Vector3[numVertices];
        Vector2[] uv = new Vector2[numVertices];
        int[] triangles = new int[numTriangles];

        float widthStep = planeWidth / widthSegments;
        float lengthStep = planeLength / lengthSegments;

        int vertexIndex = 0;

        for (int z = 0; z <= lengthSegments; z++)
        {
            for (int x = 0; x <= widthSegments; x++)
            {
                float xPos = x * widthStep;
                float zPos = z * lengthStep;

                vertices[vertexIndex] = new Vector3(xPos, 0, zPos);
                uv[vertexIndex] = new Vector2((float)x / widthSegments, (float)z / lengthSegments);

                if (x < widthSegments && z < lengthSegments)
                {
                    int topLeft = vertexIndex;
                    int topRight = topLeft + 1;
                    int bottomLeft = topLeft + widthSegments + 1;
                    int bottomRight = bottomLeft + 1;

                    triangles[vertexIndex * 6 + 0] = topLeft;
                    triangles[vertexIndex * 6 + 1] = topRight;
                    triangles[vertexIndex * 6 + 2] = bottomLeft;
                    triangles[vertexIndex * 6 + 3] = topRight;
                    triangles[vertexIndex * 6 + 4] = bottomRight;
                    triangles[vertexIndex * 6 + 5] = bottomLeft;
                }

                vertexIndex++;
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        meshFilter.mesh = mesh;
    }
}

