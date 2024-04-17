using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public ExitManager[] exits;

    public void CheckAllExits() {
        foreach (ExitManager exit in exits) 
        {
            if (!exit.IsExitReached())
                return ;
        }
        Debug.Log("All exits reached. Stage 1 completed!");
    }
}
