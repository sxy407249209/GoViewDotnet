using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using GoViewWtm.Model.GoViewModel;


namespace GoViewWtm.ViewModel.GoViewApi.GoviewProjectVMs
{
    public partial class GoviewProjectApiListVM : BasePagedListVM<GoviewProjectApi_View, GoviewProjectApiSearcher>
    {

        protected override IEnumerable<IGridColumn<GoviewProjectApi_View>> InitGridHeader()
        {
            return new List<GridColumn<GoviewProjectApi_View>>{
                this.MakeGridHeader(x => x.ProjectName),
                this.MakeGridHeader(x => x.State),
                this.MakeGridHeader(x => x.CreateTime),
                this.MakeGridHeader(x => x.IsDelete),
                this.MakeGridHeader(x => x.IndexImage),
                this.MakeGridHeader(x => x.Remarks),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<GoviewProjectApi_View> GetSearchQuery()
        {
            var query = DC.Set<GoviewProject>()
                .Where(x=>x.IsDelete==-1)
                .CheckContain(Searcher.ProjectName, x=>x.ProjectName)
                .CheckContain(Searcher.Remarks, x=>x.Remarks)
                .Select(x => new GoviewProjectApi_View
                {
				    ID = x.ID,
                    ProjectName = x.ProjectName,
                    State = x.State,
                    CreateTime = x.CreateTime,
                    IsDelete = x.IsDelete,
                    IndexImage = x.IndexImage,
                    Remarks = x.Remarks,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class GoviewProjectApi_View : GoviewProject{

    }
}
