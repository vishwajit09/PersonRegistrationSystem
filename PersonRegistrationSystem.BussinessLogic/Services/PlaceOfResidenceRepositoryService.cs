using AutoMapper;
using Microsoft.Extensions.Logging;
using PersonRegistrationSystem.Database;

namespace PersonRegistrationSystem.BussinessLogic;

public class PlaceOfResidenceRepositoryService : IPlaceOfResidenceService
{
    private readonly IPlaceOfResidenceRepository _placeOfResidenceRepository;
    private readonly ILogger<PlaceOfResidenceRepositoryService> _logger;

    private readonly IMapper _mapper;


    public PlaceOfResidenceRepositoryService(IPlaceOfResidenceRepository placeOfResidenceRepository, ILogger<PlaceOfResidenceRepositoryService> logger, IMapper mapper)
    {
        _placeOfResidenceRepository = placeOfResidenceRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<PlaceOfResidence> GetPlaceOfResidenceByIdAsync(int id)
    {
        return await _placeOfResidenceRepository.GetByIdAsync(id);
    }

    public async Task UpdatePlaceOfResidenceAsync(int id, PlaceOfResidenceDto placeOfResidencedto)
    {
        var existingPlaceOfResidence = await _placeOfResidenceRepository.GetByIdAsync(id);
        if (existingPlaceOfResidence == null)
        {
            _logger.LogInformation($"PlaceOfResidence with ID {id} not found.");
            throw new KeyNotFoundException("Place of residence not found.");
        }

        _mapper.Map(placeOfResidencedto, existingPlaceOfResidence);
        await _placeOfResidenceRepository.UpdateAsync(existingPlaceOfResidence);
        _logger.LogInformation($"PlaceOfResidence with ID {id} updated .");
    }

    public async Task<PlaceOfResidenceDto> CreatePlaceOfResidenceAsync(PlaceOfResidence placeOfResidenceo)
    {

        await _placeOfResidenceRepository.AddAsync(placeOfResidenceo);
        return _mapper.Map<PlaceOfResidenceDto>(placeOfResidenceo);
    }


}
