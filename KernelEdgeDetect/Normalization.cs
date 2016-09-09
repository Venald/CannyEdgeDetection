using System;

namespace KernelEdgeDetect
{
    class Normalization
    {
        Double[,] items;
        Double min;
        Double max;

        public Normalization(int xSize, int ySize)
        {
            items = new Double[xSize, ySize];
            min = Double.MaxValue;
            max = Double.MinValue;
        }

        public void Add(int x, int y, Double item)
        {
            items[x, y] = item;
            if (item < min) min = item;
            if (item > max) max = item;
        }

        public Double GetMin()
        {
            return min;
        }

        public Double GetMax()
        {
            return max;
        }

        public int GetItem(int x, int y)
        {
            if (x < 0 || y < 0) return 0;
            if (x > items.GetLength(0) || y > items.GetLength(1)) return 0;
            return (int)items[x, y];

        }
        public void Normalize()
        {
            for (int x = 0; x < items.GetLength(0); x++)
            {
                for (int y = 0; y < items.GetLength(1); y++)
                {
                    double d = items[x, y];
                    items[x, y] = 255 + ((d - max) * (255) / (max - min));
                }
            }
        }

    }
}
