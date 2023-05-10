namespace PoliVagas.Core.Domain;

public class Benefits
{
    public bool? HasFoodVoucher { get; set; }
    public bool? HasTransportVoucher { get; set; }
    public bool? HasHealthInsurance { get; set; }
    public bool? HasLifeInsurance { get; set; }
    public string? Others { get; set; }
}
