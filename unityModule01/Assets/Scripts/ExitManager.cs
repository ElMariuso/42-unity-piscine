using UnityEngine;

public class ExitManager : MonoBehaviour
{
    public LevelManager levelManager;
    private bool isExitReached = false;

    public bool IsExitReached()
    {
        return isExitReached;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CheckCharacterExitMatch(other.gameObject.tag, gameObject.tag))
        {
            isExitReached = true;
            levelManager.CheckAllExits();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (CheckCharacterExitMatch(other.gameObject.tag, gameObject.tag))
            isExitReached = false;
    }

    private bool CheckCharacterExitMatch(string characterTag, string exitTag)
    {
        return ((characterTag == "Character1" && exitTag == "Exit1")
            || (characterTag == "Character2" && exitTag == "Exit2")
            || (characterTag == "Character3" && exitTag == "Exit3"));
    }
}
