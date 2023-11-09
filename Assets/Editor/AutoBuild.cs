using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AutoBuild : EditorWindow
{
    private float wallThickness = 0.25f;
    private float wallHeight = 4f;
    private int numRooms = 0;
    private Vector3[] roomSize = new Vector3[0];

    private GameObject[] buildings = new GameObject[0];

    [MenuItem("Window/Building Generator")]
    public static void ShowWidow()
    {
        EditorWindow.GetWindow(typeof(AutoBuild));
    }
    private void OnGUI()
    {
        GUILayout.Label("Building Generator", EditorStyles.whiteLargeLabel);

        wallThickness = EditorGUILayout.FloatField("Wall Thickness", wallThickness);

        wallHeight = EditorGUILayout.FloatField("Wall Height", wallHeight);

        numRooms = EditorGUILayout.IntField("Number of Rooms", numRooms);
        if (numRooms > roomSize.Length)
        {
            roomSize = new Vector3[numRooms];
        }
        for (int i = 0; i < numRooms; i++)
        {
            roomSize[i].y = wallHeight;
            roomSize[i] = EditorGUILayout.Vector3Field("Room " + i + " Size", roomSize[i]);
        }


        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("Generate"))
        {
            GenerateBuilding();
        }


        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("Delete Last Building"))
        {
            DeleteLast();
        }
    }
    void GenerateBuilding()
    {
        GameObject empty = new();
        GameObject[] temp = buildings;
        buildings = new GameObject[buildings.Length + 1];
        for (int i = 0; i < temp.Length; i++)
        {
            buildings[i] = temp[i];
        }
        buildings[^1] = empty;

        empty.name = "Building " + (buildings.Length - 1);

        for (int i = 0; i < roomSize.Length; i++)
        {
            // Generate room
            GenerateRoom(roomSize[i], empty.transform, i);
        }
    }
    void CreateCube(Vector3 position, Vector3 size, Transform p)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = position;
        cube.transform.localScale = size;
        cube.transform.parent = p;
    }
    void GenerateRoom(Vector3 size, Transform p, int i)
    {
        GameObject empty = new();
        empty.transform.parent = p;
        empty.name = "Room " + i;
        // Floor
        CreateCube(Vector3.zero, new Vector3(size.x, wallThickness, size.z), empty.transform);
        // Walls
        CreateCube(new Vector3(0, wallHeight / 2, size.z / 2), new Vector3(size.x, wallHeight, wallThickness), empty.transform); // Front wall
        CreateCube(new Vector3(0, wallHeight / 2, -size.z / 2), new Vector3(size.x, wallHeight, wallThickness), empty.transform); // Back wall
        CreateCube(new Vector3(size.x / 2, wallHeight / 2, 0), new Vector3(wallThickness, wallHeight, size.z), empty.transform); // Right wall
        CreateCube(new Vector3(-size.x / 2, wallHeight / 2, 0), new Vector3(wallThickness, wallHeight, size.z), empty.transform); // Left wall
        // Roof
        CreateCube(new Vector3(0, wallHeight, 0), new Vector3(size.x, wallThickness, size.z), empty.transform);
    }
    void DeleteLast()
    {
        if (buildings.Length > 0)
        {
            DestroyImmediate(buildings[^1]);
            GameObject[] temp = buildings;
            buildings = new GameObject[buildings.Length - 1];
            for (int i = 0; i < temp.Length - 1; i++)
            {
                buildings[i] = temp[i];
            }
        }
    }
}
