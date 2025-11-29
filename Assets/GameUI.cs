using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;

    public static GameUI Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        winPanel.SetActive(false);
    }

    public void ShowWinScreen()
    {
        winPanel.SetActive(true);
        Debug.Log("WIN SHOWN!");
    }
}
