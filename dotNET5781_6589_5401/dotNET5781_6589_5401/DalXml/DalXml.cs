﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using DLAPI;
using DO;

namespace DL
{
    public sealed class DalXml : IDAL
    {
        #region singelton

        static readonly DalXml instance = new DalXml();
        public static DalXml Instance => instance;
        static DalXml() { }
        DalXml() { }

        #endregion

        #region DS XML Files

        // all XElement:

        string usersPath = @"UsersXml.xml";
        string busesPath = @"BusesXml.xml";
        string linesPath = @"LinesXml.xml";
        string stationsPath = @"StationsXml.xml";
        string lineStationsPath = @"LineStationsXml.xml";
        string followingStationsPath = @"FollowingStationsXml.xml";
        string drivingLinesPath = @"DrivingLinesXml.xml";
        string managingCodePath = @"ManagingCodeXml.xml";

        #endregion

        #region Users

        public void addUser(User user)
        {
            XElement rootElem = XMLTools.LoadListFromXMLElement(usersPath);

            XElement item = (from i in rootElem.Elements()
                             where i.Element("Username").Value == user.Username
                             select i).FirstOrDefault();

            if (item != null)
                throw new UserException("The user already exists.");

            rootElem.Add(new XElement("User",
                                   new XElement("Username", user.Username),
                                   new XElement("Password", user.Password),
                                   new XElement("IsManager", user.IsManager)));

            XMLTools.SaveListToXMLElement(rootElem, usersPath);            
        }
        public void removeUser(User user)
        {
            XElement rootElem = XMLTools.LoadListFromXMLElement(usersPath);

            XElement item = (from i in rootElem.Elements()
                             where i.Element("Username").Value == user.Username
                             select i).FirstOrDefault();

            if (item != null)
            {
                item.Remove();
                XMLTools.SaveListToXMLElement(rootElem, usersPath);
            }
            else
                throw new UserException("The user does not exist.");
        }
        public void updateUser(User user)
        {
            removeUser(getUser(user.Username));
            addUser(user);            
        }
        public User getUser(string username)
        {
            return (from i in XMLTools.LoadListFromXMLElement(usersPath).Elements()
                         where i.Element("Username").Value == username
                         select new User()
                         {
                             Username = i.Element("Username").Value,
                             Password = i.Element("Password").Value,
                             IsManager = bool.Parse(i.Element("IsManager").Value)
                         }).FirstOrDefault();
        }
        public IEnumerable<User> GetUsers()
        {
            return from i in XMLTools.LoadListFromXMLElement(usersPath).Elements()
                   select new User()
                   {
                       Username = i.Element("Username").Value,
                       Password = i.Element("Password").Value,
                       IsManager = bool.Parse(i.Element("IsManager").Value)
                   };
        }
        public IEnumerable<User> GetUsers(Predicate<User> condition)
        {
            return from i in XMLTools.LoadListFromXMLElement(usersPath).Elements()
                   let item = new User()
                   {
                       Username = i.Element("Username").Value,
                       Password = i.Element("Password").Value,
                       IsManager = bool.Parse(i.Element("IsManager").Value)
                   }
                   where condition(item)
                   select item;
        }

        #endregion

        #region Buses

