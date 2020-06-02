using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace ProcessamentoImagens
{
    public partial class frmPrincipal : Form
    {
        private Image image;
        private Bitmap imageBitmap;

        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void btnAbrirImagem_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = "";
            openFileDialog.Filter = "Arquivos de Imagem (*.jpg;*.gif;*.bmp;*.png)|*.jpg;*.gif;*.bmp;*.png";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                image = Image.FromFile(openFileDialog.FileName);
                pictBoxImg1.Image = image;
                pictBoxImg1.SizeMode = PictureBoxSizeMode.Normal;
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            pictBoxImg1.Image = null;
            pictBoxImg2.Image = null;
        }

       

        private void BtTransformar_Click(object sender, EventArgs e)
        {
            if (cbTransformacao.GetItemText(cbTransformacao.SelectedItem).Equals("Luminância sem DMA"))
            {
                Bitmap imgDest = new Bitmap(image);
                imageBitmap = (Bitmap)image;
                Filtros.convert_to_gray(imageBitmap, imgDest);
                pictBoxImg2.Image = imgDest;
            }
            else if (cbTransformacao.GetItemText(cbTransformacao.SelectedItem).Equals("Luminância com DMA"))
            {
                Bitmap imgDest = new Bitmap(image);
                imageBitmap = (Bitmap)image;
                Filtros.convert_to_grayDMA(imageBitmap, imgDest);
                pictBoxImg2.Image = imgDest;
            }
            else if (cbTransformacao.GetItemText(cbTransformacao.SelectedItem).Equals("Negativo sem DMA"))
            {
                Bitmap imgDest = new Bitmap(image);
                imageBitmap = (Bitmap)image;
                Filtros.negativo(imageBitmap, imgDest);
                pictBoxImg2.Image = imgDest;
            }
            else if (cbTransformacao.GetItemText(cbTransformacao.SelectedItem).Equals("Negativo com DMA"))
            {
                Bitmap imgDest = new Bitmap(image);
                imageBitmap = (Bitmap)image;
                Filtros.negativoDMA(imageBitmap, imgDest);
                pictBoxImg2.Image = imgDest;
            }
            else if (cbTransformacao.GetItemText(cbTransformacao.SelectedItem).Equals("Espelho Vertical"))
            {
                Bitmap imgDest = new Bitmap(image);
                imageBitmap = (Bitmap)image;
                Filtros.espelhoVert(imageBitmap, imgDest);
                pictBoxImg2.Image = imgDest;
            }
            else if (cbTransformacao.GetItemText(cbTransformacao.SelectedItem).Equals("Espelho Horizontal"))
            {
                Bitmap imgDest = new Bitmap(image);
                imageBitmap = (Bitmap)image;
                Filtros.espelhoHori(imageBitmap, imgDest);
                pictBoxImg2.Image = imgDest;
            }
            else if (cbTransformacao.GetItemText(cbTransformacao.SelectedItem).Equals("Espelho Diagonal"))
            {
                Bitmap imgDest = new Bitmap(image);
                imageBitmap = (Bitmap)image;
                Filtros.espelhoDiag(imageBitmap, imgDest);
                pictBoxImg2.Image = imgDest;
            }
            else if (cbTransformacao.GetItemText(cbTransformacao.SelectedItem).Equals("Espelho Vertical DMA"))
            {
                Bitmap imgDest = new Bitmap(image);
                imageBitmap = (Bitmap)image;
                Filtros.espelhoVertDMA(imageBitmap, imgDest);
                pictBoxImg2.Image = imgDest;
            }
            else if (cbTransformacao.GetItemText(cbTransformacao.SelectedItem).Equals("Espelho Horizontal DMA"))
            {
                Bitmap imgDest = new Bitmap(image);
                imageBitmap = (Bitmap)image;
                Filtros.espelhoHoriDMA(imageBitmap, imgDest);
                pictBoxImg2.Image = imgDest;
            }
            else if (cbTransformacao.GetItemText(cbTransformacao.SelectedItem).Equals("Espelho Diagonal DMA"))
            {
                Bitmap imgDest = new Bitmap(image);
                imageBitmap = (Bitmap)image;
                Filtros.espelhoDiagDMA(imageBitmap, imgDest);
                pictBoxImg2.Image = imgDest;
            }
            else if (cbTransformacao.GetItemText(cbTransformacao.SelectedItem).Equals("Separa RED"))
            {
                Bitmap imgDest = new Bitmap(image);
                imageBitmap = (Bitmap)image;
                Filtros.separaRed(imageBitmap, imgDest);
                pictBoxImg2.Image = imgDest;
            }
            else if (cbTransformacao.GetItemText(cbTransformacao.SelectedItem).Equals("Separa GREEN"))
            {
                Bitmap imgDest = new Bitmap(image);
                imageBitmap = (Bitmap)image;
                Filtros.separaGreen(imageBitmap, imgDest);
                pictBoxImg2.Image = imgDest;
            }
            else if (cbTransformacao.GetItemText(cbTransformacao.SelectedItem).Equals("Separa BLUE"))
            {
                Bitmap imgDest = new Bitmap(image);
                imageBitmap = (Bitmap)image;
                Filtros.separaBlue(imageBitmap, imgDest);
                pictBoxImg2.Image = imgDest;
            }
            else if (cbTransformacao.GetItemText(cbTransformacao.SelectedItem).Equals("Separa RED DMA"))
            {
                Bitmap imgDest = new Bitmap(image);
                imageBitmap = (Bitmap)image;
                Filtros.separaRedDMA(imageBitmap, imgDest);
                pictBoxImg2.Image = imgDest;
            }
            else if (cbTransformacao.GetItemText(cbTransformacao.SelectedItem).Equals("Separa GREEN DMA"))
            {
                Bitmap imgDest = new Bitmap(image);
                imageBitmap = (Bitmap)image;
                Filtros.separaGreenDMA(imageBitmap, imgDest);
                pictBoxImg2.Image = imgDest;
            }
            else if (cbTransformacao.GetItemText(cbTransformacao.SelectedItem).Equals("Separa BLUE DMA"))
            {
                Bitmap imgDest = new Bitmap(image);
                imageBitmap = (Bitmap)image;
                Filtros.separaBlueDMA(imageBitmap, imgDest);
                pictBoxImg2.Image = imgDest;
            }
            else if (cbTransformacao.GetItemText(cbTransformacao.SelectedItem).Equals("Preto & Branco"))
            {
                Bitmap imgDest = new Bitmap(image);
                imageBitmap = (Bitmap)image;
                Filtros.PeB(imageBitmap, imgDest);
                pictBoxImg2.Image = imgDest;
            }
            else if (cbTransformacao.GetItemText(cbTransformacao.SelectedItem).Equals("Preto & Branco DMA"))
            {
                Bitmap imgDest = new Bitmap(image);
                imageBitmap = (Bitmap)image;
                Filtros.PeBDMA(imageBitmap, imgDest);
                pictBoxImg2.Image = imgDest;
            }
            else if (cbTransformacao.GetItemText(cbTransformacao.SelectedItem).Equals("Inverter RED BLUE"))
            {
                Bitmap imgDest = new Bitmap(image);
                imageBitmap = (Bitmap)image;
                Filtros.invRB(imageBitmap, imgDest);
                pictBoxImg2.Image = imgDest;
            }
            else if (cbTransformacao.GetItemText(cbTransformacao.SelectedItem).Equals("Inverter RED BLUE DMA"))
            {
                Bitmap imgDest = new Bitmap(image);
                imageBitmap = (Bitmap)image;
                Filtros.invRBDMA(imageBitmap, imgDest);
                pictBoxImg2.Image = imgDest;
            }
            else if (cbTransformacao.GetItemText(cbTransformacao.SelectedItem).Equals("Rotação Horária"))
            {
                Bitmap imgDest = new Bitmap(image.Height, image.Width);
                imageBitmap = (Bitmap)image;
                Filtros.rotacaoHoraria(imageBitmap, imgDest);                
                pictBoxImg2.Image = imgDest;
            }
            else if (cbTransformacao.GetItemText(cbTransformacao.SelectedItem).Equals("Rotação Horária DMA"))
            {
                Bitmap imgDest = new Bitmap(image.Height, image.Width);
                imageBitmap = (Bitmap)image;
                Filtros.rotacaoHorariaDMA(imageBitmap, imgDest);
                pictBoxImg2.Image = imgDest;
            }
            else if (cbTransformacao.GetItemText(cbTransformacao.SelectedItem).Equals("Separar Regioes"))
            {
                Bitmap imgDest = new Bitmap(image);
                imageBitmap = (Bitmap)image;
                Filtros.separaRegioes(imageBitmap, imgDest);
                pictBoxImg2.Image = imgDest;
            }
            else if (cbTransformacao.GetItemText(cbTransformacao.SelectedItem).Equals("Separar RegioesDMA"))
            {
                Bitmap imgDest = new Bitmap(image);
                imageBitmap = (Bitmap)image;
                Filtros.separaRegioesDMA(imageBitmap, imgDest);
                pictBoxImg2.Image = imgDest;
            }

        }
    }
}
