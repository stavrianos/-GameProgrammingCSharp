using System;

namespace ProgrammingAssignment1
{
    class Program
    {
        /// <summary>
        ///     ProgrammingAssignment1 main
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome!\n");
            Console.WriteLine(
                "This application will calculate the distance between two points and the angle between those points\n");
            Console.WriteLine();
            Console.WriteLine("Please enter the X value of the first point: ");
            var x1 = Input();
            Console.WriteLine("Please enter the Y value of the first point: ");
            var y1 = Input();
            Console.WriteLine("Please enter the X value of the second point: ");
            var x2 = Input();
            Console.WriteLine("Please enter the Y value of the second point: ");
            var y2 = Input();
            //Calculate delta
            var deltaX = Math.Pow(x2 - x1, 2);
            var deltaY = Math.Pow(y2 - y1, 2);
            //Calculate distance
            var distance = Math.Sqrt(deltaX + deltaY);
            Console.WriteLine("Distance : {0:N3}", distance);
            //Calculate angle (degrees)
            var angle = (Math.Atan2(y2 - y1, x2 - x1)) * (180 / Math.PI);
            Console.WriteLine("Angle: {0:N3}", angle);
        }

        /// <summary>
        ///     Helper for the code.If user enters a wrong value then the program asks again for a proper value.
        /// </summary>
        /// <returns>The parsing float input.</returns>
        private static float Input()
        {
            var input = Console.ReadLine();
            var res = float.TryParse(input, out var t);
            if (res == false)
            {
                Console.WriteLine("Please mate enter a valid FLOAT value!");
                return Input();
            }

            return t;
        }
    }
}