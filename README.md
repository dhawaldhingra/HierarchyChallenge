
<h2 align="center">Users-Hierarchy Challenge</h3>
<hr/>

<details open="open">
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
        <li><a href="#solution-structure">Solution Structure</a></li>
        <li><a href="#solution-structure">Test Outcome</a></li>
      </ul>
    </li>
  </ol>
</details>

## About The Project

The project contains the solution to user hierarchy problem. The problem is finding all the subordinates of a particular user, given a list of users and a list of roles along with their parent role.


### Built With

The solution was built in Visual Studio 2019 running on Windows. It can be opened on Mac on either Visual Studio 2019 or VS Code. The code files are plain text files albeit with a .cs extension and therefore can be opened in any text editor such as TextEdit or Notepad. It has been built on .Net framework 4.7 and written in C#.

### Solution Structure

The solution comprises of three folders/projects. These are-
1. HierarchyChallenge.BusinessLogic
2. HierarchyChallengeTests
3. TestConsoleApp

<h4>HierarchyChallenge.BusinessLogic</h4>
The actual implementation of the solution can be found under HierarchyChallenge.BusinessLogic directory. This further contains two subdirectories which are- <br/>
a. DataObjects- This folder contains the class files representing User and Role objects <br/>
b. BusinesLogic- The contains a file called UsersRoles.cs which contains the actual logic and implementation of GetSubmordinates method. <br/>
The output of this folder/project is a class library(DLL) that can be used by any compatible client.

<h4>HierarchyChallengeTests</h4>
This directory contains the Visual Studio test project and various automated test cases. Visual Stuio 2015+ will be required to run the test cases directly from Visual Studio. A screenshot of the test results however has been provided separately.

<h4>TestConsoleApp</h4>
This is a small command line application built which allows a user to run their custom test cases. The uses need to provide four command line arguments while running this application. The arguments in the sequence they should be provided are a JSON file containing the list of users, JSON file containing the list of roles, JSON file containing the expected output and lastly the UserID who's subordinates need to be returned. This console app runs natively on Windows platform but can also be executed on Mac by using a compatibility layer such as Wine App or Crossover Office.


## Test Results
<img src="images/TestResults.PNG" alt="Test Results">
