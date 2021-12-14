using System;
using Ink.Runtime;
using PeartreeGames.EvtVariables;
using UnityEngine;

namespace PeartreeGames.EvtInkVariables
{
    public class EvtInkIntObject : EvtIntObject, IStory
    {
        [SerializeField] private EvtStoryObject story;
        public Story Story => story.Value;
        private int _count;
        public override int Value
        {
            get => Story ? (int) Story.variablesState[name] : default;
            set
            {
                if (Story != null) Story.variablesState[name] = value;
                base.Value = value;
            }
        }

        public override void Subscribe(Action listener)
        {
            if (_count == 0) Story.ObserveVariable(name, Observe);
            _count++;
            base.Subscribe(listener);
        }

        public override void Unsubscribe(Action listener)
        {
            _count--;
            if (_count == 0) Story.RemoveVariableObserver(Observe, name);
            base.Unsubscribe(listener);
        }

        public override void Subscribe(Action<int> listener)
        {
            if (_count == 0) Story.ObserveVariable(name, Observe);
            _count++;
            base.Subscribe(listener);
        }
        
        public override void Unsubscribe(Action<int> listener)
        {
            _count--;
            if (_count == 0) Story.RemoveVariableObserver(Observe, name);
            base.Unsubscribe(listener);
        }
        
        private void OnDisable()
        {
            if (Story != null) Story.RemoveVariableObserver(Observe, name);
        }

        private void Observe(string _, object value) => Value = (int)value;
    }
}