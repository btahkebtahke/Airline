using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airlines.Data.Repository;
using Airlines.Data.Entities;
using System.Data.Entity;

namespace Airlines.Data.OtherActions
{
    //Class with methods that are used in controllers
    public static class Methods
    {
        //Creating RaceTeamIDs list for selectlist
        public static IEnumerable<RaceTeam> PopulateRaceTeamDropDownList()
        {
            AirlineContext db = new AirlineContext();
            var departmentsQuery = from d in db.RaceTeams
                                   orderby d.ID
                                   select d;
            return departmentsQuery;
        }

        //Updating of the stuardesses for RaceTeam
        public static void UpdateStuardesses(int? firstStuardess, int? secondStuardess, int raceTeamID)
        {
            AirlineContext db = new AirlineContext();
            RaceTeam b = db.RaceTeams.Include(i => i.Stuardesses).FirstOrDefault(i => i.ID == raceTeamID);
            b.Stuardesses.Clear();
            db.SaveChanges();
            Stuardess a = db.Stuardesses.FirstOrDefault(i => i.ID == firstStuardess);
            if (a != null)
            {
                b.Stuardesses.Add(a);
                db.SaveChanges();
            }
            a = db.Stuardesses.FirstOrDefault(i => i.ID == secondStuardess);
            if (a != null)
            {
                b.Stuardesses.Add(a);
                db.SaveChanges();
            }
        }
        //Class for styling of dropdownlists
        public class CustomName
        {
            public string Name { get; set; }
            public int ID { get; set; }
        }
        //Making 4 dropdown lists with name of the people
        public static IEnumerable<CustomName> GetCustomPilots()
        {
            AirlineContext db = new AirlineContext();
            var a = (from s in db.Pilots
                     select new CustomName
                     {
                         Name = s.FirstName + " " + s.LastName,
                         ID = s.ID
                     }).ToList();
            return a;
        }
        public static IEnumerable<CustomName> GetCustomNavigators()
        {
            AirlineContext db = new AirlineContext();
            var a = (from s in db.Navigators
                     select new CustomName
                     {
                         Name = s.FirstName + " " + s.LastName,
                         ID = s.ID
                     }).ToList();
            return a;
        }
        public static IEnumerable<CustomName> GetCustomRadiomen()
        {
            AirlineContext db = new AirlineContext();
            var a = (from s in db.RadioMen
                     select new CustomName
                     {
                         Name = s.FirstName + " " + s.LastName,
                         ID = s.ID
                     }).ToList();
            return a;
        }

        public static IEnumerable<CustomName> GetCustomStuardesses()
        {
            AirlineContext db = new AirlineContext();
            var a = (from s in db.Stuardesses
                     select new CustomName
                     {
                         Name = s.FirstName + " " + s.LastName,
                         ID = s.ID
                     }).ToList();
            return a;
        }

        //Getting the existing stuardesses for Edit method

        public static string[] GetListOfAddedStuardesses(RaceTeam raceTeam)
        {
            string tempStuardesses = "";
            IEnumerable<Stuardess> selectedStuardesses = raceTeam.Stuardesses.Where(c => c.TeamID == raceTeam.ID).ToList();
            foreach (Stuardess i in selectedStuardesses)
            {
                tempStuardesses += i.ID + " ";
            }
            string[] stds = tempStuardesses.Split(new Char[] { ' ' });
            if (selectedStuardesses.Count() == 1 || selectedStuardesses.Count() == 0) {
                stds = new string []{ "0", "0" };
                    }
            return stds;
        }

        //Update race 
        public static void UpdateRace (int raceID, int raceTeamID)
        {
            AirlineContext db = new AirlineContext();
            Race a = db.Races.Find(raceID);
            a.RaceTeamID = raceTeamID;
            db.SaveChanges();
        }

    }
}

