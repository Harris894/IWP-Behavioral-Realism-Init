using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuzeStudios.Variables;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    internal int _highscore = 0;

    public void UpdateHighScore(int amount)
    {
        if (amount <= 0) return;

        _highscore += amount;
        GetComponent<TextMeshProUGUI>().SetText(_highscore.ToString("0000"));
    }
}
