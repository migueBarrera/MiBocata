using Microsoft.AppCenter.Crashes;

namespace Mibocata.Core.Services.Interfaces;

public interface IAppCenterService
{
    void Initialize();

    void TrackEvent(string name, IDictionary<string, string> properties = null);

    void TrackError(Exception exception, IDictionary<string, string> properties = null, params ErrorAttachmentLog[] attachments);
}
