using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruleta : MonoBehaviour
{
    public float velocidadRotacion = 300f; 
    private bool girando = false;
    private float tiempoRotacion = 2f; 
    private float tiempoTranscurrido = 0f;
    public int valorRuleta = 0;

    public FightingController fightingController; 
    public OpponentAI opponentAI;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            IniciarRotacion();
        }

        if (girando)
        {
            transform.Rotate(0, 0, velocidadRotacion * Time.deltaTime);
            tiempoTranscurrido += Time.deltaTime;

            if (tiempoTranscurrido >= tiempoRotacion)
            {
                girando = false;
                tiempoTranscurrido = 0f;
                float anguloZ = transform.eulerAngles.z;

                if (anguloZ >= 0 && anguloZ < 60)
                {
                    valorRuleta = 1;
                    Debug.Log("Ángulo: " + anguloZ + " - Intervalo: 1");
                    fightingController.HandleRuletaResult(valorRuleta);
                }
                else if (anguloZ >= 60 && anguloZ < 120)
                {
                    valorRuleta = 2;
                    Debug.Log("Ángulo: " + anguloZ + " - Intervalo: 2");
                    opponentAI.HandleRuletaResult(valorRuleta);
                }
                else if (anguloZ >= 120 && anguloZ < 180)
                {
                    valorRuleta = 3;
                    Debug.Log("Ángulo: " + anguloZ + " - Intervalo: 3");
                    fightingController.HandleRuletaResult(valorRuleta);
                }
                else if (anguloZ >= 180 && anguloZ < 240)
                {
                    valorRuleta = 4;
                    Debug.Log("Ángulo: " + anguloZ + " - Intervalo: 4");
                    opponentAI.HandleRuletaResult(valorRuleta);
                }
                else if (anguloZ >= 240 && anguloZ < 300)
                {
                    valorRuleta = 5;
                    Debug.Log("Ángulo: " + anguloZ + " - Intervalo: 5");
                    fightingController.HandleRuletaResult(valorRuleta);
                }
                else if (anguloZ >= 300 && anguloZ < 360)
                {
                    valorRuleta = 6;
                    Debug.Log("Ángulo: " + anguloZ + " - Intervalo: 6");
                    opponentAI.HandleRuletaResult(valorRuleta);
                }
            }
        }

  
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            girando = false;
            tiempoTranscurrido = 0f;
        }
    }

    public void IniciarRotacion()
    {
        girando = true;
        tiempoTranscurrido = 0f;
    }
}