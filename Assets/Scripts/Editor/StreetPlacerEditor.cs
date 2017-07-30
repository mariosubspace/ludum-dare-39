using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(StreetPieceBehaviour))]
public class StreetPlacerEditor : Editor
{
    float xSnapDistance = 11.5f;
    float zSnapDistance = 11.5f;

    StreetPieceBehaviour streetPiece;

    Tool LastTool = Tool.None;

    private void OnEnable()
    {
        LastTool = Tools.current;
        Tools.current = Tool.None;
        streetPiece = (StreetPieceBehaviour)target;
    }

    private void OnDisable()
    {
        Tools.current = LastTool;
    }

    protected virtual void OnSceneGUI()
    {
        if (Tools.current == Tool.Move)
        {
            Tools.current = Tool.None;
        }

        EditorGUI.BeginChangeCheck();
        Vector3 nextPosition = Handles.PositionHandle(streetPiece.transform.position, Quaternion.identity);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(streetPiece.transform, "Moved position.");
            Vector3 clampedPosition = new Vector3(
                ( (int)(nextPosition.x / xSnapDistance) ) * xSnapDistance,
                nextPosition.y,
                ((int)(nextPosition.z / zSnapDistance)) * zSnapDistance
                );
            streetPiece.transform.position = clampedPosition;
        }
    }
}
