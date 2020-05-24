using EcoleDirecteDownloader.Api.Contracts;
using StudiesManager.Common.Extensions;
using EcoleDirecteDownloader.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Globalization;

namespace EcoleDirecteController.Api.Controllers
{
    [ApiController]
    [Route("ecoledirecte")]
    public class MaxicoursController : ControllerBase
    {
        private readonly IEcoleDirecteService _ecoleDirecteService;

        public MaxicoursController(IEcoleDirecteService ecoleDirecteService)
        {
            _ecoleDirecteService = ecoleDirecteService;
        }

        [HttpGet]
        [Route("home")]
        public IActionResult Home()
        {
            try
            {
                var result = _ecoleDirecteService.Home();

                if (!result)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("homeworkbook")]
        public IActionResult HomeworkBook()
        {
            try
            {
                var result = _ecoleDirecteService.HomeworkBook();

                if (!result)
                    return NotFound();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("homeworkbook/worktodo/{date}")]
        public IActionResult GetWorkToDo(DateTime date)
        {
            try
            {
                var result = _ecoleDirecteService.GetWorkToDo(date);

                if (string.IsNullOrWhiteSpace(result))
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("homeworkbook/sessionscontent/{date}")]
        public IActionResult GetSessionsContent(DateTime date)
        {
            try
            {
                var result = _ecoleDirecteService.GetSessionsContent(date);

                if (string.IsNullOrWhiteSpace(result))
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
