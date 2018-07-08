using System.Collections.Generic;

namespace CryptoCube
{
    internal class RotationSequence
    {
        private readonly IEnumerable<IRotation> rotations;

        public RotationSequence(IEnumerable<IRotation> rotations)
        {
            this.rotations = rotations;           
        }

        internal void ApplyRotationsInSequence(Vertice vertice)
        {
            foreach (var rotation in rotations)
            {
                rotation.Rotate(vertice);
            }
        }
    }
}
