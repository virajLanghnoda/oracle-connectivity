using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace oracle_connectivity.Models
{
    public enum roomType
    {
        Delux,
        Super,
        Premium
    }
    public class Room
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "RoomId is required.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "RoomId must be digits only.")]
        public int RoomId { get; set; }

        [Required(ErrorMessage = "RoomNumber is required.")]
        public int RoomNumber { get; set; }

        [Required(ErrorMessage = "NumberOfPerson is required.")]
        [Range(1, 3, ErrorMessage = "NumberOfPerson must be less than 4.")]
        public int NumberOfPerson { get; set; }

        [Required(ErrorMessage = "CheckInDate is required.")]
        [DataType(DataType.DateTime)]
        public DateTime CheckInDate { get; set; }

        [Required(ErrorMessage = "CheckOutDate is required.")]
       
        public DateTime CheckOutDate { get; set; }

        [Required(ErrorMessage = "RoomType is required.")]
        public roomType RoomType { get; set; }

        public string Description { get; set; }
        public int Price
        {
            get
            {
                switch (RoomType)
                {
                    case roomType.Delux:
                        return 1000;
                    case roomType.Super:
                        return 1500;

                    case roomType.Premium:
                        return 2500;

                    default:
                        return 0;
                }
            }
        }
    }
}