        public void addBus(Bus bus)
        {
            if (DataSource.Buses.Exists(item => item.LicensePlate == bus.LicensePlate))
                throw new BusException("The bus already exists.");
            DataSource.Buses.Add(bus.Clone());
        }
        public void removeBus(Bus bus)
        {
            XElement rootElem = XMLTools.LoadListFromXMLElement(busesPath);

            XElement item = (from i in rootElem.Elements()
                             where i.Element("LicensePlate").Value == bus.LicensePlate
                             select i).FirstOrDefault();

            if (item != null)
            {
                item.Remove();
                XMLTools.SaveListToXMLElement(rootElem, busesPath);
            }
            else
                throw new BusException("The bus does not exist.");
        }
        public void updateBus(Bus bus)
        {
            removeBus(getBus(bus.LicensePlate));
            addBus(bus);
        }
        public Bus getBus(string licensePlate)
        {
            Bus bus = DataSource.Buses.Find(item => item.LicensePlate == licensePlate);
            if (bus == null)
                return null;
            return bus.Clone();
        }
        public IEnumerable<Bus> GetBuses()
        {
            return from item in DataSource.Buses
                   select item.Clone();
        }
        public IEnumerable<Bus> GetBuses(Predicate<Bus> condition)
        {
            return from item in DataSource.Buses
                   where condition(item)
                   select item.Clone();
        }

        #endregion

        #region Lines

        public int addLine(Line line)
        {
            XElement rootElem = XMLTools.LoadListFromXMLElement(linesPath);

            rootElem.Add(new XElement("Line",
                                   new XElement("ThisSerial", line.ThisSerial),//serial?
                                   new XElement("NumberLine", line.NumberLine),
                                   new XElement("Region", line.Region)));

            XMLTools.SaveListToXMLElement(rootElem, linesPath);
            return line.ThisSerial;//
        }
        public void removeLine(Line line)
        {
            XElement rootElem = XMLTools.LoadListFromXMLElement(linesPath);

            XElement item = (from i in rootElem.Elements()
                             where int.Parse(i.Element("ThisSerial").Value) == line.ThisSerial
                             select i).FirstOrDefault();

            if (item != null)
            {
                item.Remove();
                XMLTools.SaveListToXMLElement(rootElem, linesPath);
            }
            else
                throw new LineException("The line does not exist.");
        }
        public void updateLine(Line line)
        {
            removeLine(getLine(line.NumberLine));
            addLine(line);
        }
        public Line getLine(int serial)
        {
            return (from i in XMLTools.LoadListFromXMLElement(linesPath).Elements()
                         where Convert.ToInt32(i.Element("ThisSerial").Value) == serial
                         select new Line()
                         {
                             ThisSerial = serial,
                             NumberLine = Convert.ToInt32(i.Element("NumberLine").Value),
                             Region = (Regions)Enum.Parse(typeof(Regions),i.Element("Region").Value)
                         }).FirstOrDefault();

        }
        public IEnumerable<Line> GetLines()
        {
            return from i in XMLTools.LoadListFromXMLElement(linesPath).Elements()
                   select new Line()
                   {
                       ThisSerial = Convert.ToInt32(i.Element("ThisSerial").Value),
                       NumberLine = Convert.ToInt32(i.Element("NumberLine").Value),
                       Region = (Regions)Enum.Parse(typeof(Regions), i.Element("Region").Value)
                   };
        }
        public IEnumerable<Line> GetLines(Predicate<Line> condition)
        {
            return from i in XMLTools.LoadListFromXMLElement(linesPath).Elements()
                   let item = new Line()
                   {
                       ThisSerial = Convert.ToInt32(i.Element("ThisSerial").Value),
                       NumberLine = Convert.ToInt32(i.Element("NumberLine").Value),
                       Region = (Regions)Enum.Parse(typeof(Regions), i.Element("Region").Value)
                   }
                   where condition(item)
                   select item;
        }
        public int countLines()
        {
            IEnumerable<Line> lines = from i in XMLTools.LoadListFromXMLElement(linesPath).Elements()
                                      select new Line()
                                      {
                                          ThisSerial = Convert.ToInt32(i.Element("ThisSerial").Value)
                                      };
            return lines.Count();
        }

        #endregion

        #region Stations

