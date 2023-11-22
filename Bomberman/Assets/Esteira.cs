using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;


public class Esteira : NetworkBehaviour
{
    public enum Direcao { Direita, Cima, Esquerda, Baixo };
    public Direcao direcaoMovimento;
    public float velocidadeEsteira;

    private void Start()
    {
        if (IsServer)
        {
            direcaoMovimento = (Direcao)Random.Range(0, Direcao.GetValues(typeof(Direcao)).Length);

            // Escolhe uma velocidade aleat�ria de 1 a 3
            Quaternion tempRotation = transform.rotation;
            velocidadeEsteira = Random.Range(1, 4); // Valores entre 1 e 3 (inclusive)
            switch (direcaoMovimento)
            {
                case Direcao.Direita:
                    tempRotation = Quaternion.Euler(0, 0, 0);
                    break;
                case Direcao.Cima:
                    tempRotation = Quaternion.Euler(0, 0, 90);
                    break;
                case Direcao.Esquerda:
                    tempRotation = Quaternion.Euler(0, 0, 180);
                    break;
                case Direcao.Baixo:
                    tempRotation = Quaternion.Euler(0, 0, 270);
                    break;
            }
            setRotationClientRpc(tempRotation);
        }
    }

    [ClientRpc]
    private void setRotationClientRpc(Quaternion rotation)
    {
        transform.rotation = rotation;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("bomb"))
        {
            // Inicie o movimento da bomba na dire��o da esteira
            MoveBomb(other.gameObject, true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("bomb"))
        {
            // Pare o movimento da bomba
            MoveBomb(other.gameObject, false);
        }
    }

    private void MoveBomb(GameObject bomb, bool move)
    {
        Rigidbody2D rb = bomb.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            //Direcao direcaoMovimento = Direcao.Esquerda; // Defina a dire��o padr�o aqui

            switch (direcaoMovimento)
            {
                case Direcao.Esquerda:
                    rb.velocity = Vector2.left * velocidadeEsteira;
                    break;
                case Direcao.Direita:
                    rb.velocity = Vector2.right * velocidadeEsteira;
                    break;
                case Direcao.Cima:
                    rb.velocity = Vector2.up * velocidadeEsteira;
                    break;
                case Direcao.Baixo:
                    rb.velocity = Vector2.down * velocidadeEsteira;
                    break;
            }

            if (!move)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }


}
