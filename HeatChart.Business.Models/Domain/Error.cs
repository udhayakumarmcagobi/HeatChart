using System;

namespace HeatChart.Business.Models.Domain
{
    /// <summary>
    /// To store any error details
    /// </summary>
    public class Error 
    {
        public int ID { get; set; } // Auto generated ID
        public string Message { get; set; } // To Store error message
        public string InnerMessage { get; set; }
        public string StackTrace { get; set; } 
        public DateTime DateCreated { get; set; } 
    }
}
