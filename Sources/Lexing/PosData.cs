namespace Lexing;

public record struct PosData(string Filename, string SourceCode, int From = 0, int To = 0)
{
    public bool IsEnd => From >= SourceCode.Length;

    public char GetChar()
    {
        var chr = SourceCode[From];

        From++;

        return chr;
    }

    public char PeekChar(int offset = 0)
    {
        if (IsEnd)
        {
            return '\0';
        }

        var chr = SourceCode[From + offset];

        return chr;
    }

    public bool IsNext(string str, bool eat = false)
    {
        if (IsEnd)
        {
            return false;
        }

        if (From + str.Length > SourceCode.Length)
        {
            return false;
        }

        var next = SourceCode.Substring(From, str.Length);

        if (next == str)
        {
            if (eat)
            {
                From += str.Length;
            }

            return true;
        }

        return false;
    }

    public string GetWhile(Func<char, int, string, bool> predicate)
    {
        var str = "";
        var i = 0;

        while (!IsEnd && predicate(PeekChar(), i, str))
        {
            str += GetChar();
            i++;
        }

        return str;
    }

    public string GetUntil(Func<char, bool> predicate)
    {
        var str = "";

        while (!IsEnd && !predicate(PeekChar()))
        {
            str += GetChar();
        }

        return str;
    }

    public string GetUntil(string str, bool eat = false)
    {
        var str2 = "";

        while (!IsEnd && !IsNext(str))
        {
            str2 += GetChar();
        }

        if (eat)
        {
            From += str.Length;
        }

        return str2;
    }

    public string PeekUntil(char chr)
    {
        var str = "";

        var i = 0;

        while (!IsEnd && PeekChar(i) != chr)
        {
            str += PeekChar(i);

            i++;
        }

        return str;
    }

    public string PeekUntil(string str)
    {
        var str2 = "";

        var i = 0;

        while (!IsEnd && !IsNext(str))
        {
            str2 += PeekChar(i);

            i++;
        }

        return str2;
    }

    public string PeekUntil(Func<char, bool> predicate)
    {
        var str = "";

        var i = 0;

        while (!IsEnd && !predicate(PeekChar(i)))
        {
            str += PeekChar(i);

            i++;
        }

        return str;
    }
}