using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Web;

namespace TastyMVC.Models
{
    public class Image
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
        
        public Image()
        {

        }

        public Image(HttpPostedFileBase httpPostedFileBase)
        {
            using (var binaryReader = new BinaryReader(httpPostedFileBase.InputStream))
            {
                Data = binaryReader.ReadBytes(httpPostedFileBase.ContentLength);
            }
            Name = httpPostedFileBase.FileName;
        }

        public Image(HttpPostedFile image)
        {
            using (var binaryReader = new BinaryReader(image.InputStream))
            {
                Data = binaryReader.ReadBytes(image.ContentLength);
            }
            Name = image.FileName;
        }

        public string GetImageSource()
        {
            var base64 = Convert.ToBase64String(Data);
            var imgSrc = string.Format("data:image/gif;base64,{0}", base64);
            return imgSrc;
        }

    }
}