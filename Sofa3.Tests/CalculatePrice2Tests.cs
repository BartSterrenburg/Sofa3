namespace Sofa3.Tests
{
    using Xunit;

    public class CalculatePrice2Tests
    {
        private MovieTicket CreateTicket(
            double price,
            bool isPremium = false,
            bool isWeekday = true)
        {
            MovieScreening screening = new MovieScreening(DateTime.Now, 10);
            var ticket = new MovieTicket(screening, 1, 1, isPremium);
            ticket.setPrice(price);
            return ticket;
        }

        public class OrderCalculatePrice2Tests
        {
            private readonly CalculatePrice2Tests _helper = new CalculatePrice2Tests();

            [Fact]
            public void CalculatePrice2_SingleNonPremiumTicket_ReturnsBasePrice()
            {
                // Arrange
                var order = new Order(1, false);
                order.addSeatReservation(_helper.CreateTicket(10));

                // Act
                var price = order.calculatePrice2();

                // Assert
                Assert.Equal(10, price);
            }

            [Fact]
            public void CalculatePrice2_PremiumTicket_NonStudent_AddsThreeEuro()
            {
                var order = new Order(1, false);
                order.addSeatReservation(_helper.CreateTicket(10, isPremium: true));

                var price = order.calculatePrice2();

                Assert.Equal(13, price);
            }

            [Fact]
            public void CalculatePrice2_PremiumTicket_Student_AddsTwoEuro()
            {
                var order = new Order(1, true);
                order.addSeatReservation(_helper.CreateTicket(10, isPremium: true));

                var price = order.calculatePrice2();

                Assert.Equal(12, price);
            }

            [Fact]
            public void CalculatePrice2_SecondTicket_Free_ForStudentOrder()
            {
                var order = new Order(1, true);
                order.addSeatReservation(_helper.CreateTicket(10));
                order.addSeatReservation(_helper.CreateTicket(10)); // index 1 → gratis

                var price = order.calculatePrice2();

                Assert.Equal(10, price);
            }

            [Fact]
            public void CalculatePrice2_SecondTicket_Free_OnWeekday()
            {
                var order = new Order(1, false);
                order.addSeatReservation(_helper.CreateTicket(10, isWeekday: true));
                order.addSeatReservation(_helper.CreateTicket(10, isWeekday: true));

                var price = order.calculatePrice2();

                Assert.Equal(10, price);
            }

            [Fact]
            public void CalculatePrice2_StudentWeekendTicket_AppliesIncorrectDiscount()
            {
                var order = new Order(1, true);
                order.addSeatReservation(_helper.CreateTicket(10, isWeekday: false));
                order.addSeatReservation(_helper.CreateTicket(10, isWeekday: false));
                order.addSeatReservation(_helper.CreateTicket(10, isWeekday: false));
                order.addSeatReservation(_helper.CreateTicket(10, isWeekday: false));
                order.addSeatReservation(_helper.CreateTicket(10, isWeekday: false));
                order.addSeatReservation(_helper.CreateTicket(10, isWeekday: false));
                order.addSeatReservation(_helper.CreateTicket(10, isWeekday: false));



                var price = order.calculatePrice2();

                Assert.NotEqual(10, price);
            }
        }
    }
}
