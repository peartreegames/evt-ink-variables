using Ink.Runtime;
using PeartreeGames.EvtVariables;
using UnityEditor;
using UnityEngine.UIElements;

namespace PeartreeGames.EvtInkVariables.Editor
{
    
    public class EvtInkVariableObjectEditor<T> : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var value = ((EvtVariableObject<T>) target).Value;
            return new Label($"Value: {value}");
        }
    }
    [CustomEditor(typeof(EvtInkBoolObject))]
    public class EvtInkBoolObjectEditor : EvtInkVariableObjectEditor<bool> {}
    
    [CustomEditor(typeof(EvtInkIntObject))]
    public class EvtInkIntObjectEditor : EvtInkVariableObjectEditor<int> {}
    
    [CustomEditor(typeof(EvtInkStringObject))]
    public class EvtInkStringObjectEditor : EvtInkVariableObjectEditor<string> {}
    
    [CustomEditor(typeof(EvtInkFloatObject))]
    public class EvtInkFloatObjectEditor : EvtInkVariableObjectEditor<float> {}

    [CustomEditor(typeof(EvtInkListObject))]
    public class EvtInkListObjectEditor : EvtInkVariableObjectEditor<InkList>
    {
        public override VisualElement CreateInspectorGUI()
        {
            var value = ((EvtInkListObject) target).Value;
            return new Label($"Value: {value}");
        }
    }
}