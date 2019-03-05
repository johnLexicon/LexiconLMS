using System;

namespace LexiconLMS.Models
{
    public interface IDateInterval
    {
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
    }
}