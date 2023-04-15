using RPG.Core;
using RPG.Saving;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        NavMeshAgent navMeshAgent;
        Animator animator;

        float defaultSpeed;

        void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            defaultSpeed = navMeshAgent.speed;
        }

        void Update()
        {
            animator.SetFloat("velocity", navMeshAgent.velocity.magnitude);
        }

        public void StartMovementAction(Vector3 destination, float speedModifier = 1)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedModifier);
        }
        public void MoveTo(Vector3 destination, float speedModifier = 1)
        {
            navMeshAgent.speed = defaultSpeed * speedModifier;
            navMeshAgent.isStopped = false;
            navMeshAgent.destination = destination;
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        public void FootR()
        {

        }

        public void FootL()
        {

        }

        public void Teleport(Vector3 pos, Quaternion rot)
        {
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = pos;
            transform.rotation = rot;
            GetComponent<NavMeshAgent>().enabled = true;
        }

        public object SaveState()
        {
            Dictionary<string, SerializableVector3> state = new Dictionary<string, SerializableVector3>();
            state["position"] = new SerializableVector3(transform.position);
            state["rotation"] = new SerializableVector3(transform.rotation);
            return state;
        }

        public void LoadState(object loadedState)
        {
            Dictionary<string, SerializableVector3> state = (Dictionary<string, SerializableVector3>)loadedState;
            Teleport(state["position"].ToVector(), state["rotation"].ToQuaternion());
        }
    }
}
