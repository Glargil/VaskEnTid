using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using VaskEnTidLibrary.Interfaces;
using VaskEnTidLibrary.Models;


namespace VaskEnTidLibrary.Repos
{
    public class BookingRepo : IBookingRepo
    {
        private string _connectionString;
        public BookingRepo(string connectionString)
        {
            _connectionString = "Server=mssql.mkhansen.dk,1436;Database=Laundromat;User Id=sa;Password=Laundromat25;Encrypt=true;TrustServerCertificate=True;";
        }
        // Implement methods defined in IBookingRepo interface
        #region GetAllBookings
        public List<Booking> GetAllBookings()
        {
            var bookings = new List<Booking>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT BookingID, TenantID, MachineID, StartTime, EndTime FROM Booking", connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var booking = new Booking
                        {
                            BookingID = (int)reader["BookingID"],
                            TenantID = (int)reader["TenantID"],
                            MachineID = (int)reader["MachineID"],
                            BookingDate = ((DateTime)reader["StartTime"]).Date,
                            StartSlot = (Booking.LaundrySlot)((DateTime)reader["StartTime"]).Hour
                        };
                        bookings.Add(booking); // <-- Add to list
                    }
                }
                connection.Close();
            }
            return bookings;
        }
        #endregion
        #region GetBookingByID
        public Booking GetBookingByID(int id)
        {
            Booking booking = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT BookingID, TenantID, MachineID, StartTime, EndTime FROM Booking WHERE BookingID = @BookingID", connection);
                command.Parameters.AddWithValue("@BookingID", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        booking = new Booking
                        {
                            BookingID = (int)reader["BookingID"],
                            TenantID = (int)reader["TenantID"],
                            MachineID = (int)reader["MachineID"],
                            BookingDate = ((DateTime)reader["StartTime"]).Date,
                            StartSlot = (Booking.LaundrySlot)((DateTime)reader["StartTime"]).Hour
                        };
                    }
                }
                connection.Close();
            }
            return booking;
        }
        #endregion
        #region AddBooking
        public void CreateBooking(Booking booking)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO Booking (TenantID, MachineID, StartTime, EndTime) VALUES (@TenantID, @MachineID, @StartTime, @EndTime)", connection);
                command.Parameters.AddWithValue("@TenantID", booking.TenantID);
                command.Parameters.AddWithValue("@MachineID", booking.MachineID);
                command.Parameters.AddWithValue("@StartTime", booking.StartTime);
                command.Parameters.AddWithValue("@EndTime", booking.EndTime);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        #endregion
        #region UpdateBooking
        public void UpdateBooking(Booking booking)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE Booking SET TenantID = @TenantID, MachineID = @MachineID, StartTime = @StartTime, EndTime = @EndTime WHERE BookingID = @BookingID", connection);
                command.Parameters.AddWithValue("@BookingID", booking.BookingID);
                command.Parameters.AddWithValue("@TenantID", booking.TenantID);
                command.Parameters.AddWithValue("@MachineID", booking.MachineID);
                command.Parameters.AddWithValue("@StartTime", booking.StartTime);
                command.Parameters.AddWithValue("@EndTime", booking.EndTime);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        #endregion
        #region DeleteBooking
        public void DeleteBooking(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM Booking WHERE BookingID = @BookingID", connection);
                command.Parameters.AddWithValue("@BookingID", id);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        #endregion
    }
}
