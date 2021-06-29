namespace POLIFRONT_WEB_API.App_Code
{
    using Db_Core;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public partial class faux_doc
    {
        public int id_faux_doc { get; set; }
        public int id_utilisateurs { get; set; }
        public string numero_interp { get; set; }
        public string numero_migrant { get; set; }
        public System.DateTime date_heure_fd { get; set; }
        public string departement_fd { get; set; }
        public string point_fd { get; set; }
        public string localite_fd { get; set; }
        public string photo_fd { get; set; }
        public string typ_doc_fd { get; set; }
        public string motifs_fd { get; set; }
        public string desription_fd { get; set; }
        public System.DateTime date_enreg_fd { get; set; }

        public faux_doc(DataRow row)
        {
            parse(row);
        }

        private faux_doc parse(DataRow row)
        {
            id_faux_doc = int.Parse(row["id_faux_doc"].ToString());
            id_utilisateurs = int.Parse(row["id_utilisateurs"].ToString());
            //...
            return this;
        }

        public static int addFauxDoc(faux_doc doc)
        {
            try
            {
                string sql = @"INSERT INTO dbo.faux_doc
                                (id_utilisateurs
                                ,numero_interp
                                ,numero_migrant
                                ,date_heure_fd
                                ,departement_fd
                                ,point_fd
                                ,localite_fd
                                ,photo_fd
                                ,typ_doc_fd
                                ,motifs_fd
                                ,desription_fd
                                ,date_enreg_fd)
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
                                ,GETDATE())";
                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
                stmt.SetParameters(
                    doc.id_utilisateurs,
                    doc.numero_interp,
                    doc.numero_migrant,
                    doc.date_heure_fd,
                    doc.departement_fd,
                    doc.point_fd,
                    doc.localite_fd,
                    doc.photo_fd,
                    doc.typ_doc_fd,
                    doc.motifs_fd,
                    doc.desription_fd);
                return stmt.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                Utils.ErrorLogging(System.Reflection.MethodBase.GetCurrentMethod().Name, e);
                return -1;
            }
        }
    }
}
