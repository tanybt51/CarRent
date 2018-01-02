using _4.Entities.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4.Entities
{

    [MetadataType(typeof(UserValidation))] // Describes the class. Used for adding Validations
    partial class User
    {
        public string UserRoles { get; set; }
    }

    public class UserValidation
    {
        [Required]  // Required Validation  
        
        [TZ(ErrorMessage = "תעודת הזהות אינה תקינה")]
        public int TZ { get; set; }

        [Required]  // Required Validation   
        public string Email { get; set; }
        //[Range(0, 10)] // Adds Range Validation
        //public int MyProperty { get; set; }
    }


}
