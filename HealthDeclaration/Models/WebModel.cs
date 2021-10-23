using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthDeclaration.Models
{
    public class WebModel
    {
        public List<ComboBoxItem> ComboBoxs { get; set; }
        public List<RadioItem> Radios { get; set; }
        public List<TextItem> Texts { get; set; }
        public List<CheckBoxItem> CheckBoxs { get; set; }
    }
}
