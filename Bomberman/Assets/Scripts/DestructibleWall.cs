using System;
using UnityEngine;
using Unity.Netcode;

[Serializable]
public class PowerUpData
{
    public GameObject powerUpPrefab;
    [Range(0f, 1f)]
    public float spawnProbability;
}

public class DestructibleWall : NetworkBehaviour
{
    public PowerUpData[] powerUpOptions;
    public bool isBeingDestroyed = false;

    private void Start()
    {
        // Garanta que as probabilidades se somem para 1 (100%)
        NormalizeProbabilities();
    }

    private void NormalizeProbabilities()
    {
        float totalProbability = 1f;

        foreach (PowerUpData powerUpData in powerUpOptions)
        {
            totalProbability += powerUpData.spawnProbability;
        }

        // Normalize as probabilidades para que somem 1
        if (totalProbability > 0f)
        {
            foreach (PowerUpData powerUpData in powerUpOptions)
            {
                powerUpData.spawnProbability /= totalProbability;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("explosion"))
        {
            if (isBeingDestroyed)
            {
                return;
            }
            isBeingDestroyed = true;

            if (IsServer)
            {
                float randomValue = UnityEngine.Random.value; // Valor aleatório entre 0 e 1

                // Verifique qual power-up deve ser spawnado com base nas probabilidades
                int chosenPowerUpIndex = -1;
                float cumulativeProbability = 0f;

                for (int i = 0; i < powerUpOptions.Length; i++)
                {
                    cumulativeProbability += powerUpOptions[i].spawnProbability;

                    if (randomValue <= cumulativeProbability)
                    {
                        chosenPowerUpIndex = i;
                        break;
                    }
                }

                // Certifique-se de que um power-up seja escolhido antes de criá-lo
                if (chosenPowerUpIndex >= 0)
                {
                    GameObject go = Instantiate(powerUpOptions[chosenPowerUpIndex].powerUpPrefab, transform.position, Quaternion.identity);
                    go.GetComponent<NetworkObject>().Spawn();
                }

                // Destrua a parede após escolher o power-up
                Destroy(gameObject);
            }
        }
    }
}
