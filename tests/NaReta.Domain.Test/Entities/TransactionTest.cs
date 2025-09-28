namespace NaReta.Domain.Test.Entities
{
    public class TransactionTest
    {
        [Fact]
        public void Create_Transaction_Instance_Success()
        {
            // Arrange
            var transcation = new Transaction(1, "despesa", 100.02, new DateTime(2025, 1, 1), "despesa 1");
            // Act

            // Assert
        }
    }
}
