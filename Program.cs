using System;
using System.Linq;

namespace CryptoCube
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please select following operation [e]ncryption or [d]ecryption:");
            var typeKey = Console.ReadKey();
            var operationChoosen = typeKey.KeyChar == 'e' ? "encryption" : "decryption";

            Console.WriteLine($"\r\nPlease enter data for {operationChoosen}:");
            var data = Console.ReadLine();
            Console.WriteLine($"Please enter custom rotation script or hit enter to use system randomly generated one:");
            var rotationScript = Console.ReadLine();

            var rotationSet = ParseRotations(rotationScript, typeKey.KeyChar);

            EncryptAndPrint(data, rotationSet);
        }

        private static void EncryptAndPrint(string input, RotationSequence[] rotationSet)
        {
            var numberOfCubeRequired = input.Length / 8;            
            var buffer = new char[8];

            for (int i = 0; i < input.Length; i++)
            {
                var chr = input[i];
                var vertice = new Vertice(chr, i % 8);

                rotationSet[i / 8].ApplyRotationsInSequence(vertice);

                buffer[vertice.Position] = vertice.Data;
                
                if (i % 8 == 7)
                {
                    Console.Write(buffer);
                    buffer = new char[8];
                }
            }

            Console.Write(buffer.Where(c => c != default(char)).ToArray());
            Console.WriteLine();
        }

        private static RotationSequence[] ParseRotations(string rotationCommand, char type)
        {
            if (string.IsNullOrEmpty(rotationCommand))
            {

            }

            var rotations = rotationCommand
                .Split(",")
                .Select(r => new RotationSequence(r, type));

            return rotations.ToArray();
        }
    }

    internal class RotationSequence
    {
        private readonly IRotation[] rotations;

        public RotationSequence(string rotationScript, char operationType)
        {
            var commands = rotationScript.Split(":").Skip(1);

            if (operationType == 'd') {
                commands = commands.Reverse();
            }

            this.rotations = commands.Select(r =>
            {
                switch (r)
                {
                    case "U": return new RotateUp() as IRotation;
                    case "D": return new RotateDown() as IRotation;
                    case "L": return new RotateLeft() as IRotation;
                    case "R": return new RotateRight() as IRotation;
                    default:
                        throw new ArgumentException($"Invalid rotation found {r}");
                }
            }).ToArray();
        }

        internal void ApplyRotationsInSequence(Vertice vertice)
        {
            foreach (var rotation in rotations)
            {
                rotation.Rotate(vertice);
            }
        }


        internal void ApplyRotationsReversely(Vertice vertice)
        {
            foreach (var rotation in rotations)
            {
                rotation.Rotate(vertice);
            }
        }
    }

    internal interface IRotation
    {
        void Rotate(Vertice vertice);
    }

    class RotateUp : IRotation
    {
        public void Rotate(Vertice vertice)
        {
            var position = vertice.Position;

            if (position == 0 || position == 6)
                vertice.Position++;
            else if (position == 1 || position == 2)
                vertice.Position += 4;
            else if (position == 3 || position == 5)
                vertice.Position--;
            else if (position == 4 || position == 7)
                vertice.Position -= 4;
        }
    }

    class RotateDown : IRotation
    {
        public void Rotate(Vertice vertice)
        {
            var position = vertice.Position;

            if (position == 2 || position == 4)
                vertice.Position++;
            else if (position == 0 || position == 3)
                vertice.Position += 4;
            else if (position == 1 || position == 7)
                vertice.Position--;
            else if (position == 5 || position == 6)
                vertice.Position -= 4;

        }
    }

    class RotateLeft : IRotation
    {
        public void Rotate(Vertice vertice)
        {
            var position = vertice.Position;

            if (position == 5)
                vertice.Position++;
            else if (position == 4)
                vertice.Position += 3;
            else if (position == 3)
                vertice.Position -= 3;
            else if (position == 0 || position == 1)
                vertice.Position += 4;
            else if (position == 2)
                vertice.Position--;
            else if (position == 7 || position == 6)
                vertice.Position -= 4;

        }
    }

    class RotateRight : IRotation
    {
        public void Rotate(Vertice vertice)
        {
            var position = vertice.Position;

            if (position == 5 || position == 4)
                vertice.Position -= 4;
            else if (position == 7)
                vertice.Position -= 3;
            else if (position == 0)
                vertice.Position += 3;
            else if (position == 6)
                vertice.Position--;
            else if (position == 2 || position == 3)
                vertice.Position += 4;
            else if (position == 1)
                vertice.Position++;

        }
    }

    class Vertice
    {
        public Vertice(char data, int position)
        {
            this.Data = data;
            this.Position = position;
        }
        public char Data { get; set; }
        public int Position { get; set; }
    }
}
