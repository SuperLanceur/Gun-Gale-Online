using UnityEngine;
using System.Collections;

public class MeshIntoTriangles : MonoBehaviour
{
    public float materialChangeSpeed = 10f;
    private SkinnedMeshRenderer mr;
    private EnemyAI enemyHealth;

    private void Start()
    {
        
        mr = GetComponent<SkinnedMeshRenderer>();
        enemyHealth = GetComponentInParent<EnemyAI>();
       
       
        
        //OnMouseDown();
    }

    private void Update()
    {
        if (enemyHealth.health <= 0f)
        {
            OnMouseDown();
        }
    }

    IEnumerator SplitMesh()
    {
        MeshFilter MF = GetComponent<MeshFilter>();
       
        //MeshRenderer MR = GetComponent<MeshRenderer>();
        SkinnedMeshRenderer MR = GetComponent<SkinnedMeshRenderer>();
        Mesh M = MF.mesh;
        Vector3[] verts = M.vertices;
        Vector3[] normals = M.normals;
        Vector2[] uvs = M.uv;
        for (int submesh = 0; submesh < M.subMeshCount; submesh++)
        {
            int[] indices = M.GetTriangles(submesh);
            for (int i = 0; i < indices.Length; i += 3)
            {
                Vector3[] newVerts = new Vector3[3];
                Vector3[] newNormals = new Vector3[3];
                Vector2[] newUvs = new Vector2[3];
                for (int n = 0; n < 3; n++)
                {
                    int index = indices[i + n];
                    newVerts[n] = verts[index];
                    newUvs[n] = uvs[index];
                    newNormals[n] = normals[index];
                }
                Mesh mesh = new Mesh();
                mesh.vertices = newVerts;
                mesh.normals = newNormals;
                mesh.uv = newUvs;

                mesh.triangles = new int[] { 0, 1, 2, 2, 1, 0 };

                GameObject GO = new GameObject("Triangle " + (i / 3));
                
                GO.transform.position = transform.position;
                GO.transform.rotation = transform.rotation;
                //GO.AddComponent<MeshRenderer>().material = MR.materials[submesh];
                GO.AddComponent<SkinnedMeshRenderer>().material = MR.materials[submesh];
                GO.AddComponent<MeshFilter>().mesh = mesh;
                GO.AddComponent<BoxCollider>();
                
                Rigidbody rb = GO.AddComponent<Rigidbody>();
                rb.AddExplosionForce(40, transform.position, 30);
                rb.useGravity = false;
                //GO.AddComponent<Rigidbody>().AddExplosionForce(100, transform.position, 30);
                Destroy(GO, 0.2f + Random.Range(0.0f, 0.5f));
                //Destroy(GO, 5 + Random.Range(0.0f, 5.0f));
            }
        }
        MR.enabled = false;

        Time.timeScale = 0.2f;
        yield return new WaitForSeconds(0.8f);
        Time.timeScale = 1.0f;
        Destroy(gameObject);
    }
    void OnMouseDown()
    {
        Color deathColor = new Color(0, 227, 233, 100);
        Material newM = new Material(Shader.Find("Transparent/Diffuse"));
        newM.color = deathColor;
        mr.material.Lerp(mr.material, newM, materialChangeSpeed * Time.deltaTime);
        StartCoroutine(SplitMesh());
    }
}

