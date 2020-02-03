using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UnrealChan
{
    [CustomEditor(typeof(MatcapGenerator))]
    public class MatcapGeneratorEditor : Editor
    {
        SerializedProperty texturePropertiesIdxProp;

        Texture2D texture;

        void OnEnable()
        {
            texturePropertiesIdxProp = serializedObject.FindProperty("CurrentTexturePropertiesIdx");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();

            MatcapGenerator script = target as MatcapGenerator;

            texture = script.GeneratedTexture;

            if (script.TargetMaterial != null)
            {
                var properties = script.TargetMaterial.GetTexturePropertyNames();
                texturePropertiesIdxProp.intValue = EditorGUILayout.Popup("Target Texture Property", texturePropertiesIdxProp.intValue, properties);
                script.TextureProperty = properties[texturePropertiesIdxProp.intValue];
            }

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Save Matcap Texture"))
            {
                if (script.TextureProperty != string.Empty)
                {
                    var fullPath = script.SaveTexture(texture);
                    AssetDatabase.Refresh();

                    Texture2D saved = (Texture2D)AssetDatabase.LoadAssetAtPath(fullPath, typeof(Texture2D));

                    script.ApplyToMaterial(saved);
                    script.TextureProperty = string.Empty;
                }
            }

            EditorGUILayout.EndHorizontal();

            GUILayout.Label("Generated Matcap Preview");
            if (texture != null)
            {
                GUILayout.Label(texture);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
