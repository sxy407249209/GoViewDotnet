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
    [Entity(TableCnName = "大屏项目",TableName = "goviewprojects")]
    public partial class goviewprojects:BaseEntity
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
       [Display(Name ="ProjectName")]
       [Column(TypeName="nvarchar(max)")]
       [Editable(true)]
       public string ProjectName { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="State")]
       [Column(TypeName="int")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public int State { get; set; }

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
       public Guid CreateUserId { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="IsDelete")]
       [Column(TypeName="int")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public int IsDelete { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="IndexImage")]
       [Column(TypeName="nvarchar(max)")]
       [Editable(true)]
       public string IndexImage { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="Remarks")]
       [Column(TypeName="nvarchar(max)")]
       [Editable(true)]
       public string Remarks { get; set; }

       
    }
}