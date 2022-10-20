using OrdersTributoJustoTesteTecnico.Business.Settings.NotificationSettings;

namespace OrdersTributoJustoTesteTecnico.Business.Interfaces.Notification
{
    public interface INotificationHandler
    {
        List<DomainNotification> GetDomainNotificationList();
        bool AddDomainNotification(string key, string message);
    }
}
