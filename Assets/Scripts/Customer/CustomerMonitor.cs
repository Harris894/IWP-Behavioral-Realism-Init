using UnityEngine;
using UnityEngine.UI;

public class CustomerMonitor : MonoBehaviour
{
    public CustomerRuntimeSet Set;

    public Text Text;

    private int previousCount = -1;

    private void OnEnable()
    {
        UpdateText();
    }

    private void Update()
    {
        if (previousCount != Set.Items.Count)
        {
            UpdateText();
            previousCount = Set.Items.Count;
        }
    }

    public void UpdateText()
    {
        Text.text = "Customers: " + Set.Items.Count;
    }
}
