using DLAPI;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

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
        string serialPath = @"SerialXml.xml";

        #endregion

        #region Users

        public void addUser(User user)
        {
            XElement rootElem = XMLTools.LoadListFromXMLElement(usersPath);

            XElement item = (from i in rootElem.Elements()
                             where i.Element("Username").Value == user.Username
                             select i).FirstOrDefault();

            if (item != null)
                throw new UserException("The username already exists.");

            rootElem.Add(new XElement("User",
                                   new XElement("Username", user.Username),
                                   new XElement("Password", user.Password),
                                   new XElement("IsManager", user.IsManager)));

            XMLTools.SaveListToXMLElement(rootElem, usersPath);
        }
        private static string hashPassword(string password)
        {
            SHA512 shaM = new SHA512Managed();
            return Convert.ToBase64String(shaM.ComputeHash(Encoding.UTF8.GetBytes(password)));
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
            //if (DataSource.Buses.Exists(item => item.LicensePlate == bus.LicensePlate))
            //    throw new BusException("The bus already exists.");
            //DataSource.Buses.Add(bus.Clone());
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
            //Bus bus = DataSource.Buses.Find(item => item.LicensePlate == licensePlate);
            //if (bus == null)
            //    return null;
            //return bus.Clone();
            return null;
        }
        public IEnumerable<Bus> GetBuses()
        {
            //return from item in DataSource.Buses
            //       select item.Clone();
            return null;
        }
        public IEnumerable<Bus> GetBuses(Predicate<Bus> condition)
        {
            //return from item in DataSource.Buses
            //       where condition(item)
            //       select item.Clone();
            return null;
        }

        #endregion

        #region Lines

        public int addLine(Line line)
        {
            XElement rootElem = XMLTools.LoadListFromXMLElement(linesPath);

            int serial = getSerial();

            rootElem.Add(new XElement("Line",
                                   new XElement("ThisSerial", serial),
                                   new XElement("NumberLine", line.NumberLine),
                                   new XElement("Region", line.Region)));

            XMLTools.SaveListToXMLElement(rootElem, linesPath);

            return serial;
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
            removeLine(getLine(line.ThisSerial));

            XElement rootElem = XMLTools.LoadListFromXMLElement(linesPath);

            rootElem.Add(new XElement("Line",
                                   new XElement("ThisSerial", line.ThisSerial),
                                   new XElement("NumberLine", line.NumberLine),
                                   new XElement("Region", line.Region)));

            XMLTools.SaveListToXMLElement(rootElem, linesPath);
        }
        public Line getLine(int serial)
        {
            IEnumerable<XElement> list = XMLTools.LoadListFromXMLElement(linesPath).Elements();
            return (from i in list
                    where Convert.ToInt32(i.Element("ThisSerial").Value) == serial
                    select new Line()
                    {
                        ThisSerial = serial,
                        NumberLine = Convert.ToInt32(i.Element("NumberLine").Value),
                        Region = (Regions)Enum.Parse(typeof(Regions), i.Element("Region").Value)
                    }).FirstOrDefault();
        }
        public IEnumerable<Line> GetLines()
        {
            IEnumerable<XElement> list = XMLTools.LoadListFromXMLElement(linesPath).Elements();
            return from i in list
                   select new Line()
                   {
                       ThisSerial = Convert.ToInt32(i.Element("ThisSerial").Value),
                       NumberLine = Convert.ToInt32(i.Element("NumberLine").Value),
                       Region = (Regions)Enum.Parse(typeof(Regions), i.Element("Region").Value)
                   };
        }
        public IEnumerable<Line> GetLines(Predicate<Line> condition)
        {
            IEnumerable<XElement> list = XMLTools.LoadListFromXMLElement(linesPath).Elements();
            return from i in list
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
            IEnumerable<XElement> list = XMLTools.LoadListFromXMLElement(linesPath).Elements();
            IEnumerable<Line> lines = from i in list
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
            List<XElement> list = XMLTools.LoadListFromXMLElement(stationsPath).Elements().ToList();
            return (from i in list
                    where Convert.ToInt32(i.Element("ID").Value) == id
                    select new Station()
                    {
                        ID = id,
                        Name = i.Element("Name").Value,
                        Latitude = Convert.ToDouble(i.Element("Latitude").Value),
                        Longitude = Convert.ToDouble(i.Element("Longitude").Value)
                    }).FirstOrDefault();
        }
        public IEnumerable<Station> GetStations()
        {
            List<XElement> list = XMLTools.LoadListFromXMLElement(stationsPath).Elements().ToList();
            return (from i in list
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
            List<XElement> list = XMLTools.LoadListFromXMLElement(stationsPath).Elements().ToList();
            return (from i in list
                    let item = new Station()
                    {
                        ID = Convert.ToInt32(i.Element("ID").Value),
                        Name = i.Element("Name").Value,
                        Latitude = Convert.ToDouble(i.Element("Latitude").Value),
                        Longitude = Convert.ToDouble(i.Element("Longitude").Value)
                    }
                    where condition(item)
                    select item).OrderBy(item => item.ID);
        }
        public int countStations()
        {
            IEnumerable<XElement> list = XMLTools.LoadListFromXMLElement(stationsPath).Elements();
            IEnumerable<Station> stations = from i in list
                                            select new Station()
                                            {
                                                ID = Convert.ToInt32(i.Element("ID").Value)
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
            IEnumerable<XElement> list = XMLTools.LoadListFromXMLElement(lineStationsPath).Elements();
            return (from i in list
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
            IEnumerable<XElement> list = XMLTools.LoadListFromXMLElement(lineStationsPath).Elements();
            return from i in list
                   select new LineStation()
                   {
                       ID = Convert.ToInt32(i.Element("ID").Value),
                       NumberLine = Convert.ToInt32(i.Element("NumberLine").Value),
                       PathIndex = Convert.ToInt32(i.Element("PathIndex").Value)
                   };
        }
        public IEnumerable<LineStation> GetLineStations(Predicate<LineStation> condition)
        {
            IEnumerable<XElement> list = XMLTools.LoadListFromXMLElement(lineStationsPath).Elements();
            return (from i in list
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
                throw new StationException("The first station does not exist.");

            item = (from i in rootElem.Elements()
                    where Convert.ToInt32(i.Element("ID").Value) == twoFollowingStations.SecondStationID
                    select i).FirstOrDefault();

            if (item == null)
                throw new StationException("The second station does not exist.");

            rootElem = XMLTools.LoadListFromXMLElement(followingStationsPath);

            item = (from i in rootElem.Elements()
                    where (Convert.ToInt32(i.Element("FirstStationID").Value) == twoFollowingStations.FirstStationID && Convert.ToInt32(i.Element("SecondStationID").Value) == twoFollowingStations.SecondStationID) || (Convert.ToInt32(i.Element("FirstStationID").Value) == twoFollowingStations.SecondStationID && Convert.ToInt32(i.Element("SecondStationID").Value) == twoFollowingStations.FirstStationID)
                    select i).FirstOrDefault();

            if (item != null)
                throw new StationException("The following stations already exists.");

            rootElem.Add(new XElement("TwoFollowingStations",
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
            IEnumerable<XElement> list = XMLTools.LoadListFromXMLElement(followingStationsPath).Elements();
            return (from i in list
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
            IEnumerable<XElement> list = XMLTools.LoadListFromXMLElement(followingStationsPath).Elements();
            return from i in list
                   select new TwoFollowingStations()
                   {
                       FirstStationID = Convert.ToInt32(i.Element("FirstStationID").Value),
                       SecondStationID = Convert.ToInt32(i.Element("SecondStationID").Value),
                       LengthBetweenStations = Convert.ToInt32(i.Element("LengthBetweenStations").Value),
                       TimeBetweenStations = Convert.ToInt32(i.Element("TimeBetweenStations").Value)
                   };
        }
        public IEnumerable<TwoFollowingStations> GetFollowingStations(Predicate<TwoFollowingStations> condition)
        {
            IEnumerable<XElement> list = XMLTools.LoadListFromXMLElement(followingStationsPath).Elements();
            return from i in list
                   let item = new TwoFollowingStations()
                   {
                       FirstStationID = Convert.ToInt32(i.Element("FirstStationID").Value),
                       SecondStationID = Convert.ToInt32(i.Element("SecondStationID").Value),
                       LengthBetweenStations = Convert.ToInt32(i.Element("LengthBetweenStations").Value),
                       TimeBetweenStations = Convert.ToInt32(i.Element("TimeBetweenStations").Value)
                   }
                   where condition(item)
                   select item;
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
                throw new TripException("The line does not exist.");

            rootElem = XMLTools.LoadListFromXMLElement(drivingLinesPath);

            item = (from i in rootElem.Elements()
                    where Convert.ToInt32(i.Element("NumberLine").Value) == drivingLine.NumberLine && i.Element("Start").Value == drivingLine.Start.ToString()
                    select i).FirstOrDefault();

            if (item != null)
                throw new TripException("The driving line already exists.");

            rootElem.Add(new XElement("DrivingLine",
                                   new XElement("NumberLine", drivingLine.NumberLine),
                                   new XElement("StartHours", drivingLine.Start.Hours),
                                   new XElement("StartMinutes", drivingLine.Start.Minutes),
                                   new XElement("StartSeconds", drivingLine.Start.Seconds),
                                   new XElement("Frequency", drivingLine.Frequency),
                                   new XElement("EndHours", drivingLine.End.Hours),
                                   new XElement("EndMinutes", drivingLine.End.Minutes),
                                   new XElement("EndSeconds", drivingLine.End.Seconds)));

            XMLTools.SaveListToXMLElement(rootElem, drivingLinesPath);
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
            IEnumerable<XElement> list = XMLTools.LoadListFromXMLElement(drivingLinesPath).Elements();
            return (from i in list
                    where Convert.ToInt32(i.Element("NumberLine").Value) == numberLine && Convert.ToInt32(i.Element("StartHours").Value) == start.Hours && Convert.ToInt32(i.Element("StartMinutes").Value) == start.Minutes//if the hours and minutes of start is equal and the number line is same this the driving line that need.
                    select new DrivingLine()
                    {
                        NumberLine = numberLine,
                        Start = start,
                        Frequency = Convert.ToInt32(i.Element("Frequency").Value),
                        End = new TimeSpan(Convert.ToInt32(i.Element("EndHours").Value), Convert.ToInt32(i.Element("EndMinutes").Value), Convert.ToInt32(i.Element("EndSeconds").Value))
                    }).FirstOrDefault();
        }
        public IEnumerable<DrivingLine> GetDrivingLines()
        {
            IEnumerable<XElement> list = XMLTools.LoadListFromXMLElement(drivingLinesPath).Elements();
            return from i in list
                   select new DrivingLine()
                   {
                       NumberLine = Convert.ToInt32(i.Element("NumberLine").Value),
                       Start = new TimeSpan(Convert.ToInt32(i.Element("StartHours").Value), Convert.ToInt32(i.Element("StartMinutes").Value), Convert.ToInt32(i.Element("StartSeconds").Value)),
                       Frequency = Convert.ToInt32(i.Element("Frequency").Value),
                       End = new TimeSpan(Convert.ToInt32(i.Element("EndHours").Value), Convert.ToInt32(i.Element("EndMinutes").Value), Convert.ToInt32(i.Element("EndSeconds").Value))
                   };
        }
        public IEnumerable<DrivingLine> GetDrivingLines(Predicate<DrivingLine> condition)
        {
            IEnumerable<XElement> list = XMLTools.LoadListFromXMLElement(drivingLinesPath).Elements();
            return from i in list
                   let item = new DrivingLine()
                   {
                       NumberLine = Convert.ToInt32(i.Element("NumberLine").Value),
                       Start = new TimeSpan(Convert.ToInt32(i.Element("StartHours").Value), Convert.ToInt32(i.Element("StartMinutes").Value), Convert.ToInt32(i.Element("StartSeconds").Value)),
                       Frequency = Convert.ToInt32(i.Element("Frequency").Value),
                       End = new TimeSpan(Convert.ToInt32(i.Element("EndHours").Value), Convert.ToInt32(i.Element("EndMinutes").Value), Convert.ToInt32(i.Element("EndSeconds").Value))
                   }
                   where condition(item)
                   select item;
        }

        #endregion

        #region ManagingCode

        public string getManagingCode()
        {
            return XMLTools.LoadListFromXMLElement(managingCodePath).Elements("Code").FirstOrDefault().Value;
        }
        public void updateManagingCode(string code)
        {
            XElement rootElem = XMLTools.LoadListFromXMLElement(managingCodePath);
            rootElem.Elements("Code").FirstOrDefault().Value = code;
            XMLTools.SaveListToXMLElement(rootElem, managingCodePath);
        }

        #endregion

        #region Serial

        public int getSerial()
        {
            XElement rootElem = XMLTools.LoadListFromXMLElement(serialPath);
            int serial = Convert.ToInt32(rootElem.Elements("LineSerial").FirstOrDefault().Value);
            rootElem.Elements("LineSerial").FirstOrDefault().Value = (++serial).ToString();
            XMLTools.SaveListToXMLElement(rootElem, serialPath);
            return serial - 1;
        }

        #endregion
    }
}