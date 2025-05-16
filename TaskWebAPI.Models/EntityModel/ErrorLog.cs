using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWebAPI.Models.EntityModel
{
    public class ErrorLog
    {
        [Key]
		public string Id { get; set; } = Guid.NewGuid().ToString();

		[Column(TypeName = "NVARCHAR(MAX)")]
        public string? Message { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [MaxLength(100)]
        public string? Repository { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [MaxLength(100)]
        public string? Function { get; set; }

        public DateTime CreatedDate { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [MaxLength(50)]
        public string? ErrorCode { get; set; }
    }
}
