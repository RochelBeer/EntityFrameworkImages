using EntityFrameWork54.Data;

namespace EntityFrameWork54.Web.Models
{
    public class ViewModel
    {
        public List<Image> Images { get; set; }
        public Image Image { get; set; }
        public bool CannotLike { get; set; }
    }
}
