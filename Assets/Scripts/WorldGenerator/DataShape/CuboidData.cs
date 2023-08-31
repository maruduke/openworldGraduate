namespace Shape.DataShape
{
    [System.Serializable]
    public class CuboidData<T>
    {
        private T[,,] data;
        public int width, height, depth;

        public CuboidData(int width, int height, int depth)
        {
            this.width = width;
            this.height = height;
            this.depth = depth;

            this.data = new T[this.width, this.height, this.depth];
        }

        public void Fill(T value)
        {
            for (int x = 0; x < this.width; x++)
            for (int y = 0; y < this.height; y++)
            for (int z = 0; z < this.depth; z++)
                this.Set(value, x, y, z);
        }

        public void FillHeightTo(T value, int maxY, int x, int z)
        {
            if (this.height < maxY || maxY <= 0)
                return;

            for (int y = 0; y < maxY; y++)
                this.Set(value, x, y, z);
        }

        public T Get(int x, int y, int z)
        {
            return this.data[x, y, z];
        }

        public void Set(T value, int x, int y, int z)
        {
            this.data[x, y, z] = value;
        }
    }
}
