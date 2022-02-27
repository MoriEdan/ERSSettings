using ERSSettings.Commons;
using ERSSettings.Helpers;
using ERSSettings.Interfaces;

namespace ERSSettings.Conditions
{
    internal class OsVersionCondition : IStartupCondition
    {
        public bool HasProblem { get; set; }
        public ConditionsTag Tag { get; set; } = ConditionsTag.OsVersion;

        public bool Invoke()
        {
            var build = OsHelper.GetBuild();
            var hasProblem = build >= OsHelper.WIN11_MIN_SUPPORTED_BUILD || (build >= OsHelper.WIN10_MIN_SUPPORTED_BUILD & build <= OsHelper.WIN10_MAX_SUPPORTED_BUILD);
            return HasProblem = hasProblem.Invert();
        }
    }
}