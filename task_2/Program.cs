using System;
using System.IO;

namespace task_2
{
    class WorkWithFiles
    {
        static void Main()
        {
            Console.Write("Введите путь к директориии: ");
            string folder = Console.ReadLine();
            double catalogSize = 0;
            double catalogSizeTrans = SizeOfFolder(folder, ref catalogSize);
            if (catalogSizeTrans >= 0)
            {
                Console.WriteLine($"Объем занятого места на диске {folder} (в байтах) - {catalogSize} байт." +
                              $"{Environment.NewLine}");
                Console.WriteLine($"Объем занятого места на диске {folder} (в Гигабайтах) - {catalogSizeTrans:f0} ГБ.");
            }

            Console.ReadKey();
        }
        static double SizeOfFolder(string folder, ref double catalogSize)
        {
            try
            {
                DirectoryInfo directory = new DirectoryInfo(folder);
                DirectoryInfo[] directoryArr = directory.GetDirectories();
                FileInfo[] filesArr = directory.GetFiles();

                foreach (FileInfo files in filesArr)
                {
                    catalogSize = catalogSize + files.Length;
                }

                foreach (DirectoryInfo dirIter in directoryArr)
                {
                    SizeOfFolder(dirIter.FullName, ref catalogSize);
                }

                return Math.Round((double)(catalogSize / 1024 / 1024 / 1024), 1);
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine($"Ошибка: Директория не найдена - {e.Message}{Environment.NewLine}");
                return -1;
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine($"Ошибка: Нет доступа - {e.Message}{Environment.NewLine}");
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}{Environment.NewLine}");
                return 0;
            }
        }
    }
}
