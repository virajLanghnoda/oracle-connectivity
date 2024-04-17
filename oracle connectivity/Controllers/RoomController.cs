using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using oracle_connectivity.Models;

namespace oracle_connectivity.Controllers
{
    [Route("/room")]
    [ApiController]
    [EnableCors("AllowAngularApp")]
    public class RoomController : ControllerBase
    {

        //List
        public static List<Room> RoomList = new List<Room>();

        private readonly IDbConnection _conn;
       
        public RoomController(IDbConnection connection)
        {
            _conn = connection;
        }

        // GET api/values
        [HttpGet]
        [Route("/getRooms")]
        public IActionResult GetAllRoomsList()
        {
            try
            {
                _conn.Open();
                RoomList.Clear();
                var query = "SELECT * FROM Rooms";
                var command = _conn.CreateCommand();
                command.CommandText = query;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Room room = new Room
                        {
                            Name = reader["Name"].ToString(),
                            RoomId = Convert.ToInt32(reader["RoomId"]),
                            RoomNumber = Convert.ToInt32(reader["RoomNumber"]),
                            NumberOfPerson = Convert.ToInt32(reader["NumberOfPerson"]),
                            CheckInDate = DateTime.Parse(reader["CheckInDate"].ToString()),
                            CheckOutDate = DateTime.Parse(reader["CheckOutDate"].ToString()),
                            RoomType = (roomType)Enum.Parse(typeof(roomType), reader["RoomType"].ToString()),
                            Description = reader["Description"].ToString()
                        };
                        RoomList.Add(room);
                    }
                }

                return Ok(RoomList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database Error: {ex.Message}");
            }
            finally
            {
                _conn.Close();
            }

        }