        public void addStation(Station station)
        {
            XElement rootElem = XMLTools.LoadListFromXMLElement(stationsPath);

            XElement item = (from i in rootElem.Elements()
                             where Convert.ToInt32(i.Element("ID").Value) == station.ID
                             select i).FirstOrDefault();

            if (item != null)
                throw new StationException("The station already exists.");

            rootElem.Add(new XElement("Station",
                                   new XElement("ID", station.ID),
                                   new XElement("Name", station.Name),
                                   new XElement("Latitude", station.Latitude),
                                   new XElement("Longitude", station.Longitude)));

            XMLTools.SaveListToXMLElement(rootElem, stationsPath);
        }
        public void removeStation(Station station)
        {
            XElement rootElem = XMLTools.LoadListFromXMLElement(stationsPath);

            XElement item = (from i in rootElem.Elements()
                             where int.Parse(i.Element("ID").Value) == station.ID
                             select i).FirstOrDefault();

            if (item != null)
            {
                item.Remove();
                XMLTools.SaveListToXMLElement(rootElem, stationsPath);
            }
            else
                throw new StationException("The station does not exist.");
        }
        public void updateStation(Station station)
        {
            removeStation(getStation(station.ID));
            addStation(station);
        }
        public Station getStation(int id)
        {
            return (from i in XMLTools.LoadListFromXMLElement(stationsPath).Elements()
                    where Convert.ToInt32(i.Element("ID").Value) == id
                    select new Station()
                    {
                        ID = id,
                        Name = i.Element("Name").Value,
                        Latitude = Convert.ToDouble (i.Element("Latitude").Value),
                        Longitude = Convert.ToDouble(i.Element("Longitude").Value)
                    }).FirstOrDefault();
        }
        public IEnumerable<Station> GetStations()
        {
            return (from i in XMLTools.LoadListFromXMLElement(stationsPath).Elements()
                    select new Station()
                    {
                        ID = Convert.ToInt32(i.Element("ID").Value),
                        Name = i.Element("Name").Value,
                        Latitude = Convert.ToDouble(i.Element("Latitude").Value),
                        Longitude = Convert.ToDouble(i.Element("Longitude").Value)
                    }).OrderBy(item => item.ID);
        }
        public IEnumerable<Station> GetStations(Predicate<Station> condition)
        {
            return (from i in XMLTools.LoadListFromXMLElement(stationsPath).Elements()
                   let item = new Station()
                   {
                       ID = Convert.ToInt32(i.Element("ID").Value),
                       Name = i.Element("Name").Value,
                       Latitude = Convert.ToDouble(i.Element("Latitude").Value),
                       Longitude = Convert.ToDouble(i.Element("Longitude").Value)
                   }
                    where condition(item)
                    select item).OrderBy(item=>item.ID);
        }
        public int countStations()
        {
            IEnumerable<Station> stations = from i in XMLTools.LoadListFromXMLElement(stationsPath).Elements()
                                            select new Station()
                                            {
                                                ID = Convert.ToInt32(i.Element("ID").Value),
                                            };
            return stations.Count();
        }

        #endregion

        #region LineStations

