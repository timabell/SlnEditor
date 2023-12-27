using System;
using System.Collections.Generic;
using System.Linq;

namespace SlnEditor.Models
{
    public static class Extensions
    {
        /// <summary>
        /// Recurse the solution folder hierarchy looking for child projects and return all projects
        /// in the solution as one flat list.
        /// </summary>
        public static IList<IProject> FlatProjectList(this Solution solution)
        {
            var projects = new List<IProject>();
            projects.AddRange(solution.RootProjects);

            foreach (var solutionFolder in solution.RootProjects.OfType<SolutionFolder>())
            {
                AddChildProjects(projects, solutionFolder);
            }

            return projects;
        }

        /// <summary>
        /// Recursively add all child projects
        /// </summary>
        /// <param name="projects">List to add child projects to</param>
        /// <param name="project">Project </param>
        /// <exception cref="InvalidOperationException">Thrown if project already in root list</exception>
        private static void AddChildProjects(ICollection<IProject> projects, SolutionFolder project)
        {
            foreach (var childProject in project.Projects)
            {
                if (projects.Contains(childProject))
                {
                    throw new InvalidOperationException($"Project {childProject.Id} found twice. Must be either a root level project, or listed as the child of a single solution folder.");
                }
                projects.Add(childProject);
                if (childProject is SolutionFolder childSolutionFolder)
                {
                    AddChildProjects(projects, childSolutionFolder);
                }
            }
        }
    }
}
