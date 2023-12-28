using System.Net.Http.Json;
using BrightAssistant.ChartAnalyser.Models;

public class Repository
{
    public List<Session>? Sessions
    {
        get; set;
    } = new List<Session>();

    public List<SessionData> SessionDatas
    {
        get; set;
    } = new List<SessionData>();

    public Session FirstSession { get; set; }
    public SessionData FirstSessionData { get; set; }
    public Session SecondSession { get; set; }
    public SessionData SecondSessionData { get; set; }

    bool dataIsLoading;

    private async Task<SessionData?> GetSessionData(Session session, HttpClient Http)
    {

        var sessionData = SessionDatas?.FirstOrDefault(x => x.SessionId == session.id);
        if (sessionData == null && !dataIsLoading)
        {
            dataIsLoading = true;
            sessionData = await Http.GetFromJsonAsync<SessionData>($"/api/sessiondata/{session.id}");
            if (sessionData != null) SessionDatas.Add(sessionData);
        }
        dataIsLoading = false;
        return sessionData;
    }

    public async Task LoadSessionsData(HttpClient Http)
    {
        if (FirstSession == null) return;
        List<Task<SessionData>> tasks = new();
        FirstSessionData = await GetSessionData(FirstSession, Http);
        if (SecondSession != null) SecondSessionData = await GetSessionData(SecondSession, Http);
    }

    public async Task LoadSessionsAsync(HttpClient Http)
    {
        try
        {
            if (Sessions.Count == 0)
            {
                var sessions = await Http.GetFromJsonAsync<List<Session>>("/api/sessions");
                var orderedSessions = sessions.OrderByDescending(x => x.BeginTime).ToList();
                Sessions = orderedSessions;
                
            }
        }
        catch (Exception ex)
        {
            //await LogAsync(ex.ToString(), LogType.Error);
            Console.WriteLine("Error: ", ex.Message);
        }
    }

}