using Interfaces;
using UnityEngine;

namespace FiniteStateMachine
{
    public class PlayerState
    {
        protected PlayerStateMachine stateMachine;
        protected readonly Player player;
        protected UnitData data;
        protected Core core;
        public PlayerState(Player player,PlayerStateMachine stateMachine,UnitData data)
        {
            this.player = player;
            this.stateMachine = stateMachine;
            this.core = player.Core;
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