        public void addLineStation(LineStation lineStation)
        {
            XElement rootElem = XMLTools.LoadListFromXMLElement(stationsPath);

            XElement item = (from i in rootElem.Elements()
                             where Convert.ToInt32(i.Element("ID").Value) == lineStation.ID
                             select i).FirstOrDefault();

            if (item == null)
                throw new StationException("The ID not exists."); 
            rootElem = XMLTools.LoadListFromXMLElement(lineStationsPath);

            item = (from i in rootElem.Elements()
                             where Convert.ToInt32(i.Element("ID").Value) == lineStation.ID && Convert.ToInt32(i.Element("NumberLine").Value) == lineStation.NumberLine
                    select i).FirstOrDefault();

            if (item != null)
                throw new StationException("The line station already exists.");

            rootElem.Add(new XElement("LineStation",
                                   new XElement("ID", lineStation.ID),
                                   new XElement("NumberLine", lineStation.NumberLine),
                                   new XElement("PathIndex", lineStation.PathIndex)));

            XMLTools.SaveListToXMLElement(rootElem, lineStationsPath);
        }
        public void removeLineStation(LineStation lineStation)
        {
            XElement rootElem = XMLTools.LoadListFromXMLElement(lineStationsPath);

            XElement item = (from i in rootElem.Elements()
                             where int.Parse(i.Element("NumberLine").Value) == lineStation.NumberLine && int.Parse(i.Element("ID").Value) == lineStation.ID
                             select i).FirstOrDefault();

            if (item != null)
            {
                item.Remove();
                XMLTools.SaveListToXMLElement(rootElem, lineStationsPath);
            }
            else
                throw new StationException("The line station does not exist.");           
        }
        public void updateLineStation(LineStation lineStation)
        {
            removeLineStation(getLineStation(lineStation.NumberLine, lineStation.ID));
            addLineStation(lineStation);
        }
        public LineStation getLineStation(int numberLine, int id)
        {
            return (from i in XMLTools.LoadListFromXMLElement(lineStationsPath).Elements()
                    where int.Parse(i.Element("NumberLine").Value) == numberLine && int.Parse(i.Element("ID").Value) == id
                    select new LineStation()
                    {
                        ID = id,
                        NumberLine = numberLine,
                        PathIndex = Convert.ToInt32(i.Element("PathIndex").Value)
                    }).FirstOrDefault();
        }
        public IEnumerable<LineStation> GetLineStations()
        {
            return  from i in XMLTools.LoadListFromXMLElement(lineStationsPath).Elements()
                    select new LineStation()
                    {
                        ID = Convert.ToInt32(i.Element("ID").Value),
                        NumberLine = Convert.ToInt32(i.Element("NumberLine").Value),
                        PathIndex = Convert.ToInt32(i.Element("PathIndex").Value)
                    };
        }
        public IEnumerable<LineStation> GetLineStations(Predicate<LineStation> condition)
        {
            return (from i in XMLTools.LoadListFromXMLElement(lineStationsPath).Elements()
                    let item = new LineStation()
                    {
                        ID = Convert.ToInt32(i.Element("ID").Value),
                        NumberLine = Convert.ToInt32(i.Element("NumberLine").Value),
                        PathIndex = Convert.ToInt32(i.Element("PathIndex").Value)
                    }
                    where condition(item)
                    select item).OrderBy(item => item.PathIndex);
        }

        #endregion

        #region FollowingStations

