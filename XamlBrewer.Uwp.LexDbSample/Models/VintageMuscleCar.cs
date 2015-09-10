using System;

namespace XamlBrewer.Uwp.LexDbSample.Models
{
    /// <summary>
    /// Represents a vintage muscle car.
    /// </summary>
    public class VintageMuscleCar
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the day of birth.
        /// </summary>
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the picture.
        /// </summary>
        /// <remarks>Is a blob in the database.</remarks>
        public byte[] Picture { get; set; }
    }

}
