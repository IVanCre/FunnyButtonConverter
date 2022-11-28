using System;



namespace ButtonConverter
{
    class Program
	{



        [STAThread]
        static void Main(string[] arg)//если запуск идет с аргументом, значит естьу управленец и он вызывает запуск
        {
            if (arg.Length > 0)
                BehaviorLogic.StartLikeCopy();
            else
                BehaviorLogic.CheckBehavior();
        }

    }
}