        public void addTwoFollowingStations(TwoFollowingStations twoFollowingStations)
        {
            XElement rootElem = XMLTools.LoadListFromXMLElement(stationsPath);

            XElement item = (from i in rootElem.Elements()
                             where Convert.ToInt32(i.Element("ID").Value) == twoFollowingStations.FirstStationID
                             select i).FirstOrDefault();

            if (item == null)
                throw new StationException("At least one of the station does not exist.");

            item = (from i in rootElem.Elements()
                    where Convert.ToInt32(i.Element("ID").Value) == twoFollowingStations.SecondStationID
                    select i).FirstOrDefault();

            if (item == null)
                throw new StationException("At least one of the station does not exist.");
            
            rootElem = XMLTools.LoadListFromXMLElement(followingStationsPath);

            item = (from i in rootElem.Elements()
                    where (Convert.ToInt32(i.Element("FirstStationID").Value) == twoFollowingStations.FirstStationID && Convert.ToInt32(i.Element("SecondStationID").Value) == twoFollowingStations.SecondStationID) || (Convert.ToInt32(i.Element("FirstStationID").Value) == twoFollowingStations.SecondStationID && Convert.ToInt32(i.Element("SecondStationID").Value) == twoFollowingStations.FirstStationID)
                    select i).FirstOrDefault();

            if (item != null)
                throw new StationException("The following stations already exists.");

            rootElem.Add(new XElement("LineStation",
                                   new XElement("FirstStationID", twoFollowingStations.FirstStationID),
                                   new XElement("SecondStationID", twoFollowingStations.SecondStationID),
                                   new XElement("LengthBetweenStations", twoFollowingStations.LengthBetweenStations),
                                   new XElement("TimeBetweenStations", twoFollowingStations.TimeBetweenStations)));

            XMLTools.SaveListToXMLElement(rootElem, followingStationsPath);
            
        }
        public void removeTwoFollowingStations(TwoFollowingStations twoFollowingStations)
        {
            XElement rootElem = XMLTools.LoadListFromXMLElement(followingStationsPath);

            XElement item = (from i in rootElem.Elements()
                             where int.Parse(i.Element("FirstStationID").Value) == twoFollowingStations.FirstStationID && int.Parse(i.Element("SecondStationID").Value) == twoFollowingStations.SecondStationID
                             select i).FirstOrDefault();

            if (item != null)
            {
                item.Remove();
                XMLTools.SaveListToXMLElement(rootElem, followingStationsPath);
            }
            else
                throw new StationException("The two following stations do not exist.");
        }
        public void updateTwoFollowingStations(TwoFollowingStations twoFollowingStations)
        {
            removeTwoFollowingStations(getTwoFollowingStations(twoFollowingStations.FirstStationID, twoFollowingStations.SecondStationID));
            addTwoFollowingStations(twoFollowingStations);
        }
        public TwoFollowingStations getTwoFollowingStations(int firstStationID, int secondStationID)
        {
            return (from i in XMLTools.LoadListFromXMLElement(followingStationsPath).Elements()
                    where (Convert.ToInt32(i.Element("FirstStationID").Value) == firstStationID && Convert.ToInt32(i.Element("SecondStationID").Value) == secondStationID) || (Convert.ToInt32(i.Element("FirstStationID").Value) == secondStationID && Convert.ToInt32(i.Element("SecondStationID").Value) == firstStationID)
                    select new TwoFollowingStations()
                    {
                        FirstStationID = firstStationID,
                        SecondStationID = secondStationID,
                        LengthBetweenStations = Convert.ToInt32(i.Element("LengthBetweenStations").Value),
                        TimeBetweenStations = Convert.ToInt32(i.Element("TimeBetweenStations").Value)
                    }).FirstOrDefault();
        }
        public IEnumerable<TwoFollowingStations> GetFollowingStations()
        {
            return (from i in XMLTools.LoadListFromXMLElement(followingStationsPath).Elements()
                    select new TwoFollowingStations()
                    {
                        FirstStationID = Convert.ToInt32(i.Element("FirstStationID").Value),
                        SecondStationID = Convert.ToInt32(i.Element("SecondStationID").Value),
                        LengthBetweenStations = Convert.ToInt32(i.Element("LengthBetweenStations").Value),
                        TimeBetweenStations = Convert.ToInt32(i.Element("TimeBetweenStations").Value)
                    }).OrderBy(item => item.TimeBetweenStations);
        }
        public IEnumerable<TwoFollowingStations> GetFollowingStations(Predicate<TwoFollowingStations> condition)
        {
            return (from i in XMLTools.LoadListFromXMLElement(followingStationsPath).Elements()
                    let item = new TwoFollowingStations()
                    {
                        FirstStationID = Convert.ToInt32(i.Element("FirstStationID").Value),
                        SecondStationID = Convert.ToInt32(i.Element("SecondStationID").Value),
                        LengthBetweenStations = Convert.ToInt32(i.Element("LengthBetweenStations").Value),
                        TimeBetweenStations = Convert.ToInt32(i.Element("TimeBetweenStations").Value)
                    }
                    where condition(item)
                    select item).OrderBy(item => item.TimeBetweenStations);
        }

        #endregion      

        #region DrivingLines

