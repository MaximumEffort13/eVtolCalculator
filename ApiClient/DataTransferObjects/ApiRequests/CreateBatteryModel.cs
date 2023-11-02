using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient.DataTransferObjects.ApiRequests;

public class CreateBatteryModel
{
    [Required]
    public string PackName { get; set; }

    [Required]
    public string CellName { get; set; }

    [Required]
    public double CellVoltage_V { get; set; }

    [Required]
    public double CellCurrent_mA { get; set; }

    [Required]
    public double CellWeight_g { get; set; }

    [Required]
    public double CellCapacity_mWh { get; set; }

    [Required]
    public int NumberOfCellsConnectedInSeries { get; set; }

    [Required]
    public int NumberOfCellsConnectedInParallel { get; set; }

    [Required]
    public int NumberOfModulesConnectedInSeries { get; set; }

    [Required]
    public int NumberOfModulesConnectedInParallel { get; set; }

    [Required]
    public double MiscellaneousPackWeight { get; set; }
}
