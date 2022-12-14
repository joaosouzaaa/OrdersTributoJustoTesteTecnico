using OrdersTributoJustoTesteTecnico.Business.Settings.NotificationSettings;

namespace UnitTests.NotificationHandlerTests
{
    public sealed class NotificationHandlerTests
    {
        NotificationHandler _notification;

        public NotificationHandlerTests()
        {
            _notification = new NotificationHandler();
        }

        [Fact]
        public void AddNotification_ReturnsFalse_NotificationList_HasNotifications()
        {
            var key = "key here";
            var message = "message here";

            var addNotification = _notification.AddDomainNotification(key, message);
            var domainNotificationList = _notification.GetDomainNotificationList();

            Assert.False(addNotification);
            Assert.NotEmpty(domainNotificationList);
        }
    }
}
