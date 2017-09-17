using AutoMapper;
using HeatHeatChart.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelMapper
{
    public class AutoMapper
    {
        public static TDestination Map<TSource,TDestination>(TSource source)
        {
           return Mapper.Map<TSource, TDestination>(source);          
        }
    }
}
