using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HDyar.SimpleSOStateMachine
{
    [CreateAssetMenu(fileName = "Machine", menuName = "State Machine/State Machine", order = 0)]
    public class StateMachine : ScriptableObject
    {
        //Active/Gameplay
        public State CurrentState => GetCurrentState();

      

        private Stack<State> _stateGraphHistory = new Stack<State>();

        //Config
        public State DefaultState => _defaultState;
        [HideInInspector,SerializeField]
        private State _defaultState;
        
        //States
        [HideInInspector]
        [SerializeField]
        public List<State> states = new List<State>();

        /// <summary>
        /// Injects runtime dependencies and resets history. You probably want to do this on Awake in an appropriate scene.
        /// </summary>
        public void Init()
        {
            _stateGraphHistory = new Stack<State>();
            foreach (var state in states)
            {
                state.Init(this);
            }
        }

        private State GetCurrentState()
        {
            return _stateGraphHistory?.Count == 0 ? _defaultState : _stateGraphHistory.Peek();
        }

        public void EnterState(State newState)
        {
            if (_stateGraphHistory.Count > 0)
            {
                if (newState == CurrentState)
                {
                    Debug.Log("Can't enter a state from itself.");
                    return;
                }
                CurrentState.Exit();
            }
            
            _stateGraphHistory.Push(newState);
            newState.Enter();
        }

        /// <summary>
        /// If there are at least two states in state history, leave current and enter previous.
        /// </summary>
        /// <returns>True when successfully able to change states.</returns>
        public bool TryLeaveState()
        {
            if (_stateGraphHistory.Count > 1)
            {
                var previous = _stateGraphHistory.Pop();
                previous.Exit();
                
                CurrentState.Enter();
            }
            return false;
        }
        
        public void SetDefaultState(State newState)
        {
            if (states.Contains(newState))
            {
                _defaultState = newState;
            }
        }

        private void OnValidate()
        {
            //We probably just created a new state.
            //todo move this validate to editor on state creation
            if (DefaultState == null && states.Count == 1)
            {
                SetDefaultState(states[0]);
            }
        }
    }
}
