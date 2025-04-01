using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class GetFood : Sequence
    {
        public GetFood(GameObject go) : base(null, go) {
            DriverObject = go;
            Actions.Enqueue(new CheckCash(go));
            Actions.Enqueue(new PurchaseFood(go));
        }

        public override Status CheckRequirement()
        {
            if (!Finished)
                return base.CheckRequirement();
            return Status.FAILURE;
        }
    }
}