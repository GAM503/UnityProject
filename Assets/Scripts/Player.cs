using UnityEngine;
using TMPro;
public class Player : MonoBehaviour
{
    public TextMeshProUGUI healthtxt;
    public TextMeshProUGUI level_txt;
    public float level;
    public float health;

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data =  SaveSystem.LoadPlayer();
        level = data.level;
        health = data.health;
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }
    
    void Update()
    {
        health += Time.deltaTime;
        level -= Time.deltaTime;
        level_txt.text = level.ToString();
        healthtxt.text = health.ToString();
    }
}
