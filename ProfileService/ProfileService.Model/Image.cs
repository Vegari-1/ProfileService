using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProfileService.Model
{
	[Table("Images", Schema = "profile")]
	public class Image
	{
		public Guid Id { get; set; }
		public string Content { get; set; }
	}
}

