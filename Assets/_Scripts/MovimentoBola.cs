using static System.Math;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoBola : MonoBehaviour
{
   public float velocidade = 5.0f;
   private Vector3 direcao;

   GameManager gm;
    // Start is called before the first frame update
   void Start()
   {
       float dirX = UnityEngine.Random.Range(-5.0f, 5.0f);
       float dirY = UnityEngine.Random.Range(2.0f, 5.0f);

       direcao = new Vector3(dirX, dirY).normalized;

       gm = GameManager.GetInstance();
    }

   // Update is called once per frame
   void Update()
   {
       if (gm.gameState != GameManager.GameState.GAME) return;
       
       transform.position += direcao * Time.deltaTime * velocidade;

       Vector2 posicaoVP = Camera.main.WorldToViewportPoint(transform.position);
       
       if(posicaoVP.x < 0.01)
       {
           direcao = new Vector3(Abs(direcao.x), direcao.y);
       }

       if(posicaoVP.x > 0.99)
       {
           direcao = new Vector3(-Abs(direcao.x), direcao.y);
       }
       
       if(posicaoVP.y > 0.98)
       {
           direcao = new Vector3(direcao.x, -Abs(direcao.y));
       }

       if(posicaoVP.y < 0)
       {
           Reset();
       }
    }

    private void Reset()
   {
       Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
       transform.position = playerPosition + new Vector3(0, 0.5f, 0);

       float dirX = Random.Range(-5.0f, 5.0f);
       float dirY = Random.Range(2.0f, 5.0f);

       direcao = new Vector3(dirX, dirY).normalized;
       gm.vidas--;
       if(gm.vidas <= 0 && gm.gameState == GameManager.GameState.GAME)
       {
               gm.ChangeState(GameManager.GameState.ENDGAME);
       }        
   }

 
   private void OnTriggerEnter2D(Collider2D collision)
   {
       if (collision.gameObject.CompareTag("Player"))
       {
           float dirX = Random.Range(-5.0f, 5.0f);
           float dirY = Random.Range(1.0f, 5.0f);
           direcao = new Vector3(dirX, dirY).normalized;
       }
       else if (collision.gameObject.CompareTag("Tijolo"))
       {
           direcao = new Vector3(direcao.x, -direcao.y);
           gm.pontos++;
        }

   }
}