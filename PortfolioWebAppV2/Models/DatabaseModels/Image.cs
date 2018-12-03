using System.ComponentModel.DataAnnotations;

namespace PortfolioWebAppV2.Models.DatabaseModels
{
    public enum ImageType
    {
        ScreenShot,
        Icon
    }

    public class Image
    {
        public int ImageId { get; set; }
        [Required(ErrorMessage = "Nazwa pliku nie może być pusta")]
        public string FileName { get; set; }
        public ImageType ImageType { get; set; }
        public bool Favorite { get; set; }
        public string Guid { get; set; }
        public string GetLink()
        {
            switch (ImageType)
            {
                case ImageType.Icon:
                    return "damiandziuraportfolio.blob.core.windows.net/icons/" + Guid + FileName;
                case ImageType.ScreenShot:
                    return "damiandziuraportfolio.blob.core.windows.net/ScreenShots/" + Guid + FileName;
                default:
                    return null;
            }
        }
    }
}