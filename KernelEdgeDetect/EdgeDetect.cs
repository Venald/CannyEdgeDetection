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
                    int sumRx = 0;
                    int sumBx = 0;
                    int sumGx = 0;

                    int sumRy = 0;
                    int sumBy = 0;
                    int sumGy = 0;
                    // Should go thru the kernel and x
                    for (int row = -K / 2; row < K / 2; row++)
                    {
                        for (int element = -K / 2; element < K / 2; element++)
                        {
                            Color pixel = imageMap.GetPixel(x + row, y + element);
                            int coeffX = edgeDetectKernelX[row + K / 2, element + K / 2];
                            int coeffY = edgeDetectKernelY[row + K / 2, element + K / 2];

                            sumRx += pixel.R * coeffX;
                            sumBx += pixel.B * coeffX;
                            sumGx += pixel.G * coeffX;
                            sumRy += pixel.R * coeffY;
                            sumBy += pixel.B * coeffY;
                            sumGy += pixel.G * coeffY;


                        }
                    }



                    double sumR = Math.Sqrt((sumRx * sumRx) + (sumRy * sumRy));
                    double sumG = Math.Sqrt((sumGx * sumGx) + (sumGy * sumGy));
                    double sumB = Math.Sign((sumBx * sumBx) + (sumBy * sumBy));

                    //After convolution the sums aren't always in [0,255]
                    
                    //  if (sumR < 0) sumR *= -1;
                    //  if (sumG < 0) sumG *= -1;
                    //  if (sumB < 0) sumB *= -1;
                    /*sumR *= 1/3;
                    sumG *= 1/3;
                    sumB *= 1/3;*/
                    

                    //Console.WriteLine(sumR + "\n" + sumG + "\n" + sumB + "\n" );

                    if (sumR > 255) sumR = 255;
                    if (sumB > 255) sumB = 255;
                    if (sumG > 255) sumG = 255;
                    
                    //No processing in alpha channel should there be?
                    int sumA = imageMap.GetPixel(x, y).A;
                    newImage.SetPixel(x, y, Color.FromArgb(sumA, (int)sumR, (int)sumG, (int)sumB));
                }
            }
            newImage.Save("Result.png", ImageFormat.Png);
        }
    }
}
