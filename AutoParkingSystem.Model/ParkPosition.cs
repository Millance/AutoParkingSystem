using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoParkingSystem.Model
{
    [Table("park_position")]
    public class ParkPosition
    {
        /// <summary>
        /// 位置id
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// 位置名称
        /// </summary>
        [Column("position_name")]
        [MaxLength(10)]
        public string PositionName { get; set; } = string.Empty;

        /// <summary>
        /// 停放车辆的车牌号
        /// </summary>
        [Column("license_plate_number")]
        [MaxLength(10)]
        public string LicensePlateNumber { get; set; } = string.Empty;

        /// <summary>
        /// 进入位置的时间
        /// </summary>
        [Column("park_in_time")]
        public DateTime ParkInTime { get; set; } = DateTime.Now;
    }
}