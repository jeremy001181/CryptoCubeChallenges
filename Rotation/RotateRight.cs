namespace CryptoCube
{
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
}
