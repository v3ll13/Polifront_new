namespace POLIFRONT_WEB_API.App_Code
{
    using System;
    using System.Collections.Generic;
    
    public partial class oper_conj
    {
        public int id_oper_conj { get; set; }
        public int id_utilisateurs { get; set; }
        public string numero_interp_oc { get; set; }
        public string numero_migrant { get; set; }
        public System.DateTime date_heure_oc { get; set; }
        public string departement_oc { get; set; }
        public string point_oc { get; set; }
        public string localite_oc { get; set; }
        public byte[] is_partenaire { get; set; }
        public byte[] Douanes { get; set; }
        public byte[] Immigration { get; set; }
        public byte[] BLTS { get; set; }
        public System.DateTime date_enreg_oc { get; set; }
    }
}
