using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vedma0.Models.Properties
{
    public class TextArrayProperty : Property
    {
        [Required]
        private string _Values { get; set; }
        [NotMapped]
        [Required]
        [DisplayName("Значения")]
        public IList<string> Values
        {
            get
            {
                if (_Values == null)
                    return null;
                return JsonConvert.DeserializeObject<IList<string>>(_Values);
            }
            set
            {
                if (value == null)
                    _Values = null;
                else
                    _Values = JsonConvert.SerializeObject(value);
            }
        }
        public override string GetValue()
        {
            if (Values == null)
                return "пусто";
            string result = "";
            for (int i = 0; i < Values.Count; i++)
            {
                result = result + Values[i];
                if (i != Values.Count - 1)
                    result = result + "\n";
            }
            return result;
        }
    }
}
