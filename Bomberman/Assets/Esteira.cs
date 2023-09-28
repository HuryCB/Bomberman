using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esteira : MonoBehaviour
{
    public enum Direcao { Esquerda, Direita, Cima, Baixo };
    public Direcao direcaoMovimento;
    public float velocidadeEsteira;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("bomb"))
        {
            // Inicie o movimento da bomba na direção da esteira
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
            //Direcao direcaoMovimento = Direcao.Esquerda; // Defina a direção padrão aqui

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
