using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ProcessamentoImagens
{
    class Filtros
    {
        
        public static BitmapData lockDados(Bitmap bmp)
        {
            BitmapData dataSrc = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            return dataSrc;
        }

        public static void convert_to_gray(Bitmap imageBitmapSrc, Bitmap imageBitmapDest)
        {
            int width = imageBitmapSrc.Width;
            int height = imageBitmapSrc.Height;
            int r, g, b;
            Int32 gs;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //obtendo a cor do pixel
                    Color cor = imageBitmapSrc.GetPixel(x, y);

                    r = cor.R;
                    g = cor.G;
                    b = cor.B;
                    gs = (Int32)(r * 0.2990 + g * 0.5870 + b * 0.1140);

                    //nova cor
                    Color newcolor = Color.FromArgb(gs, gs, gs);

                    imageBitmapDest.SetPixel(x, y, newcolor);
                }
            }
        }
        
        public static void negativo(Bitmap imageBitmapSrc, Bitmap imageBitmapDest)
        {
            int width = imageBitmapSrc.Width;
            int height = imageBitmapSrc.Height;
            int r, g, b;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //obtendo a cor do pixel
                    Color cor = imageBitmapSrc.GetPixel(x, y);

                    r = cor.R;
                    g = cor.G;
                    b = cor.B;

                    //nova cor
                    Color newcolor = Color.FromArgb(255 - r, 255 - g, 255 - b);

                    imageBitmapDest.SetPixel(x, y, newcolor);
                }
            }
        }
        
        public static void convert_to_grayDMA(Bitmap imageBitmapSrc, Bitmap imageBitmapDest)
        {
            int width = imageBitmapSrc.Width;
            int height = imageBitmapSrc.Height;
            int pixelSize = 3;
            Int32 gs;

            //lock dados bitmap origem
            BitmapData bitmapDataSrc = imageBitmapSrc.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            //lock dados bitmap destino
            BitmapData bitmapDataDst = imageBitmapDest.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int padding = bitmapDataSrc.Stride - (width * pixelSize);

            unsafe
            {
                byte* src = (byte*)bitmapDataSrc.Scan0.ToPointer();
                byte* dst = (byte*)bitmapDataDst.Scan0.ToPointer();

                int r, g, b;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        b = *(src++); //está armazenado dessa forma: b g r 
                        g = *(src++);
                        r = *(src++);
                        gs = (Int32)(r * 0.2990 + g * 0.5870 + b * 0.1140);


                        *(dst++) = (byte)gs;
                        *(dst++) = (byte)gs;
                        *(dst++) = (byte)gs;
                    }
                    src += padding;
                    dst += padding;
                }
            }
            //unlock imagem origem
            imageBitmapSrc.UnlockBits(bitmapDataSrc);
            //unlock imagem destino
            imageBitmapDest.UnlockBits(bitmapDataDst);
        }
        
        public static void negativoDMA(Bitmap imageBitmapSrc, Bitmap imageBitmapDest)
        {
            int width = imageBitmapSrc.Width;
            int height = imageBitmapSrc.Height;
            int pixelSize = 3;

            //lock dados bitmap origem 
            BitmapData bitmapDataSrc = imageBitmapSrc.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            //lock dados bitmap destino
            BitmapData bitmapDataDst = imageBitmapDest.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int padding = bitmapDataSrc.Stride - (width * pixelSize);

            unsafe
            {
                byte* src1 = (byte*)bitmapDataSrc.Scan0.ToPointer();
                byte* dst = (byte*)bitmapDataDst.Scan0.ToPointer();

                int r, g, b;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        b = *(src1++); //está armazenado dessa forma: b g r 
                        g = *(src1++);
                        r = *(src1++);

                        *(dst++) = (byte)(255 - b);
                        *(dst++) = (byte)(255 - g);
                        *(dst++) = (byte)(255 - r);
                    }
                    src1 += padding;
                    dst += padding;
                }
            }
            //unlock imagem origem 
            imageBitmapSrc.UnlockBits(bitmapDataSrc);
            //unlock imagem destino
            imageBitmapDest.UnlockBits(bitmapDataDst);
        }

        public static void espelhoVert(Bitmap src, Bitmap dst)
        {
            int width = src.Width;
            int height = dst.Height;
            int r, g, b;

            
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //Color cor = src.GetPixel(x, y);
                   // r = cor.R;
                   // g = cor.G;
                   // b = cor.B;
                    //Color novaCor = Color.FromArgb(r, g, b);
                    dst.SetPixel(x, height-y-1, src.GetPixel(x, y));
                }
            }

        }

        public static void espelhoHori(Bitmap src, Bitmap dst)
        {
            int width = src.Width;
            int height = dst.Height;
            int r, g, b;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //Color cor = src.GetPixel(x, y);
                    // r = cor.R;
                    // g = cor.G;
                    // b = cor.B;
                    //Color novaCor = Color.FromArgb(r, g, b);
                    dst.SetPixel(width-x-1, y, src.GetPixel(x, y));
                }
            }

        }

        public static void espelhoDiag(Bitmap src, Bitmap dst)
        {
            int width = src.Width;
            int height = dst.Height;
            int r, g, b;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //Color cor = src.GetPixel(x, y);
                    // r = cor.R;
                    // g = cor.G;
                    // b = cor.B;
                    //Color novaCor = Color.FromArgb(r, g, b);
                    dst.SetPixel((width - x) - 1, (height - y) - 1, src.GetPixel(x, y));
                }
            }
        }

        public static void espelhoVertDMA(Bitmap src, Bitmap dst)
        {
            int width = src.Width;
            int height = dst.Height;
            int pixelSize = 3;

            //Lock de dados src e dst
            BitmapData dataSrc = lockDados(src);
            BitmapData dataDst = lockDados(dst);

            int padding = dataSrc.Stride - (pixelSize * width);

            unsafe
            {
                byte* pSrc = (byte*)dataSrc.Scan0.ToPointer();
                byte* pDst = (byte*)dataDst.Scan0.ToPointer();
                byte* aux;

                int r, g, b;                

                for (int y = 0; y < height; y++)
                {
                    aux = pDst + (dataDst.Stride * (height - y)); //posiciona na linha contraria que esta indexada em y

                    for (int x = 0; x < width; x++)
                    {
                        b = *(pSrc++);
                        g = *(pSrc++);
                        r = *(pSrc++);

                        *(aux++) = (byte)b;
                        *(aux++) = (byte)g;
                        *(aux++) = (byte)r;
                    }
                    pSrc += padding;
                }
            }
            src.UnlockBits(dataSrc);
            dst.UnlockBits(dataDst);
        }

        public static void espelhoHoriDMA(Bitmap src, Bitmap dst)
        {
            int width = src.Width;
            int height = dst.Height;
            int pixelSize = 3;

            //Lock de dados src e dst
            BitmapData dataSrc = lockDados(src);
            BitmapData dataDst = lockDados(dst);

            int padding = dataSrc.Stride - (pixelSize * width);

            unsafe
            {
                byte* pSrc = (byte*)dataSrc.Scan0.ToPointer();
                byte* pDst = (byte*)dataDst.Scan0.ToPointer();
                byte* aux;

                int r, g, b;

                for (int y = 0; y < height; y++)
                {                    
                    for (int x = 0; x < width; x++)
                    {
                        b = *(pSrc++);
                        g = *(pSrc++);
                        r = *(pSrc++);

                        aux = pDst + (dataDst.Stride * y) + (width * 3) - (x * 3);

                        //pDst + (dataDst.Stride * y)   ->vai para o começo linha
                        //aux += (width * 3) - (x * 3)  ->vai para o canal contrario ao presente em x

                        *(aux++) = (byte)b;
                        *(aux++) = (byte)g;
                        *(aux++) = (byte)r;

                    }
                    pSrc += padding;
                }
            }
            src.UnlockBits(dataSrc);
            dst.UnlockBits(dataDst);
        }

       public static void espelhoDiagDMA(Bitmap src, Bitmap dst)
        {
            int width = src.Width;
            int height = dst.Height;
            int pixelSize = 3;

            //Lock de dados src e dst
            BitmapData dataSrc = lockDados(src);
            BitmapData dataDst = lockDados(dst);

            int padding = dataSrc.Stride - (pixelSize * width);

            unsafe
            {
                byte* pSrc = (byte*)dataSrc.Scan0.ToPointer();
                byte* pDst = (byte*)dataDst.Scan0.ToPointer();
                byte* aux;

                int r, g, b;


                for (int y = 0; y < height; y++)
                {                    
                    for (int x = 0; x < width; x++)
                    {
                        b = *(pSrc++);
                        g = *(pSrc++);
                        r = *(pSrc++);

                        aux = pDst + (dataDst.Stride * (height - y)) + (width * 3) - (x * 3);                        

                        //pDst + (dataDst.Stride * (height - y))  ->vai para a linha contraria a indexada
                        //(width * 3) - (x * 3)    -> //vai para o canal contrario ao presente em x

                        *(aux++) = (byte)b;
                        *(aux++) = (byte)g;
                        *(aux++) = (byte)r;

                    }
                    pSrc += padding;
                }
            }
            src.UnlockBits(dataSrc);
            dst.UnlockBits(dataDst);
        }
      
        public static void separaRed(Bitmap src, Bitmap dst)
        {
            int width = src.Width;
            int height = src.Height;
            int r, g, b;

            
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color c = src.GetPixel(x, y);
                    Color nc = Color.FromArgb(c.R, 0, 0);
                    dst.SetPixel(x, y, nc);

                }
            }
        }

        public static void separaGreen(Bitmap src, Bitmap dst)
        {
            int width = src.Width;
            int height = src.Height;
            int r, g, b;


            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color c = src.GetPixel(x, y);
                    Color nc = Color.FromArgb(0, c.G, 0);
                    dst.SetPixel(x, y, nc);

                }
            }
        }

        public static void separaBlue(Bitmap src, Bitmap dst)
        {
            int width = src.Width;
            int height = src.Height;
            int r, g, b;


            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color c = src.GetPixel(x, y);
                    Color nc = Color.FromArgb(0, 0, c.B);
                    dst.SetPixel(x, y, nc);

                }
            }
        }

        public static void separaRedDMA(Bitmap src, Bitmap dst)
        {            
            int width = src.Width;
            int height = dst.Height;
            int pixelSize = 3;

            //Lock de dados src e dst
            BitmapData dataSrc = lockDados(src);
            BitmapData dataDst = lockDados(dst);

            int padding = dataSrc.Stride - (pixelSize * width);

            unsafe
            {
                byte* pSrc = (byte*)dataSrc.Scan0.ToPointer();
                byte* pDst = (byte*)dataDst.Scan0.ToPointer();


                int r;

                for (int y = 0; y < height; y++)
                {
                        
                    for (int x = 0; x < width; x++)
                    {
                        //pSrc = (byte*)(2 * sizeof(byte));

                        pSrc++;
                        pSrc++;
                        r = *(pSrc++);                      


                        *(pDst++) = 0;
                        *(pDst++) = 0;
                        *(pDst++) = (byte)r;

                    }
                    pSrc += padding;
                    pDst += padding;
                }
            }
            src.UnlockBits(dataSrc);
            dst.UnlockBits(dataDst);            
        }

        public static void separaGreenDMA(Bitmap src, Bitmap dst)
        {
            int width = src.Width;
            int height = dst.Height;
            int pixelSize = 3;

            //Lock de dados src e dst
            BitmapData dataSrc = lockDados(src);
            BitmapData dataDst = lockDados(dst);

            int padding = dataSrc.Stride - (pixelSize * width);

            unsafe
            {
                byte* pSrc = (byte*)dataSrc.Scan0.ToPointer();
                byte* pDst = (byte*)dataDst.Scan0.ToPointer();

                int g;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {                        
                        pSrc++;
                        g = *(pSrc++);
                        pSrc++;

                        *(pDst++) = 0;
                        *(pDst++) = (byte)g;
                        *(pDst++) = 0;

                    }
                    pSrc += padding;
                    pDst += padding;
                }
            }
            src.UnlockBits(dataSrc);
            dst.UnlockBits(dataDst);
        }

        public static void separaBlueDMA(Bitmap src, Bitmap dst)
        {
            int width = src.Width;
            int height = dst.Height;
            int pixelSize = 3;

            //Lock de dados src e dst
            BitmapData dataSrc = lockDados(src);
            BitmapData dataDst = lockDados(dst);

            int padding = dataSrc.Stride - (pixelSize * width);

            unsafe
            {
                byte* pSrc = (byte*)dataSrc.Scan0.ToPointer();
                byte* pDst = (byte*)dataDst.Scan0.ToPointer();

                int b;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        b = *(pSrc++);
                        pSrc++;                       
                        pSrc++;

                        *(pDst++) = (byte)b;
                        *(pDst++) = 0;
                        *(pDst++) = 0;

                    }
                    pSrc += padding;
                    pDst += padding;
                }
            }
            src.UnlockBits(dataSrc);
            dst.UnlockBits(dataDst);
        }

        public static void PeB (Bitmap src, Bitmap dst)
        {
            int width = src.Width;
            int height = src.Height;
            int r, g, b;
            Int32 gs;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //obtendo a cor do pixel
                    Color cor = src.GetPixel(x, y);

                    r = cor.R;
                    g = cor.G;
                    b = cor.B;
                    gs = (Int32)(r * 0.2990 + g * 0.5870 + b * 0.1140);

                    Color newcolor;
                    if (gs > 127)
                        newcolor = Color.FromArgb(255, 255, 255);
                    else
                        newcolor = Color.FromArgb(0, 0, 0);

                    dst.SetPixel(x, y, newcolor);
                }
            }
        }

        public static void PeBDMA(Bitmap src, Bitmap dst)
        {
            BitmapData dataSrc = lockDados(src);
            BitmapData dataDst = lockDados(dst);

            int width = src.Width;
            int heigth = src.Height;
            int padding = dataSrc.Stride - (3 * width);

            unsafe
            {
                int b, g, r;

                byte* pSrc = (byte*)dataSrc.Scan0.ToPointer();
                byte* pDst = (byte*)dataDst.Scan0.ToPointer();

                for (int y = 0; y < heigth; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        b = *(pSrc++);
                        g = *(pSrc++);
                        r = *(pSrc++);

                        if((r * 0.2990 + g * 0.5870 + b * 0.1140) > 127)
                        {
                            *(pDst++) = 255;
                            *(pDst++) = 255;
                            *(pDst++) = 255;
                        }
                        else
                        {
                            *(pDst++) = 0;
                            *(pDst++) = 0;
                            *(pDst++) = 0;
                        }  
                    }
                    pSrc += padding;
                    pDst += padding;
                }
            }
            src.UnlockBits(dataSrc);
            dst.UnlockBits(dataDst);
        }

        public static void invRB(Bitmap src, Bitmap dst)
        {
            int width = src.Width;
            int height = src.Height;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {                    
                    Color cor = src.GetPixel(x, y);  
                    Color newcolor = Color.FromArgb(cor.B, cor.G, cor.R);

                    dst.SetPixel(x, y, newcolor);
                }
            }
        }

        public static void invRBDMA(Bitmap src, Bitmap dst)
        {
            BitmapData dataSrc = lockDados(src);
            BitmapData dataDst = lockDados(dst);

            int width = src.Width;
            int heigth = src.Height;
            int padding = dataSrc.Stride - (3 * width);

            unsafe
            {
                int b, g, r;

                byte* pSrc = (byte*)dataSrc.Scan0.ToPointer();
                byte* pDst = (byte*)dataDst.Scan0.ToPointer();

                for (int y = 0; y < heigth; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        b = *(pSrc++);
                        g = *(pSrc++);
                        r = *(pSrc++);
                        
                        *(pDst++) = (byte)r;
                        *(pDst++) = (byte)g;
                        *(pDst++) = (byte)b;
                        
                    }
                    pSrc += padding;
                    pDst += padding;
                }
            }
            src.UnlockBits(dataSrc);
            dst.UnlockBits(dataDst);
        }

        public static void rotacaoHoraria(Bitmap src, Bitmap dst)
        {
            int width = src.Width;
            int height = src.Height;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //dst.SetPixel(y , width-x-1, src.GetPixel(x, y)); //anti-Horaria, pois o plano computacional funciona diferente do cartesiano
                    dst.SetPixel(height-y-1, x, src.GetPixel(x, y));
                }
            }
        }

        public static void rotacaoHorariaDMA(Bitmap src, Bitmap dst)
        {
            int width = src.Width;
            int height = src.Height;

            BitmapData dataSrc = lockDados(src);
            BitmapData dataDst = lockDados(dst);

            int padding = dataSrc.Stride - (width * 3);

            unsafe
            {
                int b, g, r;

                byte* pSrc = (byte*)dataSrc.Scan0.ToPointer();
                byte* pDst = (byte*)dataDst.Scan0.ToPointer();
                byte* aux;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        
                        b = *(pSrc++);
                        g = *(pSrc++);
                        r = *(pSrc++);

                        aux = pDst + (dataDst.Stride * x) + (height * 3) - (y * 3);

                        *(aux++) = (byte)b;
                        *(aux++) = (byte)g;
                        *(aux++) = (byte)r;

                    }
                    pSrc += padding;
                }
            }

            src.UnlockBits(dataSrc);
            dst.UnlockBits(dataDst);
        }

        public static void separaRegioes(Bitmap src, Bitmap dst)
        {
            int height = src.Height;
            int width = src.Width;

            int yMeio = height / 2;
            int xMeio = width / 2;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color cor = src.GetPixel(x, y);
                    if (x < xMeio && y < yMeio)//1° quadrante
                    {
                        dst.SetPixel(x+xMeio, y+yMeio, src.GetPixel(x, y));
                    }
                    else if (x > xMeio && y > yMeio)//4° quadrante
                    {
                        dst.SetPixel(x - xMeio, y - yMeio, src.GetPixel(x, y));
                    }
                    else if (x > xMeio && y < yMeio)//2° quadrante
                    {
                        dst.SetPixel(x - xMeio, y + yMeio, src.GetPixel(x, y));
                    }
                    else if (x < xMeio && y > yMeio)//3° quadrante
                    {
                        dst.SetPixel(x + xMeio, y - yMeio, src.GetPixel(x, y));
                    }


                }
            }
        }

        public static void separaRegioesDMA(Bitmap src, Bitmap dst)
        {
            int width = src.Width;
            int height = dst.Height;

            int xMeio = width / 2;
            int yMeio = height / 2;

            //Lock de dados src e dst
            BitmapData dataSrc = lockDados(src);
            BitmapData dataDst = lockDados(dst);

            int padding = dataSrc.Stride - (3 * width);

            unsafe
            {
                byte* pSrc = (byte*)dataSrc.Scan0.ToPointer();
                byte* pDst = (byte*)dataDst.Scan0.ToPointer();
                byte* aux;

                int r, g, b;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        b = *(pSrc++);
                        g = *(pSrc++);
                        r = *(pSrc++);

                        aux = pDst + (dataDst.Stride * y);
                        if (x < xMeio && y < yMeio)//1° quadrante
                        {
                            aux = pDst + (dataDst.Stride * (y + yMeio)) + (x * 3) + (xMeio * 3);
                        }
                        else if (x > xMeio && y > yMeio)//4° quadrante
                        {
                            aux = pDst + (dataDst.Stride * (y - yMeio)) + (x * 3)- (xMeio * 3);
                        }
                        else if (x > xMeio && y < yMeio)//2° quadrante
                        {
                            aux = pDst + (dataDst.Stride * (y + yMeio)) + (x * 3) - (xMeio * 3);
                        }
                        else if (x < xMeio && y > yMeio)//3° quadrante
                        {
                            aux = pDst + (dataDst.Stride * (y - yMeio)) + (x * 3) + (xMeio * 3);
                        }

                        *(aux++) = (byte)b;
                        *(aux++) = (byte)g;
                        *(aux++) = (byte)r;

                    }
                    pSrc += padding;
                     //pDst += padding;
                }
            }
            src.UnlockBits(dataSrc);
            dst.UnlockBits(dataDst);
        }   
    }    
}



