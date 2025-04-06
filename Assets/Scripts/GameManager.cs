using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text;

    public void UpdateTheText(string text, Color color)
    {
        this.text.text = text;
        this.text.color = color;
    }
}
