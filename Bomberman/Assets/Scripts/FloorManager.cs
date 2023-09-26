using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class FloorManager : NetworkBehaviour
{
    public GameObject floor0Dark;
    public GameObject floor1Light;
    public GameObject woodWall;

    public GameObject undestroyableWall;

    public GameObject floorType;
    public float width;
    public float height;
    public float tileDistance = 0.5f;



    // Start is called before the first frame update
    void Start()
    {
        if (!IsServer)
        {
            return;
        }

        floorType = floor0Dark;
        createWorldWall();
        createFloor();
    }
    private void createWorldWall()
    {
        for (float x = -1; x < width+1; x += tileDistance)
        {
            for (float y = -1; y < height+1; y += tileDistance)
            {
                if((y > -1 && y < height ) && (x > -1 && x < width))
                {
                    //Debug.Log("avoid pos " + x + "," + y);
                    continue;
                }
                GameObject go = Instantiate(undestroyableWall, new Vector3(x, y, 0), Quaternion.identity);
                go.GetComponent<NetworkObject>().Spawn();
            }
        }
    }

    private void createFloor()
    {
        for (float x = 0; x < width; x += tileDistance)
        {
            //if(x == width - 1)
            //{
            //    continue;
            //}
            for (float y = 0; y < height; y += tileDistance)
            {
                //if(y == height - 1)
                //{
                //    continue;
                //}
                GameObject go = Instantiate(floorType, new Vector3(x, y, 0), Quaternion.identity);
                go.GetComponent<NetworkObject>().Spawn();
                changeFloorType();
                //if (floorType.Equals(floor0Dark))
                //{
                    if((x == 0 || x == width-1) && biggerYConditionToAvoid(y,height))
                    {
                        continue;
                    }else if((x == 1 || x == width -2) && yConditionToAvoid(y, height))
                    {
                        continue;
                    }
                    go = Instantiate(woodWall, new Vector3(x, y, 0), Quaternion.identity);
                    go.GetComponent<NetworkObject>().Spawn();
                //}
            }
            changeFloorType();
        }
    }
    private bool biggerYConditionToAvoid(float y, float height)
    {
        return yConditionToAvoid(y, height) || y == 1 || y == height - 2;
    }
    private bool yConditionToAvoid(float y, float height)
    {
        return (y == 0 || y == height-1);
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
