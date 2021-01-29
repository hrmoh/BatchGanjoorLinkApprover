using RSecurityBackend.Models.Auth.Db;
using System;

namespace RSecurityBackend.Models.Auth.ViewModels
{
    /// <summary>
    /// user informtaion
    /// </summary>
    /// <remarks>
    /// a safe subset of RAppUser
    /// </remarks>
    public class PublicRAppUser
    {
        /// <summary>
        /// Id
        /// </summary>
        /// 
        /// <example>
        /// </example>
        public Guid? Id { get; set; }

        /// <summary>
        /// User Name
        /// </summary>
        /// <example>
        /// test
        /// </example>
        public string Username { get; set; }

        /// <summary>
        /// User Email
        /// </summary>
        /// <example>
        /// test@ganjoor.net
        /// </example>
        public string Email { get; set; }

        /// <summary>
        /// User Mobile Phone Number
        /// </summary>
        /// <example>
        /// +989121234567
        /// </example>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// First Name
        /// </summary>
        /// <example>
        /// Hamid Reza
        /// </example>
        public string FirstName { get; set; }

        /// <summary>
        /// Sure Name
        /// </summary>
        /// <example>
        /// Mohammadi
        /// </example>
        public string SureName { get; set; }

        /// <summary>
        /// user status
        /// </summary>
        /// <example>
        /// 1
        /// </example>
        public RAppUserStatus Status { get; set; }

        /// <summary>
        /// user image
        /// </summary>
        public Guid? RImageId { get; set; }


    }
}
