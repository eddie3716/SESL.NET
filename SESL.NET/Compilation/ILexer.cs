namespace SESL.NET.Compilation
{
    public interface ILexer
    {
        bool Next();

        Token GetToken();
    }
}
