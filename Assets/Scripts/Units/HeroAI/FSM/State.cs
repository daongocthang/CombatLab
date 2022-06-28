using Interfaces;
using UnityEngine;

namespace FiniteStateMachine
{
    public class State
    {
        protected StateMachine stateMachine;
        protected Entity entity;
        protected Core core;
        protected UnitData data;
        public State(Entity entity,StateMachine stateMachine,UnitData data)
        {
            this.entity = entity;
            this.stateMachine = stateMachine;
            core = entity.core;
            this.data = data;
        }

        public virtual void Enter()
        {
            DoChecks();
        }

        public virtual void Exit()
        {
            
        }

        public virtual void LogicUpdate()
        {
            
        }

        public virtual void PhysicUpdate()
        {
            DoChecks();
        }

        public virtual void DoChecks()
        {
            
        }

        public virtual void AnimTrigger()
        {
            
        }

    }
}