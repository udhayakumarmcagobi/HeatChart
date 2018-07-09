using HeatChart.Entities.Sql;
using HeatChart.Entities.Sql.Domain;
using HeatChart.Infrastructure.Dependency.Core;
using HeatHeatChart.ViewModels.Domain;
using ModelMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Caching;
using System.Web;

namespace HeatChart.Web.Core
{
    public class HeatChartBase : ApiControllerBaseExtended
    {
        #region Constructors
        public HeatChartBase(IDataRepositoryFactory dataRepositoryFactory)
            : base(dataRepositoryFactory)
        {
              
        }

        #endregion

        #region Private Variables

        private List<CustomerVM> _customerList = null;
        private List<SupplierVM> _supplierList = null;
        private List<ThirdPartyInspectionVM> _thirdPartyInspectionList = null;
        private List<RawMaterialFormVM> _rawMaterialFormList = null;
        private List<SpecificationsVM> _specificationList = null;
        private List<DimensionVM> _dimensionList = null;
        private List<TestVM> _testList = null;

        private List<MaterialRegisterHeader> _materialRegisterHeaders = null;
        private List<MaterialRegisterSubSeries> _materialRegisterSubSeries = null;
        private List<HeatChartHeader> _heatChartHeaders = null;

        protected HttpRequestMessage currentRequestMessage { get; set; }

        private static MemoryCache _cache = MemoryCache.Default;

        #endregion

        #region Properties
        protected List<CustomerVM> CustomerList
        {
            get
            {
                if (_customerList == null)
                {
                    _customersRepository = GetCurrentDataRepository<Customer>(currentRequestMessage);
                    var customerList = _customersRepository.GetAll().OrderByDescending(x => x.ModifiedOn).ToList();

                    _customerList = AutoMapper.Map<List<Customer>, List<CustomerVM>>(customerList);
                }
                return _customerList;
            }
        }

        protected List<SupplierVM> SupplierList
        {
            get
            {
                if (_supplierList == null)
                {
                    _suppliersRepository = GetCurrentDataRepository<Supplier>(currentRequestMessage);
                    var supplierList = _suppliersRepository.GetAll().OrderByDescending(x => x.ModifiedOn).ToList();

                    _supplierList = AutoMapper.Map<List<Supplier>, List<SupplierVM>>(supplierList);
                }
                return _supplierList;
            }
        }

        protected List<ThirdPartyInspectionVM> ThirdPartyInspectionList
        {
            get
            {
                if (_thirdPartyInspectionList == null)
                {
                    _thirdPartyInspectionsRepository = GetCurrentDataRepository<ThirdPartyInspection>(currentRequestMessage);
                    var thirdPartyInspectionList = _thirdPartyInspectionsRepository.GetAll().OrderByDescending(x => x.ModifiedOn).ToList();

                    _thirdPartyInspectionList = AutoMapper.Map<List<ThirdPartyInspection>, List<ThirdPartyInspectionVM>>(thirdPartyInspectionList);
                }
                return _thirdPartyInspectionList;
            }
        }

        protected List<RawMaterialFormVM> RawMaterialFormList
        {
            get
            {
                if (_rawMaterialFormList == null)
                {
                    _rawMaterialFormsRepository = GetCurrentDataRepository<RawMaterialForm>(currentRequestMessage);
                    var rawMaterialFormList = _rawMaterialFormsRepository.GetAll().OrderByDescending(x => x.ModifiedOn).ToList();

                    _rawMaterialFormList = AutoMapper.Map<List<RawMaterialForm>, List<RawMaterialFormVM>>(rawMaterialFormList);
                }
                return _rawMaterialFormList;
            }
        }

        protected List<SpecificationsVM> SpecificationList
        {
            get
            {
                if (_specificationList == null)
                {
                    _specificationsRepository = GetCurrentDataRepository<Specifications>(currentRequestMessage);
                    var specificationList = _specificationsRepository.GetAll().OrderByDescending(x => x.ModifiedOn).ToList();

                    _specificationList = AutoMapper.Map<List<Specifications>, List<SpecificationsVM>>(specificationList);
                }
                return _specificationList;
            }
        }

