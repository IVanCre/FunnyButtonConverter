using System.IO;

namespace ButtonConverter
{
    class FileLogic
    {
        public static string RealAppPath(string fileName)//возвращает реальное место расположение ТЕКУЩЕЙ исполняемой сборки приложения
        {
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            path += @"\" + fileName;
            return path;
        }

        //определяет путь к папке, где должен находиться endfileName
        //на основани того, что endfileName должен храниться в одной и той же папке с fulladress файлом
        public static string FindGeneralPath(string fulladress, string endfileName)
        {
            string systPath = "";
            string[] split = fulladress.Split(new char[] { '\\' });
            for (int f = 0; f < split.Length - 1; f++)
            {
                if (f > 0)
                    systPath = string.Concat(systPath, @"\" + split[f]);
                else
                    systPath = string.Concat(systPath, split[f]);
            }
            return systPath += @"\"+ endfileName; 
        }

    }
}
