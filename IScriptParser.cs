using System.Collections.Generic;

namespace CryptoCube
{
    internal interface IScriptParser
    {
        IEnumerable<RotationSequence> Parse(string rotationScript);
    }
}