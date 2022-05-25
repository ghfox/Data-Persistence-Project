using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HiscoreTable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CreateScores();
    }

    private void CreateScores()
    {
        for(int i = 0; i < GameInfo.Instance.GetNumHiscores(); i++)
        {
            CreateRecord(i);
        }
    }

    public void BackButton(){ GameInfo.Instance.GoToMenu(); }


    private void CreateRecord(int n)
    {
        string name = GameInfo.Instance.GetNameOfNthScore(n);
        int score = GameInfo.Instance.GetScoreOfNthScore(n);
        GameObject entry = new GameObject($"Entry{n}", typeof(TextMeshProUGUI));
        entry.GetComponent<TextMeshProUGUI>().SetText($"{n+1}. {name} : {score}");
        entry.transform.SetParent(transform);
        RectTransform rt = entry.GetComponent<RectTransform>();
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 40 + (n * 35), 1.0f);
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 30, 800.0f);
    }
}
