﻿using ERSSettings.Commons;
using ERSSettings.Dto;
using ERSSettings.Interfaces;
using System;
using System.Collections.Generic;
using Windows.Foundation;

namespace ERSSettings.Helpers
{
    internal class DebugHelper
    {
        private const string APP_FOLDER = "App folder";
        private const string APP_IS_RELEASE = "App is release";
        private const string APP_LOC = "App localization";
        private const string APP_THEME = "App theme";
        private const string APP_VER = "App version";
        private const string PC_NAME = "Computer name";
        private const string REG_ORG = "Registered organization";
        private const string REG_OWNER = "Registered owner";
        private const string USER_CULTURE = "User culture";
        private const string USER_DOMAIN = "User domain";
        private const string USER_NAME = "Current user";
        private const string USER_REGION = "User region";
        private static readonly object infoLogLocker = new object();
        private static readonly object statusLogLocker = new object();
        private static List<string> ErrorsLog = new List<string>();

        private static List<string> InfoLog = new List<string>
        {
            $"{OsHelper.GetProductName()} {OsHelper.GetDisplayVersion()} build: {OsHelper.GetVersion()}",
            $"{PC_NAME}: {Environment.MachineName}",
            $"{REG_ORG}: {OsHelper.GetRegisteredOrganization()}",
            $"{REG_OWNER}: {OsHelper.GetRegisteredOwner()}",
            $"{USER_NAME}: {Environment.UserName}",
            $"{USER_DOMAIN}: {Environment.GetEnvironmentVariable("userdnsdomain") ?? Environment.UserDomainName}",
            $"{USER_CULTURE}: {OsHelper.GetCurrentCultureName()}",
            $"{USER_REGION}: {OsHelper.GetRegionName()}",
            $"{APP_VER}: {AppHelper.Version}",
            $"{APP_IS_RELEASE}: {AppHelper.IsRelease}",
            $"{APP_FOLDER}: \"{AppHelper.StartupFolder}\""
        };

        private static List<string> InitLog = new List<string>();

        private static List<string> StatusLog = new List<string>();

        private static string DateTime { get => System.DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"); }

        private static void WriteInfoLog(string record)
        {
            lock (infoLogLocker)
            {
                InfoLog.Add(record);
            }
        }

        private static void WriteInfoLog(List<string> list) => InfoLog.AddRange(list);

        private static void WriteInitLog(string record) => InitLog.Add($"{DateTime} {record}");

        private static void WriteStatusLog(string record)
        {
            lock (statusLogLocker)
            {
                StatusLog.Add($"{DateTime} {record}");
            }
        }

        internal static void AdvancedSettinsVisibility(bool value) => WriteStatusLog($"Advanced settings is visible: {value}");

        internal static void AppLanguage(string language) => WriteInfoLog($"{APP_LOC}: {language}");

        internal static void AppTheme(string theme) => WriteInfoLog($"{APP_THEME}: {theme}");

        internal static void DebugMode(bool value) => WriteStatusLog($"Debug mode is: {value}");

        internal static void HasException(string message, Exception e)
        {
            ErrorsLog.AddRange(new List<string>()
            {
                $"{DateTime} {message}",
                $"{DateTime} Error information: {e.Message}",
                $"{DateTime} The method that caused the error: {e.TargetSite.Name}"
            });
        }

        internal static void HasUpdateRelease(ReleaseDto release) => WriteInfoLog(new List<string>()
        {
            $"Release version is available: {release.ERSSettings_release}",
            $"Pre-release version is available: {release.ERSSettings_pre_release}",
        });

        internal static void HasUpdateResponse() => WriteInfoLog("After checking for update a response was received from the update server");

        internal static void IsNewRelease() => WriteInfoLog("The update can be proceeded");

        internal static void LinkClicked(string link) => WriteStatusLog($"Link clicked: \"{link}\"");

        internal static void OsConditionHasProblem(IStartupCondition condition) => WriteStatusLog($"{condition.Tag} is has problem: {condition.HasProblem}");

        internal static void RiskAgreed() => WriteStatusLog("USER AGREED TO ASSUME THE RISK AND LIABILITY FOR ANY POSSIBLE DAMAGE");

        internal static void Save(string path) => FileHelper.WriteAllLines(path, InfoLog.Split(string.Empty).Merge(ErrorsLog).Split(string.Empty).Merge(InitLog).Split(string.Empty).Merge(StatusLog));

        internal static void SelectedLocalization(string localization) => WriteStatusLog($"Localization selected: {localization}");

        internal static void SelectedTheme(string value) => WriteStatusLog($"Theme selected: {value}");

        internal static void StartApplyingSettings(int actionsCount) => WriteStatusLog($"Started applying {actionsCount} setting(s)");

        internal static void StartInitTextedElements() => WriteStatusLog("Started initialization of texted elements");

        internal static void StartInitUwpApps() => WriteStatusLog("Started initialization of uwp elements");

        internal static void StartResetTextedElements() => WriteStatusLog("Started reset texted elements status");

        internal static void StartStartupConditions() => WriteStatusLog("Starting the OS conditions check");

        internal static void StopApplyingSettings(double totalSeconds) => WriteStatusLog($"Applying setting(s) took {totalSeconds:N0} second(s)");

        internal static void StopInitTextedElements(double totalSeconds) => WriteStatusLog($"It took {totalSeconds:N0} second(s) to initialize texted elements");

        internal static void StopInitUwpApps(double totalSeconds) => WriteStatusLog($"It took {totalSeconds:N0} second(s) to initialize uwp elements");

        internal static void StopResetTextedElements(double totalSeconds) => WriteStatusLog($"It took {totalSeconds:N0} second(s) to reset texted elements");

        internal static void StopSearch(string searchString, double totalSeconds, int elementFound) => WriteStatusLog($"It took {totalSeconds:N3} seconds to search for \"{searchString}\" and found {elementFound} item(s)");

        internal static void StopStartupConditions(double totalSeconds) => WriteStatusLog($"It took {totalSeconds:N0} second(s) to Os conditions checks");

        internal static void TextedElementChanged(uint elementID, ElementStatus elementStatus) => WriteStatusLog($"The {elementID} element changed status to: {elementStatus}");

        internal static void TextedElementInit(uint elementID, double totalSeconds) => WriteInitLog($"The {elementID} element was initialized in {totalSeconds:N3} second(s)");

        internal static void UpdateNotNecessary() => WriteInfoLog("No update required");

        internal static void UwpForAllUsersState(ElementStatus value) => WriteStatusLog($"The \"UWP for all users\" switch state is: {value}");

        internal static void UwpRemoved(string packageName, double totalSeconds, AsyncStatus result) => WriteStatusLog($"The UWP package {packageName} was removed in {totalSeconds:N3} second(s) with the result: {result}");

        internal static void UwpRemovedHasException(string packageName, string errorText) => WriteStatusLog($"An error occurred while removing the package {packageName}: {errorText}");

        internal static void VisibleViewChanged(string value) => WriteStatusLog($"Active view is: {value}");
    }
}