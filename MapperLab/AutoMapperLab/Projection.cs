using System;
using AutoMapper;
using AutoMapperLab.Models.Projection;
using Xunit;

namespace AutoMapperLab
{
    class Projection
    {
        public void Test()
        {
            // Model
            var calendarEvent = new CalendarEvent
            {
                Date = new DateTime(2008, 12, 15, 20, 30, 0),
                Title = "Company Holiday Party"
            };

            // Configure AutoMapper
            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<CalendarEvent, CalendarEventForm>()
                    .ForMember(dest => dest.EventDate, opt => opt.MapFrom(src => src.Date.Date))
                    .ForMember(dest => dest.EventHour, opt => opt.MapFrom(src => src.Date.Hour))
                    .ForMember(dest => dest.EventMinute, opt => opt.MapFrom(src => src.Date.Minute)));

            var mapper = new Mapper(configuration);

            // Perform mapping
            CalendarEventForm form = mapper.Map<CalendarEvent, CalendarEventForm>(calendarEvent);

            Assert.Equal(new DateTime(2008, 12, 15), form.EventDate);
            Assert.Equal(20, form.EventHour);
            Assert.Equal(30, form.EventMinute);
            Assert.Equal("Company Holiday Party", form.Title);
        }
    }
}
