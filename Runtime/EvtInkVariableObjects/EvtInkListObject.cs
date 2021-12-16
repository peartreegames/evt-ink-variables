using System;
using Ink.Runtime;
using PeartreeGames.EvtVariables;
using UnityEngine;

namespace PeartreeGames.EvtInkVariables
{
    public class EvtInkListObject : EvtVariableObject<InkList>, IStory
    {
        [SerializeField] private EvtStoryObject story;
        public Story Story => story.Value;
        private int _count;
        public override InkList Value
        {
            get => Story ? (InkList) Story.variablesState[name] : default;
            set
            {
                if (Story != null) Story.variablesState[name] = value;
                base.Value = value;
            }
        }

        protected override bool IsEqual(InkList current, InkList other)
        {
            if (Equals(current, other)) return true;
            if (current == null || other == null) return false;
            return current.Contains(other) && other.Contains(current);
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

        public override void Subscribe(Action<InkList> listener)
        {
            if (_count == 0) Story.ObserveVariable(name, Observe);
            _count++;
            base.Subscribe(listener);
        }
        
        public override void Unsubscribe(Action<InkList> listener)
        {
            _count--;
            if (_count == 0) Story.RemoveVariableObserver(Observe, name);
            base.Unsubscribe(listener);
        }
        
        private void OnDisable()
        {
            if (Story != null) Story.RemoveVariableObserver(Observe, name);
        }

        private void Observe(string _, object value) => Value = (InkList)value;
    }
}