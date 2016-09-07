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
            Bitmap newImage = new Bitmap(loaded_image);
            int K = 3; //kernel size
            int[,] edgeDetectKernel = new int[3, 3] { { -1, -1, -1 },
                                                     {  -1, -8, -1 },
                                                     {  -1, -1, -1 }};
            //  Kernel size halved for x and y
            for (int x = K / 2; x < imageMap.Width - K / 2; x++)
            {
                for (int y = K / 2; y < imageMap.Height - K / 2; y++)
                {
                    int sumR = 0;
                    int sumB = 0;
                    int sumG = 0;

                    // Should go thru the kernel and x
                    for (int row = -K / 2; row < K / 2; row++)
                    {
                        for (int element = -K / 2; element < K / 2; element++)
                        {
                            Color pixel = imageMap.GetPixel(x + row, y + element);
                            int coeff = edgeDetectKernel[row + K / 2, element + K / 2];
                            sumR += pixel.R * coeff;
                            sumB += pixel.B * coeff;
                            sumG += pixel.G * coeff;


                        }
                    }
                    //After convolution the sums aren't always in [0,255]
                    if (sumR < 0) sumR *= -1;
                    if (sumG < 0) sumG *= -1;
                    if (sumB < 0) sumB *= -1;
                    sumR /= 16;
                    sumG /= 16;
                    sumB /= 16;
                    if (sumR > 255) sumR = 255;
                    if (sumB > 255) sumB = 255;
                    if (sumG > 255) sumG = 255;
                    //No processing in alpha channel should there be?
                    int sumA = imageMap.GetPixel(x, y).A;
                    newImage.SetPixel(x, y, Color.FromArgb(sumA, sumR, sumG, sumB));
                }
            }
            newImage.Save("Result.png", ImageFormat.Png);
        }
    }
}
