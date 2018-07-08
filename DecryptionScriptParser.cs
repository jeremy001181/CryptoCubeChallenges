using System;
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
}
