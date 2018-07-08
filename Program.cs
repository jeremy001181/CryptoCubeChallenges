using System;
using System.Linq;

namespace CryptoCube
{

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
}
