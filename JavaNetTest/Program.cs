using java.io;
using java.util;

namespace JavaNetTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = new Scanner(java.lang.System.@in);
            var name = input.nextLine().trim();
            java.lang.System.@out.printf("Hello %s!", name);
        }
    }
}
