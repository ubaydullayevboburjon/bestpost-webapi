using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPost.Service.Dtos.Security;

public class VertificationDto
{
    public int Code { get; set; }

    public int Attempt { get; set; }

    public DateTime CreatedAt { get; set; }
}
