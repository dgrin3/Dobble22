using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Text;

namespace GetPictures
{
    class String2Img
    {
        // Method borrowed from naveedmurtuza/TextToImage.cs

        /// <summary>
        /// Converting text to image (png).
        /// </summary>
        /// <param name="text">text to convert</param>
        /// <param name="font">Font to use</param>
        /// <param name="textColor">text color</param>
        /// <param name="maxWidth">max width of the image</param>
        /// <param name="path">path to save the image</param>
        public static void DrawText(string text, Font font, Color textColor, int maxWidth, string path)
        {
            //first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);
            //measure the string to see how big the image needs to be
            SizeF textSize = drawing.MeasureString(text, font, maxWidth);

            //set the stringformat flags to rtl
            StringFormat sf = new StringFormat();
            //uncomment the next line for right to left languages
            //sf.FormatFlags = StringFormatFlags.DirectionRightToLeft;
            sf.Trimming = StringTrimming.Word;
            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            img = new Bitmap((int)textSize.Width, (int)textSize.Height);

            drawing = Graphics.FromImage(img);
            //Adjust for high quality
            drawing.CompositingQuality = CompositingQuality.HighQuality;
            drawing.InterpolationMode = InterpolationMode.HighQualityBilinear;
            drawing.PixelOffsetMode = PixelOffsetMode.HighQuality;
            drawing.SmoothingMode = SmoothingMode.HighQuality;
            drawing.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            //paint the background
            drawing.Clear(Color.Transparent);

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, textBrush, new RectangleF(0, 0, textSize.Width, textSize.Height), sf);

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();
            img.Save(path, ImageFormat.Png);
            img.Dispose();

        }
    }
}
