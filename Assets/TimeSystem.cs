using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeSystem : MonoBehaviour, IFreezable
{
    private TextMeshProUGUI _textMesh;

    internal float timer = 0.0f;
    public bool paused = true;

    private void Awake()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (paused) return;

        timer += Time.deltaTime;

        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);

        _textMesh.SetText(string.Format("{0:00}:{1:00}", minutes, seconds));
    }

    public void Freeze()
    {
        paused = true;
    }

    public void Defrost()
    {
        paused = false;
    }
}
