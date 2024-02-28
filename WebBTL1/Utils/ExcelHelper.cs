using ClosedXML.Excel;

namespace WebBTL1.Utils;

public abstract class ExcelHelper
{
    public static string GetValue(IXLWorksheet sheet, int row, int column)
    {
        return sheet.Cell(row, column).Value.ToString() ?? string.Empty;
    }
}