using ERSSettings.Commons;

namespace ERSSettings.Interfaces
{
    internal interface IStartupCondition
    {
        bool HasProblem { get; set; }
        ConditionsTag Tag { get; set; }

        bool Invoke();
    }
}