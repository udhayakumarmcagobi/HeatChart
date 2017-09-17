using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HeatChart.DataRepository.Sql.Interfaces;
using HeatChart.Entities.Sql;
using HeatChart.DataRepository.Sql.EFRepository;
using HeatChart.DataRepository.Sql.Infrastructure;
using HeatChart.MockData.Sql;
using HeatChart.DataRepository.Sql.UnitOfWork;
using System.Data.Common;
using System.Linq;

namespace HeatChart.Core.Test
{
    /// <summary>
    /// Summary description for MaterialRegisterHeaderTest
    /// </summary>
    [TestClass]
    public class MaterialRegisterHeaderTest
    {
        DbConnection connection;
        TestHeatChartContext databaseContext;       

        [TestInitialize]
        public void Initialize()
        {
            DBFactory dbFactory = new DBFactory();
          
            connection = Effort.DbConnectionFactory.CreateTransient();
            databaseContext = new TestHeatChartContext(connection);
           

        }

        [TestMethod]
        public void MaterialRegisterHeader_Repository_Create()
        {
            IEFRepository<MaterialRegisterHeader> materialHeaderRepository = new EFRepository<MaterialRegisterHeader>(new EFUnitOfWork());
            //Arrange

            var c = MaterialRegisterHeaderMockData.GetMaterialRegisterHeader();
            //Act
            materialHeaderRepository.Insert(c);
            materialHeaderRepository.Commit();
            databaseContext.SaveChanges();

            //Assert
        }


        [TestMethod]
        public void MaterialRegisterHeader_Repository_Get()
        {
            IEFRepository<MaterialRegisterHeader> materialHeaderRepository = new EFRepository<MaterialRegisterHeader>(new EFUnitOfWork());
            var result  = materialHeaderRepository.GetAll().ToList();

            var newResult = result;
            //Assert
        }
    }
}
