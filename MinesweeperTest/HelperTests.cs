using System;
using System.IO;
using System.Linq;
using Minesweeper;
using Xunit;
using Xunit.Sdk;
using Moq;

namespace TestProject1
{
    public class HelperTests
    {
        [Fact]
        public void TestIfReadFileCanReadFile()
        {
            var file = new Helper(new ConsoleActions());
            var actual = file.ReadFile(new FileUtils()).First().Trim();
            var expected = "44";
            Assert.Equal(expected, actual);

        } 

        [Fact]
        public void TestIfReadFileCatchesExceptionWhileReadingFile()
        {
            string fileName = "Mines.csv";
            string fileLocation = Path.Combine(Environment.CurrentDirectory, $"{fileName}");
            var fileMock = new Mock<IFile>();
            var consoleActionsMock = new Mock<IConsole>();
            
            var e = new FileNotFoundException();
            fileMock.Setup(s => s.ReadLines(fileLocation))
                .Throws(e);
            
            var file = new Helper(consoleActionsMock.Object);
            file.ReadFile(fileMock.Object);

            consoleActionsMock.Verify(s => s.WriteLine($"The file was not found: '{e}'"), Times.Once());
            
            /*var actual = file.ReadFile(fileMock.Object);
            
            string expected = $"The file was not found:";
     
            Assert.Null(actual);*/
        }
        
        
    }

    
        
}