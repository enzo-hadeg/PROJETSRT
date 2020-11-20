using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SousTitresProject
{
    public class FicSousTitre
    {
        public enum StatusLine { line, timeline, srtline, emptyLine };
        public List<string> LineOfFile;
        public List<SousTitre> allSub;
        public string path;

        public FicSousTitre()
        {
        }
        // Utilisation du streamreader afin de lire le fichier , le rediriger au ParseFile et apres ecrire les sous titre sur la console
        public void ReadFile(string path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    LineOfFile = new List<string>();

                    string line;

                    while ((line = sr.ReadLine()) != null)
                        LineOfFile.Add(line);

                    file();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        //Le parseFile va permettre d'analyser le fichier et ainsi nous permettre de filtrer les information a afficher 
        private void file()// on boucle le fichier pour un bloc cad : Line , Temps et et le sous titre mais une fois qu'on a une ligne vide ducoup on stock ce premier bloc pour reinitialiser tout les objet pour stocker le bloc suivant 
        {
            int nbSub = -1;
            string Timer = "";
            string Subs = "";
            StatusLine statusL = StatusLine.line;
            allSub = new List<SousTitre>();

            foreach (string line in LineOfFile)
            {
                if (line == "")
                {
                    statusL = StatusLine.emptyLine;
                }

                switch (statusL)
                {
                    case StatusLine.line:
                        nbSub = Int32.Parse(line);
                        statusL++;
                        break;
                    case StatusLine.timeline:
                        Timer = line;
                        statusL++;
                        break;
                    case StatusLine.srtline:
                        Subs += line + "\n";
                        break;
                    case StatusLine.emptyLine:
                        SousTitre sub = new SousTitre(nbSub, Timer, Subs);
                        if (!allSub.Contains(sub))
                            allSub.Add(sub);
                        statusL = StatusLine.line;
                        Subs = "";
                        break;
                }
            }
            SousTitre srtend = new SousTitre(nbSub, Timer, Subs);//quand on arrive au dernier bloc du doc srt il n'y a pas de ligne vide ducoup on doit le stocker en fin de boucle 
            if (!allSub.Contains(srtend))
                allSub.Add(srtend);
        }// Alors on a stocker toutes les information des sous-titre dans la classe "Sous-Titre"

        public void GetSubTitle()// redirection dans la classe Sous-titre des information 
        {
            foreach (SousTitre sub in allSub)
            {
                Task r = sub.addsrt();
            }
        }

    }
}
