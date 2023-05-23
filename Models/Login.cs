using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StocksMarket.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Username is required")] // make the field required
        [Display(Name = "Username")]  // Set the display name of the field
        public string username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        LDap ldap = new LDap();

        public bool IsValid(string _username, string _password)
        {
            bool retVal = false;

            try
            {

                string value = ldap.CheckLogin(_username, _password);

                if (value == "0")
                {
                    retVal = false;
                }
                else
                {
                    retVal = true;
                }
                return retVal;
            }
            catch (Exception ex)
            {
                retVal = false;
            }

            return retVal;

        }
    }
}
