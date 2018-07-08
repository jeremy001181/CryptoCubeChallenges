﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CryptoCube
{
    class DecryptionScriptParser : IScriptParser
    {
        public IEnumerable<RotationSequence> Parse(string rotationScript)
        {
            return rotationScript
               .Split(",")
               .Select(g =>
               {
                   var rotations = g.Split(":").Skip(1).Reverse().Select(r =>
                   {
                       switch (r)
                       {
                           case "U": return new RotateDown() as IRotation;
                           case "D": return new RotateUp() as IRotation;
                           case "L": return new RotateRight() as IRotation;
                           case "R": return new RotateLeft() as IRotation;
                           default:
                               throw new ArgumentException($"Invalid rotation found {r}");
                       }
                   });
                   return new RotationSequence(rotations);
               });
        }
    }

    class EncryptionScriptParser : IScriptParser
    {
        public IEnumerable<RotationSequence> Parse(string rotationScript)
        {
            return rotationScript
               .Split(",")
               .Select(g =>
               {
                   var rotations = g.Split(":").Skip(1).Select(r =>
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
                   });
                   return new RotationSequence(rotations);
               });
        }
    }

    class Program
    {
        const int maxNumberOfRotation = 5;
        static char[] RotationDirection = new[] { 'U', 'D', 'L', 'R' };
        static void Main(string[] args)
        {
            Console.WriteLine("Please select following operation [e]ncryption or [d]ecryption:");
            var typeKey = Console.ReadKey();
            var isEncryption = typeKey.KeyChar == 'e';

            Console.WriteLine($"\r\nPlease enter data for {(isEncryption ? "encryption" : "decryption")}");
            var data = Console.ReadLine();
            Console.WriteLine($"Please enter custom rotation script or hit enter to use system randomly generated one:");
            var rotationScript = Console.ReadLine();
            if (string.IsNullOrEmpty(rotationScript))
            {
                rotationScript = GenerateRandomRotationScript(data.Length);
                Console.WriteLine($"System generated rotation script is:\r\n{rotationScript}");
            }
            var scriptParser = isEncryption 
                ? new EncryptionScriptParser() 
                : new DecryptionScriptParser() as IScriptParser;
            var rotationSet = scriptParser.Parse(rotationScript);

            EncryptAndPrint(data, rotationSet.ToArray());

            Console.ReadKey();
        }

        private static string GenerateRandomRotationScript(int length)
        {
            var rand = new Random();
            var scripts = new string[(int)Math.Ceiling(length / 8m)];
            for (var i = 0; i < scripts.Length; i++)
            {
                // Generate rotation sequence for each cube
                var countOfRotation = rand.Next(1, maxNumberOfRotation);
                var rotationSequence = new char[countOfRotation];
                for (var j = 0; j < countOfRotation; j++)
                {
                    rotationSequence[j] = RotationDirection[rand.Next(0, 3)];
                }

                scripts[i] = string.Concat(i, ':', string.Join(':', rotationSequence));
            }

            return string.Join(',', scripts);
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

    }

    internal class RotationSequence
    {
        private readonly IEnumerable<IRotation> rotations;

        public RotationSequence(IEnumerable<IRotation> rotations)
        {
            this.rotations = rotations;
            //var commands = rotationScript.Split(":").Skip(1);
            //var isDecryption = operationType == 'd';
            //if (isDecryption) {
            //    commands = commands.Reverse();
            //}

            //this.rotations = commands.Select(r =>
            //{
            //    switch (r)
            //    {
            //        case "U": return isDecryption? new RotateDown() : new RotateUp() as IRotation;
            //        case "D": return isDecryption ? new RotateUp(): new RotateDown() as IRotation;
            //        case "L": return isDecryption ? new RotateRight():  new RotateLeft() as IRotation;
            //        case "R": return isDecryption ? new RotateLeft() : new RotateRight() as IRotation;
            //        default:
            //            throw new ArgumentException($"Invalid rotation found {r}");
            //    }
            //}).ToArray();
        }

        internal void ApplyRotationsInSequence(Vertice vertice)
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
