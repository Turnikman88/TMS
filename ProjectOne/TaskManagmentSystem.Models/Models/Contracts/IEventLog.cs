using System;

namespace ProjectOne.Models.Contracts
{
    public interface IEventLog
    {
        string Description { get; }
        DateTime EventTime { get; }

        string ViewInfo();
    }
}