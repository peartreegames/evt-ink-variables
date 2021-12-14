using System.Collections.Generic;
using Ink.Runtime;
using PeartreeGames.EvtVariables;
using UnityEngine;

namespace PeartreeGames.EvtInkVariables
{
    [CreateAssetMenu(fileName = "ink_", menuName = "Evt/Ink/Collection", order = 100)]
    public class EvtInkVariableCollection : ScriptableObject
    {
		[SerializeField] private UnityEngine.Object inkFile;
        [SerializeField] private List<EvtVariableObject> variables;
		[SerializeField] private EvtStoryObject story;

    }
}