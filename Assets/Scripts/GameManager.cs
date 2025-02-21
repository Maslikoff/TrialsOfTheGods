using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI textHelp;
    public TextMeshProUGUI textMessage;
    public string title;

    [Header("Message Settings")]
    public string startMessage = "A,D,Space - moving";
    public float messageDuration = 10f;

    [Header("Menu Settings")]
    public GameObject menuPanel;

    private PlayerCarry _playerCarry;
    private bool _isMenuVisible = false;

    private void Start()
    {
        Time.timeScale = 1;

        _playerCarry = FindObjectOfType<PlayerCarry>();

        if (menuPanel != null)
            menuPanel.SetActive(false);

        StartCoroutine(ShowStartMessage());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ToggleMenu();

        UI();
    }

    private void ToggleMenu()
    {
        _isMenuVisible = !_isMenuVisible;

        if (menuPanel != null)
            menuPanel.SetActive(_isMenuVisible);

        Time.timeScale = _isMenuVisible ? 0f : 1f;
    }

    private void UI()
    {
        if (_playerCarry.HasCarriedObject())
            textHelp.text = "*" + title + "*";
        else
            textHelp.text = "";
    }

    private IEnumerator ShowStartMessage()
    {
        textMessage.text = startMessage;

        yield return new WaitForSeconds(messageDuration);

        textMessage.text = "";
    }
}