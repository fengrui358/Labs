﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp9Lab.Init
{
    public record WeatherObservation
    {
        public DateTime RecordedAt { get; init; }

        public decimal TemperatureInCelsius { get; init; }

        public decimal PressureInMillibars { get; init; }

        public override string ToString() =>

            $"At {RecordedAt:h:mm tt} on {RecordedAt:M/d/yyyy}: " +

            $"Temp = {TemperatureInCelsius}, with {PressureInMillibars} pressure";
    }
}
