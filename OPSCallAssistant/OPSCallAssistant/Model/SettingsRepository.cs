using System;
using System.IO;
using System.Xml.Serialization;

namespace OPSCallAssistant.Model
{
    class SettingsRepository : ISettingsRepository
    {
        XmlSerializer xmlSerializer;
        UserInfo userInfo;
        string configFilePath;
        string configDirPath;
        
        public SettingsRepository()
        {
            xmlSerializer = new XmlSerializer(typeof(UserInfo));
            var appPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            configDirPath = Path.Combine(appPath, "Ozeki", "Call Assistant");
            configFilePath = Path.Combine(configDirPath, "settings.xml");

            LoadSettings();

            if (userInfo == null)
                userInfo = new UserInfo();
        }

        void LoadSettings()
        {
            try
            {
                if(!Directory.Exists(configDirPath))
                    return;

                if(!File.Exists(configFilePath))
                    return;

                using (var fs = File.OpenRead(configFilePath))
                {
                    userInfo = (UserInfo)xmlSerializer.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {
                //TODO log4net log
                
            }
       
        }

        public UserInfo GetUserInfo()
        {
            return userInfo;
        }

        public void SetUserInfo(UserInfo userInfo)
        {
            if (userInfo == null)
                this.userInfo = new UserInfo();
            else
                this.userInfo = userInfo;

            try
            {
                if (userInfo == null)
                {
                    if(File.Exists(configFilePath))
                        File.Delete(configFilePath);
                    return;
                }

                
                if (!Directory.Exists(configDirPath))
                    Directory.CreateDirectory(configDirPath);
                
                using (var fs = File.Create(configFilePath))
                {
                    xmlSerializer.Serialize(fs, userInfo);    
                }
            }
            catch (Exception ex)
            {
                
            }
        }

    }
}
