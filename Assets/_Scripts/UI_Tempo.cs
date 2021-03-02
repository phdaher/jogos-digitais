using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Tempo : MonoBehaviour
{
    GameManager gm;
    public Text counterText;

    public float seconds, minutes;
    // Start is called before the first frame update
    void Start()
    {
        counterText = GetComponent<Text>() as Text;
        gm = GameManager.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gameState == GameManager.GameState.GAME){
            minutes = (int)(Time.time/60f);
            seconds = (int)(Time.time%60f);
            counterText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }

        if (gm.vidas == 0 || gm.pontos == 48){
            minutes = 0;
            seconds = 0;
            counterText.text = "00:00";
        }
    }
}
