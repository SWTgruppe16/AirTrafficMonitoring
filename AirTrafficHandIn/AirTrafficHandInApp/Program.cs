﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficHandIn;



namespace AirTrafficHandInApp
{
    [ExcludeFromCodeCoverage]
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleLogger consoleLogger = new ConsoleLogger();
            var transponderReceiver = TransponderReceiver.TransponderReceiverFactory.CreateTransponderDataReceiver();
            var splitter = new Splitter();
            transponderReceiver.TransponderDataReady += splitter.OnTransponderData;

            var airspace = new Airspace
            {
                X = 0,
                Y = 0,
                Z = 500,
                width = 80000,
                depth = 80000,
                height = 20000
            };
            var tracker = new TrackCalculator();
            var airspace_monitor = new AirspaceMonitor(airspace, tracker);
            airspace_monitor.NewTrackInTairSpaceEvent += consoleLogger.LogTrackEntered;
            airspace_monitor.TrackLeavesAirSpaceEvent += consoleLogger.LogTrackLeft;
            
            //splitter.newTrack += delegate(object sender, NewTrackArgs trackArgs)
            //{
            //    Console.WriteLine("Nyt Track Event\n=====================");
            //    foreach (var track in trackArgs.Tracks)
            //    {
            //        Console.WriteLine(track);
            //    }
            //};

            splitter.NewTracks += airspace_monitor.OnTrackRecieved;


            //airspace_monitor.TracksInAirspaceEvent += delegate(object sender, TracksInAirspaceArgs airspaceArgs)
            //{
            //    Console.WriteLine("Current In Airspace");
            //    foreach (var track in airspaceArgs.Tracks)
            //    {
            //        Console.WriteLine(track);
            //    }
            //};

            var seperation_moniotr = new SeparationMonitor();
            airspace_monitor.TracksInAirspaceEvent += seperation_moniotr.OnTrackRecieved;

            while (true)
            {
                System.Threading.Thread.Sleep(1000);
            }

           
        }
    }
}
