using Microsoft.AspNetCore.SignalR;

namespace SignalRSample01.Hubs
{
	public class UserHub : Hub
	{
		public static int TotalViews { get; set; } = 0;
		public static int TotalUsers { get; set; } = 0;

		public override Task OnConnectedAsync()
		{
			TotalUsers++;
			Clients.All.SendAsync("updateTotalUsers", TotalUsers).GetAwaiter().GetResult();
			return base.OnConnectedAsync();
		}

		public override Task OnDisconnectedAsync(Exception? exception)
		{
			TotalUsers--;
			Clients.All.SendAsync("updateTotalUsers", TotalUsers).GetAwaiter().GetResult();
			return base.OnDisconnectedAsync(exception);
		}

		public async Task<string> NewWindowLoaded(string name)
		{
			TotalViews++;
			await Clients.All.SendAsync("updateTotalViews", TotalViews); // to all
			return $"hi {name}!";
		}

		//await Clients.Caller.SendAsync("updateTotalViews", TotalViews); // to caller
		//await Clients.Others.SendAsync("updateTotalViews", TotalViews); // to all except caller
		//await Clients.Client("[CLIENT A ID]").SendAsync("updateTotalViews", TotalViews); // to one client
		//await Clients.Clients("[CLIENT A ID]","[CLIENT B ID]").SendAsync("updateTotalViews", TotalViews); // to many clients
		//await Clients.AllExcept("[CLIENT X ID]", "[CLIENT Y ID]").SendAsync("updateTotalViews", TotalViews); // to all except the specified clients
		//await Clients.User("user@domain.com").SendAsync("receiveMessage", "user", "message"); // to one client
		//await Clients.Users("user@domain.com", "joe@domain.com").SendAsync("receiveMessage", "user", "message"); // to one client
	}
}
