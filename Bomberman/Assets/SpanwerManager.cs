using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class SpanwerManager : NetworkBehaviour
{
    // Start is called before the first frame update
    public static SpanwerManager instance;
    public List<Vector2> spawnPositions;
    public List<Vector2> readOnlySpawnPositions;
    private List<Vector2> availablePositions;
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        readOnlySpawnPositions = new List<Vector2>(spawnPositions);
        //availablePositions = new List<Vector2>(spawnPositions);
        //foreach (Player player in GameManager.instance.players)
        //{
        //    var posinarray = Random.Range(0, availablePositions.Count);
        //    player.transform.position = spawnPositions[posinarray];
        //    Debug.Log(availablePositions);
        //    availablePositions.RemoveAt(posinarray);
        //}
        //availablepositions = new list<vector2>(spawnpositions);
        //if (IsServer)
        //{

        choosePlayerspos();
        //}

        //getSpawnPositionsServerRpc();
    }

    public void resetPositions()
    {
        spawnPositions = new List<Vector2>(readOnlySpawnPositions);
        availablePositions = new List<Vector2>(spawnPositions);
    }
    public void choosePlayerspos()
    {
        //if (IsServer)
        //{

        //spawnPositions = readOnlySpawnPositions;
        //}
        //availablePositions = getSpawnPositionsServerRpc();
        //availablePositions = new List<Vector2>(spawnPositions);
        foreach (Player player in GameManager.instance.players)
        {
            var posinarray = Random.Range(0, spawnPositions.Count);
            player.transform.position = spawnPositions[posinarray];
            //Debug.Log(spawnPositions);
            Debug.Log("choosen pos: " + spawnPositions[posinarray]);
            spawnPositions.RemoveAt(posinarray);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void getSpawnPositionsServerRpc()
    {
        availablePositions = new List<Vector2>(spawnPositions);
        foreach (Player player in GameManager.instance.players)
        {
            var posInArray = Random.Range(0, availablePositions.Count);
            player.transform.position = spawnPositions[posInArray];
            Debug.Log(availablePositions);
            availablePositions.RemoveAt(posInArray);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
