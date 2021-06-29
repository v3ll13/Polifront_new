//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace POLIFRONT_WEB_API.App_Code
{
    using Db_Core;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public partial class vehicules
    {
        public string numero_migrant { get; set; }
        public string numero_interp { get; set; }
        public string nom_prop { get; set; }
        public string prenom_prop { get; set; }
        public string typ_vehicule { get; set; }
        public string marque_voit { get; set; }
        public string modele_voit { get; set; }
        public int annee_voit { get; set; }
        public string couleur_voit { get; set; }
        public string plaque_imm_voit { get; set; }
        public string numero_moteur { get; set; }
        public string numero_chassis { get; set; }
        public string numero_assurance { get; set; }
        public string numero_enregistrement { get; set; }
        public System.DateTime date_enreg_voit { get; set; }

        public vehicules(DataRow row)
        {
            parse(row);
        }

        private vehicules parse(DataRow row)
        {
            numero_migrant = row["numero_migrant"].ToString();
            numero_interp = row["numero_interp"].ToString();
            //id_utilisateurs = int.Parse(row["id_utilisateurs"].ToString());
            //...
            return this;
        }

        public static int addVehicule(vehicules ve)
        {
            try
            {
                string sql = @"INSERT INTO dbo.vehicules
                                (numero_migrant
                                ,numero_interp
                                ,nom_prop
                                ,prenom_prop
                                ,typ_vehicule
                                ,marque_voit
                                ,modele_voit
                                ,année_voit
                                ,couleur_voit
                                ,plaque_imm_voit
                                ,numero_moteur
                                ,numero_chassis
                                ,numero_assurance
                                ,numero_enregistrement
                                ,date_enreg_voit)
                            VALUES
                                (?
                                ,?
                                ,?
                                ,?
                                ,?
                                ,?
                                ,?
                                ,?
                                ,?
                                ,?
                                ,?
                                ,?
                                ,?
                                ,?
                                ,GETDATE())";
                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
                stmt.SetParameters(
                    ve.numero_migrant,
                    ve.numero_interp,
                    ve.nom_prop,
                    ve.prenom_prop,
                    ve.typ_vehicule,
                    ve.marque_voit,
                    ve.modele_voit,
                    ve.annee_voit,
                    ve.couleur_voit,
                    ve.plaque_imm_voit,
                    ve.numero_moteur,
                    ve.numero_chassis,
                    ve.numero_assurance,
                    ve.numero_enregistrement);
                return stmt.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Utils.ErrorLogging(System.Reflection.MethodBase.GetCurrentMethod().Name, e);
                return -1;
            }
        }
    }
}
