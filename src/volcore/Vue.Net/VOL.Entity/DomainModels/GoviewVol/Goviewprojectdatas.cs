/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果数据库字段发生变化，请在代码生器重新生成此Model
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOL.Entity.SystemModels;

namespace VOL.Entity.DomainModels
{
    [Entity(TableCnName = "大屏设计数据",TableName = "Goviewprojectdatas")]
    public partial class Goviewprojectdatas:BaseEntity
    {
        /// <summary>
       ///
       /// </summary>
       [Key]
       [Display(Name ="Id")]
       [Column(TypeName="int")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public int Id { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="ProjectId")]
       [Column(TypeName="int")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public int ProjectId { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="CreateTime")]
       [Column(TypeName="datetime")]
       [Editable(true)]
       public DateTime? CreateTime { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="CreateUserId")]
       [MaxLength(36)]
       [Column(TypeName="uniqueidentifier")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public int CreateUserId { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="ContentData")]
       [Column(TypeName="nvarchar(max)")]
       [Editable(true)]
       public byte[] ContentData { get; set; }

       
    }
}