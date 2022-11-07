using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;

namespace GoViewWtm.Model.GoViewModel
{
    public class GoviewProjectData : TopBasePoco
    {
        public new int ID { get; set; }
        [Display(Name = "项目名称")]
        public int ProjectId { get; set; }
        [Display(Name = "创建时间")]
        public DateTime? CreateTime { get; set; }
        [Display(Name = "创建人id")]
        public Guid CreateUserId { get; set; }
        [Display(Name = "存储数据")]
        public byte[] ContentData { get; set; }
    }
}
