using Application.DTOs.Flight;
using Application.Helpers;
using Application.Response;
using Domain.Enums;
using Domain.Models;

namespace Application.Mapper;

public static class FlightMapper
{
    public static Flight MapToFlight(this CreateFlightDto dto)
    {
        return new Flight
        {
            OriginId = dto.OriginId,
            DestinationId = dto.DestinationId,
            Departure = dto.DepartureTime,
            Arrival = dto.ArrivalTime,
            Status = FlightStatus.RegistrationOpen
        };
    }

    public static FlightResponse MapToFlightResponse(this Flight flight)
    {
        return new FlightResponse
        {
            Id = flight.Id,
            Origin = flight.Origin.MapToCityResponse(),
            Destination = flight.Destination.MapToCityResponse(),
            DepartureTime = flight.Departure,
            ArrivalTime = flight.Arrival,
            Status = flight.Status.ToString()
        };
    }
    
    public static IEnumerable<FlightResponse> MapToFlightResponse(this IEnumerable<Flight> flights)
    {
        return flights.Select(f => f.MapToFlightResponse());
    }

    public static void Update(this Flight flight, UpdateFlightDto dto)
    {
        flight.OriginId = dto.OriginId;
        flight.DestinationId = dto.DestinationId;
        flight.Departure = dto.DepartureTime;
        flight.Arrival = dto.ArrivalTime;
        flight.Status = dto.Status.GetFlightStatus();
    }
}