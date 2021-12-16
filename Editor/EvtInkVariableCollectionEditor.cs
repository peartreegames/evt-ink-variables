using System;
using System.IO;
using PeartreeGames.EvtVariables;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace PeartreeGames.EvtInkVariables.Editor
{
    [CustomEditor(typeof(EvtInkVariableCollection))]
    public class EvtInkVariableCollectionEditor : UnityEditor.Editor
    {
        private SerializedProperty _inkFileProperty;
        private SerializedProperty _variablesProperty;
        private SerializedProperty _storyProperty;

        private void OnEnable()
        {
            _inkFileProperty = serializedObject.FindProperty("inkFile");
            _variablesProperty = serializedObject.FindProperty("variables");
            _storyProperty = serializedObject.FindProperty("story");
        }

        public override VisualElement CreateInspectorGUI()
        {
            var fileInput = new PropertyField(_inkFileProperty);
            fileInput.Bind(serializedObject);
            var storyInput = new PropertyField(_storyProperty);
            storyInput.Bind(serializedObject);
            var elem = new VisualElement();
            elem.Add(fileInput);
            elem.Add(storyInput);
            
            elem.Add(new Button(() =>
            {
                _variablesProperty.ClearArray();
                var previous = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(target));
                foreach (var prev in previous)
                {
                    if (AssetDatabase.IsSubAsset(prev)) AssetDatabase.RemoveObjectFromAsset(prev);
                }

                var compiler =
                    new Ink.Compiler(
                        File.ReadAllText(AssetDatabase.GetAssetPath(_inkFileProperty.objectReferenceValue)));
                var story = compiler.Compile();
                foreach (var variable in story.variablesState)
                {
                    var varType = variable.Split("_")[0];
                    EvtVariableObject asset;
                    switch (varType)
                    {
                        case "int":
                            var evtInt = CreateInstance<EvtInkIntObject>();
                            evtInt.name = variable;
                            asset = evtInt;
                            break;
                        case "float":
                            var evtFloat = CreateInstance<EvtInkFloatObject>();
                            evtFloat.name = variable;
                            asset = evtFloat;
                            break;
                        case "bool":
                            var evtBool = CreateInstance<EvtInkBoolObject>();
                            evtBool.name = variable;
                            asset = evtBool;
                            break;
                        case "string":
                            var evtString = CreateInstance<EvtInkStringObject>();
                            evtString.name = variable;
                            asset = evtString;
                            break;
                        case "list":
                            var evtList = CreateInstance<EvtInkListObject>();
                            evtList.name = variable;
                            asset = evtList;
                            break;
                        default:
                            continue;
                    }

                    var serializedAsset = new SerializedObject(asset);
                    serializedAsset.FindProperty("story").objectReferenceValue =
                        _storyProperty.objectReferenceValue;
                    serializedAsset.ApplyModifiedProperties();
                    AssetDatabase.AddObjectToAsset(asset, target);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(asset));
                    _variablesProperty.arraySize++;
                    _variablesProperty.GetArrayElementAtIndex(_variablesProperty.arraySize - 1).objectReferenceValue =
                        asset;
                    serializedObject.ApplyModifiedProperties();
                }
                AssetDatabase.Refresh();

            }){ text = "Generate" });
            return elem;
        }
    }
}




















