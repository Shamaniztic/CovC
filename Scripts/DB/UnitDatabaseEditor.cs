#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using Sirenix.Utilities;

public class UnitDatabaseEditor : OdinEditorWindow
{
    [MenuItem("Window/Unit Database")]
    private static void OpenWindow()
    {
        GetWindow<UnitDatabaseEditor>().Show();
    }

    [InlineEditor]
    public UnitDatabase unitDatabase;

    protected override void OnEnable()
    {
        // This will set the reference to the ScriptableObject asset.
        unitDatabase = AssetDatabase.LoadAssetAtPath<UnitDatabase>("Assets/PathToYourAsset/NewUnitDatabase.asset");
    }
}
#endif
