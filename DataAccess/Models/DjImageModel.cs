﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public enum DjMediaType // Future Proofing
    {
        IMAGE,
        VIDEO,
        UNKNOWN = -1
    }
    public enum DjWebhookType // Future Proofing
    {
        DISCORD,
        UNKNOWN = -1
    }

    internal class DjImageModel
    {
    }

    // Annotations: https://entityframeworkcore.com/model-data-annotations

    public class DjImage // Base Image Class
    {
        [Key, Required]
        public Guid ID { get; set; } // generated UUID of the image

        [Required]
        public DjMediaType Type { get; set; } // Type of image. Here for future proofing

        [Required]
        public DjDumpUser Uploader { get; set; } // The user who uploaded this image, duh

        [Required]
        public List<DjDumpUser> AllowedUsers { get; set; } // Users that have access to the image (At least the Uploader and Author if applicable)

        public List<DjDumpGroup> AllowedGroups { get; set; } // Groups that have access to the image (Optional)
    }

    public class DjVRCImage : DjImage // Image class specifically for vrc pics (future proofing)
    {
        public DjVRCWorld World { get; set; } // World the image was taken in (Optional)
        public DjVRCUser Author { get; set; } // Player that took the picture (might be someone else who is uploading it) (Optional)
        public List<DjVRCUser> Players { get; set; } // VRC Users visible in image (May be an empty list)
    }

    public class DjDumpUser
    {
        [Key, Required]
        public Guid ID { get; set; } // generated UUID of the user

        [Required]
        public DjVRCUser VRCIdentity { get; set; } // VRC profile of this user

        [Required]
        public long DiscordID { get; set; } // Discord ID. Required as Discord will be primary login method

        public List<DjWebhook> Webhooks { get; set; } // List of webhooks added by the user
    }

    public class DjDumpGroup // For Image access control
    {
        [Key, Required]
        public Guid ID { get; set; } // Generated UUID of the group

        [Required]
        public List<DjDumpUser> Users { get; set; } // Users in this group
    }

    public class DjVRCWorld
    {
        [Key, Required]
        public string ID { get; set; } // VRC World ID

        [Required]
        public DjVRCUser Author { get; set; } // Author of the world. Just for novelty of looking at pictures of world of user X
    }

    public class DjVRCUser
    {
        [Key, Required]
        public string ID { get; set; } // VRC User ID

        [Required]
        public DjVRCUsername Username { get; set; } // Current VRC Username

        [Required]
        public virtual ICollection<DjVRCUsername> KnownUsernames { get; set; } // Know VRC Usernames, including current
    }

    public class DjVRCUsername
    {
        [ForeignKey("ID"), Required]
        public DjVRCUser VRCUser { get; set; }

        [Required]
        [MinLength(4, ErrorMessage = "VRC Username has to be at least 4 characters long")]
        [MaxLength(16, ErrorMessage = "VRC Username can be at most 16 characters long")]
        public string Username { get; set; }// VRC Username
    }

    public class DjWebhook
    {
        [Key, Required]
        public Guid ID { get; set; } // Generated UUID of webhook

        [Required]
        public string Name { get; set; } // Name of the Webhook. Used by the user to identify

        [Required]
        public Uri Url { get; set; } // Url of the Webhook, for obvious reasons

        [Required]
        public DjWebhookType Type { get; set; } // Type of Webhook. In case we need another type to be supported in the future
    }

}