using ERSSettings.Commons;
using ERSSettings.Interfaces;
using System;
using System.IO;

namespace ERSSettings.Conditions
{
    internal class SycnexScriptCondition : IStartupCondition
    {
        private readonly string win10DebloaterDirectory = $@"{Environment.GetEnvironmentVariable("SystemDrive")}\Temp\Windows10Debloater";

        public bool HasProblem { get; set; }
        public ConditionsTag Tag { get; set; } = ConditionsTag.SycnexScript;

        public bool Invoke() => HasProblem = Directory.Exists(win10DebloaterDirectory);
    }
}