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
            SqlConnection connection = null;
            connection = new SqlConnection(_connectionString);
            var bookings = new List<Booking>();
            try
            {
                {
                    var command = new SqlCommand("SELECT BookingID, TenantID, MachineID, BookingDate, StartSlot FROM Booking", connection);
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
                                BookingDate = ((DateTime)reader["BookingDate"]).Date,
                                StartSlot = (Booking.LaundrySlot)(int)reader["StartSlot"]
                            };
                            bookings.Add(booking);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("A database error occurred while retrieving all bookings: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all bookings: " + ex.Message, ex);
            }
            finally
            {
                if (connection != null && connection.State != System.Data.ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
            return bookings;
        }
        #endregion
        #region GetBookingByID
        public Booking GetBookingByID(int id)
        {
            SqlConnection connection = null;
            connection = new SqlConnection(_connectionString);
            Booking booking = null;
            try
            {
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
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"A database error occurred while retrieving booking with ID {id}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving booking with ID {id}: {ex.Message}", ex);
            }
            finally
            {
                if (connection != null && connection.State != System.Data.ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
            return booking;
        }
        #endregion
        #region AddBooking
        public void CreateBooking(Booking booking)
        {
            SqlConnection connection = null;
            connection = new SqlConnection(_connectionString);
            try
            {
                {
                    var command = new SqlCommand("INSERT INTO Booking (TenantID, MachineID, StartSlot, BookingDate) VALUES (@TenantID, @MachineID, @StartSlot, @BookingDate)", connection);
                    command.Parameters.AddWithValue("@TenantID", booking.TenantID);
                    command.Parameters.AddWithValue("@MachineID", booking.MachineID);
                    command.Parameters.AddWithValue("@StartSlot", booking.StartSlot);
                    command.Parameters.AddWithValue("@BookingDate", booking.BookingDate);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("A database error occurred while creating a booking: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating a booking: " + ex.Message, ex);
            }
            finally
            {
                if (connection != null && connection.State != System.Data.ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
        }
        #endregion
        #region UpdateBooking
        public void UpdateBooking(Booking booking)
        {
            SqlConnection connection = null;
            connection = new SqlConnection(_connectionString);
            try
            {
                {
                    var command = new SqlCommand("INSERT INTO Booking (TenantID, MachineID, StartSlot, BookingDate) VALUES (@TenantID, @MachineID, @StartSlot, @BookingDate)", connection);
                    command.Parameters.AddWithValue("@TenantID", booking.TenantID);
                    command.Parameters.AddWithValue("@MachineID", booking.MachineID);
                    command.Parameters.AddWithValue("@StartSlot", booking.StartSlot);
                    command.Parameters.AddWithValue("@BookingDate", booking.BookingDate);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("A database error occurred while updating a booking: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating a booking: " + ex.Message, ex);
            }
            finally
            {
                if (connection != null && connection.State != System.Data.ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
        }
        #endregion
        #region DeleteBooking
        public void DeleteBooking(int id)
        {
            SqlConnection connection = null;
            connection = new SqlConnection(_connectionString);
            try
            {
                {
                    var command = new SqlCommand("DELETE FROM Booking WHERE BookingID = @BookingID", connection);
                    command.Parameters.AddWithValue("@BookingID", id);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"A database error occurred while deleting booking with ID {id}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting booking with ID {id}: {ex.Message}", ex);
            }
            finally
            {
                if (connection != null && connection.State != System.Data.ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
        }
        #endregion
    }
}