        protected List<DimensionVM> DimensionList
        {
            get
            {
                if (_dimensionList == null)
                {
                    _dimensionRepository = GetCurrentDataRepository<Dimension>(currentRequestMessage);
                    var dimensionList = _dimensionRepository.GetAll().OrderByDescending(x => x.ModifiedOn).ToList();

                    _dimensionList = AutoMapper.Map<List<Dimension>, List<DimensionVM>>(dimensionList);
                }
                return _dimensionList;
            }
        }

        protected List<TestVM> TestList
        {
            get
            {
                if (_testList == null)
                {
                    _testsRepository = GetCurrentDataRepository<Test>(currentRequestMessage);
                    var testList = _testsRepository.GetAll().OrderByDescending(x => x.ModifiedOn).ToList();

                    _testList = AutoMapper.Map<List<Test>, List<TestVM>>(testList);
                }
                return _testList;
            }
        }

        protected List<MaterialRegisterHeader> MaterialRegisterHeaders
        {
            get
            {
                if (_materialRegisterHeaders == null)
                {
                    if (!_cache.Contains("MaterialRegisterHeaders"))
                    {
                        _materialRegisterHeaders = RefreshMaterialRegisterHeader();
                        CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();

                        cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddDays(30);

                        _cache.Add("MaterialRegisterHeaders", _materialRegisterHeaders, cacheItemPolicy);
                    }
                    else
                    {
                        _materialRegisterHeaders = _cache.Get("MaterialRegisterHeaders") as List<MaterialRegisterHeader>;
                    }
                }
                return _materialRegisterHeaders;
            }
        }

        protected List<MaterialRegisterSubSeries> MaterialRegisterSubSeries
        {
            get
            {
                if (_materialRegisterSubSeries== null)
                {
                    if (!_cache.Contains("MaterialRegisterSubSeries"))
                    {
                        _materialRegisterSubSeries = RefreshMaterialRegisterSubSeries();
                        CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();

                        cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddDays(30);

                        _cache.Add("MaterialRegisterSubSeries", _materialRegisterSubSeries, cacheItemPolicy);
                    }
                    else
                    {
                        _materialRegisterSubSeries = _cache.Get("MaterialRegisterSubSeries") as List<MaterialRegisterSubSeries>;
                    }
                }
                return _materialRegisterSubSeries;
            }
        }

        protected List<HeatChartHeader> HeatChartHeaders        {
            get
            {
                if (_heatChartHeaders == null)
                {

                    _heatChartHeadersRepository = GetCurrentDataRepository<HeatChartHeader>(currentRequestMessage);
                    _heatChartHeaders = _heatChartHeadersRepository.GetAll().OrderByDescending(x => x.ModifiedOn).ToList();
                }
                return _heatChartHeaders;
            }
        }

        #endregion

        #region Referesh Cache

        private List<MaterialRegisterHeader> RefreshMaterialRegisterHeader()
        {
            _materialRegisterHeadersRepository = GetCurrentDataRepository<MaterialRegisterHeader>(currentRequestMessage);
            _materialRegisterHeaders = _materialRegisterHeadersRepository.GetAll().OrderByDescending(x => x.ModifiedOn).ToList();

            return _materialRegisterHeaders;
        }

        private List<MaterialRegisterSubSeries> RefreshMaterialRegisterSubSeries()
        {
            _materialRegisterSubseriessRepository = GetCurrentDataRepository<MaterialRegisterSubSeries>(currentRequestMessage);
            _materialRegisterSubSeries = _materialRegisterSubseriessRepository.GetAll().ToList();

            return _materialRegisterSubSeries;
        }

        #endregion

        #region Private Methods
        private List<Type> GetRequiredRepositories()
        {
            return new List<Type>()
            {
                typeof(MaterialRegisterHeader),
                typeof(Customer),
                typeof(Supplier),
                typeof(ThirdPartyInspection),
                typeof(RawMaterialForm),
                typeof(Specifications),
                typeof(Test)
            };
        }

        #endregion

    }
}