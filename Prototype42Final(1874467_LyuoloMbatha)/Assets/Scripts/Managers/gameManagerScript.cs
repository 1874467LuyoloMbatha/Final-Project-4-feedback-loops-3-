using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStatess 
{
    COMBAT,
    ENDMATCH

}

public class gameManagerScript : MonoBehaviour
{
    public static gameManagerScript instance;

    public GameStatess currentStates;

    private void Awake()
    {
        if (instance != null && instance != this) 
        {
            Destroy(this.gameObject);
            return;
        
        }

        instance = this;
        //Allows scripts to run in every scene.
        DontDestroyOnLoad(this.gameObject);

    }
    public static void StateChanges(GameStatess newState) 
    {
        if (instance.currentStates == newState)
            return;

        instance.currentStates = newState;

        switch (newState)
        {
            case GameStatess.COMBAT:
                break;
            case GameStatess.ENDMATCH:
                break;
            
        }


    }



}
