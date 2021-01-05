using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebAPI.Models
{
    [Table("task")]
    [Serializable]
    public class TaskEntity
    {
        [Key]//主键
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//自增列
        [Column("IDtask")]
        public int IDtask { get; set; }

        [Column("UserID"),ForeignKey("userEntity")]
        public int UserID { get; set; }

        [JsonIgnore]
        public UserEntity userEntity { get; set; }

        // 0是评分，1是对比
        [Column("TaskType")]
        public int TaskType { get; set; }

        [Column("CreateTime")]
        public DateTime CreateTime { get; set; }
        
        [Column("CompleteTime")]
        public DateTime CompleteTime { get; set; }

        [Column("TaskCount")]
        public int TaskCount { get; set; }

        // 0:美观 1:安全 2:天气
        [Column("Level")]
        public int Level { get; set; }

        [Column("Area")]
        public string Area { get; set; }

        public virtual List<RateEntity> Rates { get; set; }

    }
}
