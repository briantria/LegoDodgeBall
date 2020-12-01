/**
 *  created by  : Brian Tria
 *  date        : 01/12/2020 01:52:59
 *  description :
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Unity.LEGO.Behaviours.Actions;
using Unity.LEGO.EditorExt;

namespace LegoDodgeBall
{
    [CustomEditor(typeof(CanonLookAction), true)]
    public class CanonLookActionEditor : MovementActionEditor
    {
        SerializedProperty m_gameModeProp;

        protected override void OnEnable()
        {
            base.OnEnable();
            m_gameModeProp = serializedObject.FindProperty("m_gameMode");
        }

        protected override void CreateGUI()
        {
            EditorGUILayout.PropertyField(m_AudioProp);
            EditorGUILayout.PropertyField(m_AudioVolumeProp);
            EditorGUILayout.PropertyField(m_gameModeProp);
            // EditorGUILayout.PropertyField(m_TimeProp);
            // EditorGUILayout.PropertyField(m_PauseProp);
            // EditorGUILayout.PropertyField(m_CollideProp);
            // EditorGUILayout.PropertyField(m_RepeatProp);
        }
    }
}