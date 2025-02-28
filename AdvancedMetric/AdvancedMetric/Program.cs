using System;
using System.Drawing;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace AdvancedMetrics
{

    public class Player
    {
        private int id;
        private string name;
        private Stats totals;
        private AdvShooting advStats;

        public Player(int id, string name, Stats totals, int[] games, double[] threes, double[] fieldGoals, double[] fts, double[] reb)
        {
            this.id = id;
            this.name = name;
            this.totals = totals;
            this.advStats = new AdvShooting(totals, games, threes, fieldGoals, fts, reb);

        }

        public Stats Totals
        {
            get { return totals; }   // get method
            set { totals = value; }  // set method
        }
        public AdvShooting AdvStats
        {
            get { return advStats; }   // get method
            set { advStats = value; }  // set method
        }

        public string ToString(Team team)
        {
            string text = "/nID: " + id +
                "/nName: " + name +
                advStats.ToString(team);
            text = text.Replace("/n", "" + System.Environment.NewLine);
            return text;
        }
    }

    public class Stats
    {
        private int id;
        private int year;
        private double point;
        private double reb;
        private double ast;
        private double stl;
        private double blk;
        private double tov;
        private double pf;
        private double mins;

        public Stats(int year, double point, double reb, double ast,
                                double stl, double blk, double tov, double pf, double minutes)
        {
            this.year = year;
            this.point = point;
            this.reb = reb;
            this.ast = ast;
            this.stl = stl;
            this.blk = blk;
            this.tov = tov;
            this.pf = pf;
            this.mins = minutes;
        }

        public int Year
        {
            get { return year; }   // get method
            set { year = value; }  // set method
        }

        public double Point
        {
            get { return point; }   // get method
            set { point = value; }  // set method
        }

        public double Reb
        {
            get { return reb; }   // get method
            set { reb = value; }  // set method
        }

        public double Ast
        {
            get { return ast; }   // get method
            set { ast = value; }  // set method
        }

        public double Stl
        {
            get { return stl; }   // get method
            set { stl = value; }  // set method
        }

        public double Blk
        {
            get { return blk; }   // get method
            set { blk = value; }  // set method
        }

        public double Tov
        {
            get { return tov; }   // get method
            set { tov = value; }  // set method
        }

        public double Pf
        {
            get { return pf; }   // get method
            set { pf = value; }  // set method
        }

        public double Mins
        {
            get { return mins; }   // get method
            set { mins = value; }  // set method
        }

        public string ToString(int[] gP)
        {
            // "Year:" + year,
            // "G:" + gP[0],
            // "GS:" + gP[1],
            // "PTS:" + (point/gP[0]),
            // "REB:" + (reb/gP[0]),
            // "AST:" + (ast/gP[0]),
            // "STL:" + (stl/gP[0]),
            // "BLK:" + (blk/gP[0]),
            // "TOV:" + (tov/gP[0]),
            // "PF:" + (pf/gP[0])
            string text = "/n****Stats****" +
                     "/nYear:" + year +
                     "/nG:" + gP[0] +
                     "/nGS:" + gP[1] +
                     "/nMIN:" + Math.Round((mins / gP[0]), 1) +
                     "/nPTS:" + Math.Round((point / gP[0]), 1) +
                     "/nREB:" + Math.Round((reb / gP[0]), 1) +
                     "/nAST:" + Math.Round((ast / gP[0]), 1) +
                     "/nSTL:" + Math.Round((stl / gP[0]), 1) +
                     "/nBLK:" + Math.Round((blk / gP[0]), 1) +
                     "/nTOV:" + Math.Round((tov / gP[0]), 1) +
                     "/nPF:" + Math.Round((pf / gP[0]), 1);
            text = text.Replace("/n", "" + System.Environment.NewLine);
            return text;
        }
    }

    public class AdvShooting
    {
        private Stats playerTot;
        private int[] games; // [ gamesS, gamesP]
        private double[] threes; // [3PM, 3PA]
        private double[] fieldGoals; // [FGM, FGA]
        private double[] fts; // [FTM, FTA]
        private double[] reb; // [oREB, dREB]

        public AdvShooting(Stats playerTot, int[] games, double[] threes, double[] fieldGoals, double[] fts, double[] reb)
        {
            this.playerTot = playerTot;
            this.games = games;
            this.threes = threes;
            this.fieldGoals = fieldGoals;
            this.fts = fts;
            this.reb = reb;
        }

        public Stats PlayerTot
        {
            get { return playerTot; }
            set { playerTot = value; }
        }

        public int[] Games
        {
            get { return games; }   // get method
            set { games = value; }  // set method
        }

        public double[] Threes
        {
            get { return threes; }   // get method
            set { threes = value; }  // set method
        }
        public double[] FieldGoals
        {
            get { return fieldGoals; }   // get method
            set { fieldGoals = value; }  // set method
        }

        public double[] Fts
        {
            get { return fts; }   // get method
            set { fts = value; }  // set method
        }
        public double[] Reb
        {
            get { return reb; }   // get method
            set { reb = value; }  // set method
        }

        public double findFGP()
        {
            return Math.Round( fieldGoals[0]/ fieldGoals[1], 3);
        }

        public double find3PP()
        {
            return Math.Round(threes[0] / threes[1], 3);
        }

        public double find2PP()
        {
            return Math.Round( (fieldGoals[0] - threes[0]) / (fieldGoals[1] - threes[1]) , 3);
        }

        public double findFTP()
        {
            return Math.Round(fts[0] / fts[1], 3);
        }

        public double findEFG()
        {
            // (FGM + 0.5 * 3PM) / FGA
            //double eFG = (fieldGoals[0] + (0.5 * threes[0])) / fieldGoals[1];
            return Math.Round(
                ((fieldGoals[0] + (0.5 * threes[0])) / fieldGoals[1]), 3);
        }

        public double trueShooting()
        {
            // pointAvg / ( 2 ( FGA + ( 0.44 * FTA ) ) )
            // double ts = playerAvg.PointAvg / ( 2 ( fieldGoals[1] + ( 0.44 * fts[1] ) ) )
            return Math.Round(
                playerTot.Point / ( 2 * (fieldGoals[1] + (0.44 * fts[1]))), 3);
        }

        public double usage(Team team)
        {
            //double top = 100 * (FGA + (0.44*FTA) + TOV) * TeamMIN
            //double bottom = (TeamFGA + (0.44*TeamFTA) +TeamTOV) * (5 * Min)
            double top = (fieldGoals[1] + 0.44 * fts[1] + playerTot.Tov) * (team.Mins/5);
            double bottom = (team.FieldGoals[1] + 0.44 * team.Fts[1] + team.Tov) * (playerTot.Mins);
            double usg = 100 * (top/bottom);
            return Math.Round(usg, 1);
        }

        public double gameScr()
        {
            // PTS + 0.4 * FG - 0.7 * FGA -
            // 0.4*(FTA - FT) + 0.7 * ORB +
            // 0.3 * DRB + STL + 0.7 * AST +
            // 0.7 * BLK - 0.4 * PF - TOV
            // playerTot.Point + ( (0.4 * fieldGoals[0]) - (0.7 * fieldGoals[1]) ) -
            // ( 0.4 * (fts[1] - fts[0])) + ( 0.7 * reb[0]) +
            // ( 0.3 * reb[1]) + playerTot.Stl + ( 0.7 * playerTot.Ast ) +
            // ( 0.7 * playerTot.Blk) - (0.4 * playerTot.Pf) - playerTot.Tov
            return 0;
        }

        public double threePAr()
        {
            // 3PA/FPA
            return Math.Round(threes[1] / fieldGoals[1]
                , 3);
        }

        public double ftAr()
        {
            // FTA/FGA
            return Math.Round(fts[1] / fieldGoals[1]
                , 3);
        }

        public string ToString(Team team)
        {
            string text = playerTot.ToString(games) +
                     "/n****Advanced Stats****" +
                     "/nFG %: " + findFGP() +
                     "/n3P%: " + find3PP() +
                     "/n2P%: " + find2PP() +
                     "/neFG%: " + findEFG() +
                     "/nFT%: " + findFTP() +
                     "/nTS%: " + trueShooting() +
                     "/n3PAr: " + threePAr() +
                     "/nFTr: " + ftAr() + 
                     "/nUSG%: " + usage(team) + "%";
            text = text.Replace("/n", "" + System.Environment.NewLine);
            return text;
        }
    }

    public class Team
    {
        private string teamName;
        private Player[] players = new Player[15];
        private int pointer = 0;
        private int year;
        private double point;
        private double reb;
        private double ast;
        private double stl;
        private double blk;
        private double tov;
        private double pf;
        private double mins;
        private double[] threes; // [3PM, 3PA]
        private double[] fieldGoals; // [FGM, FGA]
        private double[] fts; // [FTM, FTA]
        private double[] rebSplit; // [oREB, dREB]

        public Team(string teamName)
        {
            this.teamName = teamName;
        }

        public int Year
        {
            get { return year; }   // get method
            set { year = value; }  // set method
        }

        public double Point
        {
            get { return point; }   // get method
            set { point = value; }  // set method
        }

        public double Reb
        {
            get { return reb; }   // get method
            set { reb = value; }  // set method
        }

        public double Ast
        {
            get { return ast; }   // get method
            set { ast = value; }  // set method
        }

        public double Stl
        {
            get { return stl; }   // get method
            set { stl = value; }  // set method
        }

        public double Blk
        {
            get { return blk; }   // get method
            set { blk = value; }  // set method
        }

        public double Tov
        {
            get { return tov; }   // get method
            set { tov = value; }  // set method
        }

        public double Pf
        {
            get { return pf; }   // get method
            set { pf = value; }  // set method
        }

        public double Mins
        {
            get { return mins; }   // get method
            set { mins = value; }  // set method
        }

        public double[] Threes
        {
            get { return threes; }   // get method
            set { threes = value; }  // set method
        }
        public double[] FieldGoals
        {
            get { return fieldGoals; }   // get method
            set { fieldGoals = value; }  // set method
        }

        public double[] Fts
        {
            get { return fts; }   // get method
            set { fts = value; }  // set method
        }
        public double[] RebSplit
        {
            get { return rebSplit; }   // get method
            set { rebSplit = value; }  // set method
        }

        public void AddPlayer(Player player)
        {
            if (pointer >= 15)
            {
                Console.WriteLine("Full");
            }
            else
            {
                players[pointer] = player;
                pointer++;
            }
            //makeTotals();
        }



        public void RemovePlayer(Player player) 
        { 
        
        }

        public Player returnPlayer(int place)
        {
            return players[place];
        }

        public void resetTotals()
        {
            threes = new double[] { 0, 0 };
            fieldGoals = new double[] { 0, 0 };
            fts = new double[] { 0, 0 };
            rebSplit = new double[] { 0, 0 };
            point = 0;
            reb = 0;
            ast = 0;
            stl = 0;
            blk = 0;
            tov = 0;
            pf = 0;
            mins = 0;
    }
        public void makeTotals()
        {
            resetTotals();
            foreach (Player p in players)
            {
                if (p != null)
                {
                    year = p.Totals.Year;
                    point += p.Totals.Point;
                    reb += p.Totals.Reb;
                    ast += p.Totals.Ast;
                    stl += p.Totals.Stl;
                    blk += p.Totals.Blk;
                    tov += p.Totals.Tov;
                    pf += p.Totals.Pf;
                    mins += p.Totals.Mins;
                    threes[0] += p.AdvStats.Threes[0];
                    threes[1] += p.AdvStats.Threes[1];
                    fieldGoals[0] += p.AdvStats.FieldGoals[0];
                    fieldGoals[1] += p.AdvStats.FieldGoals[1];
                    fts[0] += p.AdvStats.Fts[0];
                    fts[1] += p.AdvStats.Fts[1];
                    rebSplit[0] += p.AdvStats.Reb[0];
                    rebSplit[1] += p.AdvStats.Reb[1];
                }
            }
        }

        public void makeFullTotals(int year,  double point, double reb, double ast, double stl,
            double blk, double tov, double pf, double mins, double[] threes, double[] fieldGoals,double[] fts, double[] rebSplit)
        {
            resetTotals();
            this.threes = threes;
            this.fieldGoals = fieldGoals;
            this.fts = fts;
            this.mins = mins;
            this.tov = tov;
            this.pf = pf;
            this.year = year;
            this.reb = reb;
            this.ast = ast;
            this.stl = stl;
            this.point = point;
            this.rebSplit = rebSplit;
            this.blk = blk;

        }

        public string ToString(Team team)
        {
            string text = "Team: " + teamName +
                "/nYear: " + year +
                "/nPoints: " + point +
                "/nRebounds: " + reb +
                "/nAssists: " + ast +
                "/nSteals: " + stl +
                "/nBlocks: " + blk +
                "/nTOV: " + tov +
                "/nPF: " + pf +
                "/nFG%: " + Math.Round(fieldGoals[0] / fieldGoals[1], 3) +
                "/n3P%: " + Math.Round(threes[0] / threes[1], 3) +
                "/nFT%: " + Math.Round(fts[0] / fts[1], 3);
            foreach (Player p in players)
            {
                if (p != null)
                {
                    text = text + "/n" + p.ToString(team);
                }
            }
            text = text.Replace("/n", "" + System.Environment.NewLine);
            return text;
        }
    }

    public class FileReader
    {
        private string fileName;

        public FileReader(string fileName)
        {
            this.fileName = fileName;
        }

        public Team readTeam(string teamName)
        {
            Team team = new Team(teamName);
            int[] games = { 0, 0 };
            double[] three = { 0, 0 };
            double[] all = { 0, 0 };
            double[] ft = { 0, 0 };
            double[] reb = { 0, 0 };
            int id = 0;;
            int year = 0;
            double point = 0;
            double totReb = 0;
            double ast = 0;
            double stl = 0;
            double blk = 0;
            double tov= 0;
            double pf= 0;
            double mins = 0;
            using (StreamReader sr = File.OpenText(fileName + "Players.txt"))
            {
                string line;
                string line2 = "";
                int lineNum = 0;
                int count = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    foreach (char i in line)
                    {
                        if ((i == '#' || i == ' ') && (count < 3))
                        {
                            count++;
                        } else if (count >= 3)
                        {
                            line2 += i;
                        }
                    }
                    //Console.WriteLine(line2);
                    switch (lineNum)
                    {
                        case 0:
                            games[0] = Int32.Parse(line2);
                            lineNum++;
                            break;
                        case 1:
                            games[1] = Int32.Parse(line2);
                            lineNum++;
                            break;
                        case 2:
                            three[0] = Int32.Parse(line2);
                            lineNum++;
                            break;
                        case 3:
                            three[1] = Int32.Parse(line2);
                            lineNum++;
                            break;
                        case 4:
                            all[0] = Int32.Parse(line2);
                            lineNum++;
                            break;
                        case 5:
                            all[1] = Int32.Parse(line2);
                            lineNum++;
                            break;
                        case 6:
                            ft[0] = Int32.Parse(line2);
                            lineNum++;
                            break;
                        case 7:
                            ft[1] = Int32.Parse(line2);
                            lineNum++;
                            break;
                        case 8:
                            reb[0] = Int32.Parse(line2);
                            lineNum++;
                            break;
                        case 9:
                            reb[1] = Int32.Parse(line2);
                            lineNum++;
                            break;
                        case 10:
                            year = Int32.Parse(line2);
                            lineNum++;
                            break;
                        case 11:
                            point = Int32.Parse(line2);
                            lineNum++;
                            break;
                        case 12:
                            totReb = Int32.Parse(line2);
                            lineNum++;
                            break;
                        case 13:
                            ast = Int32.Parse(line2);
                            lineNum++;
                            break;
                        case 14:
                            stl = Int32.Parse(line2);
                            lineNum++;
                            break;
                        case 15:
                            blk = Int32.Parse(line2);
                            lineNum++;
                            break;
                        case 16:
                            tov = Int32.Parse(line2);
                            lineNum++;
                            break;
                        case 17:
                            pf = Int32.Parse(line2);
                            lineNum++;
                            break;
                        case 18:
                            id = Int32.Parse(line2);
                            lineNum++;
                            break;
                        case 19:
                            mins = Int32.Parse(line2);
                            lineNum++;
                            break;
                        case 20:
                            Stats stats = new Stats(year, point, totReb, ast, stl, blk, tov, pf, mins);
                            Player player = new Player(id, line2, stats, games, three, all, ft, reb);
                            team.AddPlayer(player);
                            games = new int[] { 0, 0 };
                            three = new double []{ 0, 0 };
                            all = new double[] { 0, 0 };
                            ft = new double[] { 0, 0 };
                            reb = new double[] { 0, 0 };
                            lineNum = 0;
                            break;
                    }
                    line2 = "";
                    count = 0;
                }
                sr.Close();
            }
            //team = readInfo(team);
            return team;
        }

        public Team readInfo(Team team)
        {
            int games = 0;
            double[] three = { 0, 0 };
            double[] all = { 0, 0 };
            double[] ft = { 0, 0 };
            double[] reb = { 0, 0 };
            int id = 0;
            int year = 0;
            double point = 0;
            double totReb = 0;
            double ast = 0;
            double stl = 0;
            double blk = 0;
            double tov = 0;
            double pf = 0;
            double mins = 0;
            using (StreamReader sr = File.OpenText(fileName + "Team.txt"))
            {
                string line;
                string line2 = "";
                int lineNum = 0;
                int count = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    foreach (char i in line)
                    {
                        if ((i == '#' || i == ' ') && (count < 3))
                        {
                            count++;
                        }
                        else if (count >= 3)
                        {
                            line2 += i;
                        }
                    }
                    //Console.WriteLine(line2);
                    switch (lineNum)
                    {
                        case 0:
                            games = Int32.Parse(line2);
                            //Console.WriteLine(games);
                            lineNum++;
                            break;
                        case 1:
                            three[0] = Int32.Parse(line2);
                            //Console.WriteLine(three[0]);
                            lineNum++;
                            break;
                        case 2:
                            three[1] = Int32.Parse(line2);
                            //Console.WriteLine(three[1]);
                            lineNum++;
                            break;
                        case 3:
                            all[0] = Int32.Parse(line2);
                            //Console.WriteLine(all[0]);
                            lineNum++;
                            break;
                        case 4:
                            all[1] = Int32.Parse(line2);
                            //Console.WriteLine(all[1]);
                            lineNum++;
                            break;
                        case 5:
                            ft[0] = Int32.Parse(line2);
                            //Console.WriteLine(ft[0]);
                            lineNum++;
                            break;
                        case 6:
                            ft[1] = Int32.Parse(line2);
                            //Console.WriteLine(ft[1]);
                            lineNum++;
                            break;
                        case 7:
                            reb[0] = Int32.Parse(line2);
                            //Console.WriteLine(reb[0]);
                            lineNum++;
                            break;
                        case 8:
                            reb[1] = Int32.Parse(line2);
                            //Console.WriteLine(reb[1]);
                            lineNum++;
                            break;
                        case 9:
                            year = Int32.Parse(line2);
                            //Console.WriteLine(year);
                            lineNum++;
                            break;
                        case 10:
                            point = Int32.Parse(line2);
                            //Console.WriteLine(point);
                            lineNum++;
                            break;
                        case 11:
                            totReb = Int32.Parse(line2);
                            //Console.WriteLine(totReb);
                            lineNum++;
                            break;
                        case 12:
                            ast = Int32.Parse(line2);
                            //Console.WriteLine(ast);
                            lineNum++;
                            break;
                        case 13:
                            stl = Int32.Parse(line2);
                            //Console.WriteLine(stl);
                            lineNum++;
                            break;
                        case 14:
                            blk = Int32.Parse(line2);
                            //Console.WriteLine(blk);
                            lineNum++;
                            break;
                        case 15:
                            tov = Int32.Parse(line2);
                            //Console.WriteLine(tov);
                            lineNum++;
                            break;
                        case 16:
                            pf = Int32.Parse(line2);
                            //Console.WriteLine(pf);
                            lineNum++;
                            break;
                        case 17:
                            id = Int32.Parse(line2);
                            //Console.WriteLine(id);
                            lineNum++;
                            break;
                        case 18:
                            mins = Int32.Parse(line2);
                            team.makeFullTotals(year, point, totReb, ast, stl, blk, tov, pf, mins, three, all, ft, reb);
                            //Console.WriteLine(mins);
                            lineNum = 0;
                            break;
                    }
                    line2 = "";
                    count = 0;
                }
                sr.Close();
            }
            return team;
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            FileReader reader = new FileReader("denver03");
            Team denver = reader.readTeam("Denver Nuggets");
            Player meloAnt = denver.returnPlayer(0);
            AdvShooting meloAdv = meloAnt.AdvStats;
            //Console.WriteLine(meloAnt.ToString());
            denver = reader.readInfo(denver);
            Console.WriteLine(denver.ToString(denver));
        }
    }
}