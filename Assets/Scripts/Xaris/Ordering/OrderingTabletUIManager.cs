using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderingTabletUIManager : MonoBehaviour
{
    
    public float waitTime;
    
    public GameObject cupcakeButton;
    public GameObject sandwichButton;

    public Image cupcakeCooldown;
    public Image sandwichCooldown;

    public GameObject cupcakeBar;
    public GameObject sandwichBar;

    public bool cupcakeCoolingDown;
    public bool sandwichCoolingDown;


    private void Start()
    {
        waitTime = GetComponent<FoodSpawner>().preparationTime;
    }

    private void Update()
    {
        if (cupcakeCoolingDown)
        {
            cupcakeCooldown.fillAmount -= 1.0f / waitTime * Time.deltaTime;
        }

        if (sandwichCoolingDown)
        {
            sandwichCooldown.fillAmount -= 1.0f / waitTime * Time.deltaTime;
        }
    }

    public void CupcakeCDInit()
    {
        StartCoroutine(CupcakeToCooldown());
    }

    public void SandwichCDInit()
    {
        StartCoroutine(SandwichToCooldown());
    }

    public IEnumerator CupcakeToCooldown()
    {
        cupcakeCoolingDown = true;
        yield return new WaitForSeconds(waitTime);
        cupcakeButton.SetActive(true);
        cupcakeBar.SetActive(false);
        cupcakeCooldown.fillAmount = 1f;
        cupcakeCoolingDown = false;
    }

    public IEnumerator SandwichToCooldown()
    {
        sandwichCoolingDown = true;
        yield return new WaitForSeconds(waitTime);
        sandwichButton.SetActive(true);
        sandwichBar.SetActive(false);
        sandwichCooldown.fillAmount = 1f;
        sandwichCoolingDown = false;
    }


}
