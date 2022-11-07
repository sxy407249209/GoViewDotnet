using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using GoViewWtm.Model.GoViewModel;


namespace GoViewWtm.ViewModel.GoViewApi.GoviewProjectDataVMs
{
    public partial class GoviewProjectDataApiTemplateVM : BaseTemplateVM
    {
        [Display(Name = "项目名称")]
        public ExcelPropety ProjectId_Excel = ExcelPropety.CreateProperty<GoviewProjectData>(x => x.ProjectId);
        [Display(Name = "创建时间")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<GoviewProjectData>(x => x.CreateTime);

	    protected override void InitVM()
        {
        }

    }

    public class GoviewProjectDataApiImportVM : BaseImportVM<GoviewProjectDataApiTemplateVM, GoviewProjectData>
    {

    }

}
