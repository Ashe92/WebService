using StudentWebService.Console.Test.InitDataBase;

namespace StudentWebService.Console.Test
{
    class Program
    {
        static void Main(string[] args)
        { 
            InitMongoDb db = new InitMongoDb();
            db.CreateCollections();

        }
    }
}
