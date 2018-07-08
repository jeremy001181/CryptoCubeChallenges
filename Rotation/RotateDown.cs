namespace CryptoCube
{
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
}
