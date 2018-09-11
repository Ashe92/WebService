using System;

namespace StudentWebServiceConsole.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                InitMongoDb db = new InitMongoDb();

                db.CreateCollections();
                Console.WriteLine("done");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}, \n\r {ex.StackTrace}");
            }

            Console.Read();
        }
    }
}
