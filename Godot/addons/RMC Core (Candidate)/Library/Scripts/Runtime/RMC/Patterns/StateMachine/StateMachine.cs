using Godot;
using RMC.Core.Events;
using RMC.Core.Patterns.StateMachines;

namespace RMC.Core.Patterns.StateMachines
{
    /// <summary>
    /// Interface for all states
    /// </summary>
    public interface IState
    {
        void Enter();
        void Execute(double delta);
        void Exit();
    }

    /// <summary>
    /// Base class for all states
    /// </summary>
    public partial class BaseState : IState
    {
        // Fields ----------------------------------------
        protected readonly StateMachine _stateMachine;

        // Initialization -------------------------------
        protected BaseState(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        // Methods ---------------------------------------
        public virtual void Enter() { }
        public virtual void Execute(double delta) { }
        public virtual void Exit() { }

        public override string ToString()
        {
            return GetType().Name;
        }
    }

    public partial class StateMachineEvent : RmcEvent<IState, IState> { }
    
    /// <summary>
    /// State machine to manage states
    /// </summary>
    public partial class StateMachine 
    {
        // Events ----------------------------------------
        public readonly StateMachineEvent OnStateEnter = new StateMachineEvent();
        public readonly StateMachineEvent OnStateExecute = new StateMachineEvent();
        public readonly StateMachineEvent OnStateExit = new StateMachineEvent();
        
        // Fields ----------------------------------------
        private IState _previousState;
        private IState _currentState;

        // Initialization -------------------------------
        public StateMachine()
        {
        }

        // Methods ---------------------------------------
        public void StateChange(IState newState)
        {
            // NOTE: We 'change' BEFORE sending events
            //       so be aware of the event parameter naming
            _previousState = _currentState;
            _currentState = newState;
            StateExit();
            StateEnter();
        }

        private void StateEnter()
        {
            _currentState?.Enter();
            OnStateEnter.Invoke(_previousState, _currentState);
        }
        
        public void StateExecute(double delta)
        {
            _currentState?.Execute(delta);
            OnStateExecute.Invoke(_previousState, _currentState);
        }
        
        private void StateExit()
        {
            _previousState?.Exit();
            OnStateExit.Invoke(_previousState, _currentState);
        }
    }

    /// <summary>
    /// Example concrete state: Sample01
    /// </summary>
    public partial class Sample01State : BaseState
    {
        // Initialization -------------------------------
        public Sample01State(StateMachine stateMachine) : base(stateMachine) { }

        // Methods ---------------------------------------
        public override void Enter()
        {
            GD.Print("Entering Sample01State");
        }

        public override void Execute(double delta)
        {
            // GD.Print("Executing Sample01State");
        }

        public override void Exit()
        {
            GD.Print("Exiting Sample01State");
        }
    }

    /// <summary>
    /// Example concrete state: Sample02
    /// </summary>
    public partial class Sample02State : BaseState
    {
        // Initialization -------------------------------
        public Sample02State(StateMachine stateMachine) : base(stateMachine) { }

        // Methods ---------------------------------------
        public override void Enter()
        {
            GD.Print("Entering Sample02State");
        }

        public override void Execute(double delta)
        {
            // GD.Print("Executing Sample02State");
        }

        public override void Exit()
        {
            GD.Print("Exiting Sample02State");
        }
    }
}

namespace RMC.Core.Templates
{
    /// <summary>
    /// Template class demonstrating the use of the state machine
    /// </summary>
    public partial class StateMachineExample : Node
    {
        // Properties ------------------------------------

        // Fields ----------------------------------------
        private StateMachine _stateMachine;
        private Sample01State _sample01State;
        private Sample02State _sample02State;

        // Initialization -------------------------------
        public StateMachineExample()
        {
            // Example of initializing the state machine and changing states
            _stateMachine = new StateMachine();
            
            _stateMachine.OnStateEnter.AddListener(StateMachine_OnStateEnter);
            _stateMachine.OnStateExecute.AddListener(StateMachine_OnStateExecute);
            _stateMachine.OnStateExit.AddListener(StateMachine_OnStateExit);
            
            _sample01State = new Sample01State(_stateMachine);
            _sample02State = new Sample02State(_stateMachine);
            
            _stateMachine.StateChange(_sample01State);
        }

        // Methods ---------------------------------------
        public void _Process(float delta)
        {
            _stateMachine?.StateExecute(delta);
        }

        // Event Handlers --------------------------------
        private void StateMachine_OnStateEnter(IState previousState, IState currentState)
        {
            // Handle state entry logic here if needed
        }
        
        private void StateMachine_OnStateExecute(IState previousState, IState currentState)
        {
            // Handle state execution logic here if needed
        }
        
        private void StateMachine_OnStateExit(IState previousState, IState currentState)
        {
            if (previousState is Sample01State)
            {
                _stateMachine.StateChange(_sample02State);
            }
            else if (previousState is Sample02State)
            {
                _stateMachine.StateChange(_sample01State);
            }
        }
    }
}
