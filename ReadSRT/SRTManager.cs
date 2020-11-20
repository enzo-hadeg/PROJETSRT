using ReadSRT.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ReadSRT
{
    public class SRTManager
    {
        //
        public IList<SRT> getListSTR(string filePath)
        {
            var result = new List<SRT>();
            string fileText = File.ReadAllText(@filePath, System.Text.Encoding.UTF8);
            string strRegex = @"(?:\r?\n)*\d+\r?\n\d{2}:\d{2}:\d{2},\d{3} --> \d{2}:\d{2}:\d{2},\d{3}\r?\n(?<text>(.|[\r\n])+?(?=\r\n\r\n|\z))";
            Regex myRegex = new Regex(strRegex, RegexOptions.Multiline);

            var matches = myRegex.Matches(fileText);
            foreach (Match myMatch in matches)
            {
                if (myMatch.Success)
                {
                    result.Add(getSRTFromText(myMatch.Value));
                }
            }

            return result;
        }

        private SRT getSRTFromText(string input)
        {
            SRT result = new SRT();


            using (StringReader reader = new StringReader(input))
            {
                string line;
                for(int i=0; i<5 && ((line = reader.ReadLine()) != null); i++)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        i--;
                    else
                    {
                        switch (i)
                    {
                        case 0: result.SRTId = int.Parse(line);
                            break;
                        case 1:
                            var splitedTime = line.Split(new string[] { "-->" }, StringSplitOptions.None);
                            result.StartDate = DateTime.ParseExact(splitedTime[0].Trim(), "HH:mm:ss,fff",
                                      CultureInfo.InvariantCulture);
                            result.EndDate = DateTime.ParseExact(splitedTime[1].Trim(), "HH:mm:ss,fff",
                                      CultureInfo.InvariantCulture);
                            break;
                        default:
                            result.Translation += line;
                            break;
                    }
                    }
                    
                }
            }
            result.SetInterval();
            return result;
        }

   

    }
}
