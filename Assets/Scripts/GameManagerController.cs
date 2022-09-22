using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManagerController : MonoBehaviour
{
    public Text scoreText;
    public Text bulletText;

    private int score;
    public int bullets;

    public Text bronzeText;
    public Text silverText;
    public Text goldText;

    private int bronzeCoins;
    private int silverCoins;
    private int goldenCoins;

    // public GameObject player;
    // public GameObject respawnPoint;

    // private RespawnScript respawn;
    // private BoxCollider2D checkpoint;

    // public Text livesText;
    // private int lives;
    void Start()
    {
        score = 0;
        bullets = 5;

        bronzeCoins = 0;
        silverCoins = 0;
        goldenCoins = 0;

        // lives = 3;

        // checkpoint = GetComponent<BoxCollider2D>();
        // respawn = GameObject.FindGameObjectWithTag("Respawn").GetComponent<RespawnScript>();

        PrintScoreInScreen();
        PrintBulletsInScreen();

        PrintBronzeCoinsInScreen();
        PrintSilverCoinsInScreen();
        PrintGoldenCoinsInScreen();

        //CheckpointSave();
        //PrintLivesInScreen();

        LoadGame();

    }
    public void SaveGame()
    {
        var filePath = Application.persistentDataPath + "/save.data";

        FileStream file;

        if(File.Exists(filePath))
            file = File.OpenWrite(filePath);
        else
            file = File.Create(filePath);

        GameData data = new GameData();
        data.Score = score;
        data.Bronze = bronzeCoins;
        data.Silver = silverCoins;
        data.Gold = goldenCoins;
        
        //data.SaveGame = checkpoint;
        

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }
    public void LoadGame()
    {
        var filePath = Application.persistentDataPath + "/save.data";

        FileStream file;

        if(File.Exists(filePath))
        {
            file = File.OpenRead(filePath);
        }  
        else{
            Debug.LogError("No se encontró el archivo");
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        GameData data = (GameData) bf.Deserialize(file);
        file.Close();

        score = data.Score;
        PrintScoreInScreen();

        bronzeCoins = data.Bronze;
        PrintBronzeCoinsInScreen();

        silverCoins = data.Silver;
        PrintSilverCoinsInScreen();

        goldenCoins = data.Gold;
        PrintGoldenCoinsInScreen();

        //checkpoint = data.SaveGame;
    }
    public int Bronze()
    {
        return bronzeCoins;    
    }
    public int Silver()
    {
        return silverCoins;    
    }
    public int Gold()
    {
        return goldenCoins;    
    }
    public int Score()
    {
        return score;
    }
    public int Bullets()
    {
        return bullets;
    }
    public void GanarBronzeCoins(int tbronze)
    {
        bronzeCoins += tbronze;
        PrintBronzeCoinsInScreen();
    }
    public void GanarSilverCoins(int tsilver)
    {
        silverCoins += tsilver;
        PrintSilverCoinsInScreen();
    }
    public void GanarGoldenCoins(int tgolden)
    {
        goldenCoins += tgolden;
        PrintGoldenCoinsInScreen();
    }
    public void GanarPuntos(int puntos)
    {
        score += puntos;
        PrintScoreInScreen();
    }
    public void restarBalas()
    {
        bullets -= 1;
        PrintBulletsInScreen();
    }
    private void PrintScoreInScreen()
    {
        scoreText.text = "Puntuación: " + score; 
    }
    private void PrintBulletsInScreen()
    {
        bulletText.text = "Balas: " + bullets;
    }
    private void PrintBronzeCoinsInScreen()
    {
        bronzeText.text = "Bronze: " + bronzeCoins;
    }
    private void PrintSilverCoinsInScreen()
    {
        silverText.text = "Silver: " + silverCoins;
    }
    private void PrintGoldenCoinsInScreen()
    {
        goldText.text = "Gold: " + goldenCoins;
    }
    // private void CheckpointSave()
    // {
    //     // if(other.gameObject.CompareTag("Player"))
    //     // {
    //     //     player.transform.position = respawnPoint.transform.position;
    //     // }
    //     if(gameObject.CompareTag("Player"))
    //     {
    //         respawn.respawnPoint = this.gameObject;
    //         checkpoint.enabled = false;
    //     }
    // }

}

    // public void restarBalas(int total)
    // {
    //     bullets -= total;
    //     PrintBulletsInScreen();

    // }
        // public void PerderVida()
    // {
    //     lives -= 1;
    //     PrintLivesInScreen();
    // }
        // private void PrintLivesInScreen()
    // {
    //     livesText.text = "Vida: " + lives; 
    // }
        // public int Lives()
    // {
    //     return lives;
    // }