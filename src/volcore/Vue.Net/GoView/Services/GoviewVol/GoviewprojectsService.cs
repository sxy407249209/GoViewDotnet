/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下GoviewprojectsService与IGoviewprojectsService中编写
 */
using GoView.IRepositories;
using GoView.IServices;
using VOL.Core.BaseProvider;
using VOL.Core.Extensions.AutofacManager;
using VOL.Entity.DomainModels;

namespace GoView.Services
{
    public partial class GoviewprojectsService : ServiceBase<Goviewprojects, IGoviewprojectsRepository>
    , IGoviewprojectsService, IDependency
    {
    public GoviewprojectsService(IGoviewprojectsRepository repository)
    : base(repository)
    {
    Init(repository);
    }
    public static IGoviewprojectsService Instance
    {
      get { return AutofacContainerModule.GetService<IGoviewprojectsService>(); } }
    }
 }
