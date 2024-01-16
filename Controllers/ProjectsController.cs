using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.DatabaseContext;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public ProjectsController(ApplicationDbContext applicationDbContext) {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet("")]
        public List<Project> GetProjects()
        {
            List <Project> projects = _applicationDbContext.Projects.ToList<Project>();
            return projects;
        }

        [HttpPost("")]
        public Project AddProject(Project project)
        {
            _applicationDbContext.Projects.Add(project);
            _applicationDbContext.SaveChanges();
            return project;
        }

        [HttpPut("")]
        public Project? UpdateProject(Project project)
        {
            Project? existingProject = _applicationDbContext.Projects.Where(temp => temp.ProjectID == project.ProjectID).FirstOrDefault();
            if(existingProject != null)
            {
                existingProject.ProjectName = project.ProjectName;
                existingProject.DateOfStart = project.DateOfStart;
                existingProject.TeamSize = project.TeamSize;

                _applicationDbContext.SaveChanges();
                return existingProject;
            }

            return null;

        }

        [HttpDelete("")]
        public int DeleteProject(int projectID)
        {
            Project? projectToBeDeleted = _applicationDbContext.Projects.Where(temp => temp.ProjectID == projectID).FirstOrDefault();

            if(projectToBeDeleted != null)
            {
                _applicationDbContext.Projects.Remove(projectToBeDeleted);
                _applicationDbContext.SaveChanges();
                return projectID;
            }

            return -1;
        }

        [HttpGet("search")]
        public List<Project> SearchProjects(string searchby,string searchtext)
        {
            List<Project> projects = _applicationDbContext.Projects.ToList();
            if(searchtext ==  null || searchtext == "")
            {
                return projects;
            }

            if(searchby == "ProjectID")
            {
                projects = projects.Where(temp => temp.ProjectID.ToString().StartsWith(searchtext)).ToList();
            }
            else if(searchby == "ProjectName")
            {
                projects = projects.Where(temp => temp.ProjectName.StartsWith(searchtext)).ToList();
            }
            else if(searchby == "DateOfStart")
            {
                projects = projects.Where(temp => temp.DateOfStart.ToString().StartsWith(searchtext)).ToList();
            }
            else
            {
                projects = projects.Where(temp => temp.TeamSize.ToString().StartsWith(searchtext)).ToList();
            }

            return projects;
        }
    }
}
