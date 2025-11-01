# Poetizer.cs
Mobile-API for [Poetizer](https://www.poetizer.com/) creative social networking &amp; community app for reading, writing poems &amp; haiku!

```cs
using PoetizerApi;

namespace Application
{
    internal class Program
    {
        static async Task Main()
        {
            var api = new Poetizer();
            string newestPoems = await api.GetNewestPoems();
            Console.WriteLine(newestPoems);
        }
    }
}
```
