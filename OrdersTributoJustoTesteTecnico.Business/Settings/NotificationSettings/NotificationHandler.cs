using OrdersTributoJustoTesteTecnico.Business.Interfaces.Notification;

namespace OrdersTributoJustoTesteTecnico.Business.Settings.NotificationSettings
{
    public sealed class NotificationHandler : INotificationHandler
    {
        private List<DomainNotification> DomainNotificationList;

        public NotificationHandler()
        {
            DomainNotificationList = new List<DomainNotification>();
        }

        public List<DomainNotification> GetDomainNotificationList() => DomainNotificationList;

        public bool AddDomainNotification(string key, string message)
        {
            var domainNotification = new DomainNotification()
            {
                Key = key,
                Message = message
            };

            DomainNotificationList.Add(domainNotification);

            return false;
        }
    }
}
