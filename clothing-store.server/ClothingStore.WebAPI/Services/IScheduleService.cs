namespace ClothingStore.WebAPI.Services;

public interface IScheduleService
{
    void Schedule(int id, TimeSpan delay);
}