
namespace OPSCallAssistant.Model
{
    interface ISettingsRepository
    {
        UserInfo GetUserInfo();
        void SetUserInfo(UserInfo userInfo);
    }
}
