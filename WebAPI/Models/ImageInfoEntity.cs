using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    [Table("imageinfo")]
    [Serializable]
    public class ImageInfoEntity
    {
        [Key]//主键
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//自增列
        [Column("idImageInfo")]
        public int ImageInfoID { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        [Column("city")]
        public string City { get; set; }

        /// <summary>
        /// 拍摄时间
        /// </summary>
        [Column("date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// 具体地点
        /// </summary>
        [Column("detailLocation")]
        public string DetailLocation { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        [Column("district")]
        public string District { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        [Column("fileName")]
        public string FileName { get; set; }
        
        /// <summary>
        /// 省份
        /// </summary>
        [Column("province")]
        public string Province { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        [Column("longitude")]
        public float Longitude { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>

        [Column("lantitude")]
        public float Lantitude { get; set; }

        /// <summary>
        /// 图片上传者名称
        /// </summary>
        [Column("uploaderName")]
        public string UploaderName { get; set; }

        /// <summary>
        /// 该图片历史评分
        /// </summary>
        [Column("rate")]
        public float Rate { get; set; }
    }
}
