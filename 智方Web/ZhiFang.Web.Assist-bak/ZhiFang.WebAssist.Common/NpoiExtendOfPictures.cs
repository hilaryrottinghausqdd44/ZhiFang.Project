using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace ZhiFang.WebAssist.Common
{
    public class PicturesInfo
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int Dx1 { get; set; }
        public int Dx2 { get; set; }
        public int Dy1 { get; set; }
        public int Dy2 { get; set; }
        public int MinRow { get; set; }
        public int MaxRow { get; set; }
        public int MinCol { get; set; }
        public int MaxCol { get; set; }
        public Byte[] PictureData { get; private set; }
        public PicturesInfo(int dx1, int dx2, int dy1, int dy2, int minRow, int maxRow, int minCol, int maxCol, Byte[] pictureData, int height, int width)
        {
            this.Dx1 = dx1;
            this.Dx2 = dx2;
            this.Dy1 = dy1;
            this.Dy2 = dy2;

            this.MinRow = minRow;
            this.MaxRow = maxRow;
            this.MinCol = minCol;
            this.MaxCol = maxCol;
            this.PictureData = pictureData;

            this.Height = height;
            this.Width = width;
        }
        public PicturesInfo(int minRow, int maxRow, int minCol, int maxCol, Byte[] pictureData)
        {
            this.MinRow = minRow;
            this.MaxRow = maxRow;
            this.MinCol = minCol;
            this.MaxCol = maxCol;
            this.PictureData = pictureData;
        }
    }
    /// <summary>
    /// 获取Excel的已填充的图片信息信息
    /// </summary>
    public static class NpoiExtendOfPictures
    {
        public static List<PicturesInfo> GetAllPictureInfos(this ISheet sheet)
        {
            return sheet.GetAllPictureInfos(null, null, null, null);
        }

        public static List<PicturesInfo> GetAllPictureInfos(this ISheet sheet, int? minRow, int? maxRow, int? minCol, int? maxCol, bool onlyInternal = true)
        {
            if (sheet is HSSFSheet)
            {
                return GetAllPictureInfos((HSSFSheet)sheet, minRow, maxRow, minCol, maxCol, onlyInternal);
            }
            else if (sheet is XSSFSheet)
            {
                return GetAllPictureInfos((XSSFSheet)sheet, minRow, maxRow, minCol, maxCol, onlyInternal);
            }
            else
            {
                throw new Exception("未处理类型，没有为该类型添加：GetAllPicturesInfos()扩展方法！");
            }
        }

        private static List<PicturesInfo> GetAllPictureInfos(HSSFSheet sheet, int? minRow, int? maxRow, int? minCol, int? maxCol, bool onlyInternal)
        {
            List<PicturesInfo> picturesInfoList = new List<PicturesInfo>();

            var shapeContainer = sheet.DrawingPatriarch as HSSFShapeContainer;
            if (null != shapeContainer)
            {
                var shapeList = shapeContainer.Children;
                foreach (var shape in shapeList)
                {
                    if (shape is HSSFPicture && shape.Anchor is HSSFClientAnchor)
                    {
                        var picture = (HSSFPicture)shape;
                        var anchor = (HSSFClientAnchor)shape.Anchor;

                        if (IsInternalOrIntersect(minRow, maxRow, minCol, maxCol, anchor.Row1, anchor.Row2, anchor.Col1, anchor.Col2, onlyInternal))
                        {
                            //ZhiFang.Common.Log.Log.Debug("Dx1:" + anchor.Dx1 + ",Dx2:" + anchor.Dx2 + ",Dy1:" + anchor.Dy1 + ",Dy2:" + anchor.Dy2 + ",Row1:" + anchor.Row1 + ",Row2:" + anchor.Row2 + ",Col1:" + anchor.Col1 + ",Col2:" + anchor.Col2);
                            Size size = picture.GetImageDimension();
                            //ZhiFang.Common.Log.Log.Debug("Height:" + size.Height + ",Width:" + size.Width);
                            picturesInfoList.Add(new PicturesInfo(anchor.Dx1, anchor.Dx2, anchor.Dy1, anchor.Dy2, anchor.Row1, anchor.Row2, anchor.Col1, anchor.Col2, picture.PictureData.Data, size.Height, size.Width));
                        }
                    }
                }
            }

            return picturesInfoList;
        }

        private static List<PicturesInfo> GetAllPictureInfos(XSSFSheet sheet, int? minRow, int? maxRow, int? minCol, int? maxCol, bool onlyInternal)
        {
            List<PicturesInfo> picturesInfoList = new List<PicturesInfo>();

            var documentPartList = sheet.GetRelations();
            foreach (var documentPart in documentPartList)
            {
                if (documentPart is XSSFDrawing)
                {
                    var drawing = (XSSFDrawing)documentPart;
                    var shapeList = drawing.GetShapes();
                    foreach (var shape in shapeList)
                    {
                        if (shape is XSSFPicture)
                        {
                            var picture = (XSSFPicture)shape;
                            var anchor = picture.GetPreferredSize();

                            if (IsInternalOrIntersect(minRow, maxRow, minCol, maxCol, anchor.Row1, anchor.Row2, anchor.Col1, anchor.Col2, onlyInternal))
                            {
                                //ZhiFang.Common.Log.Log.Debug("Dx1:" + anchor.Dx1 + ",Dx2:" + anchor.Dx2 + ",Dy1:" + anchor.Dy1 + ",Dy2:" + anchor.Dy2 + ",Row1:" + anchor.Row1 + ",Row2:" + anchor.Row2 + ",Col1:" + anchor.Col1 + ",Col2:" + anchor.Col2);
                                //返回此图片的原始尺寸
                                Size size = picture.GetImageDimension();
                                //ZhiFang.Common.Log.Log.Debug("Height:" + size.Height + ",Width:" + size.Width);
                                picturesInfoList.Add(new PicturesInfo(anchor.Dx1, anchor.Dx2, anchor.Dy1, anchor.Dy2, anchor.Row1, anchor.Row2, anchor.Col1, anchor.Col2, picture.PictureData.Data, size.Height, size.Width));
                            }
                        }
                    }
                }
            }

            return picturesInfoList;
        }

        private static bool IsInternalOrIntersect(int? rangeMinRow, int? rangeMaxRow, int? rangeMinCol, int? rangeMaxCol,
            int pictureMinRow, int pictureMaxRow, int pictureMinCol, int pictureMaxCol, bool onlyInternal)
        {
            int _rangeMinRow = rangeMinRow ?? pictureMinRow;
            int _rangeMaxRow = rangeMaxRow ?? pictureMaxRow;
            int _rangeMinCol = rangeMinCol ?? pictureMinCol;
            int _rangeMaxCol = rangeMaxCol ?? pictureMaxCol;

            if (onlyInternal)
            {
                return (_rangeMinRow <= pictureMinRow && _rangeMaxRow >= pictureMaxRow &&
                        _rangeMinCol <= pictureMinCol && _rangeMaxCol >= pictureMaxCol);
            }
            else
            {
                return ((Math.Abs(_rangeMaxRow - _rangeMinRow) + Math.Abs(pictureMaxRow - pictureMinRow) >= Math.Abs(_rangeMaxRow + _rangeMinRow - pictureMaxRow - pictureMinRow)) &&
                (Math.Abs(_rangeMaxCol - _rangeMinCol) + Math.Abs(pictureMaxCol - pictureMinCol) >= Math.Abs(_rangeMaxCol + _rangeMinCol - pictureMaxCol - pictureMinCol)));
            }
        }
    }
}
