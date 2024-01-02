using Microsoft.AspNetCore.Mvc;
using MoominsApi.Repo;


namespace MoominsApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class MoominValley : ControllerBase
    {
        //Muumirepo kontrollerien käyttöön
        private static MoominsRepository moominsRepository = new MoominsRepository();

        //GET - kaikki muumit tai ne muumit, joiden nimessä query
        [HttpGet(Name = "MoominValley")]
        public List<Moomin> Get([FromQuery] string? name)
        {
            var moomins = moominsRepository.GetAllMoomins();

            if(string.IsNullOrEmpty(name))
            {
                return moomins;
            }
            else
            {
                var selectedMoomins = new List<Moomin>();

                selectedMoomins = moominsRepository.GetAllMoomins(name);

                return (selectedMoomins);

            }
            
        }

        [HttpPost]
        public ActionResult InsertMoomin(Moomin moomin)
        {
            var result = moominsRepository.AddMoomin(moomin); //Palauttaa booleanin true, jos muumi lisättiin
            var moominNumber = moomin.Number;
            var moominUri = "/api/MoominValley/" + moominNumber;

            if (result)
            {
                return Created(moominUri, moomin);
            }
            else
            {
                return Conflict();
            }
        }

        [HttpGet]
        [Route("moomin/{number}")]
        public ActionResult<Moomin> GetMoomin(int number)
        {
            var moomin = moominsRepository.GetMoomin(number);

            if(moomin == null)
            {
                return NotFound("Moomin not found.");
            }
            else
            {
                return(moomin);
            }
        }

        [HttpDelete]
        public ActionResult<Moomin> DeleteMoomin(int number)
        { 
            var result = moominsRepository.DeleteMoomin(number);

            if (result)
            {
                return NoContent();
            }
            else
            {
                return NotFound("Moomin not found.");
            }
        }

    }
}