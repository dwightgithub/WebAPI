using ExifLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Repositories;

namespace WebAPI
{
    public class ImageGetter
    {
        static string[] files;
        static Random rand = new Random();
        static ImageGetter(){
            files = Directory.GetFiles(@"D:\街景\成都街景2021");
        }


        public static void WriteImageToDB()
        {
            var listEXIF = new List<ImageInfoEntity>();

            foreach (var file in files)
            {
                listEXIF.Add(GetEXIF(file));
            }
        }

        public static ImageInfoEntity GetEXIF(string filePath)
        {
            var er = new ExifReader(filePath);
            var fileName = new FileInfo(filePath).Name;
            
            Double[] longitude,latitude;
            DateTime date;

            er.GetTagValue<Double[]>(ExifTags.GPSLongitude, out longitude);
            er.GetTagValue<Double[]>(ExifTags.GPSLatitude, out latitude);
            er.GetTagValue<DateTime>(ExifTags.DateTime, out date);
            var strLongitude = longitudeTrans(longitude);
            var strLatitude = longitudeTrans(latitude);

            var imageInfo = new ImageInfoEntity()
            {
                FileName = fileName,
                Date = date,
                //Lantitude = strLatitude,
                //Longitude = strLongitude,
                Province="四川省",
                City="成都市",
                UploaderName="thw",
                //Rate = (float)(rand.Next(5)+0.1*rand.Next(10))
            };

            return imageInfo;
        }

        private static string longitudeTrans(Double[] longi)
        {
            //度 + 分 / 60 + 秒 / 3600
            double a = longi[0] + longi[1] / 60 + longi[2] / 3600; 
            return a.ToString();
        }
    }
}
