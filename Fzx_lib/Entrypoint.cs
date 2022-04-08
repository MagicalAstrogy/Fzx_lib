using System;
using HarmonyLib;

namespace Fzx_lib
{
    public class Program
    {
        public Program()
        {
            Console.WriteLine("Hello World");
            var harmony = new Harmony("com.company.project.product");
            harmony.PatchAll();
        }
        public static void Main()
        {
            Console.WriteLine("Hello World");
        }
    }
}