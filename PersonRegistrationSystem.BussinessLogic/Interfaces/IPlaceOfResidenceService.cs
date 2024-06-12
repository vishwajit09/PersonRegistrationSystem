using PersonRegistrationSystem.Database;

namespace PersonRegistrationSystem.BussinessLogic;

public interface IPlaceOfResidenceService
{
    Task<PlaceOfResidence> GetPlaceOfResidenceByIdAsync(int id);
    Task UpdatePlaceOfResidenceAsync(int userId, PlaceOfResidenceDto placeOfResidence);
    public Task<PlaceOfResidenceDto> CreatePlaceOfResidenceAsync(PlaceOfResidence placeOfResidence);
}
