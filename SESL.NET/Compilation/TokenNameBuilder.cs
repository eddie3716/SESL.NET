using System.Text;

namespace SESL.NET.Compilation;

public class TokenNameBuilder
{
    private readonly StringBuilder _innerStringBuilder = new();

    public int Length => _innerStringBuilder.Length;


    public string GetTokenName(bool keepCase = false)
    {
        return keepCase ? ToString() : ToString().ToLower();
    }

    public override string ToString()
    {
        return _innerStringBuilder.ToString();
    }

    public void Append(char c)
    {
        _innerStringBuilder.Append(c);
    }

    public void Remove(int startIndex, int length)
    {
        _innerStringBuilder.Remove(startIndex, length);
    }
}