using System;
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

    public enum DjVisibility
    {
        PRIVATE,
        SHARED_SERVERS,
        PUBLIC,
        UNKNOWN = -1
    }

    public enum DjUserRank
    {
        USER,
        BETTER_USER,
        HOST,
        UNKNOWN = -1
    }

    internal class DjImageModel
    {
    }


    // Maybe todo (waaay later) : Add a way for users to upload their images to their own services (Amazon S3, FTP, google drive etc.)

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
        public virtual ICollection<DjDumpUser> AllowedUsers { get; set; } // Users that have access to the image (At least the Uploader and Author if applicable) // Whitelist. Higher prio than this.Visibility

        public virtual ICollection<DjDumpGroup> AllowedGroups { get; set; } // Groups that have access to the image (Optional) // Whitelist. Higher prio than this.Visibility

        public virtual ICollection<DjImageTag> Tags { get; set; } // Tags on this image

        [Required]
        public bool NSFW { get; set; } = false; // Whether or not image contains NSFW content

        [Required, Range(-1, 2)]
        public DjVisibility Visibility { get; set; } = DjVisibility.PRIVATE; // Visibility of the image. Defaults to Private, just in case

        [Required, Timestamp]
        public DateTime TimeUploaded { get; set; } // Time of upload

        public DateTime TimeTaken { get; set; } // Time the picture was taken (Optional)
    }

    public class DjVRCImage : DjImage // Image class specifically for vrc pics (future proofing)
    {
        public DjVRCWorld World { get; set; } // World the image was taken in (Optional)
        public DjVRCUser Author { get; set; } // Player that took the picture (might be someone else who is uploading it) (Optional)
        public virtual ICollection<DjVRCUser> Players { get; set; } // VRC Users visible in image (May be an empty list)
    }

    public class DjImageTag
    {
        [Key, Required]
        public Guid ID { get; set; }

        [Required, MinLength(2), MaxLength(15)]
        public string Name { get; set; }
    }

    public class DjBan
    {
        [Key, Required]
        public Guid ID { get; set; }

        [Required]
        public DjDumpUser User { get; set; }

        [Required, MinLength(5), MaxLength(50)]
        public string Reason { get; set; }

        [Timestamp]
        public DateTime CreatedAt { get; set; }

        public DateTime ExpiresAt { get; set; }
    }

    public class DjDumpUser
    {
        [Key, Required]
        public Guid ID { get; set; } // generated UUID of the user

        [Required]
        public DjUserRank Rank { get; set; } = DjUserRank.USER; // User rank of the user

        [Required, MinLength(2), MaxLength(20)]
        public string Name { get; set; } // Name of the user

        [Required]
        public DjUserSettings Settings { get; set; } // Settings of the user

        public DjVRCUser VRCIdentity { get; set; } // VRC profile of this user

        [Required]
        public long DiscordID { get; set; } // Discord ID. Required as Discord will be primary login method        

        public bool Adult { get; set; } = false; // Whether or not this user is allowed to see NSFW content        

        [Required]
        public bool IsBanned { get; set; } = false; // Whether or not this user is banned
    }

    public class DjUserSettings
    {
        [Key, Required]
        public int ID { get; set; } // The user settings ID

        [Required]
        public virtual ICollection<DjWebhook> Webhooks { get; set; } // List of webhooks added by the user

        [Required]
        public bool ShowNSFW { get; set; } = false; // Whether or not this user has enabled NSFW content

        [Required, Range(-1, 2)]
        public DjVisibility Visibility { get; set; } = DjVisibility.SHARED_SERVERS; // The visibility of the profile
    }

    public class DjDumpGroup // For Image access control
    {
        [Key, Required]
        public Guid ID { get; set; } // Generated UUID of the group

        [Required]
        public virtual ICollection<DjDumpUser> Users { get; set; } // Users in this group

        [Required, MinLength(3), MaxLength(15)]
        public string Name { get; set; }
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

        [Required, MinLength(3), MaxLength(20)]
        public string Name { get; set; } // Name of the Webhook. Used by the user to identify

        [Required]
        public Uri Url { get; set; } // Url of the Webhook, for obvious reasons

        [Required]
        public DjWebhookType Type { get; set; } // Type of Webhook. In case we need another type to be supported in the future
    }

}
