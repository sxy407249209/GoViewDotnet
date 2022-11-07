using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using GoViewWtm.Model.GoViewModel;


namespace GoViewWtm.ViewModel.GoViewApi.GoviewProjectDataVMs
{
    public partial class GoviewProjectDataApiListVM : BasePagedListVM<GoviewProjectDataApi_View, GoviewProjectDataApiSearcher>
    {

        protected override IEnumerable<IGridColumn<GoviewProjectDataApi_View>> InitGridHeader()
        {
            return new List<GridColumn<GoviewProjectDataApi_View>>{
                this.MakeGridHeader(x => x.ProjectId),
                this.MakeGridHeader(x => x.CreateTime),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<GoviewProjectDataApi_View> GetSearchQuery()
        {
            var query = DC.Set<GoviewProjectData>()
                .Select(x => new GoviewProjectDataApi_View
                {
				    ID = x.ID,
                    ProjectId = x.ProjectId,
                    CreateTime = x.CreateTime,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class GoviewProjectDataApi_View : GoviewProjectData{

    }
}
