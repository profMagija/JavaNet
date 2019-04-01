using JavaNet.Runtime.Plugs;

namespace java.lang
{
    [TypePlug]
    public interface CharSequence
    {
        char charAt(int index);
        int length();
        CharSequence subSequence(int start, int end);
        string ToString();
    }
}