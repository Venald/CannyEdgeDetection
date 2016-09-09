using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

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
            int[,] edgeDetectKernelX = new int[3, 3] { { -1, 0, 1 },
                                                     {  -2,  0, 2 },
                                                     {  -1, 0,  1 }};
            int[,] edgeDetectKernelY = new int[3, 3] { { -1, -2, -1 },
                                                     {    0,  0,  0 },
                                                     {    1,  2,  1 }};
            
            //  Kernel size halved for x and y
            for (int x = K / 2; x < imageMap.Width - K / 2; x++)
            {
                for (int y = K / 2; y < imageMap.Height - K / 2; y++)
                {
                    double sumXColor = 0;
                    double sumYColor = 0;
                    
                    // Should go thru the kernel and x
                    for (int row = -K / 2; row < K / 2; row++)
                    {
                        for (int element = -K / 2; element < K / 2; element++)
                        {
                            Color pixel = imageMap.GetPixel(x + row, y + element);
                            int coeffX = edgeDetectKernelX[row + K / 2, element + K / 2];
                            int coeffY = edgeDetectKernelY[row + K / 2, element + K / 2];

                            sumXColor = (pixel.R + pixel.G + pixel.B) / 3.0 * coeffX;
                            sumYColor = (pixel.R + pixel.G + pixel.B) / 3.0 * coeffX;


                        }
                    }


                    double colorSum = Math.Sqrt((sumXColor * sumXColor) + (sumYColor * sumYColor));

                    //After convolution the sums aren't always in [0,255]
                    
                    //  if (sumR < 0) sumR *= -1;
                    //  if (sumG < 0) sumG *= -1;
                    //  if (sumB < 0) sumB *= -1;
                    /*sumR *= 1/3;
                    sumG *= 1/3;
                    sumB *= 1/3;*/
                    

                    //Console.WriteLine(sumR + "\n" + sumG + "\n" + sumB + "\n" );

                    if (colorSum > 255) colorSum = 255;
                    
                    //No processing in alpha channel should there be?
                    int sumA = imageMap.GetPixel(x, y).A;
                    newImage.SetPixel(x, y, Color.FromArgb(sumA, (int)colorSum, (int)colorSum, (int)colorSum));
                }
            }
            newImage.Save("Result.png", ImageFormat.Png);
        }
    }
}
