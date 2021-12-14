using System;
using Ink.Runtime;
using PeartreeGames.EvtVariables;
using UnityEngine;

namespace PeartreeGames.EvtInkVariables
{
    public class EvtInkFloatObject : EvtFloatObject, IStory
    {
        public Story story { get; set; }
        private int _count;

        public override float Value
        {
            get => story ? (float) story.variablesState[name] : default;
            set
            {
                if (story != null) story.variablesState[name] = value;
                base.Value = value;
            }
        }

        public override void Subscribe(Action listener)
        {
            if (_count == 0) story.ObserveVariable(name, Observe);
            _count++;
            base.Subscribe(listener);
        }

        public override void Unsubscribe(Action listener)
        {
            _count--;
            if (_count == 0) story.RemoveVariableObserver(Observe, name);
            base.Unsubscribe(listener);
        }

        public override void Subscribe(Action<float> listener)
        {
            if (_count == 0) story.ObserveVariable(name, Observe);
            _count++;
            base.Subscribe(listener);
        }
        
        public override void Unsubscribe(Action<float> listener)
        {
            _count--;
            if (_count == 0) story.RemoveVariableObserver(Observe, name);
            base.Unsubscribe(listener);
        }
        
        private void OnDisable()
        {
            if (story != null) story.RemoveVariableObserver(Observe, name);
        }

        private void Observe(string _, object value) => Value = (float)value;
    }
}