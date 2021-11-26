using AutoMapper;
using EmployeeAPI.Repository.IRepository;
using EmployeeAPI.Models;
using EmployeeAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class EmployeeController : Controller
    {
        private readonly IBasicRepository _bRepo;
        private readonly IMapper _mapper;

        public EmployeeController(IBasicRepository bRepo, IMapper mapper)
        {
            _bRepo = bRepo;
            _mapper = mapper;
            //added Controller to our API
        }

        /// <summary>
        /// Get List of Employees
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200,Type =typeof(List<BasicDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetBasics()
        {
            var ObjList = _bRepo.GetBasics();//To get the employee list

            var ObjDto = new List<BasicDto>();

            foreach(var Obj in ObjList)
            {
                ObjDto.Add(_mapper.Map<BasicDto>(Obj));
            }
            return Ok(ObjDto);//API call 
        }
        /// <summary>
        /// Get Individual Employee
        /// </summary>
        /// <param name="basicId">The Id of the Employee</param>
        /// <returns></returns>
        [HttpGet("({basicId:Guid}",Name = "GetBasic")]
        [ProducesResponseType(200, Type = typeof(BasicDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetBasic(Guid basicId)
        {
            var obj = _bRepo.GetBasic(basicId);
            if(obj == null)
            {
                return NotFound();
            }
            var ObjDto = _mapper.Map<BasicDto>(obj);
            return Ok(ObjDto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(BasicDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateBasic([FromBody] BasicDto basicDto)
        {
            if(basicDto == null)
            {
                return BadRequest(ModelState);//contains all errors
            }
            if(_bRepo.BasicExists(basicDto.Name))
            {
                ModelState.AddModelError("", "Employee Exists!");
                return StatusCode(404,ModelState);
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var BObj = _mapper.Map<Basic>(basicDto);

            if(!_bRepo.CreateBasic(BObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record{BObj.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetBasic", new
            {
                basicId = BObj.Id
            }, BObj);
        }
        
        [HttpPatch("{basicId:Guid" +
            "}", Name = "UpdateBasic")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateBasic(Guid basicId, [FromBody] BasicDto basicDto)
        {
            if (basicDto == null || basicId!=basicDto.Id)
            {
                return BadRequest(ModelState);//contains all errors
            }

            var BObj = _mapper.Map<Basic>(basicDto);
            if (!_bRepo.UpdateBasic(BObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record{BObj.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{basicId:Guid}", Name = "UpdateBasic")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult DeleteBasic(Guid basicId)
        {
            if (!_bRepo.BasicExists (basicId))
            {
                return NotFound();
            }

            var BObj = _bRepo.GetBasic(basicId);
            if (!_bRepo.DeleteBasic(BObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record{BObj.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
