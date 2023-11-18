using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient.DataTransferObjects.ApiResponses;

public class DesignConstantsDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public double Value { get; set; }
}
