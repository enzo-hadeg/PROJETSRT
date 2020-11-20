
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SousTitresProject
{
    public class SousTitre
    {
        public int NombreSousTitres;
        public TimeSpan debut;
        public TimeSpan Fin;
        public string SousTitres;

        public SousTitre(int number, string timer, string sub)
        {
            NombreSousTitres = number;
            SousTitres = sub;
            ParseTimer(timer);
        }

        private void ParseTimer(string timer)// permet de recup la date de début et la date de fin de chaque sous-titre a partir du doc srt 
        {
            Regex r = new Regex(@"\d{2}:\d{2}:\d{1,2},\d{3} ?--> ?\d{2}:\d{2}:\d{1,2},\d{3}");
            // il faut verifier que la ligne en question respecte le format de date donc on fait un split de la ligne en 2 partie , date de débt et de fin 
            if (r.IsMatch(timer))
            {
                string[] splitOn = { "-->", " --> " };
                string[] timersplit = timer.Split(splitOn, StringSplitOptions.None);
                debut = Timer(timersplit[0]);
                Fin = Timer(timersplit[1]);
            }
        }

        private TimeSpan Timer(string timer) //timespan va nous permettre d'avoir un interval de temps afin de savoir quand le message va être afficher 
        {
            char[] charsplitOn = { ':', ',' };
            string[] splittimer = timer.Split(charsplitOn);

            return new TimeSpan(0, Int32.Parse(splittimer[0]), Int32.Parse(splittimer[1]), Int32.Parse(splittimer[2]), Int32.Parse(splittimer[3]));
        }
        //L'async va tout sinplement avec Task nous donner l'ordre , Ajouter , afficher et retirer pour chaque sous-titre
        public async Task addsrt()
        {
            await TimeToAdd();
            Console.WriteLine(SousTitres);
            await TimeToDisplay();
            Console.Clear();
        }

        public async Task TimeToAdd()
        {
            await Task.Delay(debut);
        }

        public async Task TimeToDisplay()
        {
            await Task.Delay(Fin - debut);
        }

        public async Task TimeToRemove()
        {
            await Task.Delay(Fin);
        }

    }
}