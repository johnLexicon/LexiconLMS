using System;

namespace LexiconLMS.Models
{
    public interface IParentDateInterval
    {
        DateTime ParentStartDate { get; set; }
        DateTime ParentEndDate { get; set; }
    }
}