        //List 2 for showing search result
        public List<Room> RoomList2 = new List<Room>();
        [HttpGet("/getRooms/{id}")]
        public IActionResult GetFromRoomNumber(int id)
        {
            try
            {
                _conn.Open();

                var query = "SELECT * FROM Rooms WHERE ROOMNUMBER= :ID ";
                var command = _conn.CreateCommand();
                command.CommandText = query;
                command.Parameters.Add(new OracleParameter("ID", id));
                RoomList2.Clear();
                using (var reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        Room room = new Room
                        {
                            Name = reader["Name"].ToString(),
                            RoomId = Convert.ToInt32(reader["RoomId"]),
                            RoomNumber = Convert.ToInt32(reader["RoomNumber"]),
                            NumberOfPerson = Convert.ToInt32(reader["NumberOfPerson"]),
                            CheckInDate = DateTime.Parse(reader["CheckInDate"].ToString()),
                            CheckOutDate = DateTime.Parse(reader["CheckOutDate"].ToString()),
                            RoomType = (roomType)Enum.Parse(typeof(roomType), reader["RoomType"].ToString()),
                            Description = reader["Description"].ToString()
                        };
                        RoomList2.Add(room);
                    }
                }

                return Ok(RoomList2);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database Error: {ex.Message}");
            }
            finally
            {
                _conn.Close();
            }
        }


        //List 3 for showing search result
        public List<Room> RoomList3 = new List<Room>();
        [HttpGet("/getRoomsid/{id}")]
        public IActionResult GetFromRoomId(int id)
        {
            try
            {
                _conn.Open();

                var query = "SELECT * FROM Rooms WHERE ROOMID= :ID ";
                var command = _conn.CreateCommand();
                command.CommandText = query;
                command.Parameters.Add(new OracleParameter("ID", id));
                RoomList3.Clear();
                using (var reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        Room room = new Room
                        {
                            Name = reader["Name"].ToString(),
                            RoomId = Convert.ToInt32(reader["RoomId"]),
                            RoomNumber = Convert.ToInt32(reader["RoomNumber"]),
                            NumberOfPerson = Convert.ToInt32(reader["NumberOfPerson"]),
                            CheckInDate = DateTime.Parse(reader["CheckInDate"].ToString()),
                            CheckOutDate = DateTime.Parse(reader["CheckOutDate"].ToString()),
                            RoomType = (roomType)Enum.Parse(typeof(roomType), reader["RoomType"].ToString()),
                            Description = reader["Description"].ToString()
                        };
                        RoomList3.Add(room);
                    }
                }
                return Ok(RoomList3);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database Error: {ex.Message}");
            }
            finally
            {
                _conn.Close();
            }
        }

        // POST 
        [HttpPost]
        [Route("/postRooms")]
        public IActionResult AddRoom(Room room)
        {
            try
            {
                _conn.Open();
                Room item = RoomList.Find(p => p.RoomId == room.RoomId);
                int index = RoomList.IndexOf(item);
                if (index >= 0)
                { }
                else
                {
                    var query = "INSERT INTO Rooms (Name, RoomId, RoomNumber, NumberOfPerson, CheckInDate, CheckOutDate, RoomType, Description, Price) " +
                                "VALUES (:Name, :RoomId, :RoomNumber, :NumberOfPerson, :CheckInDate, :CheckOutDate, :RoomType, :Description, :Price)";
                    var command = new OracleCommand(query, (OracleConnection)_conn);
                    command.Parameters.Add("Name", OracleDbType.Varchar2).Value = room.Name;
                    command.Parameters.Add("RoomId", OracleDbType.Int32).Value = room.RoomId;
                    command.Parameters.Add("RoomNumber", OracleDbType.Int32).Value = room.RoomNumber;
                    command.Parameters.Add("NumberOfPerson", OracleDbType.Int32).Value = room.NumberOfPerson;
                    command.Parameters.Add("CheckInDate", OracleDbType.Date).Value = room.CheckInDate;
                    command.Parameters.Add("CheckOutDate", OracleDbType.Date).Value = room.CheckOutDate;
                    command.Parameters.Add("RoomType", OracleDbType.Varchar2).Value = room.RoomType;
                    command.Parameters.Add("Description", OracleDbType.Varchar2).Value = room.Description;
                    command.Parameters.Add("Price", OracleDbType.Int32).Value = room.Price;
                    command.ExecuteNonQuery();

                }
                RoomList.Add(room);
                return Ok(new { message="Successfull" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            finally
            {
                _conn.Close();
            }
        }

       
        // PUT 
        [HttpPut("/updateRoom/{id}")]
        public IActionResult UpdateRoom(int id, Room room)
        {
            try
            {
                _conn.Open();

                var checkQuery = "SELECT COUNT(*) FROM Rooms WHERE RoomId = :RoomId";
                var checkCommand = new OracleCommand(checkQuery, (OracleConnection)_conn);
                checkCommand.Parameters.Add("RoomId", OracleDbType.Int32).Value = id;
                int roomCount = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (roomCount == 0)
                {
                    return NotFound("Room not found");
                }

                // Execute SQL query to update the room details in the database
                var updateQuery = "UPDATE Rooms SET Name = :Name, RoomNumber = :RoomNumber, NumberOfPerson = :NumberOfPerson, CheckInDate = :CheckInDate, CheckOutDate = :CheckOutDate, RoomType = :RoomType, Description = :Description WHERE RoomId = :RoomId";
                var command = new OracleCommand(updateQuery, (OracleConnection)_conn);
                command.Parameters.Add("Name", OracleDbType.Varchar2).Value = room.Name;
                command.Parameters.Add("RoomNumber", OracleDbType.Int32).Value = room.RoomNumber;
                command.Parameters.Add("NumberOfPerson", OracleDbType.Int32).Value = room.NumberOfPerson;
                command.Parameters.Add("CheckInDate", OracleDbType.Date).Value = room.CheckInDate;
                command.Parameters.Add("CheckOutDate", OracleDbType.Date).Value = room.CheckOutDate;
                command.Parameters.Add("RoomType", OracleDbType.Varchar2).Value = room.RoomType;
                command.Parameters.Add("Description", OracleDbType.Varchar2).Value = room.Description;
                command.Parameters.Add("RoomId", OracleDbType.Int32).Value = id;
                command.ExecuteNonQuery();

                return Ok(new { message = "Successfully updated" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            finally
            {
                _conn.Close();
            }
        }

        // DELETE api/values/5
        [HttpDelete("/deleteRoom/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {

                _conn.Open();

                var query = "DELETE FROM Rooms WHERE RoomId =: ID";
                var command = _conn.CreateCommand();
                command.CommandText = query;
                command.Parameters.Add(new OracleParameter("ID", id));

                command.ExecuteNonQuery();
                return Ok(new {message= "Successfull delete" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            finally
            {
                _conn.Close();
            }
        }
    }
}