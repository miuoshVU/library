using IronBarCode;
using Library.API.Models;

namespace Library.API.Interface
{
    public interface IQrService
    {
        public GeneratedBarcode GenerateCode(string text, QrType? type);
        public void Print(IEnumerable<GeneratedBarcode> qrCodes, string path, int margin = 20, int maxWidth = 595);
    }
}