## EvtGraph
Code for "Extending the ScriptableObject Variable Architecture to Ink Variables" [YouTube videos](https://youtu.be/Ge4Q0fDYf-Q)

## Installation
Can be installed via the Package Manager > Add Package From Git URL...

This repo has a dependency on the EvtVariable package which *MUST* be installed first. (From my understanding Unity does not allow git urls to be used as dependencies of packages)
`https://github.com/peartreegames/evt-variables.git`

then the repo can be added

`https://github.com/peartreegames/evt-ink-variables.git`

## Usage
 1. Create `EvtStoryObject` asset (Evt > Ink > StoryObject)
 2. Create `EvtInkVariableCollection` asset (Evt > Ink > Collection) for each file/group of variables*
 3. Drag `EvtStoryObject` and inkFile into `EvtInkVariableCollection` and click `Generate`

*You can modify the generate function to build the entire story with all variables from all files into one `EvtInkVariableCollection` I have broken it down by file to keep them manageable in the hierarchy, but this isn't necessary.
Modify the Story compilation to something like this

```
var compiler = new Ink.Compiler(AssetDatabase.GetAssetPath(_inkFileProperty.objectReferenceValue)), new Compiler.Options
{
	countAllVisits = true,
	fileHandler = new UnityInkFileHandler(Path.GetDirectoryName(AssetDatabase.GetAssetPath(_inkFileProperty.objectReferenceValue)))
});
```

## Updates from Video

 - Modified Story dependency in EvtInkVariables to use an EvtStoryObject set upon creation, rather than pass it in in Init function.
 - Created EvtInkListObject