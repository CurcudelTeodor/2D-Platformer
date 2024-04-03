using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenu : MonoBehaviour
{
    public void Quit()
    {
        // works after we built the game
        //Application.Quit();

        UnityEditor.EditorApplication.isPlaying = false;
    }
}
