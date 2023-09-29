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
    public List<string> nicknames;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
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

    public string GetRandomNickname()
    {
        if (nicknames.Count == 0)
        {
            Debug.LogWarning("Esgotou a lista de nomes disponíveis!");
            return "Sem Nome";
        }

        // Escolhe um índice de nome aleatório da lista disponível
        int randomIndex = UnityEngine.Random.Range(0, nicknames.Count);
        string selectedName = nicknames[randomIndex];

        // Remove o nome selecionado da lista para que não seja usado novamente
        nicknames.RemoveAt(randomIndex);

        return selectedName;
    }

    public void choosePlayerspos()
    {
        //if (!IsServer)
        //{
        //    return;
        //}
        //spawnPositions = readOnlySpawnPositions;
        //}
        //availablePositions = getSpawnPositionsServerRpc();
        //availablePositions = new List<Vector2>(spawnPositions);
        int i = 0;
        foreach (Player player in GameManager.instance.players)
        {
            //var posinarray = Random.Range(0, spawnPositions.Count);
            //var posinarray = 1;
            player.transform.position = spawnPositions[i];
            player.txtName.text = i.ToString();
            //Debug.Log(spawnPositions);
            Debug.Log("choosen pos: " + spawnPositions[i]);
            //spawnPositions.RemoveAt(posinarray);
            i++;
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
