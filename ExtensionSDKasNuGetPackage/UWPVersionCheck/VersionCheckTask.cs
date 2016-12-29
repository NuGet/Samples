using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;

namespace UWPVersionCheck
{
    public class VersionCheckTask : Task
    {
        private string minVersion;

        private string targetMinVersion;

        [Required]
        public string MinVersionSupported
        {
            get
            {
                return minVersion;
            }
            set
            {
                this.minVersion = value;
            }
        }

        [Required]
        public string TargetMinVersion
        {
            get
            {
                return targetMinVersion;
            }
            set
            {
                targetMinVersion = value;
            }
        }

        public override bool Execute()
        {
            Version version = new Version(MinVersionSupported);
            Version version2 = new Version(TargetMinVersion);
            bool flag = version2 >= version;
            bool result;
            if (flag)
            {
                base.Log.LogMessage("This package passes version check", new object[0]);
                result = true;
            }
            else
            {
                base.Log.LogError("This package cannot be used in this project since its TargetPlatformMinVersion -{0} does not match the Minimum Version -{1} supported by the package", new object[]
                {
                    version2,
                    version
                });
                result = false;
            }
            return result;
        }
    }
}

