using HeatChart.Entities.Sql.Interfaces;
using System;

namespace HeatChart.Entities.Sql.Domain
{
    /// <summary>
    /// To store any error details
    /// </summary>
    public class Error : IEntityBase
    {
        public int ID { get; set; } // Auto generated ID
        public string Message { get; set; } // To Store error message
        public string InnerMessage { get; set; }
        public string StackTrace { get; set; } 
        public DateTime DateCreated { get; set; } 
    }
}
