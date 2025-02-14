﻿using System.ComponentModel.DataAnnotations;

namespace OpenSpark.ApiGateway.InputModels
{
    public class CommentInputModel
    {
        [Required]
        public string Body { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Invalid post id")]
        public string PostId { get; set; }

        [Required]
        public string ConnectionId { get; set; }

        [Required]
        public string Callback { get; set; }
    }
}