using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Questao2;
using RestSharp;

public class Program
{
    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static int getTotalScoredGoals(string team, int year)
    {
        var data = getDataTeams(team, year, 0);
        int total = 0;

        for (int i = 1; i <= data.total_pages; i++)
        {
            foreach (var item in getDataTeams(team, year, i).data)
            {
                total += total = Convert.ToInt32(item.team1goals);
            }
        }
        return total;
    }

    public static BaseTimes getDataTeams(string team, int year, int page)
    {
        try
        {
            var client = new RestClient(string.Format("https://jsonmock.hackerrank.com/api/football_matches?year={0}&team1={1}{2}", year, team, (page > 0 ? String.Concat("&page=", page) : "")));
            var request = new RestRequest();
            request.Method = Method.Get;
            request.AddHeader("Content-Type", "application/json");

            var Data = new JsonResult(client.Execute(request));
            JsonResult dataSerial = new JsonResult(((RestSharp.RestResponseBase)Data.Value).Content);

            BaseTimes teams = JsonConvert.DeserializeObject<BaseTimes>(dataSerial.Value.ToString());

            return teams;

        }
        catch (Exception ex)
        {

            throw ex;
        }

        return null;
    }
}