        public void addDrivingLine(DrivingLine drivingLine)
        {
            XElement rootElem = XMLTools.LoadListFromXMLElement(linesPath);

            XElement item = (from i in rootElem.Elements()
                             where Convert.ToInt32(i.Element("ThisSerial").Value) == drivingLine.NumberLine
                             select i).FirstOrDefault();

            if (item == null)
                throw new StationException("The Line not exists.");
            rootElem = XMLTools.LoadListFromXMLElement(drivingLinesPath);
            item = (from i in rootElem.Elements()
                    where Convert.ToInt32(i.Element("NumberLine").Value) == drivingLine.NumberLine && i.Element("Start").Value == drivingLine.Start.ToString()
                    select i).FirstOrDefault();
            if (item != null)
                throw new StationException("The driving line already exists.");

            rootElem.Add(new XElement("DrivingLine",
                                   new XElement("NumberLine", drivingLine.NumberLine),
                                   new XElement("Start", drivingLine.Start),
                                   new XElement("Frequency", drivingLine.Frequency),
                                   new XElement("End", drivingLine.End)));

            XMLTools.SaveListToXMLElement(rootElem, lineStationsPath);
        }
        public void removeDrivingLine(DrivingLine drivingLine)
        {
            XElement rootElem = XMLTools.LoadListFromXMLElement(drivingLinesPath);

            XElement item = (from i in rootElem.Elements()
                             where int.Parse(i.Element("NumberLine").Value) == drivingLine.NumberLine && TimeSpan.Parse(i.Element("Start").Value) == drivingLine.Start
                             select i).FirstOrDefault();

            if (item != null)
            {
                item.Remove();
                XMLTools.SaveListToXMLElement(rootElem, drivingLinesPath);
            }
            else
                throw new TripException("The driving line does not exist.");
        }
        public void updateDrivingLine(DrivingLine drivingLine)
        {
            removeDrivingLine(getDrivingLine(drivingLine.NumberLine, drivingLine.Start));
            addDrivingLine(drivingLine);
        }
        public DrivingLine getDrivingLine(int numberLine, TimeSpan start)
        {
            return (from i in XMLTools.LoadListFromXMLElement(drivingLinesPath).Elements()
                    where Convert.ToInt32(i.Element("NumberLine").Value) == numberLine && i.Element("Start").Value == start.ToString()
                    select new DrivingLine()
                    {
                        NumberLine = numberLine,
                        Start = start,
                        Frequency = Convert.ToInt32(i.Element("Frequency").Value),
                        End = TimeSpan.Parse(i.Element("End").Value)
                    }).FirstOrDefault();
        }
        public IEnumerable<DrivingLine> GetDrivingLines()
        {
            return  from i in XMLTools.LoadListFromXMLElement(drivingLinesPath).Elements()
                    select new DrivingLine()
                    {
                        NumberLine = Convert.ToInt32(i.Element("NumberLine").Value),
                        Start = TimeSpan.Parse(i.Element("Start").Value),
                        Frequency = Convert.ToInt32(i.Element("Frequency").Value),
                        End = TimeSpan.Parse(i.Element("End").Value)
                    };
        }
        public IEnumerable<DrivingLine> GetDrivingLines(Predicate<DrivingLine> condition)
        {
            return from i in XMLTools.LoadListFromXMLElement(drivingLinesPath).Elements()
                   let item = new DrivingLine()
                   {
                       NumberLine = Convert.ToInt32(i.Element("NumberLine").Value),
                       Start = TimeSpan.Parse(i.Element("Start").Value),
                       Frequency = Convert.ToInt32(i.Element("Frequency").Value),
                       End = TimeSpan.Parse(i.Element("End").Value)
                   }
                   where condition(item)
                   select item;
        }

        #endregion

        #region ManagingCode

        public string getManagingCode()
        {
            return XMLTools.LoadListFromXMLElement(managingCodePath).Elements().ToString();
        }
        public void updateManagingCode(string code)
        {
            DataSource.ManagingCode = code;
        }

        #endregion
    }
}