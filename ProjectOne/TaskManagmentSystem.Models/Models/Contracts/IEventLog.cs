using System;

namespace TaskManagmentSystem.Models.Contracts
{
    public interface IEventLog
    {
        string Description { get; }
        DateTime EventTime { get; }

        string ViewInfo();
    }
}