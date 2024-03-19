using System.Text.Json.Serialization;
using service_reapprovisionnement.Enum;

namespace Dtos;

public class BonDeCommandeEtatDTO {
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public EtatCommande etat { get; set; }
}