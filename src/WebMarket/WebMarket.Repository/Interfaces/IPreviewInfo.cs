using System.Collections.Generic;

namespace WebMarket.Repository.Interfaces
{
    public interface IPreviewInfo
    {
        string GetListPhoto();
        string GetPreview();
        Dictionary<string, string> GetPhotos();
    }
}
