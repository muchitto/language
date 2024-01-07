using ErrorReporting;

namespace Lexing;

public static class PositionDataExtensions
{
    public static bool IsEnd(this ref PositionData positionData)
    {
        return positionData.From >= positionData.SourceCode.Length;
    }

    public static char GetChar(this ref PositionData positionData)
    {
        var chr = positionData.SourceCode[positionData.From];

        positionData.From++;

        return chr;
    }

    public static char PeekChar(this ref PositionData positionData, int offset = 0)
    {
        if (positionData.IsEnd())
        {
            return '\0';
        }

        var chr = positionData.SourceCode[positionData.From + offset];

        return chr;
    }

    public static bool IsNext(this PositionData positionData, string str, bool eat = false)
    {
        if (positionData.IsEnd())
        {
            return false;
        }

        if (positionData.From + str.Length > positionData.SourceCode.Length)
        {
            return false;
        }

        var next = positionData.SourceCode.Substring(positionData.From, str.Length);

        if (next == str)
        {
            if (eat)
            {
                positionData.From += str.Length;
            }

            return true;
        }

        return false;
    }

    public static string GetWhile(this ref PositionData positionData, Func<char, int, string, bool> predicate)
    {
        var str = "";
        var i = 0;

        while (!positionData.IsEnd() && predicate(positionData.PeekChar(), i, str))
        {
            str += positionData.GetChar();
            i++;
        }

        return str;
    }

    public static string GetUntil(this PositionData positionData, Func<char, bool> predicate)
    {
        var str = "";

        while (!positionData.IsEnd() && !predicate(positionData.PeekChar()))
        {
            str += positionData.GetChar();
        }

        return str;
    }

    public static string GetUntil(this ref PositionData positionData, char chr, bool eat = false)
    {
        var str = "";

        while (!positionData.IsEnd() && positionData.PeekChar() != chr)
        {
            str += positionData.GetChar();
        }

        if (eat)
        {
            positionData.From++;
        }

        return str;
    }

    public static string GetUntil(this ref PositionData positionData, string str, bool eat = false)
    {
        var str2 = "";

        while (!positionData.IsEnd() && !positionData.IsNext(str))
        {
            str2 += positionData.GetChar();
        }

        if (eat)
        {
            positionData.From += str.Length;
        }

        return str2;
    }
}