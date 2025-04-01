using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class CheckCash : Selector
    {
        public CheckCash(GameObject go) : base(null, go)
        {
            Actions.Enqueue(new CheckWallet(go));
            Actions.Enqueue(new WithdrawCash(go));
        }
    }
}