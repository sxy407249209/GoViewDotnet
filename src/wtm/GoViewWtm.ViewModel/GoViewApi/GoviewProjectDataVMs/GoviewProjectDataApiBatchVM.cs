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
    public partial class GoviewProjectDataApiBatchVM : BaseBatchVM<GoviewProjectData, GoviewProjectDataApi_BatchEdit>
    {
        public GoviewProjectDataApiBatchVM()
        {
            ListVM = new GoviewProjectDataApiListVM();
            LinkedVM = new GoviewProjectDataApi_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class GoviewProjectDataApi_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
