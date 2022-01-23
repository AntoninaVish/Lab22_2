using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab22_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите размер массива:");
            int n = Convert.ToInt32(Console.ReadLine());//ввод запрашиваем у пользователя
            Func<object, int[]> func1 = new Func<object, int[]>(GetArray);
            Task<int[]> task1 = new Task<int[]>(func1, n);//ожидаем получить массив, аргументом передаем делегат

            Action<Task<int[]>> action = new Action<Task<int[]>>(SummArray);//вычисляем сумму чисел массиваи на вход принимает исходный массив
            Task task2 = task1.ContinueWith(action);

            Action<Task<int[]>> action1 = new Action<Task<int[]>>(MaxArray);//выводим максимальное число в массиве
            Task task3 = task1.ContinueWith(action1);

            task1.Start();
            Console.ReadKey();

        }
        static int[] GetArray(object a)// первый метод который формирует массив
        {
            int n = (int)a;
            int[] array = new int[n];// создаем пустой массив = новый массив расмерностью n
            Random random = new Random();//заполняем массив случайными числами
            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(0, 10);

                Console.Write($"{array[i]} ");
               
            }
            Console.WriteLine();
            return array;//возвращаем
        }
        static void SummArray(Task<int[]> task)// метод вычисляет сумму 
        {
            int[] array = task.Result;
            int S = 0;
            for (int i = 0; i < array.Count(); i++)
            {
                S += array[i];

            }
            Console.WriteLine($"сумма чисел в массиве: {S }");

        }
        static void MaxArray(Task<int[]> task)// метод выводит максимальное число в массиве
        {
            int[] array = task.Result;
            int Max = array[0];
            
            foreach (int a in array)
            {
                if (a > Max)
                    Max = a;
            }
            Console.WriteLine("максимальное число в массиве: {0}", Max);
        }
    }

}
