using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    [Table("rate")]
    [Serializable]
    public class RateEntity
    {
        [NotMapped()]
        /// <summary>
        /// 同一批次中的编号
        /// </summary>
        public int IDinList { get; set; }

        [Key]//主键
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//自增列
        [Column("idrate")]
        public int RateID { get; set; }

        [Column("picID"), ForeignKey("ImageInfo")]
        public int PicID { get; set; }

        public ImageInfoEntity ImageInfo { get; set; }


        [Column("taskID"),ForeignKey("TaskEntity")]
        public int TaskID { get; set; }

        [JsonIgnore]
        public TaskEntity TaskEntity { get; set; }


        [Column("rateScore")]
        public float RateScore { get; set; }
        
        [Column("rateTime")]
        public DateTime RateTime { get; set; }
    }
}
