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
    public partial class GoviewProjectApiBatchVM : BaseBatchVM<GoviewProject, GoviewProjectApi_BatchEdit>
    {
        public GoviewProjectApiBatchVM()
        {
            ListVM = new GoviewProjectApiListVM();
            LinkedVM = new GoviewProjectApi_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class GoviewProjectApi_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
