using System;
using BrightAssistant.ChartAnalyser.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using BrightAssistant.ChartAnalyser.Models;
using Microsoft.AspNetCore.Http;
using static Azure.Core.HttpHeader;
using System.Diagnostics;

namespace BrightAssistant.ChartAnalyzer.Server
{
	public class DBService
	{
        CosmosClient client;
        private Container mainContainer;
        public DBService()
		{
            client = new CosmosClient($"AccountEndpoint={""};AccountKey={""}");
            mainContainer = mainContainer = client.GetContainer("BrightAssistantDB", "UserItems");
        }

        public async Task<IEnumerable<Session>> LoadSessionsAsync(string userId)
        {
            try
            {
                var Sessions = new List<Session>();
                var query = mainContainer.GetItemLinqQueryable<Session>(requestOptions: new QueryRequestOptions { PartitionKey = new PartitionKey(userId), MaxItemCount = -1 })
                                  .Where(item => !item.id.Contains("permission") && item.DeviceType != "")
                                  .ToFeedIterator();

                while (query?.HasMoreResults ?? false)
                {
                    var result = await query.ReadNextAsync();
                    Sessions.AddRange(result);
                }
                return Sessions;
            }
            catch (Exception ex)
            {
                //await LogAsync(ex.ToString(), LogType.Error);
                Console.WriteLine("Error: ", ex.Message);
                return Enumerable.Empty<Session>();
            }
        }

        public async Task<SessionData?> GetSessionDataAsync(string id, string userId)
        {
            try
            {
                var query = mainContainer.GetItemLinqQueryable<SessionData>(requestOptions: new QueryRequestOptions { PartitionKey = new PartitionKey(userId), MaxItemCount = -1 })
                                  .Where(item => item.SessionId == id)
                                  .ToFeedIterator();
                if (!(query?.HasMoreResults ?? false)) return null;
                var sessionDataCollection = await query.ReadNextAsync();
                if (sessionDataCollection.Count == 0) return null;
                return sessionDataCollection.ToList()[0];
                
            }
            catch (CosmosException ex)
            {
                Console.WriteLine("Error: ", ex.Message);
                return null;
            }
        }
    }
}

