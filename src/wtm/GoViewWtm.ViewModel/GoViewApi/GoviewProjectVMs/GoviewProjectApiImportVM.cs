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
    public partial class GoviewProjectApiTemplateVM : BaseTemplateVM
    {
        [Display(Name = "项目名称")]
        public ExcelPropety ProjectName_Excel = ExcelPropety.CreateProperty<GoviewProject>(x => x.ProjectName);
        [Display(Name = "项目状态[-1未发布,1发布]")]
        public ExcelPropety State_Excel = ExcelPropety.CreateProperty<GoviewProject>(x => x.State);
        [Display(Name = "创建时间")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<GoviewProject>(x => x.CreateTime);
        [Display(Name = "删除状态[1删除,-1未删除]")]
        public ExcelPropety IsDelete_Excel = ExcelPropety.CreateProperty<GoviewProject>(x => x.IsDelete);
        [Display(Name = "首页图片")]
        public ExcelPropety IndexImage_Excel = ExcelPropety.CreateProperty<GoviewProject>(x => x.IndexImage);
        [Display(Name = "项目介绍")]
        public ExcelPropety Remarks_Excel = ExcelPropety.CreateProperty<GoviewProject>(x => x.Remarks);

	    protected override void InitVM()
        {
        }

    }

    public class GoviewProjectApiImportVM : BaseImportVM<GoviewProjectApiTemplateVM, GoviewProject>
    {

    }

}
