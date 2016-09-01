using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Collections.Generic;
using System.Web;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Drawing;

namespace LeftHand.Gadget
{
    public class Graphics
    {
        public enum ResizeType { InSide, OutSide }

        public enum ResizeMode { ByWidth, ByHeight }
        public static Bitmap Resize(Bitmap OriginalImage, int MaxLength, ResizeMode Mode)
        {
            int OriginalWidth = OriginalImage.Width;
            int OriginalHeight = OriginalImage.Height;

            //計算圖片要縮小的比例
            double ResizePercentage = 0;

            switch (Mode)
            {
                case ResizeMode.ByWidth:
                    ResizePercentage = (double)OriginalWidth / (double)MaxLength;
                    break;
                case ResizeMode.ByHeight:
                    ResizePercentage = (double)OriginalHeight / (double)MaxLength;
                    break;
            }

            //填入要縮小的寬度與高度
            int NewWidth = (int)(Math.Ceiling(OriginalWidth / ResizePercentage));
            int NewHeight = (int)(Math.Ceiling(OriginalHeight / ResizePercentage));

            //高品質縮圖
            Bitmap ResizedBitmap = new Bitmap(NewWidth, NewHeight);
            System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(ResizedBitmap);
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
            gr.DrawImage(OriginalImage, 0, 0, NewWidth, NewHeight);

            return ResizedBitmap;
        }

        public enum ScopeMode { InScope, OutScope }
        public static Bitmap ResizeByScope(Bitmap OriginalImage, int ScopeWidth, int ScopeHeight, ScopeMode ScopeMode = ScopeMode.InScope)
        {
            Bitmap ScopeBitmap = new Bitmap(ScopeWidth, ScopeHeight);

            double WidthPercentage = (double)OriginalImage.Width / (double)ScopeWidth;
            double HeightPercentage = (double)OriginalImage.Height / (double)ScopeHeight;

            //確定縮放模式
            Bitmap ResizedBitmap = null;
            switch (ScopeMode)
            {
                case ScopeMode.InScope:
                    if (WidthPercentage > HeightPercentage)
                    { ResizedBitmap = Resize(OriginalImage, ScopeWidth, ResizeMode.ByWidth); }
                    else
                    { ResizedBitmap = Resize(OriginalImage, ScopeHeight, ResizeMode.ByHeight); }
                    break;

                case ScopeMode.OutScope:
                    if (WidthPercentage > HeightPercentage)
                    { ResizedBitmap = Resize(OriginalImage, ScopeHeight, ResizeMode.ByHeight); }
                    else
                    { ResizedBitmap = Resize(OriginalImage, ScopeWidth, ResizeMode.ByWidth); }
                    break;
            }

            //置中座標補償
            int LeftX = (ScopeBitmap.Width - ResizedBitmap.Width) / 2;
            int TopY = (ScopeBitmap.Height - ResizedBitmap.Height) / 2;

            //Graphics
            System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(ScopeBitmap);
            gr.Clear(Color.Black);
            gr.DrawImage(ResizedBitmap, LeftX, TopY, ResizedBitmap.Width, ResizedBitmap.Height);

            return ScopeBitmap;
        }
    }
}