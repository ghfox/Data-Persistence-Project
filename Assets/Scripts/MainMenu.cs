using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void NewGame() { GameInfo.Instance.NewGame(); }
    public void GoToHiscore() { GameInfo.Instance.GoToHiscore(); }
}
