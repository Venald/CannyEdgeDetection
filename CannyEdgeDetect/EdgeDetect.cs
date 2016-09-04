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

            int[,] Gaussian = new int[5, 5] { { 2,  4,  5, 4, 2 } ,
                                              { 4,  9, 12, 9, 4 },
                                              { 5, 12, 15,12, 5 },
                                              { 4,  9, 12, 9, 4 },
                                              { 2,  4,  5, 4, 2 }
                                                };
            
            for (int x = 0; x < imageMap.Width; x+=5)
            {
                for (int y = 0; y < imageMap.Height; y+=5)
                {
                    int accumalator = 0;
                    for (int row = 0; row < 5; row++)
                    {
                        for (int element = 0; element < 5; element++)
                        {
                            Color pixel = imageMap.GetPixel(x, y);
                            Color gaussianPixel = new Color(pixel.R,pixel.B,pixel.G);

                        }
                    }
                }
            }
            //loaded_image.Save("Uusi", ImageFormat.Png);

        }
    }
}
