using UnityEngine;

namespace InfiniteTree
{
    public class CheckFood : Selector
    {
        public CheckFood(GameObject go) : base(null, go)
        {
            Actions.Enqueue(new CheckInventory());
            Actions.Enqueue(new GetFood());
        }
    }
}