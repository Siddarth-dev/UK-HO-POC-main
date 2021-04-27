using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using API.Test.Common;
using Application.Batch.Commands.CreateBatch;
using Application.Batch.Queries.GetBatchDetail;
using Application.Exceptions;
using NUnit.Framework;

namespace API.Test.Controllers
{
    public class BatchControllerUnitTest //: IClassFixture
    {
        CustomWebApplicationFactory<Startup> factory = new CustomWebApplicationFactory<Startup>();
        private readonly HttpClient _client;
        public BatchControllerUnitTest()
        {
            _client = factory.CreateClient();
        }

        [Test]
        public async Task ReturnsBatchViewModel()
        {
            Guid id = new Guid("2bb5d385-8dd8-412a-b28d-08d8fcec5543");//("40F74BE9-6CCB-4CDC-DEEE-08D8EEB37A9E");
            var response = await _client.GetAsync($"/api/batch/{id}");

            response.EnsureSuccessStatusCode();

            var vm = await Utilities.GetResponseContent<BatchDetailModel>(response);

            Assert.IsInstanceOf<BatchDetailModel>(vm);
            Assert.AreEqual(id, vm.BatchId);
        }

        [Test]
        public async Task ReturnsNotFoundBatchViewModel()
        {
            Guid id = Guid.NewGuid();
            var response = await _client.GetAsync($"/api/batch/{id}");

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task CreateBatch_ReturnsBadRequest()
        {
            var command = new CreateBatchCommand
            {
                BusinessUnit = "Demo0 BU",
                ExpiryDate = DateTime.Now.AddMonths(5),
                Acl = new BatchAclModel(){
                    ReadGroups = new List<string>{
                        "TestReadGroup"
                    },
                    ReadUsers = new List<string>{
                        "TestReadUser"
                    },
                }
            };

            var content = Utilities.GetRequestContent(command);

            var response = await _client.PostAsync($"/api/batch", content);

            var vm = await Utilities.GetResponseContent<ValidationException>(response);
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task CreateBatch_ReturnsSuccess()
        {
            var command = new CreateBatchCommand
            {
                BusinessUnit = "Demo0 BU",
                ExpiryDate = DateTime.Now.AddMonths(5),
                Acl = new BatchAclModel(){
                    ReadGroups = new List<string>{
                        "TestReadGroup"
                    },
                    ReadUsers = new List<string>{
                        "TestReadUser"
                    },
                },
                Attributes = new List<BatchAttributeDetailModel>(){
                    new BatchAttributeDetailModel() {
                        Key = "KeyTest",
                        Value = "ValueTest"
                    }
                }
            };

            var content = Utilities.GetRequestContent(command);

            var response = await _client.PostAsync($"/api/batch", content);

            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}