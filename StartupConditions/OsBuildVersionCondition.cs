using ERSSettings.Commons;
using ERSSettings.Helpers;
using ERSSettings.Interfaces;

namespace ERSSettings.Conditions
{
    internal class OsBuildVersionCondition : IStartupCondition
    {
        private const ushort MIN_SUPPORT_UBR = 1151;

        public bool HasProblem { get; set; }
        public ConditionsTag Tag { get; set; } = ConditionsTag.OsBuildVersion;

        public bool Invoke()
        {
            var ubr = OsHelper.GetUpdateBuildRevision();
            var hasProblem = OsHelper.IsWindows11() ? ubr >= OsHelper.WIN11_MIN_SUPPORTED_UBR : ubr >= OsHelper.WIN10_MIN_SUPPORTED_UBR;
            return HasProblem = hasProblem.Invert();
        }
    }
}