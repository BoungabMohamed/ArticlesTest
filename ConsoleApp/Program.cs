namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IList<int> list = new List<int> { 1, 2, 3, 4 , 5 , 6 , 7 , 8 , 9 , 10};
            var t = list.SkipWhile(x => x < 4 || x > 8);

            foreach (var item in t)
            {
                Console.WriteLine(item);
            }

            
        }
    }
}
