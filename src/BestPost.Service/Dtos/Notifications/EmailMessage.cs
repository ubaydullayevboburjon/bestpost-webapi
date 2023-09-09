using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPost.Service.Dtos.Notifications;

public class EmailMessage
{
    public string Recipent { get; set; } = String.Empty;

    public string Title { get; set; } = String.Empty;

    public string Content { get; set; } = String.Empty;
}
