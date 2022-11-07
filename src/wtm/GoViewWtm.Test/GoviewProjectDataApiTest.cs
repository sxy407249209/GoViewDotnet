using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using GoViewWtm.Controllers;
using GoViewWtm.ViewModel.GoViewApi.GoviewProjectDataVMs;
using GoViewWtm.Model.GoViewModel;
using GoViewWtm.DataAccess;


namespace GoViewWtm.Test
{
    [TestClass]
    public class GoviewProjectDataApiTest
    {
        private GoviewProjectDataApiController _controller;
        private string _seed;

        public GoviewProjectDataApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<GoviewProjectDataApiController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new GoviewProjectDataApiSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            GoviewProjectDataApiVM vm = _controller.Wtm.CreateVM<GoviewProjectDataApiVM>();
            GoviewProjectData v = new GoviewProjectData();
            
            v.ID = 64;
            v.ProjectId = 92;
            v.CreateTime = DateTime.Parse("2023-01-14 22:39:40");
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<GoviewProjectData>().Find(v.ID);
                
                Assert.AreEqual(data.ID, 64);
                Assert.AreEqual(data.ProjectId, 92);
                Assert.AreEqual(data.CreateTime, DateTime.Parse("2023-01-14 22:39:40"));
            }
        }

        [TestMethod]
        public void EditTest()
        {
            GoviewProjectData v = new GoviewProjectData();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.ID = 64;
                v.ProjectId = 92;
                v.CreateTime = DateTime.Parse("2023-01-14 22:39:40");
                context.Set<GoviewProjectData>().Add(v);
                context.SaveChanges();
            }

            GoviewProjectDataApiVM vm = _controller.Wtm.CreateVM<GoviewProjectDataApiVM>();
            var oldID = v.ID;
            v = new GoviewProjectData();
            v.ID = oldID;
       		
            v.ProjectId = 85;
            v.CreateTime = DateTime.Parse("2023-10-17 22:39:40");
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.ID", "");
            vm.FC.Add("Entity.ProjectId", "");
            vm.FC.Add("Entity.CreateTime", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<GoviewProjectData>().Find(v.ID);
 				
                Assert.AreEqual(data.ProjectId, 85);
                Assert.AreEqual(data.CreateTime, DateTime.Parse("2023-10-17 22:39:40"));
            }

        }

		[TestMethod]
        public void GetTest()
        {
            GoviewProjectData v = new GoviewProjectData();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.ID = 64;
                v.ProjectId = 92;
                v.CreateTime = DateTime.Parse("2023-01-14 22:39:40");
                context.Set<GoviewProjectData>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            GoviewProjectData v1 = new GoviewProjectData();
            GoviewProjectData v2 = new GoviewProjectData();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ID = 64;
                v1.ProjectId = 92;
                v1.CreateTime = DateTime.Parse("2023-01-14 22:39:40");
                v2.ID = 69;
                v2.ProjectId = 85;
                v2.CreateTime = DateTime.Parse("2023-10-17 22:39:40");
                context.Set<GoviewProjectData>().Add(v1);
                context.Set<GoviewProjectData>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<GoviewProjectData>().Find(v1.ID);
                var data2 = context.Set<GoviewProjectData>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }

            rv = _controller.BatchDelete(new string[] {});
            Assert.IsInstanceOfType(rv, typeof(OkResult));

        }


    }
}
