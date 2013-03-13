using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using ThoughtWorks.QRCode.Codec.Util;

namespace test
{
    public partial class QRCode : Form
    {
        public QRCode()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtEncodeData.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Data must not be empty.");
                return;
            }

            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            String encoding = cboEncoding.Text;
            if (encoding == "Byte")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            }
            else if (encoding == "AlphaNumeric")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
            }
            else if (encoding == "Numeric")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE;
            }
            try
            {
                int scale = Convert.ToInt16(txtSize.Text);
                qrCodeEncoder.QRCodeScale = scale;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid size!");
                return;
            }
            try
            {
                int version = Convert.ToInt16(cboVersion.Text);
                qrCodeEncoder.QRCodeVersion = version;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid version !");
            }

            string errorCorrect = cboCorrectionLevel.Text;
            if (errorCorrect == "L")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            else if (errorCorrect == "M")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            else if (errorCorrect == "Q")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
            else if (errorCorrect == "H")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;

            qrCodeEncoder.QRCodeBackgroundColor = System.Drawing.Color.DeepSkyBlue;
            //qrCodeEncoder.
            Image image;
            String data = txtEncodeData.Text;
            image = qrCodeEncoder.Encode(data);
            pb_view.Image = image;
        }

        private void QRCode_Load(object sender, EventArgs e)
        {
            cboEncoding.SelectedIndex = 2;
            cboVersion.SelectedIndex = 6;
            cboCorrectionLevel.SelectedIndex = 1;
        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|PNG Image|*.png";
            saveFileDialog1.Title = "--Save--";
            saveFileDialog1.FileName = string.Empty;
            //saveFileDialog1.ShowDialog();

            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            if (saveFileDialog1.FileName == null)
                return;

            // save to file
            pb_view.Image.Save(saveFileDialog1.FileName);
        }

        private void btnDecode_Click(object sender, EventArgs e)
        {
            QRCodeDecoder decoder = new QRCodeDecoder();

            String decodedString = decoder.decode(new QRCodeBitmapImage(new Bitmap(pb_view.Image)));

            txtEncodeData.Text = decodedString;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1= new OpenFileDialog();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pb_view.Image = Image.FromFile( openFileDialog1.FileName.ToString());
            }
        }
    }
}
