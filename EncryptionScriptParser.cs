using System;
using System.Collections.Generic;
using System.Linq;

namespace CryptoCube
{
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
}
