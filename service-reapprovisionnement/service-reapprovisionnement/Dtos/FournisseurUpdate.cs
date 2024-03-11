using Reapprovisionnement.Models;

namespace Dtos;

public record FournisseurUpdate(string? email, string? raison_sociale, List<Produit>? produits)
{
    public static Fournisseur convertToDTO(Fournisseur f, FournisseurUpdate upd)
    {
        if (upd.email != null)
            f.email = upd.email;
        if (upd.raison_sociale != null)
            f.raison_sociale = upd.raison_sociale;
        if (upd.produits != null)
            f.produits = upd.produits;
        return f;
    }
};