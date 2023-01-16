using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour

{

    [Header("The seed used for random level generation")]
    // variable to determine the value of the seed
    public int seedValue;

    [Header("Maximum amount of room to be generated in the level")]
    public int maxRooms;
    private int roomCount;
    
    [Header("Room dimensions")]
    [Tooltip("The size of the room, determines both the width and breadth of the room")]
    public int roomWidth;
    [Tooltip("The ceiling height of the room")]
    public int roomTop;
    [Tooltip("The floor of the room. Used if there are several levels of rooms on top of each other")]
    public int roomBottom;
    
    private int roomOriginX = 0;
    private int roomOriginZ = 0;

    private Mesh mesh;

    public Material[] materials = new Material[3];

    void Start()
    
    {

        // if the seed value has been changed from its default, which is 0
        if (seedValue != 0)

        {

            // set the seed for random number generation
            Random.InitState(seedValue);

        }
        // if the seed value hasn't been changed, keep using its original value determined by unity
        
        // generate the level
        CreateRoom();
        
    }

    public void CreateRoom()

    {

        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = new Material(Shader.Find("Standard"));
        Color groundColour = new Color(0.4f, 0.2f, 0.0f, 1.0f);
        meshRenderer.material.SetColor("_Color", groundColour);

        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();

        mesh = new Mesh();
        mesh.name = "Room";

        //Vector3[] vertices = new Vector3[4]
        Vector3[] vertices = new Vector3[8]

        {

            // floor
            new Vector3(roomOriginX, roomBottom, roomOriginZ),
            new Vector3((roomOriginX + roomWidth), roomBottom, roomOriginZ),
            new Vector3(roomOriginX, roomBottom, (roomOriginZ + roomWidth)),
            new Vector3((roomOriginX + roomWidth), roomBottom, (roomOriginZ + roomWidth)),
            
            //roof
            new Vector3(roomOriginX, roomTop, roomOriginZ),
            new Vector3((roomOriginX + roomWidth), roomTop, roomOriginZ),
            new Vector3(roomOriginX, roomTop, (roomOriginZ + roomWidth)),
            new Vector3((roomOriginX + roomWidth), roomTop, (roomOriginZ + roomWidth)),

        };

        mesh.vertices = vertices;

        //int[] triangles = new int [6]
        int[] triangles = new int [12]

        {

            // floor
            0, 2, 1,
            2, 3, 1,
            
            //roof
            //4, 6, 5,
            //6, 7, 5,
            6, 4, 5,
            7, 6, 5,

        };

        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        /*Vector2[] uv = new Vector2[4]

        {

            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(1, 1)

        };

        mesh.uv = uv;
*/
        meshFilter.mesh = mesh;

        mesh.RecalculateBounds();
        MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
        //BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();

    }

}