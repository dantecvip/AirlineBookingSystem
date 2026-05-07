using AirlineBookingSystem.Notifications.Core.Entities;
using AirlineBookingSystem.Notifications.Core.Repositories;
using Dapper;
using System.Data;

namespace AirlineBookingSystem.Notifications.Infrastructure.Repositories
{
    public class NotificationRepository(IDbConnection dbConnection) : INotificationRepository
    {
        public async Task LogNotification(Notification notification)
        {
            const string sql = @"
                INSERT INTO Notifications (Id, Recipient, Message, Type, SentAt)
                VALUES (@Id, @Recipient, @Message, @Type, @SentAt)";

            await dbConnection.ExecuteAsync(sql, notification);
        }
    }
}
