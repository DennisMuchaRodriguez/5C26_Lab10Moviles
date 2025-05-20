using UnityEngine;
using TMPro;
using UnityEngine.UI;
/*
public class MainMenuUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private Button changeNameButton;
    [SerializeField] private Button playButton;
    [SerializeField] private Button scoresButton;
    [SerializeField] private Button exitButton;

    private void Start()
    {
        ConfigureMobileInput();

        GameManager.Instance.PlayerName = PlayerPrefs.GetString("PlayerName", "Jugador");
        UpdatePlayerNameDisplay();

        playButton.onClick.AddListener(OnPlayButtonClicked);
        scoresButton.onClick.AddListener(OnScoresButtonClicked);
        changeNameButton.onClick.AddListener(OnChangeNameClicked);
    }

    private void ConfigureMobileInput()
    {
#if UNITY_ANDROID || UNITY_IOS
        // Configuración específica para móviles
        nameInputField.shouldHideMobileInput = false;
        nameInputField.onSelect.AddListener(OnInputFieldSelected);
        nameInputField.onDeselect.AddListener(OnInputFieldDeselected);

        // Configurar tipo de teclado
        nameInputField.inputType = TMP_InputField.InputType.Standard;
        nameInputField.keyboardType = TouchScreenKeyboardType.Default;
        nameInputField.characterLimit = 12;
#endif
    }

    private void OnInputFieldSelected(string text)
    {
#if UNITY_ANDROID || UNITY_IOS
        // Forzar mostrar el teclado cuando se selecciona el campo
        TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, false, false, "Ingresa tu nombre");
#endif
    }

    private void OnInputFieldDeselected(string text)
    {
#if UNITY_ANDROID || UNITY_IOS
        // Ocultar el teclado cuando se deselecciona
        if (TouchScreenKeyboard.visible)
        {
            TouchScreenKeyboard.hideInput = true;
        }
#endif
    }

    public void OnChangeNameClicked()
    {
        nameInputField.text = GameManager.Instance.PlayerName;
        nameInputField.gameObject.SetActive(true);

#if UNITY_ANDROID || UNITY_IOS
        // Enfocar y activar el teclado
        nameInputField.Select();
        nameInputField.ActivateInputField();
        TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, false, false, "Ingresa tu nombre");
#endif
    }

    public void ChangePlayerName()
    {
        string newName = nameInputField.text.Trim();

        if (!string.IsNullOrEmpty(newName))
        {
            GameManager.Instance.PlayerName = newName;
            PlayerPrefs.SetString("PlayerName", newName);
            PlayerPrefs.Save();

            UpdatePlayerNameDisplay();
            nameInputField.text = "";
            nameInputField.gameObject.SetActive(false);

#if UNITY_ANDROID || UNITY_IOS
            if (TouchScreenKeyboard.visible)
            {
                TouchScreenKeyboard.hideInput = true;
            }
#endif
        }
    }

    private void UpdatePlayerNameDisplay()
    {
        playerNameText.text = $"Jugador: {GameManager.Instance.PlayerName}";
    }

    private void OnPlayButtonClicked()
    {
        GameManager.Instance.StartGame();
    }

    private void OnScoresButtonClicked()
    {
        GameManager.Instance.ShowScores();
    }
}
*/

