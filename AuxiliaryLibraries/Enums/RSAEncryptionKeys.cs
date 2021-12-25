using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuxiliaryLibraries.Enums
{
    /// <summary>
    /// RSAEncryptionKeys
    /// </summary>
    public enum RSAEncryptionKeys
    {
        /// <summary>
        /// XML File (*.xml)
        /// </summary>
        [Description("XML")]
        XML = 1,
        /// <summary>
        /// PEM File (*.pem)
        /// </summary>
        [Description("PEM")]
        PEM = 2
    }
}
