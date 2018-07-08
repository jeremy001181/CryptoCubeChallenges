namespace CryptoCube
{
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
}
