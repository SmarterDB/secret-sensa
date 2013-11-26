
using System;
using System.Collections.Generic;
using OPSSDK;
using OPSSDKCommon.Model;
using OPSSDKCommon.Model.Extension;
using Ozeki.VoIP;

namespace OPSCallAssistant.Model
{
    interface IClient
    {
        bool IsLoggedIn { get; }
        void Login(UserInfo user);
        void Logout();

        event EventHandler<VoIPEventArgs<LoginResult>> LoginCompleted;
        event EventHandler<VoIPEventArgs<ISession>> SessionCompleted;
        event EventHandler<VoIPEventArgs<ISession>> SessionCreated;
        event EventHandler<VoIPEventArgs<ErrorInfo>> ErrorOccurred;
        event EventHandler<VoIPEventArgs<List<PhoneBookItem>>> PhoneBookChanged;

        List<ExtensionInfo> ExtensionInfos { get; }
        void GetExtensionInfosAsync(Action<List<ExtensionInfo>> completed);

        IAPIExtension GetAPIExtension(string extension_name);
        void GetAPIExtensionAsync(string extension_name, Action<IAPIExtension> completed);
        void GetPhoneBook();

        UserInfo User { get; }
    }
}
