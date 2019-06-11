using System;
using System.Collections.Generic;
using System.Text;
using ApartmentReservation.Application.Infrastructure.AutoMapper;
using AutoMapper;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Infrastructure.AutoMapper
{
    public class AutoMapperProfileTests
    {
        [Fact]
        public void ConfigurationIsValid()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            mappingConfig.AssertConfigurationIsValid();
        }
    }
}