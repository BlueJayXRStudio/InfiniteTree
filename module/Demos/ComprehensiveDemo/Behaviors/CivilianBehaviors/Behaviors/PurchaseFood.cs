using UnityEngine;

namespace InfiniteTree
{
    internal class PurchaseFood : Sequence
    {
        public PurchaseFood(GameObject go) : base(null, go)
        {
            Actions.Enqueue(new BeAt(go, ExperimentBlackboard.Instance.GroceryStorePos));
            Actions.Enqueue(new a_Purchase(go));
        }
    }
}