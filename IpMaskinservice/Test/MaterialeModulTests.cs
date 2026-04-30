using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Test
{
    public class MaterialeModulTests
    {

        //test 1: Negativ antal 
        [Fact]
        public void Tilfoej_FejlVedNegativAntal()
        {
            // Arrange
            var liste = new MaterialeListe();
            var type = new MaterialeType("Olie");

            // Act & Assert
            // Tjekker om koden kaster en fejl, når antallet er 0 eller mindre
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                liste.createMaterialeLinje(-1, "Test info", type));
        }

        // Test 2:Add method 
        [Fact]
        public void Tilfoej_MaterialeLinje_Success()
        {
            // Arrange
            var liste = new MaterialeListe();
            var type = new MaterialeType("Filter");

            // Act
            liste.createMaterialeLinje(1, "Test info", type);

            // Assert
            Assert.Single(liste.MaterialeLinjeListe);
            var linje = liste.MaterialeLinjeListe[0];

            Assert.Equal(1, linje.Antal);
            Assert.Equal("Test info", linje.Information);
            Assert.Equal(type, linje.MaterialeType);
        }
    }
     
}
