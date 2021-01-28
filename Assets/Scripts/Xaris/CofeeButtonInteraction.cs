using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CofeeButtonInteraction : MonoBehaviour
{
    private Animator anim;
    private BoxCollider col;

    [SerializeField]
    private CoffeeMachineBehaviour coffemachine;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("IndexFinger"))
        {
            StartCoroutine(DisableCollider());
            anim.SetTrigger("ButtonPressed");
            
            if (this.gameObject.name== "CapuccinoButton")
            {
                coffemachine.BeginPouring("Capuccino");
                Debug.Log("Cappucinno pressed");
            }
            else
            {
                coffemachine.BeginPouring("Espresso");
            }
            
        }
    }

    private IEnumerator DisableCollider() 
    {
        col.enabled = false;
        yield return new WaitForSeconds(2f);
        col.enabled = true;
    }

}
