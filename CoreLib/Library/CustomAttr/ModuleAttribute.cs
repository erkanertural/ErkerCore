using ErkerCore.Library.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErkerCore.Library
{
    public class ModuleAttribute : Attribute
    {

        public ModuleAttribute(FeatureModule module)
        {
            FeatureModule = module;
        }

        public FeatureModule FeatureModule { get; set; }
    }
}
