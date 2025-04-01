using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class WithdrawCash : Sequence
    {
        public WithdrawCash(GameObject go) : base(null, go)
        {
            Actions.Enqueue(new BeAt(go, ExperimentBlackboard.Instance.ATMPos));
            Actions.Enqueue(new a_Withdraw(go));
        }
    }
}