using System.Collections.Generic;

namespace WebMarket.DAL.Interfaces
{
    public interface IPreviewInfo
    {
        string GetListPhoto();
        string GetPreview();
        Dictionary<string, string> GetPhotos();
    }
}
