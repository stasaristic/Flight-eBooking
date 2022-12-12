// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Flight_eBooking.Data.Enums;
using Flight_eBooking.Models;

namespace Flight_eBooking.Areas.Identity.Data
{
    public class ApplicationDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                //context.Database.EnsureCreated();

                //Destination
                if (!context.Destinations.Any())
                {
                    context.AddRange(new List<Destination>() {

                        new Destination() {
                        NameDest = "Beograd",
                        ImageDest = "http://t3.gstatic.com/licensed-image?q=tbn:ANd9GcQnW9woJrgkmZsePNxOu1IXa1rI4ImI2Ss_Cin0NS2J6HOC5rb-ue48eRZ-5aC5IjvY",
                        DescDest="Београд је главни и најнасељенији град Републике Србије и привредно, културно и образовно средиште земље. Град лежи на ушћу Саве у Дунав, где се Панонска низија спаја са Балканским полуострвом."
                        },
                        new Destination()
                        {
                            NameDest = "Kraljevo",
                            ImageDest = "http://t2.gstatic.com/licensed-image?q=tbn:ANd9GcS2ut8BzknErg1uCCRikqeY79vWc7nGnTaGlTj-_m60huUHz8F0M_drDBAa5V63t3bt",
                            DescDest = "Краљево је град у Србији у Рашком округу. Према попису из 2011. било је 64.175 становника."
                        },
                        new Destination()
                        {
                            NameDest = "Nis",
                            ImageDest = "http://t2.gstatic.com/licensed-image?q=tbn:ANd9GcQiSWrysQj8qfhJvV0shR7tYwOuZcx0VqNTy834yVlpdNrZvWxQu80IOdkzOMVFl4IM",
                            DescDest = "Ниш је највећи град на југу Републике Србије и седиште Нишавског управног округа."
                        },
                        new Destination()
                        {
                            NameDest = "Pristina",
                            ImageDest = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fsh.wikipedia.org%2Fwiki%2FPri%25C5%25A1tina&psig=AOvVaw1i9am3LsH9el1mAA4bmy37&ust=1670887657076000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCJDY1tvb8vsCFQAAAAAdAAAAABAE",
                            DescDest = "Приштина је највећи град и административни центар Аутономне Покрајине Косово и Метохија, као и Косовског управног округа. Пети је град у Србији по површини, а четврти по броју становника у ширем подручју."
                        }
                    });
                    context.SaveChanges();
                }


                //Flight
                if (!context.Flights.Any())
                {
                    context.AddRange(new List<Flight>()
                    {
                        new Flight()
                        {
                            FlightName = "Beograd-Kraljevo",
                            DepartureDate = DateTime.Now.AddDays(10),
                            TicketPrice = 100,
                            Seats = 250,
                            FlightClass = FlightClass.First,
                            DestinationDepartureId = 1,
                            DestinationArrivalId = 2
                            },
                        new Flight()
                        {
                            FlightName = "Kraljevo-Pristina",
                            DepartureDate = DateTime.Now.AddDays(10),
                            TicketPrice = 150,
                            Seats = 150,
                            FlightClass = FlightClass.Business,
                            DestinationDepartureId = 2,
                            DestinationArrivalId = 4
                        },
                        new Flight()
                        {
                            FlightName = "Nis-Kraljevo",
                            DepartureDate = DateTime.Now.AddDays(10),
                            TicketPrice = 200,
                            Seats = 200,
                            FlightClass = FlightClass.First,
                            DestinationDepartureId = 3,
                            DestinationArrivalId = 2
                        },
                        new Flight()
                        {
                            FlightName = "Kraljevo-Beograd",
                            DepartureDate = DateTime.Now.AddDays(10),
                            TicketPrice = 100,
                            Seats = 250,
                            FlightClass = FlightClass.First,
                            DestinationDepartureId = 2,
                            DestinationArrivalId = 1
                        },
                        new Flight()
                        {
                            FlightName = "Pristina-Nis",
                            DepartureDate = DateTime.Now.AddDays(10),
                            TicketPrice = 100,
                            Seats = 250,
                            FlightClass = FlightClass.First,
                            DestinationDepartureId = 3,
                            DestinationArrivalId = 4
                        },

                    });
                    context.SaveChanges();

                }


                //Reservation
                if (!context.Reservations.Any()) { }

            }
        }
    }
}
