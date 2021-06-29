using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ModularWebApp.Models
{


    public class Rootobject
    {
       
      public List<LutUsers> Content { get; set; }

      public string Status { get; set; }
    }
    public class LutUsers
    {
        public int id_utilisateurs { get; set; }
        public string matricule_agent { get; set; }
        public string nom_utils { get; set; }
        public string prenom_utils { get; set; }
        public string sexe_utils { get; set; }
        public string dat2naiss_utils { get; set; }
        public string grade_agent { get; set; }
        public string email_utils { get; set; }
        public string mot2pass_utils { get; set; }
        public string typ_compte { get; set; }
        public int is_active_utils { get; set; }
        public string date_enreg_utils { get; set; }
        public string affectation { get; set; }
        public int is_first_login { get; set; }
        public string mot2pass_utils_defaut { get; set; }



    }
}