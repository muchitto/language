namespace ErrorReporting;

public record struct PositionData(string Filename, string SourceCode, int From = 0, int To = 0)
{
    public static PositionData Test(string sourceCode = "")
    {
        return new PositionData("test", sourceCode);
    }

    public (int Line, int ColumnFrom, int ColumnTo) GetLineAndColumn()
    {
        var line = 1;
        var columnFrom = 1;
        var columnTo = 1;

        for (var i = 0; i < To; i++)
        {
            if (SourceCode[i] == '\n')
            {
                line++;
                columnFrom = 1;
                columnTo = 1;
            }
            else
            {
                if (i < From)
                {
                    columnFrom++;
                }

                columnTo++;
            }
        }

        return (line, columnFrom, columnTo);
    }

    public string GetErrorLine()
    {
        // Get the line where the error occurred from the end of the last line to the end of the line
        var startIndex = SourceCode.LastIndexOf('\n', From) + 1;
        var length = SourceCode.IndexOf('\n', startIndex) - startIndex;
        var line = SourceCode.Substring(startIndex, length);


        return line;
    }

    public string GetErrorArrow(string line)
    {
        var (_, columnFrom, columnTo) = GetLineAndColumn();
        var pointerLines = new string('-', columnFrom - 1);
        var arrow = pointerLines + new string('^', columnTo - columnFrom + 1);

        return arrow;
    }

    public string GetErrorSnippet(string line)
    {
        var arrow = GetErrorArrow(line);

        return $"{line}\n{arrow}";
    }

    public string GetFullErrorMessage(string errorMessage)
    {
        var lineStr = GetErrorLine();
        var snippet = GetErrorSnippet(lineStr);
        var (line, columnFrom, columnTo) = GetLineAndColumn();

        return $"In file {Filename}, on line {line} on column {columnFrom}:\n\t{errorMessage}\n\n{snippet}";
    }
}