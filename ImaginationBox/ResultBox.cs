using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImaginationBox
{
    public partial class ResultBox : UserControl
    {
        string _text = string.Empty;
        public int _rating = 0;

        /// <summary>
        /// Its a property!
        /// </summary>
        public string TextContent
        {
            get { return _text; }
            set { _text = value; }
        }

        // Public Methods //

        public ResultBox()
        {
            InitializeComponent();
            updateRatingLabel();
        }

        public void SetResult(Bitmap newImage, String image_text)
        {
            #region resize bitmap image to fit in box
            if (newImage.Width > pictureBox.Width)
            {
                float scale = pictureBox.Width;
                scale /= newImage.Width;
                newImage = ResizeBitmap(newImage, pictureBox.Width, (int)(newImage.Height * scale));
            }

            if (newImage.Height > pictureBox.Height)
            {
                float scale = pictureBox.Height;
                scale /= newImage.Height;
                newImage = ResizeBitmap(newImage, (int)(newImage.Width * scale), pictureBox.Height);
            }
            #endregion

            pictureBox.Image = newImage;
            pictureBox_label.Text = image_text;
            _text = image_text;
            _rating = 0;
            updateRatingLabel();
        }

        public void SetText(String image_text)
        {
            pictureBox_label.Text = image_text;
            _text = image_text;
        }
        

        // Private methods //

        //Resizes the Bitmap b into the new Width and Height and returns the new Bitmap
        private Bitmap ResizeBitmap(Bitmap b, int nWidth, int nHeight)
        {
            Bitmap result = new Bitmap(nWidth, nHeight);
            using (Graphics g = Graphics.FromImage((Image)result))
                g.DrawImage(b, 0, 0, nWidth, nHeight);
            return result;
        }

        //Updates the text label of the rating, based on current rating status
        private void updateRatingLabel()
        {
            if (_rating == 1)
                rating_label.Text = "(+1)";
            else if (_rating == -1)
                rating_label.Text = "(-1)";
            else
                rating_label.Text = "";
        }

        // Increments/Decrements the resultBox's rating
        private void btnThumbUp_Click(object sender, EventArgs e)
        {
            if (_rating == -1)
            {
                _rating = 0;
                btnThumbUp.Checked = false;
                btnThumbDown.Checked = false;
            }
            else if (_rating == 0 || _rating == 1)
            {
                _rating = 1;
                btnThumbUp.Checked = true;
                btnThumbDown.Checked = false;
            }

            updateRatingLabel();
        }
        private void btnThumbDown_Click(object sender, EventArgs e)
        {
            if (_rating == 1)
            {
                _rating = 0;
                btnThumbUp.Checked = false;
                btnThumbDown.Checked = false;
            }
            else if (_rating == 0 || _rating == -1)
            {
                _rating = -1;
                btnThumbUp.Checked = false;
                btnThumbDown.Checked = true;
            }
            updateRatingLabel();
        }

    }
}
