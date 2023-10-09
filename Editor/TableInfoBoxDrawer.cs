using System;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using System.Reflection;

[DrawerPriority(DrawerPriorityLevel.SuperPriority)]
public class TableInfoBoxDrawer : OdinAttributeDrawer<TableInfoBoxAttribute>
{
    protected override void DrawPropertyLayout(GUIContent label)
    {
        var tableAttribute = this.Attribute;
        MethodInfo method = Property.ParentType.GetMethod(tableAttribute.MethodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        if (method == null)
        {
            SirenixEditorGUI.ErrorMessageBox($"No method named {tableAttribute.MethodName} found in {Property.ParentType.Name}");
            return;
        }

        var result = method.Invoke(Property.ParentValues.Count > 0 ? Property.ParentValues[0] : null, null);
        if(result is ValueTuple<string[], string[,]> tableData)
        {
            DrawTable(tableData.Item1, tableData.Item2, Attribute.cellHeight, Attribute.cellColor, Attribute.textColor, Attribute.headerColor, Attribute.headerTextColor, Attribute.fontStyle, Attribute.headerFontStyle);
        }

        this.CallNextDrawer(label);
    }
    private void DrawTable(string[] headers, string[,] rows, float cellHeight, Color cellColor, Color textColor, Color headerColor,  Color headerTextColor, FontStyle fontStyle, FontStyle headerFontStyle)
    {
        GUIStyle cellStyle = new GUIStyle(EditorStyles.label)
        {
            fontStyle = fontStyle,
            alignment = TextAnchor.MiddleCenter
        };
        GUIStyle headerStyle = new GUIStyle(EditorStyles.label)
        {
            fontStyle = headerFontStyle,
            alignment = TextAnchor.MiddleCenter
        };
        int numColumns = headers.Length;
        float spacing = 2f; // horizontal spacing between columns
        float vSpacing = 2f; // vertical spacing between rows
        float availableWidth = EditorGUIUtility.currentViewWidth - spacing * (numColumns - 1);
        float columnWidth = availableWidth / numColumns - 2f;
        cellHeight += vSpacing;

        // Header
        GUI.contentColor = headerTextColor; 
        Rect headerRect = GUILayoutUtility.GetRect(availableWidth, cellHeight - vSpacing); // уменьшаем на vSpacing для корректировки
        for (int i = 0; i < numColumns; i++)
        {
            Rect cellRect = new Rect(headerRect.x + i * (columnWidth + spacing), headerRect.y, columnWidth, cellHeight - vSpacing);
            EditorGUI.DrawRect(cellRect, headerColor);
            cellRect.x += 1; cellRect.width -= 2;
            EditorGUI.LabelField(cellRect, headers[i], headerStyle);
        }
        GUILayout.Space(vSpacing); 

        GUI.contentColor = textColor; 
        // Rows
        for (int i = 0; i < rows.GetLength(0); i++)
        {
            Rect rowRect = GUILayoutUtility.GetRect(availableWidth, cellHeight - vSpacing); // уменьшаем на vSpacing для корректировки
            for (int j = 0; j < rows.GetLength(1); j++)
            {
                Rect cellRect = new Rect(rowRect.x + j * (columnWidth + spacing), rowRect.y, columnWidth, cellHeight - vSpacing);
                EditorGUI.DrawRect(cellRect, cellColor);
                cellRect.x += 1; cellRect.width -= 2;
                EditorGUI.LabelField(cellRect, rows[i, j], cellStyle);
            }
            if (i != rows.GetLength(0) - 1)
            {
                GUILayout.Space(vSpacing);
            }
        }
        GUI.contentColor = Color.white; 
    }


}