 using UnityEditor;
 using UnityEngine;
 
 [CustomEditor(typeof(PlayableCard)), CanEditMultipleObjects]
 public class PlayableCardEditor : Editor
 {

     public SerializedProperty
         cardType_Prop,
         effectValue_Prop,
         temporaryEffectValue_Prop,
         duration_Prop;
     
     void OnEnable () 
     {
         cardType_Prop = serializedObject.FindProperty ("cardType");
         effectValue_Prop = serializedObject.FindProperty("effectValue");
         temporaryEffectValue_Prop = serializedObject.FindProperty ("temporaryEffectValue");
         duration_Prop = serializedObject.FindProperty ("duration");
     }
     
     public override void OnInspectorGUI() {
         serializedObject.Update ();
         
         EditorGUILayout.PropertyField(cardType_Prop);
         
         CardType st = (CardType)cardType_Prop.enumValueIndex;
         
         switch(st) {
         case CardType.AttackCard:            
             EditorGUILayout.PropertyField( effectValue_Prop, new GUIContent("effectValue") );            
             break;
 
         case CardType.DefenceCard:            
             EditorGUILayout.PropertyField( effectValue_Prop, new GUIContent("effectValue") );
             EditorGUILayout.PropertyField( duration_Prop, new GUIContent("duration") ); 
             break;
 
         case CardType.PoisonCard:            
             EditorGUILayout.PropertyField( temporaryEffectValue_Prop, new GUIContent("temporaryEffectValue") );
             EditorGUILayout.PropertyField( duration_Prop, new GUIContent("duration") );           
             break;
         
         case CardType.HealCard:            
             EditorGUILayout.PropertyField( effectValue_Prop, new GUIContent("effectValue") );            
             break;
         }
         serializedObject.ApplyModifiedProperties ();
     }
 }
