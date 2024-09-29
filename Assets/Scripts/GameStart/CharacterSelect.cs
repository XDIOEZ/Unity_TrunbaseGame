using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    public static CharacterSelect Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // Uncomment if you want this to persist across scenes
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // This method will be called with the button identifier
    public void SelectCharacter(string characterIdentifier)
    {
        Debug.Log("Character selected: " + characterIdentifier);

        // Execute different operations based on the Level_Battlecharacter identifier
        switch (characterIdentifier)
        {
            //��ִ��������ѡ���,���������λ�ø�Ϊ(0,10,-10)
            case "սʿ":
                // Perform operations specific to Warrior
                Debug.Log("Warrior selected. Performing Warrior-specific operations.");
                // Add your Warrior-specific logic here
                break;
            case "��ʦ":
                // Perform operations specific to Mage
                Debug.Log("Mage selected. Performing Mage-specific operations.");
                
                // Add your Mage-specific logic here
                break;
            case "�װ�":
                // Perform operations specific to Archer
                Debug.Log("Archer selected. Performing Archer-specific operations.");
                // Add your Archer-specific logic here
                break;
            default:
                Debug.LogWarning("Unknown Level_Battlecharacter identifier: " + characterIdentifier);
                break;
        }
        TotheMap();
    }

    public void TotheMap()
    {
        Camera.main.transform.position = new Vector3(0, 10, -10);
    }
}
