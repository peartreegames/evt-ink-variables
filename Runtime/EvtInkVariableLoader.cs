﻿using Ink.Runtime;
using UnityEngine;

namespace PeartreeGames.EvtInkVariables
{
    public class EvtInkVariableLoader : MonoBehaviour
    {
        [SerializeField] private EvtStoryObject storyObject;

        [SerializeField] private TextAsset inkStory;

        private void Awake()
        {
            storyObject.Value = new Story(inkStory.text);
        }
    }
}