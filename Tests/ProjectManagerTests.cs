using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class ProjectManagerTests
{
    [TestMethod]
    public void StartProject_AddsProjectToList()
    {
        var projectManager = new ProjectManager();
        var initialProjectCount = projectManager.projects.Count;
        var title = "Test Project";
        var deadline = DateTime.Now.AddDays(7);
        var max = 10;

        projectManager.StartProject(title, deadline, max);

        Assert.AreEqual(initialProjectCount + 1, projectManager.projects.Count);
    }

    [TestMethod]
    public void FinishProject_SetsProjectInactive()
    {
        var projectManager = new ProjectManager();
        var project = new Project();
        projectManager.projects.Add(project);

        projectManager.FinishProject(project);

        Assert.IsFalse(project.active);
    }

        [Test]
    public void UpdateDaysLeft_ProjectsWithPositiveDaysLeft_NoFinishProjectCalled()
    {
        ProjectManager projectManager = new ProjectManager();
        Project project1 = new Project("Project1", DateTime.Now.AddDays(5));
        Project project2 = new Project("Project2", DateTime.Now.AddDays(10));

        projectManager.projects.Add(project1);
        projectManager.projects.Add(project2);

        projectManager.UpdateDaysLeft();

        Assert.That(project1.active, Is.True);
        Assert.That(project2.active, Is.True);
    }

    [Test]
    public void UpdateDaysLeft_ProjectsWithZeroDaysLeft_FinishProjectCalled()
    {
        ProjectManager projectManager = new ProjectManager();
        Project project1 = new Project("Project1", DateTime.Now);
        Project project2 = new Project("Project2", DateTime.Now.AddDays(-1));

        projectManager.projects.Add(project1);
        projectManager.projects.Add(project2);

        projectManager.UpdateDaysLeft();

        Assert.That(project1.active, Is.False);
        Assert.That(project2.active, Is.False);
    }
}
