namespace CryptoCube
{
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
}
