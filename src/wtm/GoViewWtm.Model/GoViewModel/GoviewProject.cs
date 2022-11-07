using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;

namespace GoViewWtm.Model.GoViewModel
{
    public class GoviewProject:TopBasePoco
    {
        public new int ID { get; set; }
        [Display(Name = "项目名称")]
        public string ProjectName { get; set; }
        [Display(Name = "项目状态[-1未发布,1发布]")]
        public int State { get; set; }
        [Display(Name = "创建时间")]
        public DateTime? CreateTime { get; set; }
        [Display(Name = "创建人id")]
        public Guid CreateUserId { get; set; }
        [Display(Name = "删除状态[1删除,-1未删除]")]
        public int IsDelete { get; set; }
        [Display(Name = "首页图片")]
        public string IndexImage { get; set; }
        [Display(Name = "项目介绍")]
        public int Remarks { get; set; }

    }
}
