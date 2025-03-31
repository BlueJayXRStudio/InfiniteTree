using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class TransportPatient : Behavior
    {
        private GameObject DriverObject;
        private GameObject Patient;

        private List<Behavior> ToDo = new();

        public TransportPatient(GameObject driverObject, GameObject patient) {
            // Debug.Log("transporting patient!");
            DriverObject = driverObject;
            Patient = patient;

            // ToDo.Add(new ToWaypoints(this.Patient.GetComponent<Attributes>().GetPos));

            var MoveToPatient = DriverObject.GetComponent<EMSBehaviorFactory>().GetNewMoveBehavior(DriverObject, Patient.GetComponent<Attributes>().GetPos);

            var MoveToHospital = DriverObject.GetComponent<EMSBehaviorFactory>().GetNewMoveBehavior(DriverObject, ExperimentBlackboard.Instance.HospitalPos);

            ToDo.Add(new MoveTo(DriverObject, Patient.GetComponent<Attributes>().GetPos));
            ToDo.Add(new PickUp(Patient));
            ToDo.Add(new MoveTo(DriverObject, ExperimentBlackboard.Instance.HospitalPos));
            ToDo.Add(new DropOff(Patient));
        }

        public Status Step(Stack<Behavior> memory, GameObject go, Status message)
        {
            memory.Push(this);

            if (message == Status.SUCCESS)
                return Status.SUCCESS;

            memory.Push(new Sequence(ToDo));
            return Status.RUNNING;
        }
    }
}