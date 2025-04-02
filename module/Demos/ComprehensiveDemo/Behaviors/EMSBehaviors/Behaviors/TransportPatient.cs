using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class TransportPatient : Behavior
    {
        private GameObject DriverObject;
        private GameObject Patient;

        private List<Behavior> ToDo = new();

        public TransportPatient(GameObject driverObject, GameObject patient) : base(driverObject) {
            // Debug.Log("transporting patient!");
            DriverObject = driverObject;
            Patient = patient;

            // ToDo.Add(new ToWaypoints(this.Patient.GetComponent<Attributes>().GetPos));

            // var MoveToPatient = DriverObject.GetComponent<EMSBehaviorFactory>().GetNewMoveBehavior(DriverObject, Patient.GetComponent<Attributes>().GetPos);
            // var MoveToHospital = DriverObject.GetComponent<EMSBehaviorFactory>().GetNewMoveBehavior(DriverObject, ExperimentBlackboard.Instance.HospitalPos);

            ToDo.Add(new MoveTo(DriverObject, Patient.GetComponent<Attributes>().GetPos, false));
            ToDo.Add(new PickUp(Patient));
            ToDo.Add(new MoveTo(DriverObject, ExperimentBlackboard.Instance.HospitalPos, false));
            ToDo.Add(new DropOff(Patient));
        }

        public override Status CheckRequirement()
        {
            throw new System.NotImplementedException();
        }

        public override Status Step(Stack<Behavior> memory, GameObject go, Status message)
        {
            memory.Push(this);

            go.GetComponent<EMSAttributes>().lightsController.transform.gameObject.SetActive(true);
            if (message == Status.SUCCESS) {
                go.GetComponent<EMSAttributes>().lightsController.transform.gameObject.SetActive(false);
                return Status.SUCCESS;
            }

            memory.Push(new Sequence(ToDo, null));
            return Status.RUNNING;
        }
        
    }
}