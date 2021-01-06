using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    [Table("pairwise")]
    [Serializable]
    public class PairwiseEntity
    {
        [NotMapped()]
        /// <summary>
        /// 同一批次中的编号
        /// </summary>
        public int IDinList { get; set; }

        [Key]//主键
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//自增列
        [Column("idPairwise")]
        public int PairwiseID { get; set; }

        [Column("picAID"), ForeignKey("ImageAInfo")]
        public int PicAID { get; set; }

        public ImageInfoEntity ImageAInfo { get; set; }

        [Column("picBID"), ForeignKey("ImageBInfo")]
        public int PicBID { get; set; }

        public ImageInfoEntity ImageBInfo { get; set; }


        [Column("taskID"), ForeignKey("TaskEntity")]
        public int TaskID { get; set; }

        [JsonIgnore]
        public TaskEntity TaskEntity { get; set; }


        [Column("pairWinner")]
        public int Pairwinner { get; set; }

        [Column("pairTime")]
        public DateTime PairTime { get; set; }
    }
}
