using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMachineBehaviour : MonoBehaviour
{
    public GameObject coffeeCup;

    public float preparationTime;

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private ParticleSystem coffeeParticles;
    [SerializeField]
    private GameObject capuccinoPrefab;
    [SerializeField]
    private GameObject espressoPrefab;
    [SerializeField]
    private Transform canvas;

    private AudioSource audioSource;

    string coffeeCupInSlot;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CoffeeCup"))
        {
            if (canvas.gameObject.activeInHierarchy)
            {
                canvas.gameObject.SetActive(false);
            }

            coffeeCup = other.gameObject;
            if (other.gameObject.name=="CapuccinoEmpty")
            {
                //do capuccino stuff
                coffeeCupInSlot = "Capuccino";

            }
            else if (other.gameObject.name == "EspressoEmpty")
            {
                //do espresso stuff
                coffeeCupInSlot = "Espresso";
            }
            else
            {
                Debug.LogError("Cup of coffee not found");
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CoffeeCup"))
        {
            coffeeCupInSlot = null;
        }
    }

    public void BeginPouring(string typeOfCoffee)
    {
        if (typeOfCoffee == coffeeCupInSlot)
        {
            Debug.Log(typeOfCoffee);
            StartCoroutine(StartCoffeeMachine(typeOfCoffee));
        }
        else
        {
            canvas.gameObject.SetActive(true);
        }
    }


    IEnumerator StartCoffeeMachine(string typeOfCoffee)
    {
        Debug.Log("Coffemachine started making: " + typeOfCoffee);
        AudioManager.instance.PlayFXSound("CoffeeMachine", audioSource);
        yield return new WaitForSeconds(5f);
        coffeeParticles.gameObject.SetActive(true);
        yield return new WaitForSeconds(preparationTime);
        Destroy(coffeeCup);
        coffeeParticles.gameObject.SetActive(false);
        AudioManager.instance.StopSoundFX(audioSource);
        if (typeOfCoffee == "Capuccino")
        {
            GameObject newCupOfCoffee = Instantiate(capuccinoPrefab, spawnPoint.position, Quaternion.identity);
            newCupOfCoffee.name = typeOfCoffee;
        }
        else if (typeOfCoffee == "Espresso")
        {
            GameObject newCupOfCoffee = Instantiate(espressoPrefab, spawnPoint.position, Quaternion.identity);
            newCupOfCoffee.name = typeOfCoffee;
        }
        

    }

}
