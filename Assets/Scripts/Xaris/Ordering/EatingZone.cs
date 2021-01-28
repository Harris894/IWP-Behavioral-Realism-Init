using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatingZone : MonoBehaviour
{
    [SerializeField]
    private OrderingAction orderManager;
    public string orderItemName;
    GameObject currentGameObjectInside;


    public void RegisterOrderManager(OrderingAction _ordermanager)
    {
        orderManager = _ordermanager;
        Debug.Log("EatingZone: orderManager = " + _ordermanager.name);
    }

    public void UnregisterOrderManager()
    {
        orderManager = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Deliverable"))
        {
            orderItemName = other.gameObject.name.ToLower();
            currentGameObjectInside = other.gameObject;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Deliverable"))
        {
            orderItemName = null;
            currentGameObjectInside = null;
        }
    }

    private IEnumerator ConfirmOrder()
    {
        if (orderManager != null)
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("EatingZone: orderItem = " + orderItemName);
            if (orderItemName != null)
            {
                bool correctOrder = orderManager.CheckDeliveredOrder(orderItemName);

                
                AIComponent _aiComponent = orderManager.GetComponent<AIComponent>();
                _aiComponent.ReceiveOrder(correctOrder);
            }
            else
            {
                Debug.Log("No itemName has been passed");
            }
            
        }
        else
        {
            Debug.Log("No customer for this serving zone");
        }
    }

    public void ConfirmOrder2()
    {

        StartCoroutine(ConfirmOrder());
    }


    public void DestroyObjectInside()
    {
        Destroy(currentGameObjectInside);
    }

}
