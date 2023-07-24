using IronBarCode;
using Library.API.Interface;
using Library.API.Models;
using System.Drawing;

namespace Library.API.Service
{
    public class QrService : IQrService
    {
        public void Print(IEnumerable<GeneratedBarcode> qrCodes, string path, int margin = 20, int maxWidth = 595)
        {
            int qrHeight = qrCodes.Max(c => c.Height);
            int qrWidth = qrCodes.Max(c => c.Width);

            var maxHeight = (maxWidth / qrWidth) - 1;
            maxHeight = qrCodes.Count() / maxHeight + 2;
            maxHeight = (qrHeight + margin) * maxHeight;

            Bitmap result = new Bitmap(maxWidth, maxHeight);

            using (Graphics g = Graphics.FromImage(result))
            {
                var p = new Point(margin, margin);
                foreach (var img in qrCodes)
                {
                    g.DrawImage(img.ToBitmap(), p);

                    if (p.X + 2 * qrWidth + margin >= maxWidth)
                        p.Offset(-p.X - qrWidth, qrHeight + margin);
                    p.Offset(img.Width + margin, 0);
                }
            }
            result.Save(path);
        }

        public GeneratedBarcode GenerateCode(string text, QrType? type)
        {
            switch (type)
            {
                case QrType.book:
                    var qrCode = IronBarCode.QRCodeWriter.CreateQrCodeWithLogo(text, "BookLogoPath").ChangeBarCodeColor(Color.Green);
                    return qrCode;

                case QrType.shelf:
                    return IronBarCode.QRCodeWriter.CreateQrCodeWithLogo(text, "ShelfNamePath").ChangeBarCodeColor(Color.Green);

                default:
                    return IronBarCode.QRCodeWriter.CreateQrCode(text).ChangeBarCodeColor(Color.Green);
            }
        }
    }
}
