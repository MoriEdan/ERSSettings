using ERSSettings.Commons;
using ERSSettings.Helpers;
using ERSSettings.Interfaces;
using System.Linq;

namespace ERSSettings.Conditions
{
    internal class SingleInstanceCondition : IStartupCondition
    {
        private readonly string APP_PROCESS_NAME = AppHelper.AppName;
        public bool HasProblem { get; set; }
        public ConditionsTag Tag { get; set; } = ConditionsTag.SingleInstance;

        public bool Invoke() => HasProblem = ProcessHelper.GetProcessIdentity(APP_PROCESS_NAME).Count() > 1;
    }
}