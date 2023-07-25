using System.Text;
using AStar.Organisation.Core.Application.IServices;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;

namespace AStar.Organisation.Infrastructure.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RandomApiController : Controller
    {
        private readonly IGetRandomApiService _getRandomApiService;
        private readonly IProducer<Null, string> _producer;        
        public RandomApiController(IGetRandomApiService getRandomApiService,ProducerConfig config)
        {
            _getRandomApiService = getRandomApiService;
            _producer = new ProducerBuilder<Null, string>(config).Build();
        }
        
        [HttpGet]
        public async Task<IActionResult> GenerateRandomApi()
        {
            var cardInfoDto = await _getRandomApiService.GetRandomName();
            
            return Ok(cardInfoDto);
        }
        
        [HttpPost]
        public IActionResult SendMessage([FromBody] string message)
        {
            try
            {
                var topic = "test-topic";

                var deliveryReport = _producer.ProduceAsync(topic, new Message<Null, string> { Value = message }).Result;

                return Ok($"Сообщение успешно отправлено на Partition: {deliveryReport.TopicPartitionOffset}");
            }
            catch (ProduceException<Null, string> e)
            {
                return BadRequest($"Ошибка отправки сообщения: {e.Error.Reason}");
            }
        }
    }
}