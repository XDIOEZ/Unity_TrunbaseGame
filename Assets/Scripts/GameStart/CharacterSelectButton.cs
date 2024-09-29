using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectButton : MonoBehaviour
{
    public string characterName; // Name of the Level_Battlecharacter to select
    private Button button; // Reference to the button component

    private void Start()
    {
        // Get the button component
        button = GetComponent<Button>();
        if (button != null)
        {
            // Add a listener to the button click event
            button.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogError("Button component not found on " + gameObject.name);
        }
    }

    private void OnButtonClick()
    {
        // Pass the Level_Battlecharacter name to the CharacterSelect singleton
        if (CharacterSelect.Instance != null)
        {
            CharacterSelect.Instance.SelectCharacter(characterName);
        }
        else
        {
            Debug.LogError("CharacterSelect Instance not found.");
        }
    }
}
