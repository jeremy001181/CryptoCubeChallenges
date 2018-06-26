using System;
using System.Collections.Generic;
using System.Linq;

namespace CryptoCube
{
    class Program
    {
        static void Main(string[] args)
        {
            var encrypted = Encrypt(args[0], rotation);

            Console.Write(encrypted);
        }

        private static string Encrypt(string input, string rotation)
        {
            var numberOfCubeRequired = input.Length / 8;
            var cubes = new List<Cube>();

            for (int i = 0; i < numberOfCubeRequired; i++)
            {
                var vertices = input.Skip(i * 8).Take(8).Select((c, p) => new Vertice(p, c));
                var cube = new Cube(vertices);

                cube.Rotate();
                cube.Print();
                cubes.Add(cube);
            }
        }
    }

    class Cube
    {
        private readonly IEnumerable<Vertice> vertices;
        
        public Cube(IEnumerable<Vertice> vertices)
        {
            this.vertices = vertices;
        }

        private Stack<string> Rotation { get; set; }

        public void Rotate()
        {
            foreach(var rotation in Rotation)
            {
                rotator.Rotate(vertices);
            }
        }

        public void Print()
        {

        }
    }

    class Vertice
    {
        private readonly char data;

        public Vertice(int position, char data)
        {
            Position = position;
            this.data = data;
        }

        public int Position { get; private set; }
        public void Up() {
            if (Position == 0 || Position == 6)
                Position++;
            else if (Position == 1 || Position == 2)
                Position += 4;
            else if (Position == 3 || Position == 5)
                Position--;
            else if (Position == 4 || Position == 7)
                Position -= 4;
        }
        public void Down()
        {
            if (Position == 2 || Position == 4)
                Position++;
            else if (Position == 0 || Position == 3)
                Position += 4;
            else if (Position == 1 || Position == 7)
                Position--;
            else if (Position == 5 || Position == 6)
                Position -= 4;
        }

        public void Left()
        {
            if (Position == 5)
                Position++;
            else if (Position == 4)
                Position += 3;
            else if (Position == 3)
                Position -= 3 ;
            else if (Position == 0 || Position == 1)
                Position += 4;
            else if (Position == 2)
                Position--;
            else if (Position == 7 || Position == 6)
                Position -= 4;
        }

        public void Right()
        {
            if (Position == 5 || Position == 4)
                Position -= 4;
            else if (Position == 7)
                Position -= 3;
            else if (Position == 0)
                Position += 3;
            else if (Position == 6)
                Position--;
            else if (Position == 2 || Position == 3)
                Position += 4;
            else if (Position == 1)
                Position++;
        }
    }
}
