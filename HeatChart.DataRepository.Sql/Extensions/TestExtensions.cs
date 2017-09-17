using HeatChart.DataRepository.Sql.Interfaces;
using HeatChart.Entities.Sql;
using HeatChart.Entities.Sql.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatChart.DataRepository.Sql.Extensions
{
    public static class TestExtensions
    {
        public static Test GetSingleByTestName(this IEFRepository<Test> TestRepository, string name)
        {
            return TestRepository.GetFirstOrDefault(x => x.Name.Equals(name));
        }

        public static Test GetSingleByTestID(this IEFRepository<Test> TestRepository, int id)
        {
            return TestRepository.GetFirstOrDefault(x => x.ID == id);
        }

        public static bool TestExists(this IEFRepository<Test> testsRepository, string name)
        {
            return testsRepository.GetAll()
                .Any(c => c.Name.ToLower().Equals(name.ToLower()));
        }
    }
}
