using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("userinfo")]
    [Serializable]
    public class UserEntity
    {
        [Key]//主键
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//自增列
        [Column("idUserInfo")]
        public int IdUserInfo { get; set; }

        [Column("name")]
        public string Name { get; set; }
        [Column("nickName")]
        public string NickName { get; set; }
        [Column("faceID")]
        public int FaceID { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [Column("createDate")]
        public DateTime CreateDate { get; set; }
        [Column("lastLoginDate")]
        public DateTime LastLoginDate { get; set; }

        [Column("age")]
        public int Age { get; set; }

        [Column("score")]
        public int Score { get; set; }

        [Column("experience")]
        public int Experience { get; set; }

        public virtual List<TaskEntity> Tasks { get; set; }
    }
}
