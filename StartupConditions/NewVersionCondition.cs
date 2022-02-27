using Newtonsoft.Json;
using ERSSettings.Commons;
using ERSSettings.Dto;
using ERSSettings.Helpers;
using ERSSettings.Interfaces;
using System;
using System.IO;
using System.Net;

namespace ERSSettings.Conditions
{
    internal class NewVersionCondition : IStartupCondition
    {
        public bool HasProblem { get; set; }
        public ConditionsTag Tag { get; set; } = ConditionsTag.NewVersion;

        public bool Invoke()
        {
            try
            {
                HttpWebRequest request = WebRequest.CreateHttp(AppHelper.ERSSettingsVersionsJson);
                request.UserAgent = AppHelper.UserAgent;
                var response = request.GetResponse();
                DebugHelper.HasUpdateResponse();
                using (Stream dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    var serverResponse = reader.ReadToEnd();
                    var release = JsonConvert.DeserializeObject<ReleaseDto>(serverResponse);
                    DebugHelper.HasUpdateRelease(release);
                    var releasedVersion = new Version(AppHelper.IsRelease ? release.ERSSettings_release : release.ERSSettings_pre_release);
                    var hasNewVersion = releasedVersion > AppHelper.Version;

                    if (hasNewVersion)
                    {
                        DebugHelper.IsNewRelease();
                        ToastHelper.ShowUpdateToast(currentVersion: $"{AppHelper.Version}", newVersion: $"{releasedVersion}");
                    }
                    else
                    {
                        DebugHelper.UpdateNotNecessary();
                    }

                    return HasProblem = hasNewVersion;
                }
            }
            catch (WebException e)
            {
                DebugHelper.HasException("An error occurred while checking for an update", e);
                return HasProblem;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.Replace(":", null));
            }
        }
    }
}