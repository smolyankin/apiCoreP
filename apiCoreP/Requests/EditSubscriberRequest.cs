using System.ComponentModel.DataAnnotations;

namespace apiCoreP.Requests
{
    /// <summary>
    /// update subscriber request
    /// </summary>
    public class EditSubscriberRequest
    {
        /// <summary>
        /// name
        /// </summary>
        [Required]
        public long Id { get; set; }

        /// <summary>
        /// inn
        /// </summary>
        [StringLength(12)]
        public string Inn { get; set; }

        /// <summary>
        /// short subscriber title
        /// </summary>
        [StringLength(50)]
        public string ShortTitle { get; set; }

        /// <summary>
        /// full subscriber title
        /// </summary>
        [StringLength(4000)]
        public string FullTitle { get; set; }

        /// <summary>
        /// subscriber address
        /// </summary>
        [StringLength(200)]
        public string Address { get; set; }

        /// <summary>
        /// subscriber phones
        /// </summary>
        [StringLength(200)]
        public string Phones { get; set; }

        /// <summary>
        /// owner full name
        /// </summary>
        [StringLength(200)]
        public string OwnerName { get; set; }

        /// <summary>
        /// subscriber representative
        /// </summary>
        [StringLength(4000)]
        public string Representative { get; set; }

        /// <summary>
        /// representative phones
        /// </summary>
        [StringLength(200)]
        public string RepresentativePhones { get; set; }
    }
}
