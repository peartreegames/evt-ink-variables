using System;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

namespace PeartreeGames.EvtInkVariables
{
    public class EvtInkVariableLoader : MonoBehaviour
    {
        [SerializeField] private List<EvtInkVariableCollection> collections;
        [SerializeField] private EvtStoryObject storyObject;

        [SerializeField] private TextAsset inkStory;

        private void Awake()
        {
            storyObject.Value = new Story(inkStory.text);
        }

        private void Start()
        {
            foreach(var col in collections) col.Init(storyObject.Value);
        }
    }
}