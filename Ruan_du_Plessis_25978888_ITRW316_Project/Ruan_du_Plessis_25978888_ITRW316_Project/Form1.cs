using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace Ruan_du_Plessis_25978888_ITRW316_Project
{
    public partial class frmITRW316Projek : Form
    {
        private Bitmap bOriginal = null;
        private Bitmap bPreview = null;
        private Bitmap bResult = null;

        public frmITRW316Projek()
        {
            InitializeComponent();

            cbGrid.Items.Add(BitmapExtension.BlurType.Average3x3);
            cbGrid.Items.Add(BitmapExtension.BlurType.Average5x5);
            cbGrid.Items.Add(BitmapExtension.BlurType.Average7x7);
            cbGrid.Items.Add(BitmapExtension.BlurType.Average9x9);
            cbGrid.Items.Add(BitmapExtension.BlurType.Average11x11);

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            timer1.Start();
            FilterApplication(true);
            btnSave.Visible = true;
        }

        private void cbThreads_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((cbThreads.Text != "") && (cbGrid.Text != ""))
            {
                btnStart.Enabled = true;
            }
        }

        private void cbGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((cbThreads.Text != "") && (cbGrid.Text != ""))
            {
                btnStart.Enabled = true;
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            lblGrid.Visible = true;
            lblThreads.Visible = true;
            cbGrid.Visible = true;
            cbThreads.Visible = true;
            btnStart.Visible = true;

            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Choose the picture you want to use.";
            openFile.Filter = "Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg";
            openFile.Filter += "|Bitmap Images(*.bmp)|*.bmp";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                StreamReader sRead = new StreamReader(openFile.FileName);
                bOriginal = (Bitmap)Bitmap.FromStream(sRead.BaseStream);
                sRead.Close();

                bPreview = bOriginal.CopyCanvas(pbOriginal.Width);
                pbOriginal.Image = bPreview;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FilterApplication(false);

            if (bResult != null)
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Title = "Specify a file name and file path";
                saveFile.Filter = "Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg";
                saveFile.Filter += "|Bitmap Images(*.bmp)|*.bmp";

                if (saveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string fileExtension = Path.GetExtension(saveFile.FileName).ToUpper();
                    ImageFormat imgFormat = ImageFormat.Png;

                    if (fileExtension == "BMP")
                    {
                        imgFormat = ImageFormat.Bmp;
                    }
                    else if (fileExtension == "JPG")
                    {
                        imgFormat = ImageFormat.Jpeg;
                    }

                    StreamWriter streamWriter = new StreamWriter(saveFile.FileName, false);
                    bResult.Save(streamWriter.BaseStream, imgFormat);
                    streamWriter.Flush();
                    streamWriter.Close();

                    bResult = null;
                }
            }
        }

        private void FilterApplication(bool preview)
        {
            if (bPreview == null || cbGrid.SelectedIndex == -1)
            {
                return;
            }

            Bitmap selected = null;
            Bitmap result = null;

            if (preview == true)
            {
                selected = bPreview;
            }
            else
            {
                selected = bOriginal;
            }

            if (selected != null)
            {
                BitmapExtension.BlurType bType =
                    ((BitmapExtension.BlurType)cbGrid.SelectedItem);

                bResult = selected.BlurFilter(bType);
            }

            if (bResult != null)
            {
                if (preview == true)
                {
                    pbBlurred.Image = bResult;
                }
                else
                {
                    result = bResult;
                }
            }
        }
    }
}
