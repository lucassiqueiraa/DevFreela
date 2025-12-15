using DevFreela.Application.Notification.ProjectCreated;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Notification.ProjectCreatedpublish 
{
    public class FreelanceNotificationHandler : INotificationHandler<ProjectCreatedNotification>
    {
        public Task Handle(ProjectCreatedNotification notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"[Notification] - New project created: Id {notification.Id}, Title: {notification.Title}, Total Cost: {notification.TotalCost}");

            return Task.CompletedTask;
        }
    }
}
