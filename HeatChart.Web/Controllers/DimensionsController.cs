using HeatChart.DataRepository.Sql.Interfaces;
using HeatChart.Entities.Sql.Domain;
using HeatChart.Web.Core;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Linq;
using HeatHeatChart.ViewModels.Domain;
using HeatChart.DataRepository.Sql.Extensions;
using HeatChart.Web.Infrastructure.Extensions;
using System;
using System.Net;
using ModelMapper;

namespace HeatChart.Web.Controllers
{
    [Authorize(Roles = "Admin, User")]
    [RoutePrefix("api/dimensions")]
    public class DimensionsController : ApiControllerBase
    {
        private readonly IEFRepository<Error> _errorRepository;
        private readonly IEFRepository<Dimension> _dimensionsRepository;
        public DimensionsController(IEFRepository<Dimension> dimensionsRepository,
            IEFRepository<Error> errorRepository
           ) : base(errorRepository)
        {
            _errorRepository = errorRepository;
            _dimensionsRepository = dimensionsRepository;
        }


        [HttpGet]
        [Route("search/{page:int=0}/{pageSize=4}/{filter?}")]
        public HttpResponseMessage Search(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {
            int currentPage = page.Value; int currentPageSize = pageSize.Value;

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<Dimension> dimensions = null;

                int totalDimensions = 0;

                if (!string.IsNullOrEmpty(filter))
                {
                    filter = filter.Trim().ToLower();

                    dimensions = _dimensionsRepository.FindBy(c => c.Name.ToLower().Contains(filter) && c.IsDeleted == false)
                        .OrderByDescending(x => x.ModifiedOn)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totalDimensions = _dimensionsRepository.FindBy(c => c.IsDeleted == false && c.Name.ToLower().Contains(filter)).Count();
                }
                else
                {
                    dimensions = _dimensionsRepository.GetAll().Where(c => c.IsDeleted == false)
                               .OrderByDescending(x => x.ModifiedOn)
                                .Skip(currentPage * currentPageSize)
                                .Take(currentPageSize)
                            .ToList();

                    totalDimensions = _dimensionsRepository.FindBy(x => x.IsDeleted == false).Count();
                }
                
                IEnumerable<DimensionVM> dimensionVM = AutoMapper.Map<IEnumerable<Dimension>, IEnumerable<DimensionVM>>(dimensions);

                PaginationSet<DimensionVM> pagedSet = new PaginationSet<DimensionVM>()
                {
                    Page = currentPage,
                    TotalCount = totalDimensions,
                    TotalPages = (int)Math.Ceiling((decimal)totalDimensions / currentPageSize),
                    Items = dimensionVM
                };
                response = request.CreateResponse<PaginationSet<DimensionVM>>(HttpStatusCode.OK, pagedSet);
                return response;
            });
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(HttpRequestMessage request, DimensionVM dimensionVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest,
                        ModelState.Keys.SelectMany(k => ModelState[k].Errors).Select(m => m.ErrorMessage).ToArray());
                }
                else
                {
                    if (_dimensionsRepository.DimensionExists(dimensionVM.Name))
                    {
                        ModelState.AddModelError("Invalid user", "Email or Name already exists");
                        response = request.CreateResponse(HttpStatusCode.BadRequest,
                            ModelState.Keys.SelectMany(k => ModelState[k].Errors).Select(m => m.ErrorMessage).ToArray());
                    }
                    else
                    {
                        Dimension newDimension = new Dimension();
                        newDimension = AutoMapper.Map<DimensionVM, Dimension>(dimensionVM);

                        _dimensionsRepository.Insert(newDimension);
                        _dimensionsRepository.Commit();

                        // Update view model 
                        dimensionVM = AutoMapper.Map<Dimension, DimensionVM>(newDimension);
                        response = request.CreateResponse<DimensionVM>(HttpStatusCode.Created, dimensionVM);
                    }
                }
                return response;
            });
        }

        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, DimensionVM dimensionVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest,
                        ModelState.Keys.SelectMany(k => ModelState[k].Errors).Select(m => m.ErrorMessage).ToArray());
                }
                else
                {
                    Dimension _dimension = _dimensionsRepository.GetSingleByDimensionID(dimensionVM.ID);
                    _dimension.UpdateDimension(dimensionVM);

                    _dimensionsRepository.Commit();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, DimensionVM dimensionVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest,
                        ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                              .Select(m => m.ErrorMessage).ToArray());
                }
                else
                {
                    dimensionVM.IsDeleted = true;
                    Dimension _dimension = _dimensionsRepository.GetSingleByDimensionID(dimensionVM.ID);
                    _dimension.UpdateDimension(dimensionVM);

                    _dimensionsRepository.Commit();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }

                return response;
            });
        }

    }
}
