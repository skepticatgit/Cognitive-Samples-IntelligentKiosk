﻿// 
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license.
// 
// Microsoft Cognitive Services: http://www.microsoft.com/cognitive
// 
// Microsoft Cognitive Services Github:
// https://github.com/Microsoft/Cognitive
// 
// Copyright (c) Microsoft Corporation
// All rights reserved.
// 
// MIT License:
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// 

using IntelligentKioskSample.Views;
using System;
using System.ComponentModel;
using System.IO;
using Windows.Storage;

namespace IntelligentKioskSample
{
    internal class SettingsHelper : INotifyPropertyChanged
    {
        public event EventHandler SettingsChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        private static SettingsHelper instance;

        static SettingsHelper()
        {
            instance = new SettingsHelper();
        }

        public void Initialize()
        {
            LoadRoamingSettings();
            Windows.Storage.ApplicationData.Current.DataChanged += RoamingDataChanged;
        }

        private void RoamingDataChanged(ApplicationData sender, object args)
        {
            LoadRoamingSettings();
            instance.OnSettingsChanged();
        }

        private void OnSettingsChanged()
        {
            if (instance.SettingsChanged != null)
            {
                instance.SettingsChanged(instance, EventArgs.Empty);
            }
        }

        private void OnSettingChanged(string propertyName, object value)
        {
            ApplicationData.Current.RoamingSettings.Values[propertyName] = value;

            instance.OnSettingsChanged();
            instance.OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (instance.PropertyChanged != null)
            {
                instance.PropertyChanged(instance, new PropertyChangedEventArgs(propertyName));
            }
        }

        public static SettingsHelper Instance
        {
            get
            {
                return instance;
            }
        }

        private void LoadRoamingSettings()
        {
            object value = ApplicationData.Current.RoamingSettings.Values["FaceApiKey"];
            if (value != null)
            {
                this.FaceApiKey = value.ToString();
            }

            value = ApplicationData.Current.RoamingSettings.Values["FaceApiKeyRegion"];
            if (value != null)
            {
                this.FaceApiKeyRegion = value.ToString();
            }

            value = ApplicationData.Current.RoamingSettings.Values["VisionApiKey"];
            if (value != null)
            {
                this.VisionApiKey = value.ToString();
            }

            value = ApplicationData.Current.RoamingSettings.Values["VisionApiKeyRegion"];
            if (value != null)
            {
                this.VisionApiKeyRegion = value.ToString();
            }

            value = ApplicationData.Current.RoamingSettings.Values["WorkspaceKey"];
            if (value != null)
            {
                this.WorkspaceKey = value.ToString();
            }

            value = ApplicationData.Current.RoamingSettings.Values["CameraName"];
            if (value != null)
            {
                this.CameraName = value.ToString();
            }

            value = ApplicationData.Current.RoamingSettings.Values["MinDetectableFaceCoveragePercentage"];
            if (value != null)
            {
                uint size;
                if (uint.TryParse(value.ToString(), out size))
                {
                    this.MinDetectableFaceCoveragePercentage = size;
                }
            }

            value = ApplicationData.Current.RoamingSettings.Values["ShowDebugInfo"];
            if (value != null)
            {
                bool booleanValue;
                if (bool.TryParse(value.ToString(), out booleanValue))
                {
                    this.ShowDebugInfo = booleanValue;
                }
            }

            value = ApplicationData.Current.RoamingSettings.Values["DriverMonitoringSleepingThreshold"];
            if (value != null)
            {
                double threshold;
                if (double.TryParse(value.ToString(), out threshold))
                {
                    this.DriverMonitoringSleepingThreshold = threshold;
                }
            }

            value = ApplicationData.Current.RoamingSettings.Values["DriverMonitoringYawningThreshold"];
            if (value != null)
            {
                double threshold;
                if (double.TryParse(value.ToString(), out threshold))
                {
                    this.DriverMonitoringYawningThreshold = threshold;
                }
            }
        }

        public void RestoreAllSettings()
        {
            ApplicationData.Current.RoamingSettings.Values.Clear();
        }

        private string faceApiKey = string.Empty;
        public string FaceApiKey
        {
            get { return this.faceApiKey; }
            set
            {
                this.faceApiKey = value;
                this.OnSettingChanged("FaceApiKey", value);
            }
        }

        private string faceApiKeyRegion = string.Empty;
        public string FaceApiKeyRegion
        {
            get { return this.faceApiKeyRegion; }
            set
            {
                this.faceApiKeyRegion = value;
                this.OnSettingChanged("FaceApiKeyRegion", value);
            }
        }

        private string visionApiKey = string.Empty;
        public string VisionApiKey
        {
            get { return this.visionApiKey; }
            set
            {
                this.visionApiKey = value;
                this.OnSettingChanged("VisionApiKey", value);
            }
        }

        private string visionApiKeyRegion = string.Empty;
        public string VisionApiKeyRegion
        {
            get { return this.visionApiKeyRegion; }
            set
            {
                this.visionApiKeyRegion = value;
                this.OnSettingChanged("VisionApiKeyRegion", value);
            }
        }

        private string workspaceKey = string.Empty;
        public string WorkspaceKey
        {
            get { return workspaceKey; }
            set
            {
                this.workspaceKey = value;
                this.OnSettingChanged("WorkspaceKey", value);
            }
        }

        private string cameraName = string.Empty;
        public string CameraName
        {
            get { return cameraName; }
            set
            {
                this.cameraName = value;
                this.OnSettingChanged("CameraName", value);
            }
        }

        private uint minDetectableFaceCoveragePercentage = 7;
        public uint MinDetectableFaceCoveragePercentage
        {
            get { return this.minDetectableFaceCoveragePercentage; }
            set
            {
                this.minDetectableFaceCoveragePercentage = value;
                this.OnSettingChanged("MinDetectableFaceCoveragePercentage", value);
            }
        }

        private bool showDebugInfo = false;
        public bool ShowDebugInfo
        {
            get { return showDebugInfo; }
            set
            {
                this.showDebugInfo = value;
                this.OnSettingChanged("ShowDebugInfo", value);
            }
        }

        private double driverMonitoringSleepingThreshold = RealtimeDriverMonitoring.DefaultSleepingApertureThreshold;
        public double DriverMonitoringSleepingThreshold
        {
            get { return this.driverMonitoringSleepingThreshold; }
            set
            {
                this.driverMonitoringSleepingThreshold = value;
                this.OnSettingChanged("DriverMonitoringSleepingThreshold", value);
            }
        }

        private double driverMonitoringYawningThreshold = RealtimeDriverMonitoring.DefaultYawningApertureThreshold;
        public double DriverMonitoringYawningThreshold
        {
            get { return this.driverMonitoringYawningThreshold; }
            set
            {
                this.driverMonitoringYawningThreshold = value;
                this.OnSettingChanged("DriverMonitoringYawningThreshold", value);
            }
        }

        public string[] AvailableApiRegions
        {
            get
            {
                return new string[]
                {
                    "westus",
                    "westus2",
                    "eastus",
                    "eastus2",
                    "westcentralus",
                    "southcentralus",
                    "westeurope",
                    "northeurope",
                    "southeastasia",
                    "eastasia",
                    "japaneast",
                    "australiaeast",
                    "brazilsouth"
                };
            }
        }
    }
}