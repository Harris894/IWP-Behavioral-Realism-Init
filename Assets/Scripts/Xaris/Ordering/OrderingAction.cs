using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderingAction : MonoBehaviour
{
    [SerializeField]
    private OrderableItems itemsToOrder;
    [SerializeField]
    public List<string> pendingOrders = new List<string>();
    [SerializeField]
    private List<string> completedOrders = new List<string>();

    private AIComponent _aiComponent;

    private void Awake()
    {
        _aiComponent = GetComponent<AIComponent>();
    }

    /// <summary>
    /// Creates a "random" order to be used in the game, adds it to a the list of pending orders.
    /// </summary>
    public void CreateOrder()
    {
        pendingOrders.Add(RandomItemName());
        AudioManager.instance.PlayOrderingSound(pendingOrders[0], this.GetComponent<AudioSource>());
    }

    /// <summary>
    /// Randomly pulls a string from the scriptable object with the orderableItems
    /// </summary>
    private string RandomItemName()
    {
        string itemName = "";
        int randomNum = Random.Range(0, itemsToOrder.orderableItems.Count);

        itemName = itemsToOrder.orderableItems[randomNum];
        Debug.Log("OrderingAction: orderName = " + itemName);

        return itemName;
    }
    
    /// <summary>
    /// Check whether the provided order is the same as the desired one.
    /// </summary>
    /// <param name="_itemName">The name of the item that was received, provided by the EatingZone script</param>
    /// <returns>Whether the provided order was wrong or not</returns>
    public bool CheckDeliveredOrder(string _itemName)
    {
        
        for (int i = 0; i < pendingOrders.Count; i++)
        {
            Debug.LogError(_itemName);
            if (pendingOrders[i].ToLower().Equals(_itemName))
            {
                completedOrders.Add(_itemName);
                pendingOrders.RemoveAt(i);
                Debug.Log("Order was correct");
                
                return true;

            }
            
        }
        Debug.Log("Order received was incorrect");
        Debug.Log(_itemName);
        return false;
    }

    public void FinishedConsuming()
    {
    }
}
