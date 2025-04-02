using UnityEngine;

namespace InfiniteTree
{
    public class CheckFood : Selector
    {
        public CheckFood(GameObject go) : base(null, go)
        {
            Debug.Log("Checking For Food");
            Actions.Enqueue(new CheckInventory(go));
            Actions.Enqueue(new GetFood(go));
        }
    }
}