using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB.API.BLL;
using WEB.API.Contracts;
using WEB.API.Entities.Models;

namespace WEB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeBLL employeeBLL;
        private readonly ILoggerManager _logger;

        public EmployeesController(EmployeeBLL employeeBLL, ILoggerManager _logger)
        {
            this.employeeBLL = employeeBLL;
            this._logger = _logger;
        }

        
        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            using (employeeBLL)
            {
                try
                {
                    var employees = await employeeBLL.GetAll();
                    _logger.LogInfo($"Returned all employees from database.");
                    return Ok(employees);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Something went wrong inside GetAllEmployees action: {ex.Message}");
                    return StatusCode(500, "Internal server error");
                }
            }
        }
        

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(Guid id)
        {
            try
            {
                using (employeeBLL)
                {
                    var employee = await employeeBLL.Get(id);

                    if (employee == null )
                    {
                        _logger.LogError($"Employee with id: {id}, hasn't been found in db.");
                        return NotFound();
                    }
                    else
                    {
                        _logger.LogInfo($"Returned employee with id: {id}");
                        return Ok(employee);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetEmployee action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/Employees
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    _logger.LogError("Employee object sent from client is null.");
                    return BadRequest("Employee object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Employee object sent from client.");
                    return BadRequest("Invalid model object");
                }

                using (employeeBLL)
                {
                    await employeeBLL.Create(employee);
                    return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside PostEmployee action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(Guid id, Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    _logger.LogError("Employee object sent from client is null.");
                    return BadRequest("Employee object is null");
                }

                if (!id.Equals(employee.Id))
                {
                    _logger.LogError("Employee Id must be equal to employee object.");
                    return BadRequest("Employee Id must be equal to employee object.");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid employee object sent from client.");
                    return BadRequest("Invalid model object");
                }
                using (employeeBLL)
                {
                    await employeeBLL.Update(id, employee);
                    return NoContent();
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError($"Something went wrong when it was updating the employee inside PutEmployee action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside PutEmployee action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(Guid id)
        {
            try
            {
                using (employeeBLL)
                {
                    var employee = await employeeBLL.Get(id);
                    if (employee ==  null)
                    {
                        _logger.LogError($"Employee with id: {id}, hasn't been found in db.");
                        return NotFound($"Employee with id: {id}, hasn't been found in db.");
                    }
                    await employeeBLL.Delete(employee);
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteEmployee action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }

    }
}
