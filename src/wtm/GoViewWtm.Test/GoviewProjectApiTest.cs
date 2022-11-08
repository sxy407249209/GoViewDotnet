using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using GoViewWtm.Controllers;
using GoViewWtm.ViewModel.GoViewApi.GoviewProjectVMs;
using GoViewWtm.Model.GoViewModel;
using GoViewWtm.DataAccess;


namespace GoViewWtm.Test
{
    [TestClass]
    public class GoviewProjectApiTest
    {
        private GoviewProjectApiController _controller;
        private string _seed;

        public GoviewProjectApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<GoviewProjectApiController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

       

        [TestMethod]
        public void CreateTest()
        {
            GoviewProjectApiVM vm = _controller.Wtm.CreateVM<GoviewProjectApiVM>();
            GoviewProject v = new GoviewProject();
            
            v.ID = 50;
            v.ProjectName = "hQfVW4Xn8IvcEmds";
            v.State = 66;
            v.CreateTime = DateTime.Parse("2024-03-08 22:41:44");
            v.IsDelete = 58;
            v.IndexImage = "2i";
            v.Remarks = "83";
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<GoviewProject>().Find(v.ID);
                
                Assert.AreEqual(data.ID, 50);
                Assert.AreEqual(data.ProjectName, "hQfVW4Xn8IvcEmds");
                Assert.AreEqual(data.State, 66);
                Assert.AreEqual(data.CreateTime, DateTime.Parse("2024-03-08 22:41:44"));
                Assert.AreEqual(data.IsDelete, 58);
                Assert.AreEqual(data.IndexImage, "2i");
                Assert.AreEqual(data.Remarks, 83);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            GoviewProject v = new GoviewProject();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.ID = 50;
                v.ProjectName = "hQfVW4Xn8IvcEmds";
                v.State = 66;
                v.CreateTime = DateTime.Parse("2024-03-08 22:41:44");
                v.IsDelete = 58;
                v.IndexImage = "2i";
                v.Remarks = "83";
                context.Set<GoviewProject>().Add(v);
                context.SaveChanges();
            }

            GoviewProjectApiVM vm = _controller.Wtm.CreateVM<GoviewProjectApiVM>();
            var oldID = v.ID;
            v = new GoviewProject();
            v.ID = oldID;
       		
            v.ProjectName = "DsPHCfV";
            v.State = 41;
            v.CreateTime = DateTime.Parse("2022-03-29 22:41:44");
            v.IsDelete = 43;
            v.IndexImage = "gk";
            v.Remarks = "38";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.ID", "");
            vm.FC.Add("Entity.ProjectName", "");
            vm.FC.Add("Entity.State", "");
            vm.FC.Add("Entity.CreateTime", "");
            vm.FC.Add("Entity.IsDelete", "");
            vm.FC.Add("Entity.IndexImage", "");
            vm.FC.Add("Entity.Remarks", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<GoviewProject>().Find(v.ID);
 				
                Assert.AreEqual(data.ProjectName, "DsPHCfV");
                Assert.AreEqual(data.State, 41);
                Assert.AreEqual(data.CreateTime, DateTime.Parse("2022-03-29 22:41:44"));
                Assert.AreEqual(data.IsDelete, 43);
                Assert.AreEqual(data.IndexImage, "gk");
                Assert.AreEqual(data.Remarks, 38);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            GoviewProject v = new GoviewProject();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.ID = 50;
                v.ProjectName = "hQfVW4Xn8IvcEmds";
                v.State = 66;
                v.CreateTime = DateTime.Parse("2024-03-08 22:41:44");
                v.IsDelete = 58;
                v.IndexImage = "2i";
                v.Remarks = "83";
                context.Set<GoviewProject>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            GoviewProject v1 = new GoviewProject();
            GoviewProject v2 = new GoviewProject();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ID = 50;
                v1.ProjectName = "hQfVW4Xn8IvcEmds";
                v1.State = 66;
                v1.CreateTime = DateTime.Parse("2024-03-08 22:41:44");
                v1.IsDelete = 58;
                v1.IndexImage = "2i";
                v1.Remarks = "83";
                v2.ID = 13;
                v2.ProjectName = "DsPHCfV";
                v2.State = 41;
                v2.CreateTime = DateTime.Parse("2022-03-29 22:41:44");
                v2.IsDelete = 43;
                v2.IndexImage = "gk";
                v2.Remarks = "38";
                context.Set<GoviewProject>().Add(v1);
                context.Set<GoviewProject>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<GoviewProject>().Find(v1.ID);
                var data2 = context.Set<GoviewProject>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }

            rv = _controller.BatchDelete(new string[] {});
            Assert.IsInstanceOfType(rv, typeof(OkResult));

        }


    }
}
