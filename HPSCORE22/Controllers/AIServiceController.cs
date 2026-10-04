using CSWMS.AIServices;
using CSWMS.AIServices.BusinessLayer;
using CSWMS.AIServices.Model;
using CSWMS.AIServices.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.AI;
using System.Text.Json;
using System.Threading.Tasks;
namespace CSWMS.Controllers
{
    public class AIServiceController : Controller
    {
        public readonly IAIBusinessLayer _aIService;
      //  public readonly IAIRepository _aIServiceSQL;
        public AIServiceController(IAIBusinessLayer aIService, IAIRepository aIServiceSQL)
        {
            _aIService=aIService;
            _aIServiceSQL= aIServiceSQL;
        }
        // GET: AI Question Page
        public IActionResult GetQuestion()
        {
            return View("~/Views/AIService/GetQuestion.cshtml");
        }

        // SetUp : Generate Vector Database from SQL Server and Save to Qdrant
        [HttpGet]
        public JsonResult GenerateVectorDatabase()
        {
            var objects = _aIService.GetDatabaseSchema().Result; // Fetch database objects synchronously
            var result = objects.Select(x => _aIService.ConvertDataBaseObjectToSting(x)).ToList(); // Convert each DataTable to a string representation and store in a list
            foreach (var databaseObject in objects)
            {
                var embedding = _aIService.ConvertingDatabaseTextToEmbedding(databaseObject.ObjectName).Result;  // Generate schema embedding synchronously
                
                var QdrantSaveResult = _aIService.SaveToQdrantAsync(databaseObject.ObjectName, databaseObject.ObjectType, databaseObject.Definition, embedding).Result; // Save the converted data and embedding to Qdrant synchronously
            }
            return Json(new { success = true, message = "Vector database generated successfully." }); // Return a JSON response indicating success
        }

        //    public async Task<IActionResult> GetQuestion()
        //    {
        //        //AIServiceModel AIService=new AIServiceModel();
        //        //var schema = await _aIServiceSQL.GetDatabaseSchemaAsync();
        //        //var Convertdataset=  _aIServiceSQL.ConvertSchemaToTableTexts(schema);
        //        //var Convertingdata=  _aIServiceSQL.ConvertDatatabletostring(Convertdataset);
        //        //
        //        //
        //        var objects =
        //await _aIServiceSQL.GetDatabaseObjectsAsync();
        //      //  AIService
        //        return View("~/Views/AIService/GetQuestion.cshtml" );
        //    }

        //[HttpPost]
        //public async Task<IActionResult> SaveQuestion(string question)
        //{
        //    {
        //        try
        //        {
        //            // document Qdrant embedding Part start
        //            var embedding = await _aIService.GenerateQuestionEmbeddingAsync(question);
        //            var SaveINQrant = await _aIService.SaveToQdrantAsync(question, embedding);
        //            // document Qdrant embedding Part End
        //            return Ok(embedding);
        //        }
        //        catch (ArgumentException ex)
        //        {
        //            return BadRequest(new
        //            {
        //                success = false,
        //                message = ex.Message
        //            });
        //        }
        //    }
        //}
        //[HttpPost]
        //public async Task<IActionResult> SaveQuestion(string question)
        //{
        //    if (string.IsNullOrWhiteSpace(question))
        //    {
        //        return BadRequest(new
        //        {
        //            success = false,
        //            message = "Question is required."
        //        });
        //    }

        //    // Step 1: Generate embedding for user's question
        //    var embeddingQuestion =
        //        await _aIServiceSQL.GenerateQuestionEmbeddingAsyncSqlQuestion(question);

        //    if (embeddingQuestion == null || embeddingQuestion.Length == 0)
        //    {
        //        return BadRequest(new
        //        {
        //            success = false,
        //            message = "Failed to generate embedding for the question."
        //        });
        //    }

        //    // Step 2: Search Qdrant
        //    var searchResult =
        //        await _aIServiceSQL.SearchQdrantAsync(embeddingQuestion);

        //    // Step 3: Convert Qdrant JSON to C# object
        //    var qdrantResponse =
        //        JsonSerializer.Deserialize<QdrantSearchResponse>(searchResult);

        //    if (qdrantResponse == null ||
        //        qdrantResponse.Result == null ||
        //        qdrantResponse.Result.Count == 0)
        //    {
        //        return BadRequest(new
        //        {
        //            success = false,
        //            message = "No relevant schema found."
        //        });
        //    }

        //    // Step 4: Extract relevant schema
        //    var schemaText = string.Join(
        //        "\n\n",
        //        qdrantResponse.Result
        //            .Where(x => x.Payload != null &&
        //                        !string.IsNullOrWhiteSpace(x.Payload.Text))
        //            .Select(x => x.Payload.Text)
        //    );

        //    // Step 5: Generate SQL using Ollama
        //    var generatedSQL =
        //        await _aIServiceSQL.GenerateSqlAsync(
        //            question,
        //            schemaText
        //        );

        //    // Temporary: return generated SQL
        //    return Content(generatedSQL, "text/plain");
        //}
        [HttpPost]
        public async Task<IActionResult> SaveQuestion(string question)
        {
            if (string.IsNullOrWhiteSpace(question))
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Question is required."
                });
            }

            // Step 1: Generate embedding for user's question
            var embeddingQuestion =
                await _aIServiceSQL.GenerateQuestionEmbeddingAsyncSqlQuestion(question);

            if (embeddingQuestion == null || embeddingQuestion.Length == 0)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Failed to generate embedding for the question."
                });
            }

            // Step 2: Search Qdrant
            var searchResult =
                await _aIServiceSQL.SearchQdrantAsync(embeddingQuestion);

            // Step 3: Convert Qdrant JSON to C# object
            var qdrantResponse =
                JsonSerializer.Deserialize<QdrantSearchResponse>(searchResult);

            if (qdrantResponse == null ||
                qdrantResponse.Result == null ||
                qdrantResponse.Result.Count == 0)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "No relevant schema found."
                });
            }

            // Step 4: Extract relevant schema
            var schemaText = string.Join(
                "\n\n",
                qdrantResponse.Result
                    .Where(x => x.Payload != null &&
                                !string.IsNullOrWhiteSpace(x.Payload.Text))
                    .Select(x => x.Payload.Text)
            );

            // Step 5: Classify / understand the question
            var classification =
                await _aIServiceSQL.ClassifyQuestionAsync(
                    question,
                    schemaText
                );

            if (string.IsNullOrWhiteSpace(classification))
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Failed to understand the question."
                });
            }

            // Temporary: return classification
            return Content(classification, "application/json");

            // Step 6: Generate SQL
            /*
            var generatedSQL =
                await _aIServiceSQL.GenerateSqlAsync(
                    question,
                    schemaText
                );

            return Content(generatedSQL, "text/plain");
            */
        }
    }
}
