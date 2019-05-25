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
    public class BaseTextArrayProperty : BaseProperty
    {
        [Required]
        private string _DefaultValues { get; set; }
        [NotMapped]
        [Required]
        [DisplayName("Значения")]
        public IList<string> DefaultValues
        {
            get => JsonConvert.DeserializeObject<IList<string>>(_DefaultValues);
            set =>  _DefaultValues = JsonConvert.SerializeObject(value);
        }
        public override string GetValue()
        {
            if (DefaultValues.Count==0)
                return "пусто";
            string result = "";
            for (int i=0;i<DefaultValues.Count;i++)
            {
                result = result + DefaultValues[i];
                if (i!=DefaultValues.Count-1)
                    result = result + "\n";
            }
            return result;
        }

        public override string GetPropertyType()
        {
            return "Свойство массивом строк";
        }
    }
}
