
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CG1
{
    public partial class Form1 : Form
    {
        Bitmap image; //Объект Bitmap
        string imagefile = "C:\\Users\\Дарья\\Pictures\\maxresdefault.jpg";
        public Form1()
        {
            InitializeComponent();
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (imagefile == "") image = new Bitmap("C:\\Users\\Дарья\\Pictures\\maxresdefault.jpg");
            else image = new Bitmap(imagefile);
            pictureBox1.Image = image; // Визуализация изображения на форме
            pictureBox1.Refresh();
        }
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog(); //Диалог для открытия файла
            dialog.Filter = "Image files|*.png;*.jpg;*.bmp|All files(*.*)|*.*"; //Файлы-изображения
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                image = new Bitmap(dialog.FileName); // Проверка на выбор файла
            }
            imagefile = dialog.FileName;
            //image = new Bitmap("C:\\Users\\Дарья\\Pictures\\maxresdefault.jpg");
            pictureBox1.Image = image; // Визуализация изображения на форме
            pictureBox1.Refresh();
        }
        private void инверсияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InventFilter filter = new InventFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Bitmap newImage = ((Filters)e.Argument).processImage(image, backgroundWorker1);
            if (backgroundWorker1.CancellationPending != true)
                image = newImage;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
            progressBar1.Value = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        private void размытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new BlurFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void фильтрГауссаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GaussianFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void grayScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GrayScaleFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void сепияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new SepiaFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void увеличениеЯркостиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new BrightnessFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void операторСобеляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new SobelFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void увеличениеРезкостиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new SharpnessFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void серыйМирToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GrayWorldFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }
       
        private void идеальныйОтражательToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Filters filter = new IdealFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void медианныйФильтрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new MedianFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void матричныеToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void сдвигToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            Filters filter = new Shift(100, 0);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void поворотToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new turning(0, 0, Math.PI/2);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void размытиеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Filters filter = new wave();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Cохранить картинку как..";
                sfd.OverwritePrompt = true;
                sfd.CheckPathExists = true;
                sfd.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.JPG)|*.JPG|Image Files(*.GIF)|*.GIF|Image Files(*.PNG)|*.PNG|All Files(*.*)|*.*";
                sfd.ShowHelp = true;
                if(sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pictureBox1.Image.Save(sfd.FileName);             
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void binaryTranslationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters Bin =  new toBin();
            backgroundWorker1.RunWorkerAsync(Bin);
        }

        private void dilationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Morphology dil = new Dilation();
            backgroundWorker1.RunWorkerAsync(dil);
        }

        private void erosinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Morphology eros = new Erosion();
            backgroundWorker1.RunWorkerAsync(eros);
        }

        private void openingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Morphology open = new Opening();
            backgroundWorker1.RunWorkerAsync(open);
        }

        private void closingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Morphology close = new Closing();
            backgroundWorker1.RunWorkerAsync(close);

        }

        private void gradToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Morphology grad = new Grad();
            backgroundWorker1.RunWorkerAsync(grad);
        }

        private void topHatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Morphology hat = new TopHat();
            backgroundWorker1.RunWorkerAsync(hat);
        }

        private void blackHatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Morphology bhat = new BlackHat();
            backgroundWorker1.RunWorkerAsync(bhat);
        }

        private void резкостьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters sharp = new Sharpness();
            backgroundWorker1.RunWorkerAsync(sharp);

        }

        private void binaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters Bin = new toBin();
            backgroundWorker1.RunWorkerAsync(Bin);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}
