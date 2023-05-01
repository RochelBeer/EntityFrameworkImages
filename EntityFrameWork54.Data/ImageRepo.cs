using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameWork54.Data
{
    public class ImageRepo
    {
        private string _connectionString;
        public ImageRepo(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Image> GetImages()
        {
            using var context = new ImageDbContext(_connectionString);
            return context.Images.ToList();
        }
        public Image GetImageById(int id)
        {
            using var context = new ImageDbContext(_connectionString);
            return context.Images.FirstOrDefault(image => image.Id == id);
        }
        public void AddImage(Image image)
        {
            using var context = new ImageDbContext(_connectionString);
            context.Add(image);
            context.SaveChanges();
        }
        public void AddLike(int id)
        {
            using var context = new ImageDbContext(_connectionString);
            Image dbImage = GetImageById(id);
            dbImage.Likes = dbImage.Likes + 1;
            context.Entry(dbImage).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
