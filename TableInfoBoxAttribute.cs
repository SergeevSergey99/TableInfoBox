using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public class TableInfoBoxAttribute : Attribute
{
    public string MethodName { get; set; }
    public readonly float cellHeight;
    public readonly Color cellColor;
    public readonly Color textColor;
    public readonly Color headerColor;
    public readonly Color headerTextColor;
    public readonly FontStyle fontStyle;
    public readonly FontStyle headerFontStyle;

    public TableInfoBoxAttribute(string methodName, float cellHeight = 20f, 
        string cellColorHex = "#FFFFFF", 
        string textColorHex = "#000000", 
        string headerColorHex = "#AAAAAA", 
        string headerTextColorHex = "#000000", 
        FontStyle fontStyle = FontStyle.Normal,
        FontStyle headerFontStyle = FontStyle.Bold)
    {
        MethodName = methodName;
        this.cellHeight = cellHeight;
        this.cellColor = ColorUtility.TryParseHtmlString(cellColorHex, out var cellCol) ? cellCol : Color.white;
        this.textColor = ColorUtility.TryParseHtmlString(textColorHex, out var textCol) ? textCol : Color.black;
        this.headerColor = ColorUtility.TryParseHtmlString(headerColorHex, out var headCol) ? headCol : Color.gray;
        this.headerTextColor = ColorUtility.TryParseHtmlString(headerTextColorHex, out var headerTextCol) ? headerTextCol : Color.black;
        this.fontStyle = fontStyle;
        this.headerFontStyle = headerFontStyle;
    }
}