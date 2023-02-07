using Microsoft.VisualBasic.FileIO;
using System;
using System.Runtime.InteropServices;

namespace Trails
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            FileStream file = File.Open("name.txt", FileMode.Create);
            using (BinaryWriter bw = new BinaryWriter(file))
            {
                bw.Write("Hello world");
                bw.Close();
            }

            Console.WriteLine("Code Run!");
            Thread.Sleep(5000);
            Console.WriteLine("Done!");
            file.Close();
        }
    }
}