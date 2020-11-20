using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SousTitresProject
{
    class Program
    {
        static void Main(string[] args)
        {
            FicSousTitre f = new FicSousTitre();

            f.ReadFile(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Interstellar.txt");


            f.GetSubTitle();

            Console.Read();
        }
    }
}
