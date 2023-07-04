using Hangfire;

namespace ClothingStore.WebAPI.Services;

public class ScheduleService : IScheduleService
{
    private readonly ISignalRService _signalRService;

    public ScheduleService(ISignalRService signalRService)
    {
        _signalRService = signalRService;
    }

    public void Schedule(int id, TimeSpan delay)
    {
        BackgroundJob.Schedule(() => _signalRService.DeleteExpiredCartItem(id), delay);
    }
}