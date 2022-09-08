using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerController : MonoBehaviour
{
    public Text scoreText;
    public Text livesText;
    public Text balasText;
    private int score;
    private int lives;
    private int balas;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        lives = 3;
        balas = 5;

        PrintScoreInScreen();
        PrintLivesInScreen();
        PrintBalasInScreen();
    }
    public int Score()
    {
        return score;
    }
    public int Lives()
    {
        return lives;
    }
    public int Balas()
    {
        return balas;
    }
    public void GanarPuntos(int puntos)
    {
        score += puntos;
        PrintScoreInScreen();
    }
    public void PerderVida()
    {
        lives -= 1;
        PrintLivesInScreen();
    }
    public void restarBalas(int total)
    {
        balas -= total;
        PrintBalasInScreen();

        //If (balas < 0)
        //{}
    }
    private void PrintScoreInScreen()
    {
        scoreText.text = "Puntaje: " + score; 
    }
    private void PrintLivesInScreen()
    {
        livesText.text = "Vida: " + lives; 
    }
    private void PrintBalasInScreen()
    {
        balasText.text = "Balas: " + balas;
    }
}
