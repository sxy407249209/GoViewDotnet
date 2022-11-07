using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using GoViewWtm.Model.GoViewModel;


namespace GoViewWtm.ViewModel.GoViewApi.GoviewProjectVMs
{
    public partial class GoviewProjectApiSearcher : BaseSearcher
    {
        [Display(Name = "项目名称")]
        public String ProjectName { get; set; }
        [Display(Name = "项目介绍")]
        public Int32? Remarks { get; set; }

        protected override void InitVM()
        {
        }

    }
}
