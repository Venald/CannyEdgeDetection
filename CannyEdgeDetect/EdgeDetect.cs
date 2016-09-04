using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace CannyEdgeDetect
{
    class EdgeDetect
    {
        static void Main(string[] args)
        {
            Image loaded_image = Image.FromFile("Test.png");
            Bitmap imageMap = new Bitmap(loaded_image);
            Bitmap newImage = new Bitmap(imageMap.Width, imageMap.Height);
            int K = 3;
            int[,] Gaussian = new int[3, 3] { {1,2,1 },
                                              {2,4,2 },
                                              {1,2,1 }};
            //  5 / 2 = Kernel size halfed
            for (int x = K / 2; x < imageMap.Width - K / 2; x++)
            {
                for (int y = K / 2; y < imageMap.Height - K / 2; y++)
                {
                    int sumR = 0;
                    int sumB = 0;
                    int sumG = 0;
                    int sumA = 0;
                    for (int row = -K / 2; row < K / 2; row++)
                    {
                        for (int element = -K / 2; element < K / 2; element++)
                        {
                            Color pixel = imageMap.GetPixel(x + row, y + element);
                            int coeff = Gaussian[row + K / 2, element + K / 2];
                            sumA = pixel.A;
                            sumR = pixel.R * coeff;
                            sumB = pixel.B * coeff;
                            sumG = pixel.G * coeff;


                        }
                    }
                    sumR /= 16;
                    sumB /= 16;
                    sumG /= 16;
                    if (sumR > 255) sumR = 255;
                    if (sumB > 255) sumB = 255;
                    if (sumG > 255) sumG = 255;
                    newImage.SetPixel(x, y, Color.FromArgb(sumA, sumR, sumG, sumB));
                }
            }
            newImage.Save("Result.png", ImageFormat.Png);
        }
    }
}
