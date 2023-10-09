# TableInfoBox Attribute for Odin Inspector

**TableInfoBox** is a custom attribute for [Odin Inspector](https://odininspector.com/) within Unity that allows for the effortless display of tabular data directly within the inspector.

![Sample of TableInfoBox in action](https://github.com/SergeevSergey99/TableInfoBox/blob/main/tablepng.png)

## Features
- Displays tabular data with headers and rows.
- Customizable styling for cells and headers.
- Intuitive interface for adding tables to the inspector.

## Installation

1. Ensure you have [Odin Inspector](https://odininspector.com/) installed.
2. Copy and import the attribute files into your Unity project.
3. Ready to use!

## Usage

To begin, define a method in your class or ScriptableObject that will return the headers and rows for your table:

```csharp
public (string[], string[,]) GetTableData()
{
    // Your code returning the table data
}
```

Now, simply apply the TableInfoBox attribute to any property or field:
```csharp
[TableInfoBox("GetTableData")]
public AnimationCurve Curve;
```

## Configuration
TableInfoBox offers several parameters for display customization:

cellHeight: Height of the cell.
cellColorHex: Background color for the cells.
textColorHex: Text color for the cells.
headerColorHex: Background color for the headers.
headerTextColor: Text color for the header cells.
fontStyle: Font style for cell text.
headerFontStyle: Font style for header cell text.

## My Example
```csharp
[CreateAssetMenu(fileName = "StatCurves", menuName = "ScriptableObjects/StatCurves", order = 1)]
public class StatCurves : ScriptableObject
{
    [TableInfoBox("GetTableData", 
        25, 
        "#DDFFFF",
        "#111111",
        "BBBBBB",
        "000000")]
    public AnimationCurve Curve;
    
    public int GetValue(int level)
    {
        return Mathf.RoundToInt(Curve.Evaluate(level));
    }
    public int GetBonus(int level)
    {
        return Mathf.FloorToInt((GetValue(level) - 10) / 2f);
    }
    
    public (string[], string[,]) GetTableData()
    {
        List<string> headers = new();
        List<string> values = new();
        List<string> bonus = new();
        headers.Add("Level");
        values.Add("Stat");    
        bonus.Add("Bonus");
        for (int i = Stats.MIN_LEVEL; i <= Stats.MAX_LEVEL; i++)
        {
            headers.Add(i.ToString());
            values.Add(GetValue(i).ToString());
            bonus.Add(GetBonus(i).ToString());
            
        }
        string[,] rows = new string[2,values.Count];
        for (int i = 0; i < values.Count; i++)
        {
            rows[0, i] = values[i];
            rows[1, i] = bonus[i];
            if (i > 0)
                rows[1, i] = "+" + rows[1, i];
        }
            
        return (headers.ToArray(), rows);
    }

}
```

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
