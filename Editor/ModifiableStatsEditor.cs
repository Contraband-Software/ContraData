// namespace Software.Contraband.Data.Editor
// {
//     public class ModifiableStatsEditor
//     { 
//         [CustomEditor(typeof(GenericStatsAsset<>), true)] // CanEditMultipleObjects
//         public class GenericStatsAssetEditor : UnityEditor.Editor
//         {
//             private SimpleTreeView test;
//             [SerializeField] TreeViewState m_TreeViewState;
//             
//             private void OnEnable()
//             {
//                 
//             }
//             
//             public override void OnInspectorGUI ()
//             {
//              DrawDefaultInspector();
//              
//              EditorGUILayout.Separator();
//              EditorGUILayout.LabelField("Values", EditorStyles.boldLabel);
//              
//              EditorGUILayout.BeginHorizontal();
//              
//              EditorGUILayout.LabelField("Firmware Asset Type");
//              
//              EditorGUILayout.EndHorizontal();
//              
//              test.OnGUI();
//             }
//          }
//     }
// }