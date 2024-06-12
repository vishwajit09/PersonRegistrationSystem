
using PersonRegistrationSystem.Database;

namespace PersonRegistrationSystem.BussinessLogic
{
    // public class ValidationService : IValidationService
    // {
    //     private readonly IPersonInformationRepository _personInformationRepository;

    //     public ValidationService(IPersonInformationRepository personInformationRepository)
    //     {
    //         _personInformationRepository = personInformationRepository;
    //     }

    //     // public bool GetEmail(string email)
    //     // {
    //     //     return _personInformationRepository.GetByEmailAsync(email);

    //     // }

    //     // public bool GetPersonalCode(string persononalcode)
    //     // {
    //     //     return _personInformationRepository.GetByPersoanlCodeAsync(persononalcode);

    //     // }
    // }

    public interface IValidationService
    {
        bool GetEmail(string email);
        bool GetPersonalCode(string personalCode);
    }
}


