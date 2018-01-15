using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingAssignment1
{
    class Program
    {
        /// <summary>
        /// ProgrammingAssignment1 main
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome!\n");
            Console.WriteLine("This application will calculate the distance between two points and the angle between those points\n");
            Console.WriteLine();
            Console.WriteLine("Please enter the X value of the first point: ");
            float x1 = Input();
            Console.WriteLine("Please enter the Y value of the first point: ");
            float y1 = Input();
            Console.WriteLine("Please enter the X value of the second point: ");
            float x2 = Input();
            Console.WriteLine("Please enter the Y value of the second point: ");
            float y2 = Input();
            //Calculate delta
            double deltaX = Math.Pow(x2 - x1, 2);
            double deltaY = Math.Pow(y2 - y1, 2);
            //Calculate distance
            double distance = Math.Sqrt(deltaX + deltaY);
            Console.WriteLine("Distance : {0}",distance.ToString("N3"));
            //Calculate angle (degrees)
            double angle = (Math.Atan2(y2 - y1, x2 - x1)) * (180 / Math.PI);
            Console.WriteLine("Angle: {0}",angle.ToString("N3"));
        }

        /// <summary>
        /// Helper for the code.If user enters a wrong value then the program asks again for a proper value.
        /// </summary>
        /// <returns>The parsing float input.</returns>
        private static float Input()
        {
            string input = Console.ReadLine();
            bool res = float.TryParse(input, out float t);
            if (res==false)
            {
                Console.WriteLine("Please mate enter a valid FLOAT value!");
                return Input();
            }
            return t;
        }
    }

}
