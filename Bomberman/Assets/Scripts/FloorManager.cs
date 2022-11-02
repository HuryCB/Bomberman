using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class FloorManager : MonoBehaviour
{
    public GameObject floor0Dark;
    public GameObject floor1Light;
    public GameObject woodWall;

    public GameObject floorType;
    public float width;
    public float height;
    public float tileDistance = 0.5f;



    // Start is called before the first frame update
    void Start()
    {
        floorType = floor0Dark;
        createFloor();
    }

    private void createFloor()
    {
        for (float i = 0; i < width; i += tileDistance)
        {
            for (float j = 0; j < height; j += tileDistance)
            {
                GameObject go = Instantiate(floorType, new Vector3(i, j, 0), Quaternion.identity);
                go.GetComponent<NetworkObject>().Spawn();
                if (floorType.Equals(floor0Dark))
                {
                    go = Instantiate(woodWall, new Vector3(i, j, 0), Quaternion.identity);
                    go.GetComponent<NetworkObject>().Spawn();
                }
                changeFloorType();
            }
            changeFloorType();
        }
    }

    private void changeFloorType()
    {
        if (floorType.Equals(floor0Dark))
        {
            floorType = floor1Light;
        }
        else
        {
            floorType = floor0Dark;
        }
    }
}
