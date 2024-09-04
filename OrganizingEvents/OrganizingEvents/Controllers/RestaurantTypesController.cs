using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using OrganizingEvents.Models;

namespace OrganizingEvents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantTypesController : ControllerBase
    {
        private readonly IMongoCollection<RestaurantTypes> _restaurantTypes;

        public RestaurantTypesController(IMongoClient client)
        {
            var database = client.GetDatabase("OrganizingEventsTest");
            _restaurantTypes = database.GetCollection<RestaurantTypes>("RestaurantTypes");
        }

        //Get
        [HttpGet("GetAllList")]
        public ActionResult<List<RestaurantTypes>> Get() =>
           _restaurantTypes.Find(restaurants => true).ToList();


        [HttpGet("GetRestaurantTypesById/{id:length(24)}", Name = "GetRestaurantTypes")]
        public ActionResult<RestaurantTypes> Get(string id)
        {
            var restaurantTypes = _restaurantTypes.Find<RestaurantTypes>(o => o.Id == id).FirstOrDefault();

            if (restaurantTypes == null)
            {
                return NotFound();
            }

            return restaurantTypes;
        }

        //Post
        [HttpPost("Add")]
        public ActionResult<RestaurantTypes> Create(RestaurantTypes restaurant)
        {
            try
            {
                // Generate a unique ID
                restaurant.Id = ObjectId.GenerateNewId().ToString(); // Generate a unique ObjectId and convert it to string
                _restaurantTypes.InsertOne(restaurant);
                return CreatedAtRoute("GetRestaurantTypes", new { id = restaurant.Id }, restaurant);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}